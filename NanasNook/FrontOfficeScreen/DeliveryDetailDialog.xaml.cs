﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace NanasNook
{
	/// <summary>
	/// Interaction logic for DeliveryDetailDialog.xaml
	/// </summary>
	public partial class DeliveryDetailDialog : Window
	{
		public DeliveryDetailDialog()
		{
			this.InitializeComponent();
			
			// Insert code required on object creation below this point.
		}

        private void OK_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = true;
        }
	}
}