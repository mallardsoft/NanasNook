using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NanasNook.Model
{
    public partial class Order
    {
		public void SetCurrentContactInformation(
			string name,
			string phoneNumber)
		{
			Community.AddFact(new ContactInformation(
				this, 
                CurrentContactInformationSet, 
                name, 
                phoneNumber));
		}

		public void SetCurrentCakeDetails(
            string size, 
            string cakeFlavor, 
            string frostingFlavor, 
            string mainColor, 
            string decorationColor, 
            string message, 
            string cakeInstructions)
        {
            Community.AddFact(new CakeDetail(
                this,
                Community.AddFact(new CakeSize(Company, size)),
                Community.AddFact(new CakeFlavor(Company, cakeFlavor)),
                Community.AddFact(new FrostingFlavor(Company, frostingFlavor)),
                Community.AddFact(new FrostingColor(Company, mainColor)),
                Community.AddFact(new FrostingColor(Company, decorationColor)),
                CurrentCakeDetails,
                message,
                cakeInstructions));
        }

        public void SetCurrentDeliveryDetails(
            string city, 
            string streetAddress, 
            DateTime expectedDeliveryDate, 
            string deliveryInstructions)
        {
            Community.AddFact(new DeliveryDetail(
                this,
                Community.AddFact(new City(Company, city)),
                CurrentDeliveryDetails,
                streetAddress,
                expectedDeliveryDate,
                deliveryInstructions));
        }

        public string Status
        {
            get
            {
                return
                    IsBacklogged ? "Backlogged" :
                    IsInProgress ? "In Progress" :
                    IsCompleted ? "Completed" :
                    IsPending ? "Delivered" :
                        "Closed";
            }
        }
    }
}
