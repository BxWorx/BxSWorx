using System;
using System.Collections.Generic;
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
				private IList<string> LoadTblRdr_Header_Filter(	string			userID
																							,	string			sessionName
																							,	DateTime		dateFrom
																							,	DateTime		dateTo		)
					{
						IList<string>	lt_Filter	= new List<string>();

						lt_Filter.Add("    DESTSYS EQ SPACE"		);
						lt_Filter.Add("AND DESTAPP EQ SPACE"		);
						lt_Filter.Add("AND FORMID  EQ SPACE"		);
						lt_Filter.Add("AND QATTRIB EQ SPACE"		);
						lt_Filter.Add("AND MANDANT EQ SY-MANDT"	);
						lt_Filter.Add("AND DATATYP EQ '%BDC'"		);

						if ( ! userID.Length.Equals(0) && ! userID.Equals("*")  )
							{
								lt_Filter.Add(	String.Concat(" AND CREATOR LIKE ", 
																													"'", 
																													i_UserId.Replace("*"c,"%"c).ToUpper(),
																													"'" ) )

							}

				If Not i_UserId.Length.Equals(0) AndAlso Not i_UserId.Equals("*"c)
					Me.co_TblRdr_Hdr.Value.Add_Filter(String.Concat(" AND CREATOR LIKE ", 
																													"'", 
																													i_UserId.Replace("*"c,"%"c).ToUpper(),
																													"'" ) )
				End If

				If Not i_SessionName.Length.Equals(0) AndAlso Not i_SessionName.Equals("*"c)
					Me.co_TblRdr_Hdr.Value.Add_Filter(String.Concat(" AND GROUPID LIKE ",
																													"'", 
																													i_SessionName.Replace("*"c,"%"c).ToUpper(), 
																													"'" ) )
				End If

				Me.co_TblRdr_Hdr.Value.Add_Filter(String.Concat(" AND CREDATE BETWEEN ",
																												"'", 
																												i_DateFrom.ToString("yyyyMMdd", CultureInfo.InvariantCulture),
																												"'",
																												" AND ",
																												"'", 
																												i_DateTo.ToString("yyyyMMdd", CultureInfo.InvariantCulture),
																												"'" ) )


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
