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
            CakeSize cakeSizeFact = Community.AddFact(new CakeSize(Company, size));
            CakeFlavor cakeFlavorFact = Community.AddFact(new CakeFlavor(Company, cakeFlavor));
            FrostingFlavor frostingFlavorFact = Community.AddFact(new FrostingFlavor(Company, frostingFlavor));
            FrostingColor mainColorFact = Community.AddFact(new FrostingColor(Company, mainColor));
            FrostingColor decorationColorFact = Community.AddFact(new FrostingColor(Company, decorationColor));
            CakeDetail prototype = new CakeDetail(
                this,
                cakeSizeFact,
                cakeFlavorFact,
                frostingFlavorFact,
                mainColorFact,
                decorationColorFact,
                CurrentCakeDetails,
                message,
                cakeInstructions);
            Community.AddFact(prototype);
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
