using System.Windows;
using System.Windows.Controls;
using NanasNook.ViewModel.FrontOfficeScreen;
using UpdateControls.XAML;

namespace NanasNook
{
	/// <summary>
	/// Interaction logic for FrontOfficeControl.xaml
	/// </summary>
	public partial class FrontOfficeControl : UserControl
	{
		public FrontOfficeControl()
		{
			this.InitializeComponent();
		}

		private void NewOrder_Click(object sender, RoutedEventArgs e)
		{
			FrontOfficeViewModel vm = ForView.Unwrap<FrontOfficeViewModel>(this.DataContext);
			if (vm != null)
			{
				NewOrderDialog dialog = new NewOrderDialog();
				OrderSnapshot newOrderDetails = new OrderSnapshot();
				dialog.DataContext = new OrderSnapshotViewModel(vm.Company, newOrderDetails);
				if (dialog.ShowDialog() ?? false)
				{
					vm.NewOrder(newOrderDetails);
				}
			}
		}

        private void ChangeContactInformation_Click(object sender, RoutedEventArgs e)
        {
            FrontOfficeOrderDetailsViewModel vm = ForView.Unwrap<FrontOfficeOrderDetailsViewModel>(((FrameworkElement)sender).DataContext);
            if (vm != null)
            {
                ContactInformationDialog dialog = new ContactInformationDialog();
                OrderSnapshotViewModel orderSnapshotViewModel = vm.CreateOrderSnapshotViewModel();
                dialog.DataContext = orderSnapshotViewModel;
                if (dialog.ShowDialog() ?? false)
                {
                    vm.ChangeContactInformation(orderSnapshotViewModel.OrderSnapshot.ContactInformation);
                }
            }
        }

        private void ChangeCakeInformation_Click(object sender, RoutedEventArgs e)
        {
            FrontOfficeOrderDetailsViewModel vm = ForView.Unwrap<FrontOfficeOrderDetailsViewModel>(((FrameworkElement)sender).DataContext);
            if (vm != null)
            {
                CakeDetailDialog dialog = new CakeDetailDialog();
                OrderSnapshotViewModel orderSnapshotViewModel = vm.CreateOrderSnapshotViewModel();
                dialog.DataContext = orderSnapshotViewModel;
                if (dialog.ShowDialog() ?? false)
                {
                    vm.ChangeCakeDetail(orderSnapshotViewModel.OrderSnapshot.CakeDetail);
                }
            }
        }

        private void ChangeDeliveryInformation_Click(object sender, RoutedEventArgs e)
        {
            FrontOfficeOrderDetailsViewModel vm = ForView.Unwrap<FrontOfficeOrderDetailsViewModel>(((FrameworkElement)sender).DataContext);
            if (vm != null)
            {
                DeliveryDetailDialog dialog = new DeliveryDetailDialog();
                OrderSnapshotViewModel orderSnapshotViewModel = vm.CreateOrderSnapshotViewModel();
                dialog.DataContext = orderSnapshotViewModel;
                if (dialog.ShowDialog() ?? false)
                {
                    vm.ChangeDeliveryDetail(orderSnapshotViewModel.OrderSnapshot.DeliveryDetail);
                }
            }
        }
	}
}