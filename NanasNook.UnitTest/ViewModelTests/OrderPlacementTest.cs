using System;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NanasNook.Model;
using UpdateControls.Correspondence;
using UpdateControls.Correspondence.Memory;
using f = NanasNook.ViewModel.FrontOfficeScreen;
using k = NanasNook.ViewModel.KitchenScreen;

namespace NanasNook.UnitTest.ViewModelTests
{
    [TestClass]
    public class OrderPlacementTest
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

        #endregion

        [TestMethod]
        public void OrderACake_AppearsInBacklog()
        {
            k.OrderSummaryViewModel order = PlaceOrder();

            Assert.AreEqual("Full Sheet 3\"", order.Size);
            Assert.AreEqual("Allen", order.City);
            Assert.AreEqual(new DateTime(2010, 8, 19, 14, 0, 0), order.ExpectedDeliveryDate);
        }

        [TestMethod]
        public void PullAnOrder_DisappearsFromBacklog()
        {
            k.OrderSummaryViewModel order = PlaceOrder();
            PullOrder(order);

            Assert.IsFalse(_kitchenViewModel.BackloggedOrders.Any());
        }

        [TestMethod]
        public void PullAnOrder_AppearsInOrdersInProgress()
        {
            k.OrderSummaryViewModel order = PlaceOrder();
            PullOrder(order);

            Assert.AreEqual(1, _kitchenViewModel.OrdersInProgress.Count());
        }

        [TestMethod]
        public void PullAnOrder_OrderInProgressIsSelected()
        {
            k.OrderSummaryViewModel order = PlaceOrder();
            PullOrder(order);

            Assert.AreEqual(_kitchenViewModel.OrdersInProgress.Single(), _kitchenViewModel.SelectedInProgressOrder);
        }

        [TestMethod]
        public void PullAnOrder_StateChangesToInProgress()
        {
            k.OrderSummaryViewModel order = PlaceOrder();
            PullOrder(order);
            Synchronize();

            Assert.AreEqual("In Progress", _frontOfficeViewModel.PendingOrders.Single().Status);
        }

        #region User emulation

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

        private k.OrderSummaryViewModel PlaceOrder()
        {
            EnterNewOrderDetails();
            _frontOfficeViewModel.CreateOrder.Execute(null);
            Synchronize();
            return _kitchenViewModel.BackloggedOrders.Single();
        }

        private void PullOrder(k.OrderSummaryViewModel order)
        {
            _kitchenViewModel.SelectedBackloggedOrder = order;
            _kitchenViewModel.Pull.Execute(null);
        }

        #endregion

        private void Synchronize()
        {
            while (_frontOfficeCommunity.Synchronize() || _kitchenCommunity.Synchronize()) ;
        }
    }
}
