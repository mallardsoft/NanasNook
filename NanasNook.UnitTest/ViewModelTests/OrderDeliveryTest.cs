using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using UpdateControls.Correspondence;
using f = NanasNook.ViewModel.FrontOfficeScreen;
using k = NanasNook.ViewModel.KitchenScreen;
using UpdateControls.Correspondence.Memory;
using NanasNook.Model;

namespace NanasNook.UnitTest.ViewModelTests
{
    [TestClass]
    public class OrderDeliveryTest
    {
        private Community _frontOfficeCommunity;
        private Community _kitchenCommunity;
        private f.FrontOfficeViewModel _frontOfficeViewModel;
        private k.KitchenViewModel _kitchenViewModel;

        [TestInitialize]
        public void Initialize()
        {
            MemoryCommunicationStrategy sharedCommunicationStrategy = new MemoryCommunicationStrategy();

            ProvisionFrontOffice(sharedCommunicationStrategy);
            ProvisionKitchen(sharedCommunicationStrategy);
            PlacePullAndCompleteOrder();
        }

        #region Initialization

        private Company _frontOfficeCompany;
        private void ProvisionFrontOffice(MemoryCommunicationStrategy sharedCommunicationStrategy)
        {
            _frontOfficeCommunity = new Community(new MemoryStorageStrategy())
                .AddCommunicationStrategy(sharedCommunicationStrategy)
                .RegisterAssembly(typeof(Machine))
                .Subscribe(() => _frontOfficeCompany);
            _frontOfficeCompany = _frontOfficeCommunity.AddFact(new Company("Nanas Nook"));
            _frontOfficeViewModel = new f.FrontOfficeViewModel(_frontOfficeCompany, new f.FrontOfficeNavigationModel());
        }

        private Company _kitchenCompany;
        private void ProvisionKitchen(MemoryCommunicationStrategy sharedCommunicationStrategy)
        {
            _kitchenCommunity = new Community(new MemoryStorageStrategy())
                .AddCommunicationStrategy(sharedCommunicationStrategy)
                .RegisterAssembly(typeof(Machine))
                .Subscribe(() => _kitchenCompany);
            _kitchenCompany = _kitchenCommunity.AddFact(new Company("Nanas Nook"));
            Kitchen kitchen = _kitchenCompany.NewKitchen("Market Street");
            _kitchenViewModel = new k.KitchenViewModel(kitchen, new k.KitchenNavigationModel());
        }

        private void EnterNewOrderDetails()
        {
            _frontOfficeViewModel.NewOrderDetails.ContactInformation.Name = "Michael L Perry";
            _frontOfficeViewModel.NewOrderDetails.ContactInformation.PhoneNumber = "097-0286";

            _frontOfficeViewModel.NewOrderDetails.CakeDetail.Size = "Full Sheet 3\"";
            _frontOfficeViewModel.NewOrderDetails.CakeDetail.CakeFlavor = "Red Velvet";
            _frontOfficeViewModel.NewOrderDetails.CakeDetail.FrostingFlavor = "Cream Cheese";
            _frontOfficeViewModel.NewOrderDetails.CakeDetail.MainColor = "White";
            _frontOfficeViewModel.NewOrderDetails.CakeDetail.DecorationColor = "Red";
            _frontOfficeViewModel.NewOrderDetails.CakeDetail.Message = "Happy Birthday, Mike";
            _frontOfficeViewModel.NewOrderDetails.CakeDetail.CakeInstructions = "Red rosettes";

            _frontOfficeViewModel.NewOrderDetails.DeliveryDetail.City = "Allen";
            _frontOfficeViewModel.NewOrderDetails.DeliveryDetail.StreetAddress = "412 Chyenne";
            _frontOfficeViewModel.NewOrderDetails.DeliveryDetail.ExpectedDeliveryDate = new DateTime(2010, 8, 19, 14, 0, 0);
            _frontOfficeViewModel.NewOrderDetails.DeliveryDetail.DeliveryInstructions = "Come to the back gate.";
        }

        private void PlacePullAndCompleteOrder()
        {
            EnterNewOrderDetails();
            _frontOfficeViewModel.CreateOrder.Execute(null);
            Synchronize();
            _kitchenViewModel.SelectedBackloggedOrder = _kitchenViewModel.BackloggedOrders.Single();
            _kitchenViewModel.Pull.Execute(null);
            _kitchenViewModel.Complete.Execute(null);
            Synchronize();
        }

        #endregion

        [TestMethod]
        public void DeliverAnOrder_DisappearsFromOrdersCompleted()
        {
            DeliverOrder();

            Assert.IsFalse(_kitchenViewModel.OrdersCompleted.Any());
        }

        [TestMethod]
        public void DeliverAnOrder_NoOrderIsSelected()
        {
            DeliverOrder();

            Assert.IsNull(_kitchenViewModel.SelectedOrderDetails);
        }

        [TestMethod]
        public void DeliverAnOrder_StatusIsDelivered()
        {
            DeliverOrder();
            Synchronize();

            Assert.AreEqual("Delivered", _frontOfficeViewModel.PendingOrders.Single().Status);
        }

        #region User emulation

        private void DeliverOrder()
        {
            _kitchenViewModel.Deliver.Execute(null);
        }

        #endregion

        private void Synchronize()
        {
            while (_frontOfficeCommunity.Synchronize() || _kitchenCommunity.Synchronize()) ;
        }
    }
}
