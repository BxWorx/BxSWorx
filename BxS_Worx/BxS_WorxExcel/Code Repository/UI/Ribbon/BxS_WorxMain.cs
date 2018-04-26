using System;
using System.Threading.Tasks;
using System.Threading;
//.........................................................
using Microsoft.Office.Tools.Ribbon;
using Microsoft.Office.Interop.Excel;
//.........................................................
using BxS_WorxExcel.Main;
using BxS_WorxIPX.BDC;

using static	BxS_WorxExcel.Main.EXL_Constants;
//•••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••
namespace BxS_WorxExcel
	{
	public partial class BxS_WorxMain
		{
			#region "Declarations"

				private	const	string	cz_Path	=  @"GitHub\BxSWorx\BxS_Worx\BxS_zWorxIPX_UT\Test Resources";

				private	const	string	_Nme	=  "Test-00"									;
				private	const	string	_Path	=  @"GitHub\BxSWorx\BxS_Worx\BxS_zWorxIPX_UT\Test Resources";

				private				string	_User	;
				private				string	_Full	;

				internal Lazy< IBDC_Controller >	_BDCCntlr		;
				internal Lazy< Handler_Excel >		_HndlrExcel	;

				private	IBDC_Controller	BDCCntlr	{ get { return	this._BDCCntlr.Value	;	}	}

			#endregion

			//===========================================================================================
			#region "Methods: Private"

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				private void BxS_WorxMain_Load(object sender, RibbonUIEventArgs e)
					{
						this._BDCCntlr		= new	Lazy< IBDC_Controller >	( ()=>	Globals.ThisAddIn._IPXCntlr.Value.Create_BDCController() , cz_LM );
						this._HndlrExcel	= new	Lazy< Handler_Excel >		( ()=>	new	Handler_Excel() , cz_LM );
						//...
						this._User	= Environment.GetFolderPath(Environment.SpecialFolder.UserProfile);
					}

				#pragma warning	disable	RCS1163

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				private async void Button1_Click( object sender , RibbonControlEventArgs e )
					{
						await Task.Run( () => {
																		IRequest	x = this.BDCCntlr.Create_Request();
																		ISession	s	= this.BDCCntlr.Create_Session();
																		//.............................................
																		this._HndlrExcel.Value.LoadWSData( s );
																		this.SetFullPath(s.WSID);
																		//...
																		x.Add_Session( s );
																		this._BDCCntlr.Value.DispatchRequest_ToFile( x , this._Full );
																		//.....................
																		this._HndlrExcel.Value.WriteStatusbar( s.WSStore.Count.ToString() );
																		Thread.Sleep(300);
																		this._HndlrExcel.Value.ResetStatusBar();
																	} ).ConfigureAwait(false);
					}

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				private async void Button2_Click( object sender , RibbonControlEventArgs e )
					{
						await Task.Run( () => {
																		IRequest x = this.BDCCntlr.Create_Request();
																		//...
																		foreach ( Worksheet lo_WS in this._HndlrExcel.Value.GetWBWSManifest() )
																			{
																				ISession	s	= this.BDCCntlr.Create_Session();
																				this._HndlrExcel.Value.LoadWSdataIntoSession( s , lo_WS , true );
																				x.Add_Session( s );
																			}
																		this.SetFullPath("DPB");
																		this._BDCCntlr.Value.DispatchRequest_ToFile( x , this._Full );
																		//.....................
																		this._HndlrExcel.Value.WriteStatusbar( x.Count.ToString() );
																		Thread.Sleep(300);
																		this._HndlrExcel.Value.ResetStatusBar();
																	} ).ConfigureAwait(false);
					}

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				private async void Button3_Click( object sender , RibbonControlEventArgs e )
					{
						await Task.Run( () => {
																		IXMLConfig x = this._BDCCntlr.Value.Create_XMLConfig();
																		x.SAPTCode = "XD03";
																		this._HndlrExcel.Value.WriteConfig( this._BDCCntlr.Value.SerializeXMLConfig( x ) , "B3" );
																	} ).ConfigureAwait(false);
					}

				#pragma warning	restore	RCS1163

				//.

					//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
					private void SetFullPath( string name )	=>	this._Full	= $@"{this._User}\{_Path}\{name}.xml" ;

				//.

			#endregion

		}
	}
