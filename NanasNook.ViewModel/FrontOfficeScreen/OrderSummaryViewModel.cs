using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NanasNook.Model;

namespace NanasNook.ViewModel.FrontOfficeScreen
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

        public string Name
        {
            get
            {
                return _order.CurrentContactInformationSet
                    .Select(c => c.Name)
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

        public string Status
        {
            get { return _order.Status; }
        }

        public override bool Equals(object obj)
        {
            if (obj == this)
                return true;
            OrderSummaryViewModel that = obj as OrderSummaryViewModel;
            if (that == null)
                return false;
            return this._order == that._order;
        }

        public override int GetHashCode()
        {
            return _order.GetHashCode();
        }
	}
}
