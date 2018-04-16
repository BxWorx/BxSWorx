using System.Collections.Generic;
//.........................................................
using BxS_WorxNCO.BDCSession.DTO;
//•••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••
namespace BxS_WorxNCO.SAPSession.API
{
	public class SAP_Session_Profile : ISAP_Session_Profile
		{
			#region "Properties"

				public	string							SessionName		{ get; set; }
				public	string							SAPTCode			{ get; set; }
				public	DTO_BDC_CTU					CTUParams			{ get; set; }
				public	IList<DTO_BDC_Data>	BDCDataList		{ get; set; }

			#endregion

		}
}