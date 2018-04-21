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
			#region "Documentation"

				//	*"----------------------------------------------------------------------
				//	00:	QID				000000 000020 C Queue identification (unique key)
				//	01:	TRANS			000020 000010 I Transaction counter: Batch input, statistics
				//	02:	BLOCK			000030 000010 I Message counter: Batch input, statistics
				//	03:	SEGMT			000040 000005 s Queue data segmentation number
				//	04:	MSGCOUNT	000045 000005 s Queue data block number
				//	05:	VARLEN		000050 000005 s Queue data length of user data
				//	06:	VARDATA		000055 007902 C Long String Used to Store BDC Objects in the Database
				//	*"----------------------------------------------------------------------

			#endregion

			//===========================================================================================
			#region "Declarations"

				private const char	cx_NullChar	= (char) 0;
				//.................................................
				private const int		cx_Blck	= 2;
				private const int		cx_Segm	= 3;
				private const int		cx_Data	= 6;

			#endregion

			//===========================================================================================
			#region "Methods: Exposed"

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				internal void ProcessSAPSessionDDICInfo( ISAP_Session_Profile profile )
					{
						foreach (DTO_BDC_Data lo_Data in profile.BDCData.BDCData.Where( x=>			! string.IsNullOrEmpty	( x.FieldName  )
																																								&&	! x.FieldName.Equals		( "BDC_OKCODE" )
																																								&&  ! x.FieldName.Equals		( "BDC_CURSOR" )	)	)
							{
								string[] lt_Split = lo_Data.FieldName.Split('-');

								if (lt_Split.Length.Equals(2))
									{
										profile.DDICInfo.AddUpdateText( lt_Split[0] , lt_Split[1] );
									}
							}
					}

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				internal void ProcessSAPSessionDataHeader( TblRdr_Data tblRdr , ISAP_Session_Profile profile )
					{
						string[] lt_Hdr	= tblRdr.OutData[0].GetString(0).Split( tblRdr.Delimeter );
						//.............................................
						profile.SAPTCode	= this.SetTransaction	( lt_Hdr[cx_Data] );

						this.SetCTU( lt_Hdr[cx_Data] , profile.CTUParams	);
					}

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				internal void ProcessSAPSessionData( TblRdr_Data tblRdr , ISAP_Session_Profile profile )
					{
						IList< BDCSession_Raw > lt_RawData	=	new	List< BDCSession_Raw >();

						string[]	lt_Rowdata;
						string[]	lt_Values	;
						//.............................................
						for (int r = 1; r < tblRdr.OutData.Count; r++)
							{
								lt_Rowdata	= tblRdr.OutData[r].GetString(0).Split( tblRdr.Delimeter );
								lt_Values		= lt_Rowdata[cx_Data].Split( cx_NullChar );

								var lo	= new BDCSession_Raw {	Blockcount	= long.Parse( lt_Rowdata[cx_Blck] )
																							, SegmentNo		= int	.Parse( lt_Rowdata[cx_Segm] )
																							, BDCList			= lt_Values														};

								lt_RawData.Add( lo );
							}
						//.............................................
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
														DTO_BDC_Data lo_Data	= profile.Create_BDC_DTO();

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
						tblRdr.Delimeter	= '|'		;
					}

			#endregion

			//===========================================================================================
			#region "Methods: Private"

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				private string SetTransaction( string rawData )
					{
						return	rawData.Substring( 2 , 20 ).Trim()	?? "*****";
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

				//=========================================================================================
				private class BDCSession_Raw
					{
						#region "Properties"

							protected internal	long						Blockcount	;
							protected internal	int							SegmentNo   ;
							protected	internal	IList<string>		BDCList     ;

						#endregion

					}

			#endregion

		}
}
