﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NanasNook.Model;
using UpdateControls;

namespace NanasNook.ViewModel.FrontOfficeScreen
{
    public class FrontOfficeNavigationModel
    {
        private OrderSnapshot _newOrderDetails = new OrderSnapshot();
        private Order _selectedOrder;

        public OrderSnapshot NewOrderDetails
        {
            get { return _newOrderDetails; }
        }

        #region Independent properties
        // Generated by Update Controls --------------------------------
        private Independent _indSelectedOrder = new Independent();

        public Order SelectedOrder
        {
            get { _indSelectedOrder.OnGet(); return _selectedOrder; }
            set { _indSelectedOrder.OnSet(); _selectedOrder = value; }
        }
        // End generated code --------------------------------
        #endregion
    }
}
