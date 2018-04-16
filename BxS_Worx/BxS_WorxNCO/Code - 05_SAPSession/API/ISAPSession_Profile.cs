using System.Collections.Generic;
using BxS_WorxNCO.BDCSession.DTO;
//•••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••
namespace BxS_WorxNCO.BDCSession.API
{
	public interface ISAPSession_Profile
		{
			#region "Properties"

				string							SessionName		{ get; set; }
				string							SAPTCode			{ get; set; }
				DTO_BDC_CTU					CTUParams			{ get; set; }
				IList<DTO_BDC_Data>	BDCDataList		{ get; set; }

			#endregion

		}
}