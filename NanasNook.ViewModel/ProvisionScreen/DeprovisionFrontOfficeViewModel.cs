using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Input;
using UpdateControls.XAML;
using NanasNook.Model;

namespace NanasNook.ViewModel.ProvisionScreen
{
    public class DeprovisionFrontOfficeViewModel : DeprovisionViewModel
    {
        private ProvisionFrontOffice _provision;

        public DeprovisionFrontOfficeViewModel(ProvisionFrontOffice provision)
        {
            _provision = provision;
        }

        public override ICommand Deprovision
        {
            get
            {
                return MakeCommand
                    .Do(() => _provision.Deprovision());
            }
        }
    }
}
