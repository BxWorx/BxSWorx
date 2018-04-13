using System;
//.........................................................
using SMC	= SAP.Middleware.Connector;
//.........................................................
using BxS_WorxNCO.RfcFunction.Main;

using static	BxS_WorxNCO.Main										.NCO_Constants;
using	static	BxS_WorxNCO.RfcFunction.TableReader	.TblRdr_Constants;
//•••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••
namespace BxS_WorxNCO.RfcFunction.TableReader
{
	internal class TblRdr_Function	: RfcFncBase
		{
			#region "Documentation"

				//	*"----------------------------------------------------------------------
				//	*"*"Local Interface:
				//	*"  IMPORTING
				//	*"     VALUE(QUERY_TABLE)	TYPE  DD02L-TABNAME
				//	*"     VALUE(DELIMITER)		TYPE  SONV-FLAG		OPTIONAL
				//	*"     VALUE(NO_DATA)			TYPE  SONV-FLAG		OPTIONAL
				//	*"     VALUE(ROWSKIPS)		TYPE  SOID-ACCNT	OPTIONAL
				//	*"     VALUE(ROWCOUNT)		TYPE  SOID-ACCNT	OPTIONAL
				//	*"  EXPORTING
				//	*"     VALUE(OUT_TABLE) TYPE  DD02L-TABNAME
				//	*"  TABLES
				//	*"      OPTIONS			STRUCTURE  RFC_DB_OPT
				//	*"      FIELDS			STRUCTURE  RFC_DB_FLD
				//	*"      TBLOUT128		STRUCTURE  /BODS/TAB128
				//	*"      TBLOUT512		STRUCTURE  /BODS/TAB512
				//	*"      TBLOUT2048	STRUCTURE  /BODS/TAB2048
				//	*"      TBLOUT8192	STRUCTURE  /BODS/TAB8192
				//	*"      TBLOUT30000	STRUCTURE  /BODS/TAB30K
				//	*"  EXCEPTIONS
				//	*"      TABLE_NOT_AVAILABLE
				//	*"      TABLE_WITHOUT_DATA
				//	*"      OPTION_NOT_VALID
				//	*"      FIELD_NOT_VALID
				//	*"      NOT_AUTHORIZED
				//	*"      DATA_BUFFER_EXCEEDED
				//	*"----------------------------------------------------------------------

			#endregion

			//===========================================================================================
			#region "Constructors"

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				internal TblRdr_Function( TblRdr_Profile	profile	)	: base(	profile )
					{
						this.MyProfile	= new Lazy< TblRdr_Profile >(	()=> (TblRdr_Profile) this.Profile , cz_LM );
					}

			#endregion

			//===========================================================================================
			#region "Declarations"

				internal	readonly	Lazy< TblRdr_Profile >	MyProfile;

			#endregion

			//===========================================================================================
			#region "Properties"

				private	TblRdr_IndexFNC	FNCIndex	{ get {	return	this.MyProfile.Value._FNCIndex.Value	; } }

			#endregion

			//===========================================================================================
			#region "Methods: Exposed"

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				internal void Process(	TblRdr_Data					dto
															, SMC.RfcDestination	rfcDestination )
					{
						try
							{
								this.Setup( dto );
								//.........................................
								this.Invoke( rfcDestination );
								//.........................................
								this.ProcessOutput( dto );
							}
						catch (Exception)
							{
							throw;
							}
						finally
							{
							}
					}

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				internal void Reset()
					{
						this.Profile.ReadyProfile();
						//.............................................
						this.NCORfcFunction.GetTable( this.FNCIndex.Options ).Clear();
						this.NCORfcFunction.GetTable( this.FNCIndex.Fields	).Clear();
					}

			#endregion

			//===========================================================================================
			#region "Methods: Private"

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				private void ProcessOutput( TblRdr_Data dto )
					{
						string					lc_Nme	= this.NCORfcFunction.GetString				( this.FNCIndex.OutTable );
						int							ln_Idx	= this.GetOutTableIndex								( lc_Nme );
						TblRdr_IndexOUT	lo_Idx	= this.MyProfile.Value.CreateOutIndex	( lc_Nme );

						this.MyProfile.Value.LoadStructureIndex( lo_Idx );
						dto.OutData	= lo_Idx.CreateTable();
						//.............................................
						this.LoadTable( dto.Fields	, this.FNCIndex.Fields );
						this.LoadTable( dto.OutData , ln_Idx );
					}

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				private void Setup( TblRdr_Data dto )
					{
						this.NCORfcFunction.SetValue( this.FNCIndex.QryTable	, dto.QueryTable	);
						this.NCORfcFunction.SetValue( this.FNCIndex.Delimeter	, dto.Delimeter		);
						this.NCORfcFunction.SetValue( this.FNCIndex.SkipRows	,	dto.SkipRows		);
						this.NCORfcFunction.SetValue( this.FNCIndex.RowsCount	,	dto.ReturnRows	);

						this.NCORfcFunction.SetValue( this.FNCIndex.NoData		,	dto.NoData	? cz_True	: cz_False	)	;
						//.............................................
						this.NCORfcFunction.GetTable( this.FNCIndex.Options )	.Append( dto.Options  );
						this.NCORfcFunction.GetTable( this.FNCIndex.Fields )	.Append( dto.Fields		);
					}

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				private	void LoadTable( SMC.IRfcTable	data , int index )
					{
						data.Clear();
						//.............................................
						data.Append ( this.NCORfcFunction.GetTable( index ) );
					}

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				private	int GetOutTableIndex( string name )
					{
						switch ( name )
							{
								case cz_OutTab512		:	return	this.FNCIndex.OutTab512		;
								case cz_OutTab2048	:	return	this.FNCIndex.OutTab2048	;
								case cz_OutTab8192	:	return	this.FNCIndex.OutTab8192	;
								case cz_OutTab30000	:	return	this.FNCIndex.OutTab30000	;
								default							:	return	this.FNCIndex.OutTab128		;
							}
					}

			#endregion

		}
}
