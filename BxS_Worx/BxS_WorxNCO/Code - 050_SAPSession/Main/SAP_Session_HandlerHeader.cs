using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
//.........................................................
using BxS_WorxNCO.RfcFunction.TableReader;
using BxS_WorxNCO.SAPSession.API;
//•••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••
namespace BxS_WorxNCO.SAPSession.Main
{
	internal class SAP_Session_HandlerHeader
		{
			#region "Properties"

				private	const	int	cx_Idx_Usr	= 0;
				private	const	int	cx_Idx_Nme	= 1;
				private	const	int	cx_Idx_Dte	= 2;
				private	const	int	cx_Idx_Tme	= 3;
				private	const	int	cx_Idx_Cnt  = 4;
				private	const	int	cx_Idx_QID  = 5;

			#endregion

			//===========================================================================================
			#region "Methods: Exposed"

			//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
			internal void ProcessSAPSessionData( TblRdr_Data tblRdr , IList< ISAP_Session_Header > list )
					{
						IEnumerable< ISAP_Session_Header >	lq_Query	= from lc_Row	in tblRdr.OutData
																															let lt_Flds	= lc_Row.GetString(0).Split( tblRdr.Delimeter )
																																select new SAP_Session_Header
																																	{
																																			UserID       = lt_Flds[cx_Idx_Usr].Trim()
																																		,	SessionName  = lt_Flds[cx_Idx_Nme].Trim()
																																		,	CreationDate = DateTime.ParseExact	( lt_Flds[cx_Idx_Dte] , "yyyyMMdd"	,CultureInfo.InvariantCulture)
																																		,	CreationTime = TimeSpan.ParseExact	( lt_Flds[cx_Idx_Tme] , "hhmmss"		,CultureInfo.InvariantCulture)
																																		,	Count        = int.Parse( lt_Flds[cx_Idx_Cnt] )
																																		,	QID          = lt_Flds[cx_Idx_QID]
																																	};

						list	= lq_Query.ToList();
					}

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				internal void Compile_Filter(		TblRdr_Data	tblRdr
																			,	string			userID
																			,	string			sessionName
																			,	DateTime		dateFrom
																			,	DateTime		dateTo			)
					{
						tblRdr.LoadOption( "    DESTSYS EQ SPACE"		 );
						tblRdr.LoadOption( "AND DESTAPP EQ SPACE"		 );
						tblRdr.LoadOption( "AND FORMID  EQ SPACE"		 );
						tblRdr.LoadOption( "AND QATTRIB EQ SPACE"		 );
						tblRdr.LoadOption( "AND MANDANT EQ SY-MANDT" );
						tblRdr.LoadOption( "AND DATATYP EQ '%BDC'"	 );
						//.............................................
						if ( ! userID.Length.Equals(0) && ! userID.Equals("*")  )
							{
								tblRdr.LoadOption( String.Concat(" AND CREATOR LIKE " , "'" , userID.Replace("*","%").ToUpper() , "'" ) );
							}
						//.............................................
						if ( ! sessionName.Length.Equals(0) && ! sessionName.Equals("*")  )
							{
								tblRdr.LoadOption( String.Concat(" AND GROUPID LIKE " , "'" , sessionName.Replace("*","%").ToUpper() , "'" ) );
							}
						//.............................................
						tblRdr.LoadOption( String.Concat(		" AND CREDATE BETWEEN " , "'" , dateFrom	.ToString("yyyyMMdd", CultureInfo.InvariantCulture) , "'"
																							,	                " AND " , "'" , dateTo		.ToString("yyyyMMdd", CultureInfo.InvariantCulture) , "'" ) );
					}

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				internal void LoadTblRdr( TblRdr_Data tblRdr )
					{
						tblRdr.Reset();
						tblRdr.QueryTable	= "APQI";

						tblRdr.LoadField( "USERID"	 );
						tblRdr.LoadField( "GROUPID"	 );
						tblRdr.LoadField( "CREDATE"	 );
						tblRdr.LoadField( "CRETIME"	 );
						tblRdr.LoadField( "TRANSCNT" );
						tblRdr.LoadField( "QID"			 );
					}

			#endregion

		}
}
