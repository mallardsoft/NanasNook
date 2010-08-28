using System;
using System.Collections.Generic;
using System.Linq;
using NanasNook.Model;

namespace NanasNook.ViewModel.FrontOfficeScreen
{
	public class OrderSnapshotViewModel
	{
		private Company _company;
		private OrderSnapshot _orderSnapshot;

		public OrderSnapshotViewModel(Company company, OrderSnapshot orderSnapshot)
		{
			_company = company;
			_orderSnapshot = orderSnapshot;
		}

        public OrderSnapshot OrderSnapshot
        {
            get { return _orderSnapshot; }
        }

        public string Name
        {
			get { return _orderSnapshot.ContactInformation.Name; }
			set { _orderSnapshot.ContactInformation.Name = value; }
		}

		public string PhoneNumber
		{
			get { return _orderSnapshot.ContactInformation.PhoneNumber; }
			set { _orderSnapshot.ContactInformation.PhoneNumber = value; }
		}

		public string CakeSize
		{
			get { return _orderSnapshot.CakeDetail.Size; }
			set { _orderSnapshot.CakeDetail.Size = value; }
		}

		public IEnumerable<string> CakeSizes
		{
			get { return _company.CakeSizes.Select(s => s.Name); }
		}

		public string CakeFlavor
		{
			get { return _orderSnapshot.CakeDetail.CakeFlavor; }
			set { _orderSnapshot.CakeDetail.CakeFlavor = value; }
		}

		public IEnumerable<string> CakeFlavors
		{
			get { return _company.CakeFlavors.Select(f => f.Name); }
		}

		public string FrostingFlavor
		{
			get { return _orderSnapshot.CakeDetail.FrostingFlavor; }
			set { _orderSnapshot.CakeDetail.FrostingFlavor = value; }
		}

		public IEnumerable<string> FrostingFlavors
		{
			get { return _company.FrostingFlavors.Select(f => f.Name); }
		}

		public string MainColor
		{
			get { return _orderSnapshot.CakeDetail.MainColor; }
			set { _orderSnapshot.CakeDetail.MainColor = value; }
		}

		public string DecorationColor
		{
			get { return _orderSnapshot.CakeDetail.DecorationColor; }
			set { _orderSnapshot.CakeDetail.DecorationColor = value; }
		}

		public IEnumerable<string> FrostingColors
		{
			get { return _company.FrostingColors.Select(c => c.Name); }
		}

		public string Message
		{
			get { return _orderSnapshot.CakeDetail.Message; }
			set { _orderSnapshot.CakeDetail.Message = value; }
		}

		public string CakeInstructions
		{
			get { return _orderSnapshot.CakeDetail.CakeInstructions; }
			set { _orderSnapshot.CakeDetail.CakeInstructions = value; }
		}

		public string City
		{
			get { return _orderSnapshot.DeliveryDetail.City; }
			set { _orderSnapshot.DeliveryDetail.City = value; }
		}

		public IEnumerable<string> Cities
		{
			get { return _company.Cities.Select(c => c.Name); }
		}

		public string StreetAddress
		{
			get { return _orderSnapshot.DeliveryDetail.StreetAddress; }
			set { _orderSnapshot.DeliveryDetail.StreetAddress = value; }
		}

		public DateTime ExpectedDeliveryDate
		{
			get { return _orderSnapshot.DeliveryDetail.ExpectedDeliveryDate.Date; }
            set { _orderSnapshot.DeliveryDetail.ExpectedDeliveryDate = value + _orderSnapshot.DeliveryDetail.ExpectedDeliveryDate.TimeOfDay; }
		}

        public int ExpectedDeliveryTimeSlot
        {
            get
            {
                DateTime expectedDeliveryDate = _orderSnapshot.DeliveryDetail.ExpectedDeliveryDate;
                return
                    expectedDeliveryDate.Hour * 2 +
                    expectedDeliveryDate.Minute / 30;
            }
            set
            {
                _orderSnapshot.DeliveryDetail.ExpectedDeliveryDate =
                    _orderSnapshot.DeliveryDetail.ExpectedDeliveryDate.Date +
                    TimeSpan.FromMinutes(value * 30);
            }
        }

		public string DeliveryInstructions
		{
			get { return _orderSnapshot.DeliveryDetail.DeliveryInstructions; }
			set { _orderSnapshot.DeliveryDetail.DeliveryInstructions = value; }
		}
	}
}
