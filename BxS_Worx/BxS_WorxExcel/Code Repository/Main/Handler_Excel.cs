using System.Collections.Generic;
//.........................................................
using Microsoft.Office.Interop.Excel;
//.........................................................
using BxS_WorxIPX.BDC;
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

				private	Application	_ThisAPP	{ get { return	Globals.ThisAddIn.Application; } }

			#endregion

			//===========================================================================================
			#region "Methods: Exposed"

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				internal	string	GetStatusbar		()							=>	this._ThisAPP.StatusBar					;
				internal	void		WriteStatusbar	( string msg )	=>	this._ThisAPP.StatusBar = msg		;
				internal	void		ResetStatusBar	()							=>	this._ThisAPP.StatusBar = false	;

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				internal IList< IExcelBDCSessionWS > GetWBWSManifest( bool loadData = false )
					{
						IList< IExcelBDCSessionWS >	lt_List		= new List< IExcelBDCSessionWS >();
						//.............................................
						foreach ( Workbook lo_WB in this._ThisAPP.Workbooks )
							{
								foreach ( Worksheet lo_WS in lo_WB.Worksheets )
									{
										IExcelBDCSessionWS	lo_BDCWS =	this.CreateExcelWS( lo_WS, loadData );
										lt_List.Add( lo_BDCWS );
									}
							}
						//.............................................
						return	lt_List;
					}

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				internal IExcelBDCSessionWS GetWSData( string WBID = null , string WSID = null )
					{
						return	this.CreateExcelWS( this.GetWS( WBID , WSID ) , true );
					}

			#endregion

			//===========================================================================================
			#region "Methods: Private"

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				private IExcelBDCSessionWS CreateExcelWS( Worksheet lo_WS , bool loadData = false )
					{
						IExcelBDCSessionWS	lo_BDCWS	= Globals.ThisAddIn._IPXCntlr.Value.CreateBDCSessionWS();
						//.............................................
						lo_BDCWS.WBID				= lo_WS.Parent.Name														;
						lo_BDCWS.WSID				= lo_WS.Name																	;
						lo_BDCWS.UsedAddress = lo_WS.UsedRange.Address										;
						lo_BDCWS.WSCells			= loadData	?	lo_WS.UsedRange.Value	: null	;
						//.............................................
						return	lo_BDCWS;
					}

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				private Worksheet GetWS( string forWB , string forWS )
					{
						if ( string.IsNullOrEmpty( forWS ) || string.IsNullOrEmpty( forWB ) )
							{
								return	this._ThisAPP.ActiveSheet as Worksheet;
							}
						else
							{
								return	this._ThisAPP.Workbooks[forWB].Worksheets[forWS] as Worksheet;
							}
					}

			#endregion

		}
}
