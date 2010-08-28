using System;
using System.Linq;
using NanasNook.Model;

namespace NanasNook.ViewModel.KitchenScreen
{
    public class OrderSummaryViewModel
    {
        private Order _order;

        public OrderSummaryViewModel(Order order)
        {
            _order = order;
        }

        public Order Order
        {
            get { return _order; }
        }

        public string Size
        {
            get
            {
                return _order.CurrentCakeDetails
                    .Select(d => d.Size.Name)
                    .FirstOrDefault();
            }
        }

        public string City
        {
            get
            {
                return _order.CurrentDeliveryDetails
                    .Select(d => d.City.Name)
                    .FirstOrDefault();
            }
        }

        public DateTime ExpectedDeliveryDate
        {
            get
            {
                return _order.CurrentDeliveryDetails
                    .Select(d => d.ExpectedDeliveryDate)
                    .FirstOrDefault();
            }
        }

        public override bool Equals(object obj)
        {
            if (obj == this)
                return true;
            OrderSummaryViewModel that = obj as OrderSummaryViewModel;
            if (that == null)
                return false;
            return this._order.Equals(that._order);
        }

        public override int GetHashCode()
        {
            return _order.GetHashCode();
        }
    }
}
