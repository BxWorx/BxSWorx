using System.Collections.Generic;
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

				internal Handler_Excel	_HndlrExcel	;
				internal Handler_BDC		_HndlrBDC		;

			#endregion

			//===========================================================================================
			#region "Methods: Private"

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				private void BxS_WorxMain_Load(object sender, RibbonUIEventArgs e)
					{
						this._HndlrExcel	= new	Handler_Excel	();
						this._HndlrBDC		= new	Handler_BDC		();
					}

				#pragma warning	disable	RCS1163

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				private async void Button1_Click( object sender , RibbonControlEventArgs e )
					{
						await Task.Run( () => {
																		IExcel_WSSource		lo_WS		=	this._HndlrExcel.GetWSData();
																		IExcel_BDCRequestManager	lo_RM	= this._HndlrBDC.Create_ExcelBDCRequestManager();


																		this._HndlrBDC.WriteDataXML( lo_WS );
																		//.....................
																		this._HndlrExcel.WriteStatusbar( lo_WS.WSNo.ToString() );
																		Thread.Sleep(300);
																		this._HndlrExcel.ResetStatusBar();
																	} ).ConfigureAwait(false);
					}

				//DTO_ExcelWorksheet X = this._HndlrExcel._ObjSerialiser.Value.DeSerialize<DTO_ExcelWorksheet>( lc_XML );
				//this._HndlrExcel._WSDTOParser.Value.Parse1Dto2D( X );

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				private async void Button2_Click( object sender , RibbonControlEventArgs e )
					{
						IList<IExcel_WSSource> x = this._HndlrExcel.GetWBWSManifest( true );

						


						await Task.Run( () => {
																		IExcel_WSSource	lo_WS		=	this._HndlrExcel.GetWSData();
																		this._HndlrBDC.WriteDataXML( lo_WS );
																		//.....................
																		this._HndlrExcel.WriteStatusbar( lo_WS.WSNo.ToString() );
																		Thread.Sleep(300);
																		this._HndlrExcel.ResetStatusBar();
																	} ).ConfigureAwait(false);

					}

				#pragma warning	restore	RCS1163

			#endregion

		}
	}
