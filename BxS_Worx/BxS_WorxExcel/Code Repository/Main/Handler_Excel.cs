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

				private	Application	ThisAPP		{ get { return	Globals.ThisAddIn.Application			; } }

			#endregion

			//===========================================================================================
			#region "Methods: Exposed"

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
						session.IsTest				= isTest						;
						session.IsOnline			= isOnline					;
						//...
						session.WBID					= lo_WS.Parent.Name	;
						session.WSID					= lo_WS.Name				;
						session.WSNo					= lo_WS.Index				;
						//...
						session.UsedAddress	= lo_WS.UsedRange.Address										;
						session.WSCells			= loadData	?	lo_WS.UsedRange.Value	: null	;
					}

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				internal void WriteConfig( string xml , string address = "$A$1" )
					{
						Worksheet x = this.GetWS( null , null );
						Range     r = x.Range[address];
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

			#endregion

		}
}
