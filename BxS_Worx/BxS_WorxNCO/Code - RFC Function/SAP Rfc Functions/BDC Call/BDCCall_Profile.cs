using BxS_WorxNCO.RfcFunction.Common;
//•••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••
namespace BxS_WorxNCO.RfcFunction.BDCTran
{
	internal class BDCCall_Profile : RfcFncProfile
		{
			#region "Function Parameters"

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				internal BDCCall_Profile( string FncName )	: base( FncName )
					{	}

			#endregion

			//===========================================================================================
			#region "Function indicies"

				#region "Function Parameters"

					[SAPFncAttribute(Name = "IF_TCODE"							)]						public	int ParIdx_TCode	{ get; set;	}
					[SAPFncAttribute(Name = "IF_SKIP_FIRST_SCREEN"	)]						public	int ParIdx_Skip1	{ get; set;	}
					[SAPFncAttribute(Name = "IS_OPTIONS"						)]						public	int ParIdx_CTUOpt	{ get; set;	}
					[SAPFncAttribute(Name = "IT_BDCDATA"						)]						public	int ParIdx_TabBDC	{ get; set;	}
					[SAPFncAttribute(Name = "ET_MSG"								)]						public	int	ParIdx_TabMSG	{ get; set;	}
					[SAPFncAttribute(Name = "CT_SETGET_PARAMETER"		)]						public	int ParIdx_TabSPA	{ get; set;	}

				#endregion
				//.................................................
				#region "CTUOptions"

					[SAPFncAttribute(Stru	= "CTU_PARAMS"	,	Name = "DISMODE"	)]	public	int CTUOpt_DspMde	{ get; set;	}
					[SAPFncAttribute(Stru	= "CTU_PARAMS"	,	Name = "UPDMODE"	)]	public	int CTUOpt_UpdMde	{ get; set;	}
					[SAPFncAttribute(Stru	= "CTU_PARAMS"	,	Name = "CATTMODE"	)]	public	int CTUOpt_CATMde	{ get; set;	}
					[SAPFncAttribute(Stru	= "CTU_PARAMS"	,	Name = "DEFSIZE"	)]	public	int CTUOpt_DefSze	{ get; set;	}
					[SAPFncAttribute(Stru	= "CTU_PARAMS"	,	Name = "RACOMMIT"	)]	public	int CTUOpt_NoComm	{ get; set;	}
					[SAPFncAttribute(Stru	= "CTU_PARAMS"	,	Name = "NOBINPT"	)]	public	int CTUOpt_NoBtcI	{ get; set;	}
					[SAPFncAttribute(Stru	= "CTU_PARAMS"	,	Name = "NOBIEND"	)]	public	int CTUOpt_NoBtcE	{ get; set;	}

				#endregion
				//.................................................
				#region "SPA Data"

					[SAPFncAttribute(Stru	= "RFC_SPAGPA"	,	Name = "PARID"	)]		public	int SPADat_MID	{ get; set;	}
					[SAPFncAttribute(Stru	= "RFC_SPAGPA"	,	Name = "PARVAL"	)]		public	int SPADat_Val	{ get; set;	}

				#endregion
				//.................................................
				#region "BDC Data"

					[SAPFncAttribute(Stru	= "BDCDATA"	,	Name = "PROGRAM"	)]			public	int BDCDat_Prg	{ get; set;	}
					[SAPFncAttribute(Stru	= "BDCDATA"	,	Name = "DYNPRO"		)]			public	int BDCDat_Dyn	{ get; set;	}
					[SAPFncAttribute(Stru	= "BDCDATA"	,	Name = "DYNBEGIN"	)]			public	int BDCDat_Bgn	{ get; set;	}
					[SAPFncAttribute(Stru	= "BDCDATA"	,	Name = "FNAM"			)]			public	int BDCDat_Fld	{ get; set;	}
					[SAPFncAttribute(Stru	= "BDCDATA"	,	Name = "FVAL"			)]			public	int BDCDat_Val	{ get; set;	}

				#endregion
				//.................................................
				#region "Messages"

					[SAPFncAttribute(Stru	= "BDCMSGCOLL"	,	Name = "TCODE"		)]	public	int TabMsg_TCode	{ get; set;	}
					[SAPFncAttribute(Stru	= "BDCMSGCOLL"	,	Name = "DYNAME"		)]	public	int TabMsg_DynNm	{ get; set;	}
					[SAPFncAttribute(Stru	= "BDCMSGCOLL"	,	Name = "DYNUMB"		)]	public	int TabMsg_DynNo	{ get; set;	}
					[SAPFncAttribute(Stru	= "BDCMSGCOLL"	,	Name = "MSGTYP"		)]	public	int TabMsg_MsgTp	{ get; set;	}
					[SAPFncAttribute(Stru	= "BDCMSGCOLL"	,	Name = "MSGSPRA"	)]	public	int TabMsg_Lang		{ get; set;	}
					[SAPFncAttribute(Stru	= "BDCMSGCOLL"	,	Name = "MSGID"		)]	public	int TabMsg_MsgID	{ get; set;	}
					[SAPFncAttribute(Stru	= "BDCMSGCOLL"	,	Name = "MSGNR"		)]	public	int TabMsg_MsgNo	{ get; set;	}
					[SAPFncAttribute(Stru	= "BDCMSGCOLL"	,	Name = "MSGV1"		)]	public	int TabMsg_MsgV1	{ get; set;	}
					[SAPFncAttribute(Stru	= "BDCMSGCOLL"	,	Name = "MSGV2"		)]	public	int TabMsg_MsgV2	{ get; set;	}
					[SAPFncAttribute(Stru	= "BDCMSGCOLL"	,	Name = "MSGV3"		)]	public	int TabMsg_MsgV3	{ get; set;	}
					[SAPFncAttribute(Stru	= "BDCMSGCOLL"	,	Name = "MSGV4"		)]	public	int TabMsg_MsgV4	{ get; set;	}
					[SAPFncAttribute(Stru	= "BDCMSGCOLL"	,	Name = "ENV"			)]	public	int TabMsg_Envir	{ get; set;	}
					[SAPFncAttribute(Stru	= "BDCMSGCOLL"	,	Name = "FLDNAME"	)]	public	int TabMsg_Fldnm	{ get; set;	}

				#endregion

			#endregion

		}
}
