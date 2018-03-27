using System;
//.........................................................
using BxS_WorxNCO.Destination.API;
using BxS_WorxNCO.RfcFunction.Main;
//•••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••
namespace BxS_WorxNCO.RfcFunction.BDCTran
{
	internal class BDCCall_Profile : RfcFncProfile
		{
			#region "Function Parameters"

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				internal BDCCall_Profile(		string						fncName
																	,	IRfcDestination		rfcDestination
																	, BDCCall_Factory		factory					)	: base(		fncName
																																								, rfcDestination )
					{
						this._Factory		= factory	??	throw		new	ArgumentException( $"{typeof(BDCCall_Profile).Namespace}:- Factory null" );
						//.............................................
						this._FNCIndex	= this._Factory.CreateIndexFNC();
						this._CTUIndex	= this._Factory.CreateIndexCTU();
						this._SPAIndex	= this._Factory.CreateIndexSPA();
						this._BDCIndex	= this._Factory.CreateIndexBDC();
						this._MSGIndex	= this._Factory.CreateIndexMSG();
					}

			#endregion

			//===========================================================================================
			#region "Declarations"

				private const	string	cz_StrCTU		= "CTU_PARAMS"	;
				private const	string	cz_StrSPA		= "RFC_SPAGPA"	;
				private const	string	cz_StrBDC		= "BDCDATA"			;
				private const	string	cz_StrMSG		= "BDCMSGCOLL"	;
				//.................................................
				private	readonly	BDCCall_Factory	_Factory;

			#endregion

			//===========================================================================================
			#region "Properties"

				internal	readonly	BDCCall_IndexFNC		_FNCIndex;
				internal	readonly	BDCCall_IndexCTU		_CTUIndex;
				internal	readonly	BDCCall_IndexSPA		_SPAIndex;
				internal	readonly	BDCCall_IndexBDC		_BDCIndex;
				internal	readonly	BDCCall_IndexMSG		_MSGIndex;

			#endregion

			//===========================================================================================
			#region "Methods: Exposed"

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				internal BDCCall_Header CreateBDCCallHeader( bool withDefaults = true )
					{
						this.ReadyProfile();

						return	this._Factory.CreateBDCHeader(	this._RfcDestination.CreateRfcStructure( cz_StrCTU )
																									,	this._CTUIndex
																									,	withDefaults																					);
					}

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				internal BDCCall_Lines CreateBDCCallLines()
					{
						this.ReadyProfile();

						return	this._Factory.CreateBDCLines(		this._RfcDestination.CreateRfcTable( cz_StrSPA )
																									,	this._RfcDestination.CreateRfcTable( cz_StrBDC )
																									,	this._RfcDestination.CreateRfcTable( cz_StrMSG )
																									,	this._SPAIndex
																									,	this._BDCIndex																		);
					}

			#endregion

			////===========================================================================================
			//#region "Function indicies"

			//	#region "Function Parameters"

			//		[SAPFncAttribute(Name = "IF_TCODE"							)]				public	int ParIdx_TCode	{ get; set;	}
			//		[SAPFncAttribute(Name = "IF_SKIP_FIRST_SCREEN"	)]				public	int ParIdx_Skip1	{ get; set;	}
			//		[SAPFncAttribute(Name = "IS_OPTIONS"						)]				public	int ParIdx_CTUOpt	{ get; set;	}
			//		[SAPFncAttribute(Name = "IT_BDCDATA"						)]				public	int ParIdx_TabBDC	{ get; set;	}
			//		[SAPFncAttribute(Name = "ET_MSG"								)]				public	int	ParIdx_TabMSG	{ get; set;	}
			//		[SAPFncAttribute(Name = "CT_SETGET_PARAMETER"		)]				public	int ParIdx_TabSPA	{ get; set;	}

			//	#endregion
			//	//.................................................
			//	#region "CTUOptions"

			//		[SAPFncAttribute(Stru	= cz_StrCTU	,	Name = "DISMODE"	)]	public	int CTUOpt_DspMde	{ get; set;	}
			//		[SAPFncAttribute(Stru	= cz_StrCTU	,	Name = "UPDMODE"	)]	public	int CTUOpt_UpdMde	{ get; set;	}
			//		[SAPFncAttribute(Stru	= cz_StrCTU	,	Name = "CATTMODE"	)]	public	int CTUOpt_CATMde	{ get; set;	}
			//		[SAPFncAttribute(Stru	= cz_StrCTU	,	Name = "DEFSIZE"	)]	public	int CTUOpt_DefSze	{ get; set;	}
			//		[SAPFncAttribute(Stru	= cz_StrCTU	,	Name = "RACOMMIT"	)]	public	int CTUOpt_NoComm	{ get; set;	}
			//		[SAPFncAttribute(Stru	= cz_StrCTU	,	Name = "NOBINPT"	)]	public	int CTUOpt_NoBtcI	{ get; set;	}
			//		[SAPFncAttribute(Stru	= cz_StrCTU	,	Name = "NOBIEND"	)]	public	int CTUOpt_NoBtcE	{ get; set;	}

			//	#endregion
			//	//.................................................
			//	#region "SPA Data"

			//		[SAPFncAttribute(Stru	= cz_StrSPA	,	Name = "PARID"	)]		public	int SPADat_MID	{ get; set;	}
			//		[SAPFncAttribute(Stru	= cz_StrSPA	,	Name = "PARVAL"	)]		public	int SPADat_Val	{ get; set;	}

			//	#endregion
			//	//.................................................
			//	#region "BDC Data"

			//		[SAPFncAttribute(Stru	= cz_StrBDC	,	Name = "PROGRAM"	)]	public	int BDCDat_Prg	{ get; set;	}
			//		[SAPFncAttribute(Stru	= cz_StrBDC	,	Name = "DYNPRO"		)]	public	int BDCDat_Dyn	{ get; set;	}
			//		[SAPFncAttribute(Stru	= cz_StrBDC	,	Name = "DYNBEGIN"	)]	public	int BDCDat_Bgn	{ get; set;	}
			//		[SAPFncAttribute(Stru	= cz_StrBDC	,	Name = "FNAM"			)]	public	int BDCDat_Fld	{ get; set;	}
			//		[SAPFncAttribute(Stru	= cz_StrBDC	,	Name = "FVAL"			)]	public	int BDCDat_Val	{ get; set;	}

			//	#endregion
			//	//.................................................
			//	#region "Messages"

			//		[SAPFncAttribute(Stru	= cz_StrMSG	,	Name = "TCODE"		)]	public	int TabMsg_TCode	{ get; set;	}
			//		[SAPFncAttribute(Stru	= cz_StrMSG	,	Name = "DYNAME"		)]	public	int TabMsg_DynNm	{ get; set;	}
			//		[SAPFncAttribute(Stru	= cz_StrMSG	,	Name = "DYNUMB"		)]	public	int TabMsg_DynNo	{ get; set;	}
			//		[SAPFncAttribute(Stru	= cz_StrMSG	,	Name = "MSGTYP"		)]	public	int TabMsg_MsgTp	{ get; set;	}
			//		[SAPFncAttribute(Stru	= cz_StrMSG	,	Name = "MSGSPRA"	)]	public	int TabMsg_Lang		{ get; set;	}
			//		[SAPFncAttribute(Stru	= cz_StrMSG	,	Name = "MSGID"		)]	public	int TabMsg_MsgID	{ get; set;	}
			//		[SAPFncAttribute(Stru	= cz_StrMSG	,	Name = "MSGNR"		)]	public	int TabMsg_MsgNo	{ get; set;	}
			//		[SAPFncAttribute(Stru	= cz_StrMSG	,	Name = "MSGV1"		)]	public	int TabMsg_MsgV1	{ get; set;	}
			//		[SAPFncAttribute(Stru	= cz_StrMSG	,	Name = "MSGV2"		)]	public	int TabMsg_MsgV2	{ get; set;	}
			//		[SAPFncAttribute(Stru	= cz_StrMSG	,	Name = "MSGV3"		)]	public	int TabMsg_MsgV3	{ get; set;	}
			//		[SAPFncAttribute(Stru	= cz_StrMSG	,	Name = "MSGV4"		)]	public	int TabMsg_MsgV4	{ get; set;	}
			//		[SAPFncAttribute(Stru	= cz_StrMSG	,	Name = "ENV"			)]	public	int TabMsg_Envir	{ get; set;	}
			//		[SAPFncAttribute(Stru	= cz_StrMSG	,	Name = "FLDNAME"	)]	public	int TabMsg_Fldnm	{ get; set;	}

			//	#endregion

			//#endregion

		}
}
