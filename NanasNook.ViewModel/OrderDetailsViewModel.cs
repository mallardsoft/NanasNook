using System;
using System.Linq;
using NanasNook.Model;

namespace NanasNook.ViewModel
{
    public abstract class OrderDetailsViewModel
    {
        public abstract Order Order { get; }

        public string Name
        {
            get
            {
                return Order.CurrentContactInformationSet
                    .Select(c => c.Name)
                    .FirstOrDefault();
            }
        }

        public string Phone
        {
            get
            {
                return Order.CurrentContactInformationSet
                    .Select(c => c.PhoneNumber)
                    .FirstOrDefault();
            }
        }

        public string Status
        {
            get { return Order.Status; }
        }

        public string Kitchen
        {
            get
            {
                return Order.Pulls
                    .Select(p => p.Kitchen.Name)
                    .FirstOrDefault();
            }
        }

        public string CakeSize
        {
            get
            {
                return Order.CurrentCakeDetails
                    .Select(d => d.Size.Name)
                    .FirstOrDefault();
            }
        }

        public string CakeFlavor
        {
            get
            {
                return Order.CurrentCakeDetails
                    .Select(d => d.CakeFlavor.Name)
                    .FirstOrDefault();
            }
        }

        public string FrostingFlavor
        {
            get
            {
                return Order.CurrentCakeDetails
                    .Select(d => d.FrostingFlavor.Name)
                    .FirstOrDefault();
            }
        }

        public string MainColor
        {
            get
            {
                return Order.CurrentCakeDetails
                    .Select(d => d.MainColor.Name)
                    .FirstOrDefault();
            }
        }

        public string DecorationColor
        {
            get
            {
                return Order.CurrentCakeDetails
                    .Select(d => d.DecorationColor.Name)
                    .FirstOrDefault();
            }
        }

        public string Message
        {
            get
            {
                return Order.CurrentCakeDetails
                    .Select(d => d.Message)
                    .FirstOrDefault();
            }
        }

        public string CakeInstructions
        {
            get
            {
                return Order.CurrentCakeDetails
                    .Select(d => d.CakeInstructions)
                    .FirstOrDefault();
            }
        }

        public string City
        {
            get
            {
                return Order.CurrentDeliveryDetails
                    .Select(d => d.City.Name)
                    .FirstOrDefault();
            }
        }

        public string StreetAddress
        {
            get
            {
                return Order.CurrentDeliveryDetails
                    .Select(d => d.StreetAddress)
                    .FirstOrDefault();
            }
        }

        public DateTime ExpectedDeliveryDate
        {
            get
            {
                return Order.CurrentDeliveryDetails
                    .Select(d => d.ExpectedDeliveryDate)
                    .FirstOrDefault();
            }
        }

        public string DeliveryInstructions
        {
            get
            {
                return Order.CurrentDeliveryDetails
                    .Select(d => d.DeliveryInstructions)
                    .FirstOrDefault();
            }
        }
    }
}
