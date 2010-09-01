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
        private Company _frontOfficeCompany;
        private Kitchen _kitchen;
        private Community _kitchenCommunity;
        private MemoryCommunicationStrategy _sharedCommunication;
        private Machine _kitchenMachine;

        [TestInitialize]
        public void initialize()
        {
            _sharedCommunication = new MemoryCommunicationStrategy();
            _frontOfficeCommunity = new Community(new MemoryStorageStrategy())
                .AddCommunicationStrategy(_sharedCommunication)
                .RegisterAssembly(typeof(Machine));

            _kitchenCommunity = new Community(new MemoryStorageStrategy())
                .AddCommunicationStrategy(_sharedCommunication)
                .RegisterAssembly(typeof(Machine))
                .Subscribe(() => _kitchenMachine.CurrentProvisionKitchen
                    .Select(provision => provision.Kitchen.Company));

            Machine frontOfficeMachine = _frontOfficeCommunity.AddFact(new Machine());
            _frontOfficeCompany = _frontOfficeCommunity.AddFact(new Company("NanasNook"));
            _frontOfficeCommunity.AddFact(new ProvisionFrontOffice(frontOfficeMachine, _frontOfficeCompany));

            _kitchenMachine = _kitchenCommunity.AddFact(new Machine());
            Company kitchenCompany = _kitchenCommunity.AddFact(new Company("NanasNook"));
            _kitchen = _kitchenCommunity.AddFact(new Kitchen(kitchenCompany, "Main Street"));
            _kitchenCommunity.AddFact(new ProvisionKitchen(_kitchenMachine, _kitchen));

        }

        [TestMethod]
        public void WhenFrontOfficePlacesOrder_ShouldAppearInKitchen()
        {
            _frontOfficeCommunity.AddFact(new Order(_frontOfficeCompany));

            _frontOfficeCommunity.Synchronize();
            _kitchenCommunity.Synchronize();

            Order backloggedOrder = _kitchen.Company.BackloggedOrders.Single();
            Assert.IsNotNull(backloggedOrder);
        }
    }
}
