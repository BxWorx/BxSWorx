using System;
using System.Threading.Tasks;
using System.Threading;
//.........................................................
using Microsoft.Office.Tools.Ribbon;
//.........................................................
using BxS_WorxExcel.Main;
using BxS_WorxIPX.BDC;
//•••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••
namespace BxS_WorxExcel
	{
	public partial class BxS_WorxMain
		{
			#region "Declarations"

				internal Lazy<IBDCRequestManager>	_BDCMngr;

				internal Handler_Excel	_HndlrExcel	;

			#endregion

			//===========================================================================================
			#region "Methods: Private"

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				private void BxS_WorxMain_Load(object sender, RibbonUIEventArgs e)
					{
						this._BDCMngr	= new	Lazy<IBDCRequestManager>( ()=>		Globals.ThisAddIn._IPXCntlr.Value.Create_BDCRequestManager()
																																, LazyThreadSafetyMode.ExecutionAndPublication );
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
																		this._BDCMngr.Value.Write_BDCRequest( $@"C:\ProgramData\BxS_Worx\{lo_WS.WSID}.xml" );
																		//.....................
																		this._HndlrExcel.WriteStatusbar( lo_WS.WSCells.GetUpperBound(0).ToString() );
																		Thread.Sleep(300);
																		this._HndlrExcel.ResetStatusBar();
																	} ).ConfigureAwait(false);
					}

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				private async void Button2_Click( object sender , RibbonControlEventArgs e )
					{
						//IList<IExcel_BDCWorksheet> x = this._HndlrExcel.GetWBWSManifest( true );

						


						//await Task.Run( () => {
						//												IExcel_BDCWorksheet	lo_WS		=	this._HndlrExcel.GetWSData();
						//												this._HndlrBDC.WriteDataXML( lo_WS );
						//												//.....................
						//												this._HndlrExcel.WriteStatusbar( lo_WS.WSNo.ToString() );
						//												Thread.Sleep(300);
						//												this._HndlrExcel.ResetStatusBar();
						//											} ).ConfigureAwait(false);

					}

				#pragma warning	restore	RCS1163

			#endregion

		}
	}
