using UpdateControls;

namespace NanasNook.ViewModel.FrontOfficeScreen
{
	public class CakeDetailSnapshot
	{
		private string _size = string.Empty;
		private string _cakeFlavor = string.Empty;
		private string _frostingFlavor = string.Empty;
		private string _mainColor = string.Empty;
		private string _decorationColor = string.Empty;
		private string _message = string.Empty;
		private string _cakeInstructions = string.Empty;

		#region Independent properties
		// Generated by Update Controls --------------------------------
		private Independent _indSize = new Independent();
		private Independent _indCakeFlavor = new Independent();
		private Independent _indFrostingFlavor = new Independent();
		private Independent _indMainColor = new Independent();
		private Independent _indDecorationColor = new Independent();
		private Independent _indMessage = new Independent();
		private Independent _indCakeInstructions = new Independent();

		public string Size
		{
			get { _indSize.OnGet(); return _size; }
			set { _indSize.OnSet(); _size = value; }
		}

		public string CakeFlavor
		{
			get { _indCakeFlavor.OnGet(); return _cakeFlavor; }
			set { _indCakeFlavor.OnSet(); _cakeFlavor = value; }
		}

		public string FrostingFlavor
		{
			get { _indFrostingFlavor.OnGet(); return _frostingFlavor; }
			set { _indFrostingFlavor.OnSet(); _frostingFlavor = value; }
		}

		public string MainColor
		{
			get { _indMainColor.OnGet(); return _mainColor; }
			set { _indMainColor.OnSet(); _mainColor = value; }
		}

		public string DecorationColor
		{
			get { _indDecorationColor.OnGet(); return _decorationColor; }
			set { _indDecorationColor.OnSet(); _decorationColor = value; }
		}

		public string Message
		{
			get { _indMessage.OnGet(); return _message; }
			set { _indMessage.OnSet(); _message = value; }
		}

		public string CakeInstructions
		{
			get { _indCakeInstructions.OnGet(); return _cakeInstructions; }
			set { _indCakeInstructions.OnSet(); _cakeInstructions = value; }
		}
		// End generated code --------------------------------
		#endregion

		public void Reset()
		{
			Size = string.Empty;
			CakeFlavor = string.Empty;
			FrostingFlavor = string.Empty;
			MainColor = string.Empty;
			DecorationColor = string.Empty;
			Message = string.Empty;
			CakeInstructions = string.Empty;
		}
	}
}