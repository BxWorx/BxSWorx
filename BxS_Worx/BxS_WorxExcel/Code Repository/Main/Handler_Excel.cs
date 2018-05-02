using System;
using System.Text;
using System.Collections.Generic;
//.........................................................
using Microsoft.Office.Interop.Excel;
//.........................................................
using BxS_WorxNCO.API;
using BxS_WorxNCO.Destination.API;

using BxS_WorxIPX.Main;
using BxS_WorxIPX.BDC;

using static	BxS_WorxExcel.Main.EXL_Constants;
//•••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••
namespace BxS_WorxExcel.Main
{
	internal class Handler_Excel
		{
			#region "Constructors"

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				internal Handler_Excel( Application application )
					{
						this._App	= application;
						//...
						this._NCOCntlr		= new	Lazy<INCO_Controller>	(	()=>	NCO_Controller.Instance	, cz_LM );
						this._IPXCntlr		= new	Lazy<IIPX_Controller>	(	()=>	IPX_Controller.Instance	, cz_LM );
						//...
						this._BDCCntlr		= new	Lazy<IBDC_Controller>	( ()=>	this._IPXCntlr.Value.Create_BDCController() , cz_LM );
					}

			#endregion

			//===========================================================================================
			#region "Declarations"

				private readonly	Application	_App;
				//...
				internal Lazy< INCO_Controller >	_NCOCntlr	;
				internal Lazy< IIPX_Controller >	_IPXCntlr	;
				internal Lazy< IBDC_Controller >	_BDCCntlr		;

			#endregion

			//===========================================================================================
			#region "Properties"

				private	Application			ThisAPP		{ get { return	Globals.ThisAddIn.Application	; } }
				private IBDC_Controller BDCCntlr	{ get { return	this._BDCCntlr.Value					; } }

			#endregion

			//===========================================================================================
			#region "Methods: Exposed"

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				internal IRequest CreateRequest( IList<string> wsList )
					{
						var x = this._BDCCntlr.Value.Create_Request();
						//.............................................

						var lo_Ssn	= this._BDCCntlr.Value.Create_Session();
				

						x.Add_Session( lo_Ssn );
						//.............................................
						return	x;
					}





				internal	IList<string>	GetSAPSystems()	=>	this._NCOCntlr.Value.GetSAPINIList();


				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				internal	string	GetStatusbar		()							=>	this.ThisAPP.StatusBar					;
				internal	void		WriteStatusbar	( string msg )	=>	this.ThisAPP.StatusBar = msg		;
				internal	void		ResetStatusBar	()							=>	this.ThisAPP.StatusBar = false	;

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				internal IList< Worksheet > GetWBWSManifest()
					{
						IList< Worksheet >	lt_List		= new List< Worksheet >();
						//.............................................
						foreach ( Workbook lo_WB in this.ThisAPP.Workbooks )
							{
								foreach ( Worksheet lo_WS in lo_WB.Worksheets )
									{
										lt_List.Add( lo_WS );
									}
							}
						//.............................................
						return	lt_List;
					}

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				internal void LoadWSData(	ISession	session
																, string		WBID			= null
																, string		WSID			= null
																, bool			loadData	= true
																, bool			isTest		= false
																, bool			isOnline	= false )
					{
						this.LoadWSdataIntoSession( session , this.GetWS( WBID , WSID ) , loadData , isTest , isOnline );
					}

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				internal void LoadWSdataIntoSession(	ISession	session
																						,	Worksheet	lo_WS
																						, bool			loadData	= false
																						, bool			isTest		= false
																						, bool			isOnline	= false	)
					{
						session.IsTest		= isTest		;
						session.IsOnline	= isOnline	;
						//...
						session.WBID	= lo_WS.Parent.Name	;
						session.WSID	= lo_WS.Name				;
						session.WSNo	= lo_WS.Index				;
						//...
						session.UsedAddress	= lo_WS.UsedRange.Address	;

						if ( loadData )
							{
								this.WSToSession( session , lo_WS );
							}
					}

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				internal void WriteConfig( string xml , string address = "$A$1" )
					{
						Worksheet x = this.GetWS( null , null );
						Range     r = x.Range[address];
						//...
						r.Value	= xml;
					}

			#endregion

			//===========================================================================================
			#region "Methods: Private"

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

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				// process each worksheet object array into dictionary with dictionary key having the 
				// syntax of WSCell[row , col ] == "row,col"
				//
				private void WSToSession(		ISession	session
																	,	Worksheet	lo_WS		)
					{
						object[,]	la_WSCells	=	lo_WS.UsedRange.Value	;
						//.............................................
						session.WSData.Clear();

						if ( la_WSCells == null )
							{
								session.RowLB		= -1;
								session.RowUB		= -1;
								session.ColLB		= -1;
								session.ColUB		= -1;
							}
						else
							{
								var	lo_SB		= new StringBuilder();
								//...
								session.RowLB		= la_WSCells.GetLowerBound(0);
								session.RowUB		= la_WSCells.GetUpperBound(0);
								session.ColLB		= la_WSCells.GetLowerBound(1);
								session.ColUB		= la_WSCells.GetUpperBound(1);
								//...
								for ( int	r = session.RowLB; r <= session.RowUB; r++ )
									{
										for ( int c = session.ColLB; c <= session.ColUB; c++ )
											{
												if ( la_WSCells[r,c] != null )
													{
														if ( la_WSCells[r,c].ToString().Contains( this.BDCCntlr.XmlConfigTag ) )
															{
																session.XMLConfig		=	this.BDCCntlr.DeserializeXMLConfig( la_WSCells[r,c].ToString() );
																continue;
															}
														//...
														lo_SB.Clear();
														lo_SB.AppendFormat( $"{r.ToString()},{c.ToString()}" );
														//...
														session.WSData.Add( lo_SB.ToString() , la_WSCells[r,c].ToString() );
													}
											}
									}
							}
					}

			#endregion

		}
}
