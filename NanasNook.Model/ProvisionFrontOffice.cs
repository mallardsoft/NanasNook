using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NanasNook.Model
{
    public partial class ProvisionFrontOffice
    {
        public void Deprovision()
        {
            Community.AddFact(new DeprovisionFrontOffice(this));
        }
    }
}
