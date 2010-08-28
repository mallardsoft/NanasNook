using System.Collections.Generic;
using System.Linq;
using System.Windows.Input;
using NanasNook.Model;
using UpdateControls.XAML;
using System;

namespace NanasNook.ViewModel.KitchenScreen
{
    public class PullSummaryViewModel
    {
        private Pull _pull;

        public PullSummaryViewModel(Pull pull)
        {
            _pull = pull;
        }

        public Pull Pull
        {
            get { return _pull; }
        }

        public string Size
        {
            get
            {
                return _pull.Order.CurrentCakeDetails
                    .Select(d => d.Size.Name)
                    .FirstOrDefault();
            }
        }

        public string City
        {
            get
            {
                return _pull.Order.CurrentDeliveryDetails
                    .Select(d => d.City.Name)
                    .FirstOrDefault();
            }
        }

        public DateTime ExpectedDeliveryDate
        {
            get
            {
                return _pull.Order.CurrentDeliveryDetails
                    .Select(d => d.ExpectedDeliveryDate)
                    .FirstOrDefault();
            }
        }

        public override bool Equals(object obj)
        {
            if (obj == this)
                return true;
            PullSummaryViewModel that = obj as PullSummaryViewModel;
            if (that == null)
                return false;
            return this._pull.Equals(that._pull);
        }

        public override int GetHashCode()
        {
            return _pull.GetHashCode();
        }
    }
}
