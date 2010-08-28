using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NanasNook.Model
{
    public partial class Company
    {
        public Kitchen NewKitchen(string kitchenName)
        {
            return Community.AddFact(new Kitchen(this, kitchenName));
        }

        public Order NewOrder()
        {
            return Community.AddFact(new Order(this));
        }
    }
}
