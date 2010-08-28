using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Input;
using UpdateControls.XAML;
using NanasNook.Model;

namespace NanasNook.ViewModel.ProvisionScreen
{
	public class ProvisionViewModel
	{
		private Machine _machine;
        private ProvisionNavigationModel _navigation;

        public ProvisionViewModel(Machine machine, ProvisionNavigationModel navigation)
        {
            _machine = machine;
            _navigation = navigation;
        }

        public string CompanyName
        {
            get { return _navigation.CompanyName; }
            set { _navigation.CompanyName = value; }
        }

        public string KitchenName
        {
            get { return _navigation.KitchenName; }
            set { _navigation.KitchenName = value; }
        }

        public ICommand ProvisionFrontOffice
        {
            get
            {
                return MakeCommand
                    .When(() => !string.IsNullOrEmpty(_navigation.CompanyName))
                    .Do(() => _machine.ProvisionFrontOfficeMachine(_navigation.CompanyName));
            }
        }

        public ICommand ProvisionKitchen
        {
            get
            {
                return MakeCommand
                    .When(() => !string.IsNullOrEmpty(_navigation.CompanyName) && !string.IsNullOrEmpty(_navigation.KitchenName))
                    .Do(() => _machine.ProvisionKitchenMachine(_navigation.CompanyName, _navigation.KitchenName));
            }
        }
	}
}
