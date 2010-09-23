using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NBehave.Narrator.Framework;
using UpdateControls.Correspondence;
using NanasNook.Model;
using UpdateControls.Correspondence.Memory;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace NanasNook.Behavior
{
    [ActionSteps]
    public class CompanyFulfillsAnOrder
    {
        private Community _frontOfficeCommunity;
        private Community _kitchenCommunity;
        private Company _frontOfficeCompany;
        private Kitchen _kitchen;

        [Given("a front office machine and a kitchen machine in the same community")]
        public void SetupFrontOfficeAndKitchen()
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

        [When("the front office places an order")]
        public void FrontOfficePlacesAnOrder()
        {
            _frontOfficeCommunity.AddFact(new Order(_frontOfficeCompany));
            Synchronize();
        }

        [Then("kitchen sees empty backlog")]
        public void KitchenSeesAnEmptyBacklog()
        {
            Assert.IsFalse(_kitchen.Company.BackloggedOrders.Any());
        }

        [Then("kitchen sees a backlogged order")]
        public void KitchenSeesABackloggedOrder()
        {
            Assert.IsTrue(_kitchen.Company.BackloggedOrders.Any());
        }

        private void Synchronize()
        {
            while (_frontOfficeCommunity.Synchronize() || _kitchenCommunity.Synchronize()) ;
        }
    }
}
