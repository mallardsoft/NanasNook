﻿using UpdateControls;

namespace NanasNook.ViewModel.ProvisionScreen
{
    public class ProvisionNavigationModel
    {
        private string _companyName;
        private string _kitchenName;

        #region Independent properties
        // Generated by Update Controls --------------------------------
        private Independent _indCompanyName = new Independent();
        private Independent _indKitchenName = new Independent();

        public string CompanyName
        {
            get { _indCompanyName.OnGet(); return _companyName; }
            set { _indCompanyName.OnSet(); _companyName = value; }
        }

        public string KitchenName
        {
            get { _indKitchenName.OnGet(); return _kitchenName; }
            set { _indKitchenName.OnSet(); _kitchenName = value; }
        }
        // End generated code --------------------------------
        #endregion
    }
}
