using System;
using System.Linq;
using NanasNook.Model;

namespace NanasNook.ViewModel.KitchenScreen
{
    public class CompletedSummaryViewModel
    {
        private Completed _completed;

        public CompletedSummaryViewModel(Completed completed)
        {
            _completed = completed;
        }

        public Completed Completed
        {
            get { return _completed; }
        }

        public string City
        {
            get
            {
                return _completed.Commitment.Pull.Order.CurrentDeliveryDetails
                    .Select(d => d.City.Name)
                    .FirstOrDefault();
            }
        }

        public DateTime ExpectedDeliveryDate
        {
            get
            {
                return _completed.Commitment.Pull.Order.CurrentDeliveryDetails
                    .Select(d => d.ExpectedDeliveryDate)
                    .FirstOrDefault();
            }
        }

        public string Size
        {
            get
            {
                return _completed.Commitment.CakeDetail
                    .Select(d => d.Size.Name)
                    .FirstOrDefault();
            }
        }

        public override bool Equals(object obj)
        {
            if (obj == this)
                return true;
            CompletedSummaryViewModel that = obj as CompletedSummaryViewModel;
            if (that == null)
                return false;
            return this._completed.Equals(that._completed);
        }

        public override int GetHashCode()
        {
            return _completed.GetHashCode();
        }
    }
}
