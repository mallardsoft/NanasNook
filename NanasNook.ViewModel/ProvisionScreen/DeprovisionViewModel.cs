﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Input;

namespace NanasNook.ViewModel.ProvisionScreen
{
    public abstract class DeprovisionViewModel
    {
        public abstract ICommand Deprovision { get; }
    }
}
