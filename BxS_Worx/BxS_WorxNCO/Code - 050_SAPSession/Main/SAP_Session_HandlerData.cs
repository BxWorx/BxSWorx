using System;
using System.Collections.Generic;
using System.Linq;
//.........................................................
using BxS_WorxNCO.RfcFunction.TableReader;
using BxS_WorxNCO.SAPSession.API;
using BxS_WorxNCO.BDCSession.DTO;

using static	BxS_WorxNCO.Main.NCO_Constants;
//•••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••
namespace BxS_WorxNCO.SAPSession.Main
{
	internal class SAP_Session_HandlerData
		{
			#region "Methods: Exposed"

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				internal void ProcessSAPSessionData( TblRdr_Data tblRdr , ISAP_Session_Profile profile )
					{
						const char vbNullChar = (char) 0;
						//.............................................
						IEnumerable< BDCSession_Raw >	lq_Query	= from lc_Row	in tblRdr.OutData
																												let lt_Flds	= lc_Row.GetString(0).Split( tblRdr.Delimeter )
																												let lt_Splt	= lt_Flds[6].Split( vbNullChar ).ToList()
																													select new BDCSession_Raw {		Blockcount	= long	.Parse( lt_Flds[2] )
																																											, SegmentNo		= int		.Parse( lt_Flds[3] )
																																											, BDCList			= lt_Splt };

						IList< BDCSession_Raw > lt_RawData	= lq_Query.OrderBy( x => x.Blockcount ).ToList();
						//.............................................
						this.SetTransaction	( lt_RawData[0].BDCList[0] , profile.SAPTCode	 );
						this.SetCTU					( lt_RawData[0].BDCList[0] , profile.CTUParams );

						for (int r = 1; r < lt_RawData.Count; r++)
							{
								if ( lt_RawData[r].BDCList[0].Substring(0,1).Equals("M") )
									{
										DTO_BDC_Data lo_Scrn	= profile.BDCData.CreateDataDTO();

										this.SetDynPro( lt_RawData[r].BDCList[21] , lo_Scrn );
										profile.BDCData.AddBDCData( lo_Scrn );

										for (int i = 23; i < lt_RawData[r].BDCList.Count; i += 2)
											{
												if ( ! string.IsNullOrWhiteSpace( lt_RawData[r].BDCList[i] ) )
													{
														DTO_BDC_Data lo_Data	= profile.BDCData.CreateDataDTO();

														lo_Data.FieldName		= lt_RawData[r].BDCList[ i   ];
														lo_Data.FieldValue	= lt_RawData[r].BDCList[ i+1 ];

														profile.BDCData.AddBDCData(	lo_Data );
													}
											}
									}
							}
					}

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				internal void Compile_Filter( TblRdr_Data tblRdr , string qID )
					{
						tblRdr.LoadOption( 								"			TRANS EQ '1'" );
						tblRdr.LoadOption( String.Concat( " AND	QID		EQ ", "'", qID.ToUpper(), "'" ) );
					}

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				internal void LoadTblRdr( TblRdr_Data tblRdr )
					{
						tblRdr.Reset();
						tblRdr.QueryTable	= "APQD";
					}

			#endregion

			//===========================================================================================
			#region "Methods: Private"

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				private void SetTransaction( string rawData , string sapTCode )
					{
						sapTCode	= rawData.Substring( 2 , 20 ).Trim()	?? "*****";
					}

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				private void SetCTU( string rawData , DTO_BDC_CTU ctu )
					{
						const	int	lx_Dismode	= 24;
						const	int	lx_Updmode	= 25;
						const	int	lx_Catmode	= 26;
						const	int	lx_Defsize	= 27;
						const	int	lx_RACommt	= 28;
						const	int	lx_NoBInpt	= 29;
						const	int	lx_NoBIEnd	= 30;
						//.............................................
						ctu.DisplayMode		= char.Parse( rawData.Substring( lx_Dismode , 1 ) );
						ctu.UpdateMode		= char.Parse( rawData.Substring( lx_Updmode , 1 ) );
						ctu.CATTMode			= char.Parse( rawData.Substring( lx_Catmode , 1 ) );
						ctu.DefaultSize		= char.Parse( rawData.Substring( lx_Defsize , 1 ) );
						ctu.NoCommit			= char.Parse( rawData.Substring( lx_RACommt , 1 ) );
						ctu.NoBatchInpFor	= char.Parse( rawData.Substring( lx_NoBInpt , 1 ) );
						ctu.NoBatchInpAft	= char.Parse( rawData.Substring( lx_NoBIEnd , 1 ) );
					}

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				private void SetDynPro( string rawData , DTO_BDC_Data bdcData )
					{
						bdcData.ProgramName		= rawData.Substring( 00	, 40 ).Trim();
						bdcData.Dynpro				=	rawData.Substring( 40	,	04 ).Trim();
						bdcData.Begin					= cz_True	;
					}

			#endregion

			//°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°
			#region "Classes: Private"

				private class BDCSession_Raw
					{
						#region "Properties"

							protected internal	long					Blockcount	;
							protected internal	int						SegmentNo   ;
							protected	internal	IList<string>	BDCList     ;

						#endregion

					}

			#endregion

		}
}
