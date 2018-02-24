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
	internal class Handler_Excel
		{
			#region "Constructors"

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				internal Handler_Excel()
					{	}

			#endregion

			//===========================================================================================
			#region "Declarations"
			#endregion

			//===========================================================================================
			#region "Methods: Exposed"

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				internal void ResetStatusBar()
					{
						Globals.ThisAddIn.Application.StatusBar = false;
					}

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
				internal void GetWSActive( DTO_BDCSessionRequest DTO )
					{
						DTO_ExcelAppManifest x = this.GetWSActive();
						//.............................................
						DTO.WBID				= x.WBID;
						DTO.WSID				= x.WSID;
						DTO.UsedAddress	= x.UsedAddress;
					}

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				internal DTO_ExcelAppManifest GetWSActive()
					{
						Worksheet lo_WS	= this.GetWS();

						if (lo_WS != null)
							{
								return	new DTO_ExcelAppManifest	{
																										WBID				= lo_WS.Parent.Name,
																										WSID				= lo_WS.Name,
																										UsedAddress = lo_WS.UsedRange.Address
																									};
							}
						//.............................................
						return	null;
					}

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				internal void LoadWSCells( DTO_BDCSessionRequest DTO )
					{
						Worksheet lo_WS	= this.GetWS( DTO.WBID , DTO.WSID );
						if (lo_WS == null)	return;
						//.............................................
						DTO.UsedAddress	= lo_WS.UsedRange.Address;
						DTO.WSCells			= lo_WS.UsedRange.Value;
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
