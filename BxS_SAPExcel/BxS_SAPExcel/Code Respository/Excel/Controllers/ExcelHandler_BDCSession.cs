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
	internal class ExcelHandler_BDCSession
		{
			#region "Constructors"

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				internal ExcelHandler_BDCSession()
					{	}

			#endregion

			//===========================================================================================
			#region "Declarations"

				internal readonly Lazy<IIPX_Controller>

					_IPXCntlr		= new Lazy<IIPX_Controller>	( ()=>	IPX_Controller.Instance
																													, LazyThreadSafetyMode
																															.ExecutionAndPublication );

			#endregion

			//===========================================================================================
			#region "Methods: Exposed"

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				internal DTO_BDCSessionRequest CreateBDCSessionRequest()
					{
						return	this._IPXCntlr.Value.CreateBDCSessionRequest();
					}

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				internal DTO_BDCSessionResult CreateBDCSessionResult()
					{
						return	this._IPXCntlr.Value.CreateBDCSessionResult();
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
