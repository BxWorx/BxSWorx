using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;
//.........................................................
using Microsoft.Office.Tools.Ribbon;
//.........................................................
using BxS_WorxExcel.Main;
using BxS_WorxExcel.DTO;
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

				private Lazy<BxSExcel>	_BxS;

				//internal Lazy< Excel_Handler >		_HndlrExcel	;

				//private	IBDC_Controller	BDCCntlr	{ get { return	this._HndlrExcel.Value._BDCCntlr.Value	;	}	}

			#endregion

			//===========================================================================================
			#region "Methods: Private"

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				private void BxS_WorxMain_Load(object sender, RibbonUIEventArgs e)
					{
						this._BxS	= new	Lazy<BxSExcel>	( ()=>	new	BxSExcel()	, cz_LM );
						//...
						this._User	= Environment.GetFolderPath(Environment.SpecialFolder.UserProfile);
					}

				#pragma warning	disable	RCS1163

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				private void DropDown1_Load(object sender , RibbonControlEventArgs e)
					{
						foreach ( string lo_SS in this._BxS.Value.GetSAPiniList() )
							{
								RibbonDropDownItem x = this.Factory.CreateRibbonDropDownItem();
								x.Label	= lo_SS	;				//$"{lo_SS.ID} | {lo_SS.SAPName} | {lo_SS.IsSSO} ";
								this.dropDown1.Items.Add( x );
							}
						this.dropDown1.Label	= this.dropDown1.Items.FirstOrDefault()?.Label;
					}

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				private async void WriteActive_Click( object sender , RibbonControlEventArgs e )
					{
						await Task.Run( () => {
																		DTO_WSNode	x = this._BxS.Value.GetActiveWSNode();
																		IRequest		r =	this._BxS.Value.CreateRequest();
																		//...
																		this.SetFullPath( x.WSName );
																		this._BxS.Value.WriteRequestToFile( r , this._Full );
																		//IRequest	x = this.BDCCntlr.Create_Request();
																		//ISession	s	= this.BDCCntlr.Create_Session();
																		//.............................................
																		//this._HndlrExcel.Value.LoadWSData( s );
																		//this.SetFullPath(s.WSID);
																		////...
																		//x.Add_Session( s );
																		//this.BDCCntlr.DispatchRequest_ToFile( x , this._Full );
																		//.....................
																		//this._HndlrExcel.Value.WriteStatusbar( s.WSData.Count.ToString() );
																		//Thread.Sleep(300);
																		//this._HndlrExcel.Value.ResetStatusBar();
																	} ).ConfigureAwait(false);
					}

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				private async void WriteAll_Click( object sender , RibbonControlEventArgs e )
					{
						await Task.Run( () => {
																		this.SetFullPath("DPB");
																		//...
																		IList<DTO_WSNode> x = this._BxS.Value.GetManifest();
																		IRequest					r =	this._BxS.Value.CreateRequest( x );
																		this._BxS.Value.WriteRequestToFile( r , this._Full );

																		//IRequest x = this.BDCCntlr.Create_Request();
																		////...
																		//foreach ( Worksheet lo_WS in this._HndlrExcel.Value.GetWBWSManifest() )
																		//	{
																		//		ISession	s	= this.BDCCntlr.Create_Session();
																		//		this._HndlrExcel.Value.LoadWSdataIntoSession( s , lo_WS , true );
																		//		x.Add_Session( s );
																		//	}
																		//this.BDCCntlr.DispatchRequest_ToFile( x , this._Full );
																		//.....................
																		//this._HndlrExcel.Value.WriteStatusbar( x.Count.ToString() );
																		//Thread.Sleep(300);
																		//this._HndlrExcel.Value.ResetStatusBar();
																	} ).ConfigureAwait(false);
					}

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				private async void SaveXMLCfg_Click( object sender , RibbonControlEventArgs e )
					{
						await Task.Run( () => {
																		IXMLConfig x = this._BxS.Value.CreateXMLConfig();
																		x.SAPTCode = "XD03";
																		this._BxS.Value.WriteConfig( x , "B3" );
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
