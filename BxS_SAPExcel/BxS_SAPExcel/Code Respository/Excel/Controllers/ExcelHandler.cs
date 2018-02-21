using System.Collections.Generic;
using Microsoft.Office.Interop.Excel;
//.........................................................
using BxS_SAPIPC.BDCData;
//•••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••
namespace BxS_SAPExcel.Excel
{
	internal class ExcelHandler
		{
			#region "Methods: Exposed"

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				internal IList<DTO_WBWSManifest> WBWSManifest()
					{
						IList<DTO_WBWSManifest>	lt_List		= new List<DTO_WBWSManifest>();
						//.............................................
						foreach ( Workbook lo_WB in Globals.ThisAddIn.Application.Workbooks )
							{
								foreach ( Worksheet lo_WS in lo_WB.Worksheets )
									{
										var lo = new DTO_WBWSManifest	{
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
				internal void LoadWSActive( DTO_ExcelWorksheet wsDTO )
					{
						Worksheet lo_WS	= this.GetWS();

						wsDTO.WSID	= lo_WS.Name;
						wsDTO.WBID	= ( (Workbook)lo_WS.Parent ).Name;
						//.............................................
						wsDTO.UsedAddress	= lo_WS.UsedRange.Address;
						wsDTO.WSCells			= lo_WS.UsedRange.Value;
					}

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				internal void LoadWSCells( DTO_ExcelWorksheet wsDTO )
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
