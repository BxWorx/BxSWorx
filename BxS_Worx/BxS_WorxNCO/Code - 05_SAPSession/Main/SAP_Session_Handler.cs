using System;
using System.Collections.Generic;
using System.Globalization;
//.........................................................
using SMC	= SAP.Middleware.Connector;
//.........................................................
using BxS_WorxNCO.RfcFunction.TableReader;
//•••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••
namespace BxS_WorxNCO.Destination.Config
{
	internal class SAP_Session_Handler
		{
			#region "Properties"


			#endregion




			//===========================================================================================
			#region "Methods: Private"

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				private IList<string> Compile_Data_Filter( string	qID )
					{
						return	new List<string>	{								"			TRANS EQ '1'",
																				String.Concat(" AND	QID		EQ ", "'", qID.ToUpper(), "'")
																			};
					}

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				private IList<string> Compile_Header_Filter(	string			userID
																										,	string			sessionName
																										,	DateTime		dateFrom
																										,	DateTime		dateTo		)
					{
						IList<string>	lt_Filter	= new List<string>
							{
									"    DESTSYS EQ SPACE"
								,	"AND DESTAPP EQ SPACE"
								,	"AND FORMID  EQ SPACE"
								,	"AND QATTRIB EQ SPACE"
								,	"AND MANDANT EQ SY-MANDT"
								,	"AND DATATYP EQ '%BDC'"
							};
						//.............................................
						//.............................................
						if ( ! userID.Length.Equals(0) && ! userID.Equals("*")  )
							{
								lt_Filter.Add(	String.Concat(" AND CREATOR LIKE " , "'" , userID.Replace("*","%").ToUpper() , "'" ) );
							}

						if ( ! sessionName.Length.Equals(0) && ! sessionName.Equals("*")  )
							{
								lt_Filter.Add(	String.Concat(" AND GROUPID LIKE " , "'" , sessionName.Replace("*","%").ToUpper() , "'" ) );
							}

						lt_Filter.Add(String.Concat(	" AND CREDATE BETWEEN " , "'" , dateFrom	.ToString("yyyyMMdd", CultureInfo.InvariantCulture) , "'"
																				,	                " AND " , "'" , dateTo		.ToString("yyyyMMdd", CultureInfo.InvariantCulture) , "'" ) );
						//.............................................
						return	lt_Filter;
					}

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				private void LoadTblRdr_Header( TblRdr_Data lo_TblRdr )
					{
						lo_TblRdr.QueryTable	= "APQI";

						lo_TblRdr.LoadField( "USERID"	 );
						lo_TblRdr.LoadField("GROUPID"	 );
						lo_TblRdr.LoadField("CREDATE"	 );
						lo_TblRdr.LoadField("CRETIME"	 );
						lo_TblRdr.LoadField("TRANSCNT" );
						lo_TblRdr.LoadField("QID"			 );
					}

			#endregion



		}
}
