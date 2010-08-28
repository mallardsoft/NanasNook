using NanasNook.Model;

namespace NanasNook.ViewModel.KitchenScreen
{
    public class KitchenCompletedDetailsViewModel : OrderDetailsViewModel
    {
        private Completed _completed;

        public KitchenCompletedDetailsViewModel(Completed completed)
        {
            _completed = completed;
        }

        public Completed Completed
        {
            get { return _completed; }
        }

        public override Order Order
        {
            get { return _completed.Commitment.Pull.Order; }
        }
    }
}
