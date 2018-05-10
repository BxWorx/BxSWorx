using System;
//•••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••
namespace BxS_WorxIPX.NCO
{
	public struct DTO_SAPSessionRequest
		{
			#region "Properties"

				public	string		User	{ get; set; }
				public	string		Name	{ get; set; }
				public	DateTime	From	{ get; set; }
				public	DateTime	To		{ get; set; }

			#endregion

		}
}