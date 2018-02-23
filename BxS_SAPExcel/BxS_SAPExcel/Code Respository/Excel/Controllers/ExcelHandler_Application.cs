using System;
using System.Collections.Generic;
using System.Threading;
//.........................................................
using Microsoft.Office.Interop.Excel;
//.........................................................
using BxS_SAPIPX.Main;
using BxS_SAPIPX.Excel;
//•••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••
namespace BxS_SAPExcel.Main
{
	internal class ExcelHandler_Application
		{
			#region "Constructors"

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				internal ExcelHandler_Application()
					{	}

			#endregion

			//===========================================================================================
			#region "Declarations"
			#endregion

			//===========================================================================================
			#region "Methods: Exposed"

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				internal string GetStatusbar()
					{
						return	Globals.ThisAddIn.Application.StatusBar;
					}

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				internal void WriteStatusbar( string msg )
					{
						Globals.ThisAddIn.Application.StatusBar = msg;
					}

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				internal IList<DTO_ExcelAppManifest> WBWSManifest()
					{
						IList<DTO_ExcelAppManifest>	lt_List		= new List<DTO_ExcelAppManifest>();
						//.............................................
						foreach ( Workbook lo_WB in Globals.ThisAddIn.Application.Workbooks )
							{
								foreach ( Worksheet lo_WS in lo_WB.Worksheets )
									{
										var lo = new DTO_ExcelAppManifest	{
																										WBID				= lo_WB.Name,
																										WSID				= lo_WS.Name,
																										UsedAddress = lo_WS.UsedRange.Address
																									};

										lt_List.Add( lo );
									}
							}
						//.............................................
						return	lt_List;
					}

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				internal void LoadWSActive( DTO_BDCSessionRequest wsDTO )
					{
						Worksheet lo_WS	= this.GetWS();

						wsDTO.WSID	= lo_WS.Name;
						wsDTO.WBID	= ( (Workbook)lo_WS.Parent ).Name;
						//.............................................
						wsDTO.UsedAddress	= lo_WS.UsedRange.Address;
						wsDTO.WSCells			= lo_WS.UsedRange.Value;
					}

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				internal void LoadWSCells( DTO_BDCSessionRequest wsDTO )
					{
						Worksheet lo_WS	= this.GetWS( wsDTO.WBID , wsDTO.WSID );
						if (lo_WS == null)	return;
						//.............................................
						wsDTO.UsedAddress	= lo_WS.UsedRange.Address;
						wsDTO.WSCells			= lo_WS.UsedRange.Value;
					}

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				private Worksheet GetWS( string forWB = null , string forWS = null )
					{
						try
							{
								if ( string.IsNullOrEmpty( forWS ) || string.IsNullOrEmpty( forWB ) )
									{ return	Globals.ThisAddIn.Application.ActiveSheet as Worksheet; }
								else
									{	return	Globals.ThisAddIn.Application.Workbooks[forWB].Worksheets[forWS] as Worksheet; }
							}
						catch (System.Exception)
							{
								return	null;
							}
					}

			#endregion

		}
}
