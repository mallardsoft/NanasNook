using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NanasNook.Model
{
    public partial class Commitment
    {
        public Completed Complete()
        {
            return Community.AddFact(new Completed(this));
        }
    }
}
