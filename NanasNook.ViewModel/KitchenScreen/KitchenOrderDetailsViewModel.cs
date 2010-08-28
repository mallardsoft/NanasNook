using NanasNook.Model;

namespace NanasNook.ViewModel.KitchenScreen
{
    public class KitchenOrderDetailsViewModel : OrderDetailsViewModel
    {
        private Order _order;

        public KitchenOrderDetailsViewModel(Order order)
        {
            _order = order;
        }

        public override Order Order
        {
            get { return _order; }
        }
    }
}
