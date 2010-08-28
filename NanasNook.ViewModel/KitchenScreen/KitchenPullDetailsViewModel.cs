using NanasNook.Model;

namespace NanasNook.ViewModel.KitchenScreen
{
    public class KitchenPullDetailsViewModel : OrderDetailsViewModel
    {
        private Pull _pull;

        public KitchenPullDetailsViewModel(Pull pull)
        {
            _pull = pull;
        }

        public override Order Order
        {
            get { return _pull.Order; }
        }
    }
}
