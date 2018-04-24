using System;
using System.Threading.Tasks;
using System.Threading;
//.........................................................
using Microsoft.Office.Tools.Ribbon;
using Microsoft.Office.Interop.Excel;
//.........................................................
using BxS_WorxExcel.Main;
using BxS_WorxIPX.BDC;
using BxS_WorxIPX.BDCExcel;

using static	BxS_WorxExcel.Main.EXL_Constants;
//•••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••
namespace BxS_WorxExcel
	{
	public partial class BxS_WorxMain
		{
			#region "Declarations"

				private	const	string	cz_Path	=  @"C:\Users\BMA\GitHub\BxSWorx\BxS_Worx\BxS_zWorxIPX_UT\Test Resources";

				internal Lazy<IBDCRequestManager>	_BDCMngr;

				internal Handler_Excel	_HndlrExcel	;

			#endregion

			//===========================================================================================
			#region "Methods: Private"

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				private void BxS_WorxMain_Load(object sender, RibbonUIEventArgs e)
					{
						this._BDCMngr			= new	Lazy< IBDCRequestManager >( ()=>	Globals.ThisAddIn._IPXCntlr.Value.Create_BDCRequestManager() , cz_LM );
						this._HndlrExcel	= new	Handler_Excel	();
					}

				#pragma warning	disable	RCS1163

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				private async void Button1_Click( object sender , RibbonControlEventArgs e )
					{
						await Task.Run( () => {
																		IExcel_BDCWorksheet		lo_WS		=	this._BDCMngr.Value.Create_BDCWorksheet();
																		//.............................................
																		this._HndlrExcel.GetWSData( lo_WS );
																		this._BDCMngr.Value.Clear();
																		this._BDCMngr.Value.Add_BDCWorksheet( lo_WS );
																		this._BDCMngr.Value.Write_BDCRequest( $@"{cz_Path}\{lo_WS.WSID}.xml" );
																		//.....................
																		this._HndlrExcel.WriteStatusbar( lo_WS.WSCells.GetUpperBound(0).ToString() );
																		Thread.Sleep(300);
																		this._HndlrExcel.ResetStatusBar();
																	} ).ConfigureAwait(false);
					}

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				private async void Button2_Click( object sender , RibbonControlEventArgs e )
					{
						this._BDCMngr.Value.Clear();
						//.............................................
						await Task.Run( () => {
																		foreach ( Worksheet lo_WS in this._HndlrExcel.GetWBWSManifest() )
																			{
																				IExcel_BDCWorksheet		lo_BDCWS		=	this._BDCMngr.Value.Create_BDCWorksheet();
																				this._HndlrExcel.CreateExcelWS( lo_BDCWS , lo_WS , true );
																				this._BDCMngr.Value.Add_BDCWorksheet( lo_BDCWS );
																			}
																		this._BDCMngr.Value.Write_BDCRequest( $@"{cz_Path}\DPB.xml" );
																		//.....................
																		this._HndlrExcel.WriteStatusbar( this._BDCMngr.Value.WSCount.ToString() );
																		Thread.Sleep(300);
																		this._HndlrExcel.ResetStatusBar();
																	} ).ConfigureAwait(false);
					}

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				private async void Button3_Click( object sender , RibbonControlEventArgs e )
					{
						await Task.Run( () => {
																		BDCXMLConfig x = this._BDCMngr.Value.Create_BDCXmlConfig();
																		this._HndlrExcel.WriteConfig( this._BDCMngr.Value.SerializeXMLConfig( x ) );
																	} ).ConfigureAwait(false);
					}

				#pragma warning	restore	RCS1163

			#endregion

		}
	}
