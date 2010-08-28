using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NanasNook.Model
{
    public partial class ProvisionKitchen
    {
        public void Deprovision()
        {
            Community.AddFact(new DeprovisionKitchen(this));
        }
    }
}
