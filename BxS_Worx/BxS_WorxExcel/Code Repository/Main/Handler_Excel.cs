using System.Collections.Generic;
//.........................................................
using Microsoft.Office.Interop.Excel;
//.........................................................
using BxS_WorxIPX.BDC;
using BxS_WorxIPX.Main;
//•••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••
namespace BxS_WorxExcel.Main
{
	internal class Handler_Excel
		{
			#region "Constructors"

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				internal Handler_Excel()
					{	}

			#endregion

			//===========================================================================================
			#region "Properties"

				private	Application				ThisAPP		{ get { return	Globals.ThisAddIn.Application			; } }
				private	IIPX_Controller		IPXCntlr	{ get	{	return	Globals.ThisAddIn._IPXCntlr.Value	; } }

			#endregion

			//===========================================================================================
			#region "Methods: Exposed"

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				internal	string	GetStatusbar		()							=>	this.ThisAPP.StatusBar					;
				internal	void		WriteStatusbar	( string msg )	=>	this.ThisAPP.StatusBar = msg		;
				internal	void		ResetStatusBar	()							=>	this.ThisAPP.StatusBar = false	;

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				internal IList< IExcelBDC_WS > GetWBWSManifest( bool loadData = false )
					{
						IList< IExcelBDC_WS >	lt_List		= new List< IExcelBDC_WS >();
						//.............................................
						foreach ( Workbook lo_WB in this.ThisAPP.Workbooks )
							{
								foreach ( Worksheet lo_WS in lo_WB.Worksheets )
									{
										IExcelBDC_WS	lo_BDCWS =	this.CreateExcelWS( lo_WS, loadData );
										lt_List.Add( lo_BDCWS );
									}
							}
						//.............................................
						return	lt_List;
					}

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				internal IExcelBDC_WS GetWSData( string WBID = null , string WSID = null )
					{
						return	this.CreateExcelWS( this.GetWS( WBID , WSID ) , true );
					}

			#endregion

			//===========================================================================================
			#region "Methods: Private"

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				private IExcelBDC_WS CreateExcelWS(		Worksheet lo_WS
																						, bool			loadData	= false
																						, bool			isTest		= false
																						, bool			isOnline	= false	)
					{
						IExcelBDC_WS	lo_BDCWS	= this.IPXCntlr.Create_ExcelBDCWS();
						//.............................................
						lo_BDCWS.IsTest				= isTest						;
						lo_BDCWS.IsOnline			= isOnline					;
						lo_BDCWS.WBID					= lo_WS.Parent.Name	;
						lo_BDCWS.WSID					= lo_WS.Name				;
						lo_BDCWS.WSNo					= lo_WS.Index				;

						lo_BDCWS.UsedAddress	= lo_WS.UsedRange.Address										;
						lo_BDCWS.WSCells			= loadData	?	lo_WS.UsedRange.Value	: null	;
						//.............................................
						return	lo_BDCWS;
					}

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				private Worksheet GetWS( string forWB , string forWS )
					{
						if ( string.IsNullOrEmpty( forWS ) || string.IsNullOrEmpty( forWB ) )
							{
								return	this.ThisAPP.ActiveSheet as Worksheet;
							}
						else
							{
								return	this.ThisAPP.Workbooks[forWB].Worksheets[forWS] as Worksheet;
							}
					}

			#endregion

		}
}
