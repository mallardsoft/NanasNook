using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NanasNook.Model;
using UpdateControls.XAML;
using System.Windows.Input;

namespace NanasNook.ViewModel.FrontOfficeScreen
{
    public class FrontOfficeViewModel
    {
        private Company _company;
        private FrontOfficeNavigationModel _navigation;

        public FrontOfficeViewModel(Company company, FrontOfficeNavigationModel navigation)
        {
            _company = company;
            _navigation = navigation;
        }

		public Company Company
		{
			get { return _company; }
		}

		public OrderSnapshot NewOrderDetails
        {
            get { return _navigation.NewOrderDetails; }
        }

        public ICommand CreateOrder
        {
            get
            {
                return MakeCommand
                    .Do(() =>
                    {
                        Order order = _company.NewOrder();
                        order.SetCurrentCakeDetails(
                            _navigation.NewOrderDetails.CakeDetail.Size,
							_navigation.NewOrderDetails.CakeDetail.CakeFlavor,
							_navigation.NewOrderDetails.CakeDetail.FrostingFlavor,
							_navigation.NewOrderDetails.CakeDetail.MainColor,
							_navigation.NewOrderDetails.CakeDetail.DecorationColor,
							_navigation.NewOrderDetails.CakeDetail.Message,
							_navigation.NewOrderDetails.CakeDetail.CakeInstructions);
                        order.SetCurrentDeliveryDetails(
                            _navigation.NewOrderDetails.DeliveryDetail.City,
							_navigation.NewOrderDetails.DeliveryDetail.StreetAddress,
							_navigation.NewOrderDetails.DeliveryDetail.ExpectedDeliveryDate,
							_navigation.NewOrderDetails.DeliveryDetail.DeliveryInstructions);
                        _navigation.SelectedOrder = order;
                    });
            }
        }

		public void NewOrder(OrderSnapshot newOrderDetails)
		{
            Order order = _company.NewOrder();
			order.SetCurrentContactInformation(
				newOrderDetails.ContactInformation.Name,
				newOrderDetails.ContactInformation.PhoneNumber);
            order.SetCurrentCakeDetails(
                newOrderDetails.CakeDetail.Size,
				newOrderDetails.CakeDetail.CakeFlavor,
				newOrderDetails.CakeDetail.FrostingFlavor,
				newOrderDetails.CakeDetail.MainColor,
				newOrderDetails.CakeDetail.DecorationColor,
				newOrderDetails.CakeDetail.Message,
				newOrderDetails.CakeDetail.CakeInstructions);
            order.SetCurrentDeliveryDetails(
                newOrderDetails.DeliveryDetail.City,
				newOrderDetails.DeliveryDetail.StreetAddress,
				newOrderDetails.DeliveryDetail.ExpectedDeliveryDate,
				newOrderDetails.DeliveryDetail.DeliveryInstructions);
            _navigation.SelectedOrder = order;
		}

        public IEnumerable<OrderSummaryViewModel> PendingOrders
        {
            get
            {
                return _company.PendingOrders
                    .Select(o => new OrderSummaryViewModel(o));
            }
        }

        public OrderSummaryViewModel SelectedOrderSummary
        {
            get
            {
                return _navigation.SelectedOrder == null
                    ? null
                    : new OrderSummaryViewModel(_navigation.SelectedOrder);
            }
            set
            {
            	_navigation.SelectedOrder = value == null ? null : value.Order;
            }
        }

        public FrontOfficeOrderDetailsViewModel SelectedOrderDetails
        {
            get
            {
                return _navigation.SelectedOrder == null
                    ? null
                    : new FrontOfficeOrderDetailsViewModel(_navigation.SelectedOrder);
            }
        }
    }
}
