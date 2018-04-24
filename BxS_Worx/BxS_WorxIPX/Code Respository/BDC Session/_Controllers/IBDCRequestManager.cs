﻿using BxS_WorxIPX.ExcelBDC;
using BxS_WorxIPX.SAPBDC;
//•••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••
namespace BxS_WorxIPX.BDC
{
	public interface IBDCRequestManager
		{
			//===========================================================================================
			#region "Properties"

				int WSCount	{ get; }

			#endregion

			//===========================================================================================
			#region "Methods: Exposed"

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				ISAP_Logon						Create_SAPLogon()			;
				IExcel_BDCWorksheet		Create_BDCWorksheet()	;
				BDCXMLConfig					Create_BDCXmlConfig()	;

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				void Set_SAPLogon			( ISAP_Logon					sapLogon	)	;
				void Add_BDCWorksheet	( IExcel_BDCWorksheet	bdcWS )			;
				//...
				void						Write_BDCRequest	( string pathName )	;
				ISAP_BDCRequest	Read_BDCRequest		( string pathName )	;
				//...
				string				SerializeXMLConfig		( BDCXMLConfig config )	;
				BDCXMLConfig	DeserializeXMLConfig	( string config  )			;
				//...
				void Clear();

			#endregion

		}
}