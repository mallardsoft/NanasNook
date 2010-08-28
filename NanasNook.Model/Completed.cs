using System.Linq;

namespace NanasNook.Model
{
    public partial class Completed
    {
        public void Deliver()
        {
            DeliveryDetail deliveryDetail = Commitment.Pull.Order.CurrentDeliveryDetails.FirstOrDefault();
            if (deliveryDetail != null)
                Community.AddFact(new Delivered(this, deliveryDetail));
        }
    }
}
