﻿using BxS_WorxNCO.Common;
using BxS_WorxNCO.BDCSession.DTO;
using BxS_WorxNCO.RfcFunction.DDIC;
//•••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••
namespace BxS_WorxNCO.SAPSession.API
{
	public interface ISAP_Session_Profile
		{
			#region "Properties"

				int	Count		{ get; }
				//.................................................
				string	SessionName		{ get; set; }
				string	SAPTCode			{ get; set; }
				//.................................................
				DTO_BDC_CTU								CTUParams		{ get; }
				BDC_Collection						BDCData			{ get; }
				DDICInfo_FieldCollection	DDICInfo		{ get; }

			#endregion

			//===========================================================================================
			#region "Methods: Exposed"

				DTO_BDC_Data	Create_BDC_DTO();

			#endregion

		}
}