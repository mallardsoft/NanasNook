using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NanasNook.Model;
using System.Windows.Input;
using UpdateControls.XAML;

namespace NanasNook.ViewModel.ProvisionScreen
{
    public class DeprovisionKitchenViewModel : DeprovisionViewModel
    {
        private ProvisionKitchen _provisionKitchen;

        public DeprovisionKitchenViewModel(ProvisionKitchen provisionKitchen)
        {
            _provisionKitchen = provisionKitchen;
        }

        public override ICommand Deprovision
        {
            get
            {
                return MakeCommand
                    .Do(() => _provisionKitchen.Deprovision());
            }
        }
    }
}
