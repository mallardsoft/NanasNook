
namespace NanasNook.ViewModel.FrontOfficeScreen
{
    public class OrderSnapshot
    {
		private ContactInformationSnapshot _contactInformation = new ContactInformationSnapshot();
		private CakeDetailSnapshot _cakeDetail = new CakeDetailSnapshot();
		private DeliveryDetailSnapshot _deliveryDetail = new DeliveryDetailSnapshot();

		public ContactInformationSnapshot ContactInformation
		{
			get { return _contactInformation; }
		}

		public CakeDetailSnapshot CakeDetail
		{
			get { return _cakeDetail; }
		}

		public DeliveryDetailSnapshot DeliveryDetail
		{
			get { return _deliveryDetail; }
		}
    }
}
