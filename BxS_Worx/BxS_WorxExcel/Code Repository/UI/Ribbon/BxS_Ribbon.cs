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

using BxS_WorxExcel.Code_Repository.UI.SAP;


using static	BxS_WorxExcel.Main.EXL_Constants;
using BxS_WorxExcel.Code_Repository.UI.SAP.Favourites;
using BxS_WorxExcel.Code_Repository.UI.User_Controls;
using BxS_WorxExcel.Code_Repository.UI.User_Controls.WSConfig;
using Microsoft.Office.Interop.Excel;
using BxS_WorxExcel.UI;
//•••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••
namespace BxS_WorxExcel
	{
	public partial class BxS_Ribbon
		{

			#region "Declarations"

				private	const	string	cz_Path	=  @"GitHub\BxSWorx\BxS_Worx\BxS_zWorxIPX_UT\Test Resources";

				private	const	string	_Nme	=  "Test-00"									;
				private	const	string	_Path	=  @"GitHub\BxSWorx\BxS_Worx\BxS_zWorxIPX_UT\Test Resources";

				private				string	_User	;
				private				string	_Full	;

				private Lazy<BxS_XLController>	_BxSXLCntlr;

			#endregion

			//===========================================================================================
			#region "Properties"

				private BxS_XLController BxSXLCntlr	{ get	{	return	this._BxSXLCntlr.Value;	}	}

			#endregion

			//===========================================================================================
			#region "Methods: Private"

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				private void BxS_WorxMain_Load(object sender, RibbonUIEventArgs e)
					{
						this._BxSXLCntlr	= new	Lazy<BxS_XLController>	( ()=>	new	BxS_XLController()	, cz_LM );
						//...
						this._User	= Environment.GetFolderPath(Environment.SpecialFolder.UserProfile);

					}

				#pragma warning	disable	RCS1163

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				private void DropDown1_Load(object sender , RibbonControlEventArgs e)
					{
						//foreach ( string lo_SS in this.BxSCntlr.GetSAPiniList() )
						//	{
						//		RibbonDropDownItem x = this.Factory.CreateRibbonDropDownItem();
						//		x.Label	= lo_SS	;				//$"{lo_SS.ID} | {lo_SS.SAPName} | {lo_SS.IsSSO} ";
						//		this.dropDown1.Items.Add( x );
						//	}
						//this.dropDown1.Label	= this.dropDown1.Items.FirstOrDefault()?.Label;
					}

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				private void Session_Click(object sender , RibbonControlEventArgs e)
					{
						//var x = new Form1( BxSXLCntlr. true );
						//x.Show();
					}

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				private void Request_Click( object sender , RibbonControlEventArgs e )
					{
						//var x = new Form1();
						//x.Show();
					}

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				private void Submit_Click( object sender , RibbonControlEventArgs e )
					{
						var y = new SAPFavWindow();

						y.Show();

						//var x = new SAPFavourites();

						//x.Show();


						//var x = this.BxSCntlr.FavHndlr.CreateEntry();

						//x.SAPSysID	= this.dropDown1.SelectedItem.Label;

						//this.BxSCntlr.FavHndlr.TopTen.Add( x );
					}

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				private async void WriteActive_Click( object sender , RibbonControlEventArgs e )
					{
						await Task.Run( () => {
																		DTO_WSNode  x = this.BxSXLCntlr.GetActiveWSNode();
																		IRequest    r =	this.BxSXLCntlr.CreateRequest( x.WBName );
																		//...
																		this.SetFullPath( x.WSName );
																		this.BxSXLCntlr.WriteRequestToFile( r , this._Full );
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
																		IList<DTO_WSNode> x = this.BxSXLCntlr.GetManifest();
																		IRequest          r =	this.BxSXLCntlr.CreateRequest( x , "DPB" );
																		this.BxSXLCntlr.WriteRequestToFile( r , this._Full );
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
																		IXMLConfig x = this.BxSXLCntlr.CreateXMLConfig();
																		x.SAPTCode = "XD03";
																		this.BxSXLCntlr.WriteConfig( x , "B3" );
																	} ).ConfigureAwait(false);
					}

				#pragma warning	restore	RCS1163

				//.

					//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
					private void SetFullPath( string name )	=>	this._Full	= $@"{this._User}\{_Path}\{name}.xml" ;

		//.

		#endregion

		private void Xbtn_NewBDCWS_Click(object sender , RibbonControlEventArgs e)
			{
				Worksheet WS	= Globals.ThisAddIn.XLHndlr.AddWorksheet();
				var WSHndlr		= new BDCWorksheet();
				WSHndlr.Format( WS );
			}

		private	readonly	Lazy<UIHost>					_UIHost		= new	Lazy<UIHost>( ()=>	new	UIHost() , cz_LM );
		//private readonly	Lazy<IMVVMController>	_MVVM	= new	Lazy<IMVVMController>( ()=> { var x =  new MVVMController_SAPBDC();
		//																																									x.Startup();
		//																																									return	x; } , cz_LM );

		private void MVVM_Click(object sender , RibbonControlEventArgs e)
			{
				this._UIHost.Value.ShowUI("A");
				//this._UIHost.Value.ShowUI("A");


				//this._MVVM.Value.Startup();
				//this._MVVM.Value.ToggleView();
			}
		}
	}
