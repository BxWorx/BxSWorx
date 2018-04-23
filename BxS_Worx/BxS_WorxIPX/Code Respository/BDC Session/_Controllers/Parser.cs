using System;
using System.Collections.Generic;
using System.Text;
//.........................................................
using BxS_WorxIPX.SAPBDCSession;
//•••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••
namespace BxS_WorxIPX.BDC
{
	internal class Parser
		{
			#region "Constructors"

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				internal Parser( Func<ISAP_BDCSession> factory )
					{
						this._Factory	= factory;
					}

			#endregion

			//===========================================================================================
			#region "Declarations"

				private	const string	cz_Cfg	= "BDCXMLConfig";
				//.................................................
				private	readonly	Func<ISAP_BDCSession>		_Factory;

			#endregion

			//===========================================================================================
			#region "Methods: Exposed"

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				internal void ParseRequest( IExcel_BDCRequest excel , ISAP_BDCRequest sap )
					{
						// Convert the SAP logon
						//
						sap.SAPLogon?.Transfer( excel.SAPLogon );

						// process each worksheet into a BDC Session
						//
						foreach (	KeyValuePair<Guid , IExcel_BDCWorksheet> ls_kvp in excel.Worksheets )
							{
								sap.Sessions.Add( ls_kvp.Key , this.ParseWSToRSession( ls_kvp.Value ) );
							}
					}

			#endregion

			//===========================================================================================
			#region "Methods: Private"

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				private ISAP_BDCSession ParseWSToRSession( IExcel_BDCWorksheet ws )
					{
						ISAP_BDCSession lo_Ssn	= this._Factory();
						//.............................................
						lo_Ssn.WBID					= ws.WBID					;
						lo_Ssn.WSID					= ws.WSID					;
						lo_Ssn.WSNo					= ws.WSNo					;
						lo_Ssn.UsedAddress		= ws.UsedAddress	;
						//.............................................
						lo_Ssn.IsTest				= ws.IsTest				;
						lo_Ssn.IsOnline			= ws.IsOnline			;
						lo_Ssn.IsBDCSession	= ws.IsBDCSession	;
						lo_Ssn.IsActive			= ws.IsActive			;
						//.............................................
						lo_Ssn.WSData1D.Clear();

						if ( ws.WSCells == null )
							{
								lo_Ssn.RowLB		= -1	;
								lo_Ssn.RowUB		= -1	;
								lo_Ssn.ColLB		= -1	;
								lo_Ssn.ColUB		= -1	;
							}
						else
							{
								var		lo_SB				= new StringBuilder();
								bool	lb_SrchCfg	= true;
								//.........................................
								lo_Ssn.RowLB		= ws.WSCells.GetLowerBound(0);
								lo_Ssn.RowUB		= ws.WSCells.GetUpperBound(0);
								lo_Ssn.ColLB		= ws.WSCells.GetLowerBound(1);
								lo_Ssn.ColUB		= ws.WSCells.GetUpperBound(1);
								//.........................................
								for ( int	r = lo_Ssn.RowLB; r <= lo_Ssn.RowUB; r++ )
									{
										for ( int c = lo_Ssn.ColLB; c <= lo_Ssn.ColUB; c++ )
											{
												if ( ws.WSCells[r,c] != null )
													{
														// Search for CONFIG cell, store seperately and do not write to container.
														//
														if ( lb_SrchCfg )
															{
																if ( ws.WSCells[r,c].ToString().Contains( cz_Cfg ) )
																	{
																		lo_Ssn.XMLConfig	= ws.WSCells[r,c].ToString();
																		lb_SrchCfg	= false;
																		continue;
																	}
															}
														//.............................
														lo_SB.Clear();
														lo_SB.AppendFormat( $"{r.ToString()},{c.ToString()}" );

														lo_Ssn.WSData1D.Add( lo_SB.ToString() , ws.WSCells[r,c].ToString() );
													}
											}
									}
							}
						//.............................................
						return	lo_Ssn;
					}

			#endregion

		}
}
