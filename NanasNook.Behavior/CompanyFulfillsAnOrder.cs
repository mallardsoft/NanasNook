using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NBehave.Narrator.Framework;
using UpdateControls.Correspondence;
using NanasNook.Model;
using UpdateControls.Correspondence.Memory;
//using Microsoft.VisualStudio.TestTools.UnitTesting;
using NUnit.Framework;

namespace NanasNook.Behavior
{
    [ActionSteps]
    public class CompanyFulfillsAnOrder
    {
        private MemoryCommunicationStrategy _sharedCommunication;
        private Community _frontOfficeCommunity;
        private Community _kitchenCommunity;
        private Company _frontOfficeCompany;
        private Kitchen _kitchen;

        [Given("a community")]
        public void SetupCommunity()
        {
            _sharedCommunication = new MemoryCommunicationStrategy();
        }

        [Given("a front office machine")]
        public void SetupFrontOffice()
        {
            _frontOfficeCommunity = new Community(new MemoryStorageStrategy())
                .AddCommunicationStrategy(_sharedCommunication)
                .RegisterAssembly(typeof(Machine))
                .Subscribe(() => _frontOfficeCompany);

            Machine frontOfficeMachine = _frontOfficeCommunity.AddFact(new Machine());
            _frontOfficeCompany = _frontOfficeCommunity.AddFact(new Company("NanasNook"));
            _frontOfficeCommunity.AddFact(new ProvisionFrontOffice(frontOfficeMachine, _frontOfficeCompany));
        }

        [Given("a kitchen machine")]
        public void SetupKitchen()
        {
            _kitchenCommunity = new Community(new MemoryStorageStrategy())
                .AddCommunicationStrategy(_sharedCommunication)
                .RegisterAssembly(typeof(Machine))
                .Subscribe(() => _kitchen.Company);

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

        [When("the kitchen pulls an order")]
        public void KitchenPullsAnOrder()
        {
            _kitchenCommunity.AddFact(new Pull(_kitchen.Company.BackloggedOrders.Single(), _kitchen));
            Synchronize();
        }

        [When("the kitchen commits to an order")]
        public void KitchenCommitsToAnOrder()
        {
            _kitchenCommunity.AddFact(
                new Commitment(
                    _kitchen.PullsInProgress.Single(),
                    Enumerable.Empty<CakeDetail>(),
                    Enumerable.Empty<Commitment>()));
        }

        [When("the kitchen completes an order")]
        public void KitchenCompletesAnOrder()
        {
            _kitchenCommunity.AddFact(
                new Completed(
                    _kitchen.PullsInProgress.Single().CurrentCommitments.Single()));
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

        [Then("the order is backlogged")]
        public void TheOrderIsBacklogged()
        {
            Assert.IsTrue(_frontOfficeCompany.BackloggedOrders.Single().IsBacklogged);
        }

        [Then("the order is in progress")]
        public void TheOrderIsInProgress()
        {
            Assert.IsTrue(_frontOfficeCompany.PendingOrders.Single().IsInProgress);
        }

        [Then("the order is completed")]
        public void TheOrderIsCompleted()
        {
            Assert.IsTrue(_frontOfficeCompany.PendingOrders.Single().IsCompleted);
        }

        private void Synchronize()
        {
            while (_frontOfficeCommunity.Synchronize() || _kitchenCommunity.Synchronize()) ;
        }
    }
}
