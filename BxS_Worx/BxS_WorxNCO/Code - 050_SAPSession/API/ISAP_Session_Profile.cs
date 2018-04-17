using BxS_WorxNCO.BDCSession.DTO;
using BxS_WorxNCO.Common;
//•••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••
namespace BxS_WorxNCO.SAPSession.API
{
	public interface ISAP_Session_Profile
		{
			#region "Properties"

				string	SessionName		{ get; set; }
				string	SAPTCode			{ get; set; }
				//.............................................
				DTO_BDC_CTU			CTUParams		{ get; }
				BDC_Collection	BDCData			{ get; }

			#endregion

		}
}