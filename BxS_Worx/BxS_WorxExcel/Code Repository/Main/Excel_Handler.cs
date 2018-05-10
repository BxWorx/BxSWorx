using System.Collections.Generic;
//.........................................................
using Microsoft.Office.Interop.Excel;
//.........................................................
using BxS_WorxExcel.DTO;
//•••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••
namespace BxS_WorxExcel.Main
{
	internal class Excel_Handler : IExcel
		{
			#region "Constructors"

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				internal Excel_Handler()
					{	}

			#endregion

			//===========================================================================================
			#region "Properties"

				public	string	CurrentAddress	{ get	{	return	this.ThisApp.ActiveCell.Address; } }

			#endregion

			//===========================================================================================
			#region "Methods: Exposed"

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				public void	ScreenUpdating( bool AsOn = true )
					{
					}

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				public Worksheet GetActiveWorksheet()
					{
						return	this.GetWS( default(DTO_WSNode) );
					}

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				public Worksheet AddWorksheet()
					{
						return	this.ThisApp.ActiveWorkbook.Worksheets.Add();
					}

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				public void WriteConfig( string xml , string address = "$A$1" )
					{
						var	lo_WS	=	this.ThisApp.ActiveSheet as Worksheet;
						//...
						Range	r = lo_WS.Range[address];
						//...
						r.Value	= xml;
					}

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				public DTO_WSNode	GetActiveWSNode()
					{
						var	lo_WS	=	this.ThisApp.ActiveSheet as Worksheet;
						//...
						return	new DTO_WSNode	{
																				WBName = lo_WS.Parent.Name
																			,	WSName = lo_WS.Name
																		};
					}

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				public IList<DTO_WSNode>	GetManifest()
					{
						IList<DTO_WSNode>	lt	= new List<DTO_WSNode>();
						//...
						foreach ( Workbook lo_WB in this.ThisApp.Workbooks )
							{
								foreach ( Worksheet lo_WS in lo_WB.Worksheets )
									{
										var ls_Node = new DTO_WSNode	{
																											WBName = lo_WB.Name
																										,	WSName = lo_WS.Name
																									};
										lt.Add( ls_Node );
									}
							}
						//...
						return	lt;
					}

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				public DTO_WSData GetWSData( DTO_WSNode wsNode )
					{
						Worksheet lo_WS = this.GetWS( wsNode );
						//...
						return	new DTO_WSData	{
																				WBID        = lo_WS.Parent.Name
																			,	WSID        = lo_WS.Name
																			,	UsedAddress = lo_WS.UsedRange.Address
																			,	WSCells     = lo_WS.UsedRange.Value
																		};
					}

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				internal	string	GetStatusbar		()							=>	this.ThisApp.StatusBar					;
				internal	void		WriteStatusbar	( string msg )	=>	this.ThisApp.StatusBar = msg		;
				internal	void		ResetStatusBar	()							=>	this.ThisApp.StatusBar = false	;

			#endregion

			//===========================================================================================
			#region "Methods: Everything Private"

				//_________________________________________________________________________________________
				// *** Properties

					private	Application	ThisApp		{ get { return	Globals.ThisAddIn.Application	; } }

				//_________________________________________________________________________________________
				// *** Methods

					//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
					private Worksheet GetWS( DTO_WSNode wsNode )
						{
							if (		EqualityComparer<DTO_WSNode>.Default.Equals(wsNode , default(DTO_WSNode))
									||	string.IsNullOrEmpty( wsNode.WBName )
									||	string.IsNullOrEmpty( wsNode.WSName ) )
								{
									return	this.ThisApp.ActiveSheet as Worksheet;
								}
							else
								{
									return	this.ThisApp.Workbooks[wsNode.WBName].Worksheets[wsNode.WSName] as Worksheet;
								}
						}

		#endregion

		}
}
