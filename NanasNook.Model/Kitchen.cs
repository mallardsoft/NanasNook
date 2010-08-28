using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NanasNook.Model
{
    public partial class Kitchen
    {
        public Pull PullOrder(Order order)
        {
            Pull pull = Community.AddFact(new Pull(order, this));
            Community.AddFact(new Commitment(pull, order.CurrentCakeDetails, Enumerable.Empty<Commitment>()));
            return pull;
        }
    }
}
