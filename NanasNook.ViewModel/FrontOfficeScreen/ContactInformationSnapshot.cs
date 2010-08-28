using UpdateControls;

namespace NanasNook.ViewModel.FrontOfficeScreen
{
	public class ContactInformationSnapshot
	{
		private string _name = string.Empty;
		private string _phoneNumber = string.Empty;

		#region Independent properties
		// Generated by Update Controls --------------------------------
		private Independent _indName = new Independent();
		private Independent _indPhoneNumber = new Independent();

		public string Name
		{
			get { _indName.OnGet(); return _name; }
			set { _indName.OnSet(); _name = value; }
		}

		public string PhoneNumber
		{
			get { _indPhoneNumber.OnGet(); return _phoneNumber; }
			set { _indPhoneNumber.OnSet(); _phoneNumber = value; }
		}
		// End generated code --------------------------------
		#endregion

		public void Reset()
		{
			Name = string.Empty;
			PhoneNumber = string.Empty;
		}
	}
}
