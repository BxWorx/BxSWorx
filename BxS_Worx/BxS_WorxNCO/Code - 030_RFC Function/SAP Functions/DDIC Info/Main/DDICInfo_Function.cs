using System;
using System.Collections.Generic;
using System.Linq;
//.........................................................
using SMC	= SAP.Middleware.Connector;
//.........................................................
using BxS_WorxNCO.RfcFunction.Main;

using static	BxS_WorxNCO.Main.NCO_Constants;
//•••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••
namespace BxS_WorxNCO.RfcFunction.DDIC
{
	internal class DDICInfo_Function	: RfcFncBase
		{
			#region "Documentation"

				//	function ddif_fieldinfo_get.
				//	*"----------------------------------------------------------------------
				//	*"*"Lokale Schnittstelle:
				//	*"  IMPORTING
				//	*"     VALUE(TABNAME)				TYPE  DDOBJNAME
				//	*"     VALUE(FIELDNAME)			TYPE  DFIES-FIELDNAME		DEFAULT ' '
				//	*"     VALUE(LANGU)					TYPE  SY-LANGU					DEFAULT SY-LANGU
				//	*"     VALUE(LFIELDNAME)		TYPE  DFIES-LFIELDNAME	DEFAULT ' '
				//	*"     VALUE(ALL_TYPES)			TYPE  DDBOOL_D					DEFAULT ' '
				//	*"     VALUE(GROUP_NAMES)		TYPE  DDBOOL_D					DEFAULT ' '
				//	*"     VALUE(UCLEN)					TYPE  UNICODELG					OPTIONAL
				//	*"     VALUE(DO_NOT_WRITE)	TYPE  DDBOOL_D					DEFAULT ' '
				//	*"  EXPORTING
				//	*"     VALUE(X030L_WA)			TYPE  X030L
				//	*"     VALUE(DDOBJTYPE)			TYPE  DD02V-TABCLASS
				//	*"     VALUE(DFIES_WA)			TYPE  DFIES
				//	*"     VALUE(LINES_DESCR)		TYPE  DDTYPELIST
				//	*"  TABLES
				//	*"      DFIES_TAB						STRUCTURE	DFIES				OPTIONAL
				//	*"      FIXED_VALUES				TYPE			DDFIXVALUES OPTIONAL
				//	*"  EXCEPTIONS
				//	*"      NOT_FOUND
				//	*"      INTERNAL_ERROR
				//	*"----------------------------------------------------------------------

			#endregion

			//===========================================================================================
			#region "Constructors"

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				internal DDICInfo_Function( DDICInfo_Profile	profile	)	: base(	profile )
					{
						this.MyProfile	= new Lazy< DDICInfo_Profile >	(	()=> (DDICInfo_Profile) this.Profile , cz_LM );
					}

			#endregion

			//===========================================================================================
			#region "Declarations"

				internal	readonly	Lazy< DDICInfo_Profile >	MyProfile;

			#endregion

			//===========================================================================================
			#region "Properties"

				private	DDICInfo_IndexFNC			FNCIndex	{ get {	return	this.MyProfile.Value._FNCIndex.Value	; } }
				private	DDICInfo_IndexDFIES		TABIndex	{ get {	return	this.MyProfile.Value._DFSIndex.Value	; } }

				private	int		Idx_TabNme		{ get {	return	this.FNCIndex.TblNm	; } }
				private	int		Idx_NoWrit		{ get {	return	this.FNCIndex.NoWrt	; } }
				private	int		Idx_DFTble		{ get {	return	this.FNCIndex.DFIES	; } }

			#endregion

			//===========================================================================================
			#region "Methods: Exposed"

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				internal DDICInfo_FieldCollection	CreateFieldCollection()	=> this.MyProfile.Value.CreateFieldCollection();

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				internal void Process(	DDICInfo_FieldCollection	dto
															, SMC.RfcDestination				rfcDestination )
					{
						try
							{
								foreach (	string lc_TblNme in dto.TableNames )
									{
										this.NCORfcFunction.SetValue( this.Idx_TabNme , lc_TblNme	);
										this.NCORfcFunction.SetValue( this.Idx_NoWrit , cz_True		);
										//.............................................
										this.Invoke( rfcDestination );
										//.............................................
										this.ProcessFields( lc_TblNme , dto );
									}
							}
						catch (Exception)
							{
							throw;
							}
						finally
							{
							}
					}

			#endregion

			//===========================================================================================
			#region "Methods: Private"

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				private void ProcessFields( string tableName ,	DDICInfo_FieldCollection	dto	)
					{
						IList<string>	lt_Nmes	= dto.GetFieldList( tableName ).Select( f => f.FldName ).ToList() ;
						SMC.IRfcTable							lt_Flds	= this.NCORfcFunction.GetTable( this.Idx_DFTble );

						var y = lt_Flds.Where( r => { string n = r.GetString(this.TABIndex.Name);
																					return	lt_Nmes.Contains( n );
																				} ).Select


						string[] lt = null;

						x.w

						IEnumerable< string >	lq_Query	= from lc_Row	in lt_Flds

																								let lc_nme	= lc_Row.GetString( this.TABIndex.Name )

																									join f in lt on 

																								let lc_Txt	= lc_Row.GetString( this.TABIndex.Txt	 )
			
			
			IEnumerable< ISAP_Session_Header >	lq_Query	= from lc_Row	in tblRdr.OutData
																															let lt_Flds	= lc_Row.GetString(0).Split( tblRdr.Delimeter )
																																select new SAP_Session_Header
																																	{
																																			UserID       = lt_Flds[cx_Idx_Usr].Trim()
																																		,	SessionName  = lt_Flds[cx_Idx_Nme].Trim()
																																		,	CreationDate = DateTime.ParseExact	( lt_Flds[cx_Idx_Dte] , "yyyyMMdd"	, CultureInfo.InvariantCulture)
																																		,	CreationTime = TimeSpan.ParseExact	( lt_Flds[cx_Idx_Tme] , "hhmmss"		, CultureInfo.InvariantCulture)
																																		,	Count        = int.Parse( lt_Flds[cx_Idx_Cnt] )
																																		,	QID          = lt_Flds[cx_Idx_QID]
																																	};

						list	= lq_Query.ToList();

					}

			#endregion

			private class FieldDTO
			{
				string fld { get;  set; }
				string txt { get;  set; }
			}

		}
}
