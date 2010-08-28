using System.Linq;
using NanasNook.Model;

namespace NanasNook.ViewModel.FrontOfficeScreen
{
    public class FrontOfficeOrderDetailsViewModel : OrderDetailsViewModel
    {
        private Order _order;

        public FrontOfficeOrderDetailsViewModel(Order order)
        {
            _order = order;
        }

        public override Order Order
        {
            get { return _order; }
        }

        public OrderSnapshotViewModel CreateOrderSnapshotViewModel()
        {
            OrderSnapshot newOrderDetails = new OrderSnapshot();
            InitializeContactInformation(newOrderDetails.ContactInformation);
            InitializeCakeDetail(newOrderDetails.CakeDetail);
            InitializeDeliveryDetail(newOrderDetails.DeliveryDetail);
            return new OrderSnapshotViewModel(_order.Company, newOrderDetails);
        }

        private void InitializeContactInformation(ContactInformationSnapshot snapshot)
        {
            var contactInformation = _order.CurrentContactInformationSet.FirstOrDefault();

            if (contactInformation != null)
            {
                snapshot.Name = contactInformation.Name;
                snapshot.PhoneNumber = contactInformation.PhoneNumber;
            }
        }

        private void InitializeCakeDetail(CakeDetailSnapshot snapshot)
        {
            CakeDetail cakeDetail = _order.CurrentCakeDetails.FirstOrDefault();

            if (cakeDetail != null)
            {
                snapshot.Size = cakeDetail.Size.Name;
                snapshot.CakeFlavor = cakeDetail.CakeFlavor.Name;
                snapshot.FrostingFlavor = cakeDetail.FrostingFlavor.Name;
                snapshot.MainColor = cakeDetail.MainColor.Name;
                snapshot.DecorationColor = cakeDetail.DecorationColor.Name;
                snapshot.Message = cakeDetail.Message;
                snapshot.CakeInstructions = cakeDetail.CakeInstructions;
            }
        }

        private void InitializeDeliveryDetail(DeliveryDetailSnapshot snapshot)
        {
            DeliveryDetail deliveryDetail = _order.CurrentDeliveryDetails.FirstOrDefault();

            if (deliveryDetail != null)
            {
                snapshot.City = deliveryDetail.City.Name;
                snapshot.StreetAddress = deliveryDetail.StreetAddress;
                snapshot.ExpectedDeliveryDate = deliveryDetail.ExpectedDeliveryDate;
                snapshot.DeliveryInstructions = deliveryDetail.DeliveryInstructions;
            }
        }

        public void ChangeContactInformation(ContactInformationSnapshot snapshot)
        {
            _order.SetCurrentContactInformation(
                snapshot.Name,
                snapshot.PhoneNumber);
        }

        public void ChangeCakeDetail(CakeDetailSnapshot snapshot)
        {
            _order.SetCurrentCakeDetails(
                snapshot.Size,
                snapshot.CakeFlavor,
                snapshot.FrostingFlavor,
                snapshot.MainColor,
                snapshot.DecorationColor,
                snapshot.Message,
                snapshot.CakeInstructions);
        }

        public void ChangeDeliveryDetail(DeliveryDetailSnapshot snapshot)
        {
            _order.SetCurrentDeliveryDetails(
                snapshot.City,
                snapshot.StreetAddress,
                snapshot.ExpectedDeliveryDate,
                snapshot.DeliveryInstructions);
        }
    }
}
