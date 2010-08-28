
namespace NanasNook.Model
{
    public partial class Machine
    {
        public void ProvisionFrontOfficeMachine(string companyName)
        {
            Company company = Community.AddFact(new Company(companyName));
            Community.AddFact(new ProvisionFrontOffice(this, company));
        }

        public void ProvisionKitchenMachine(string companyName, string kitchenName)
        {
            Company company = Community.AddFact(new Company(companyName));
            Kitchen kitchen = Community.AddFact(new Kitchen(company, kitchenName));
            Community.AddFact(new ProvisionKitchen(this, kitchen));
        }
    }
}
