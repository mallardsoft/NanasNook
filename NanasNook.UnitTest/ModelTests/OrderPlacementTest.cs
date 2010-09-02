using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using UpdateControls.Correspondence;
using UpdateControls.Correspondence.Memory;
using NanasNook.Model;

namespace NanasNook.UnitTest.ModelTests
{
    [TestClass]
    public class OrderPlacementTest
    {
        private Community _frontOfficeCommunity;
        private Community _kitchenCommunity;
        private Company _frontOfficeCompany;
        private Kitchen _kitchen;

        [TestInitialize]
        public void Initialize()
        {
            MemoryCommunicationStrategy sharedCommunication = new MemoryCommunicationStrategy();
            _frontOfficeCommunity = new Community(new MemoryStorageStrategy())
                .AddCommunicationStrategy(sharedCommunication)
                .RegisterAssembly(typeof(Machine))
                .Subscribe(() => _frontOfficeCompany);

            _kitchenCommunity = new Community(new MemoryStorageStrategy())
                .AddCommunicationStrategy(sharedCommunication)
                .RegisterAssembly(typeof(Machine))
                .Subscribe(() => _kitchen.Company);

            Machine frontOfficeMachine = _frontOfficeCommunity.AddFact(new Machine());
            _frontOfficeCompany = _frontOfficeCommunity.AddFact(new Company("NanasNook"));
            _frontOfficeCommunity.AddFact(new ProvisionFrontOffice(frontOfficeMachine, _frontOfficeCompany));

            Machine kitchenMachine = _kitchenCommunity.AddFact(new Machine());
            Company kitchenCompany = _kitchenCommunity.AddFact(new Company("NanasNook"));
            _kitchen = _kitchenCommunity.AddFact(new Kitchen(kitchenCompany, "Main Street"));
            _kitchenCommunity.AddFact(new ProvisionKitchen(kitchenMachine, _kitchen));

        }

        [TestMethod]
        public void WhenFrontOfficePlacesOrder_ShouldAppearInKitchen()
        {
            _frontOfficeCommunity.AddFact(new Order(_frontOfficeCompany));

            Synchronize();

            Order backloggedOrder = _kitchen.Company.BackloggedOrders.Single();
            Assert.IsNotNull(backloggedOrder);
        }

        [TestMethod]
        public void WhenKitchenPullsOrder_ShouldDisappearFromBacklog()
        {
            _frontOfficeCommunity.AddFact(new Order(_frontOfficeCompany));

            Synchronize();

            Order backloggedOrder = _kitchen.Company.BackloggedOrders.Single();
            _kitchenCommunity.AddFact(new Pull(backloggedOrder, _kitchen));

            Synchronize();

            Assert.IsFalse(_frontOfficeCompany.BackloggedOrders.Any(), "The front office should see no backlogged orders.");
        }

        [TestMethod]
        public void WhenKitchenPullsOrderFromFrontOfficeCompany_ShouldThrow()
        {
            _frontOfficeCommunity.AddFact(new Order(_frontOfficeCompany));

            Synchronize();

            Order backloggedOrder = _frontOfficeCompany.BackloggedOrders.Single();
            try
            {
                _kitchenCommunity.AddFact(new Pull(backloggedOrder, _kitchen));
                Assert.Fail("AddFact should have thrown.");
            }
            catch (CorrespondenceException ex)
            {
                Assert.AreEqual("A fact cannot be added to a different community than its predecessors.", ex.Message);
            }
        }

        private void Synchronize()
        {
            while (_frontOfficeCommunity.Synchronize() || _kitchenCommunity.Synchronize()) ;
        }
    }
}
