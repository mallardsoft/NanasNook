using System.Linq;
using NanasNook.Model;
using NanasNook.ViewModel.FrontOfficeScreen;
using NanasNook.ViewModel.ProvisionScreen;
using NanasNook.ViewModel.KitchenScreen;
using System;

namespace NanasNook
{
    public class MainViewModel
    {
        private Machine _machine;
        private ProvisionNavigationModel _provisionNavigationModel = new ProvisionNavigationModel();
        private FrontOfficeNavigationModel _frontOfficeNavigationModel = new FrontOfficeNavigationModel();
        private KitchenNavigationModel _kitchenNavigationModel = new KitchenNavigationModel();

        public MainViewModel(Machine machine)
        {
            _machine = machine;
        }

        public string Title
        {
            get
            {
                ProvisionFrontOffice provisionFrontOffice = _machine.CurrentProvisionFrontOffice.FirstOrDefault();
                ProvisionKitchen provisionKitchen = _machine.CurrentProvisionKitchen.FirstOrDefault();
                return
                    provisionFrontOffice != null ? string.Format("Company {0}",
                        provisionFrontOffice.Company.Identifier) :
                    provisionKitchen != null ? string.Format("Kitchen {0} {1}",
                        provisionKitchen.Kitchen.Company.Identifier,
                        provisionKitchen.Kitchen.Name) :
                    "Provision your machine";
            }
        }

        public ProvisionViewModel Provision
        {
            get
            {
                return _machine.CurrentProvisionFrontOffice.Any() || _machine.CurrentProvisionKitchen.Any()
                    ? null
                    : new ProvisionViewModel(_machine, _provisionNavigationModel);
            }
        }

        public DeprovisionViewModel Deprovision
        {
            get
            {
                ProvisionFrontOffice provisionFrontOffice = _machine.CurrentProvisionFrontOffice.FirstOrDefault();
                ProvisionKitchen provisionKitchen = _machine.CurrentProvisionKitchen.FirstOrDefault();
                return
                    provisionFrontOffice != null ? (DeprovisionViewModel)new DeprovisionFrontOfficeViewModel(provisionFrontOffice) :
                    provisionKitchen != null ? (DeprovisionViewModel)new DeprovisionKitchenViewModel(provisionKitchen) :
                    null;
            }
        }

        public FrontOfficeViewModel FrontOffice
        {
            get
            {
                ProvisionFrontOffice provisionFrontOffice = _machine.CurrentProvisionFrontOffice.FirstOrDefault();
                return provisionFrontOffice == null
                    ? null
                    : new FrontOfficeViewModel(provisionFrontOffice.Company, _frontOfficeNavigationModel);
            }
        }

        public KitchenViewModel Kitchen
        {
            get
            {
                ProvisionKitchen provisionKitchen = _machine.CurrentProvisionKitchen.FirstOrDefault();
                return provisionKitchen == null
                    ? null
                    : new KitchenViewModel(provisionKitchen.Kitchen, _kitchenNavigationModel);
            }
        }
    }
}
