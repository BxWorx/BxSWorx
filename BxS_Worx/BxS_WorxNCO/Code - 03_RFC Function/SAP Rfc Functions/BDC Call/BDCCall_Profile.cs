using System;
//.........................................................
using BxS_WorxNCO.Main;
using BxS_WorxNCO.Destination.API;
using BxS_WorxNCO.RfcFunction.Main;
//•••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••
namespace BxS_WorxNCO.RfcFunction.BDCTran
{
	internal class BDCCall_Profile : RfcFncProfile
		{
			#region "Function Parameters"

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				internal BDCCall_Profile(		string									fncName
																	,	IRfcDestination					rfcDestination
																	,	Func< BDCCall_Header	>	createHeader
																	, Func< BDCCall_Lines		>	createLines		)	: base(		fncName
																																										, rfcDestination )
					{
						this._CreateHeader	= createHeader	;
						this._CreateLines		= createLines		;
					}

			#endregion

			//===========================================================================================
			#region "Declarations"

				private const	string	cz_StrCTU	= "CTU_PARAMS"	;
				private const	string	cz_StrSPA	= "RFC_SPAGPA"	;
				private const	string	cz_StrBDC	= "BDCDATA"			;
				private const	string	cz_StrMSG	= "BDCMSGCOLL"	;

				private	readonly	Func< BDCCall_Header >	_CreateHeader ;
				private	readonly	Func< BDCCall_Lines	>		_CreateLines	;

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				internal BDCCall_Header CreateBDCCallHeader( bool withDefaults = true )
					{
						this.ReadyProfile();
						BDCCall_Header lo_Head	= this._CreateHeader();
						//.............................................
						lo_Head.CTUParms	= this._RfcDestination.CreateRfcStructure( cz_StrCTU );

						if ( withDefaults )
							{
								lo_Head.ShowSAPGui	= false;
								lo_Head.Skip1st			= false;

								lo_Head.CTUParms[ this.CTUOpt_DspMde ].SetValue( BDCCall_Constants.lz_CTU_A );
								lo_Head.CTUParms[ this.CTUOpt_UpdMde ].SetValue( BDCCall_Constants.lz_CTU_A	);
								lo_Head.CTUParms[ this.CTUOpt_DefSze ].SetValue( NCO_Constants.cz_False			);
								lo_Head.CTUParms[ this.CTUOpt_CATMde ].SetValue( NCO_Constants.cz_False			);
							}
						//.............................................
						return	lo_Head;
					}

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				internal BDCCall_Lines CreateBDCCallLines()
					{
						this.ReadyProfile();
						BDCCall_Lines lo_Lines	=	this._CreateLines();
						//.............................................
						lo_Lines.SPAData	= this._RfcDestination.CreateRfcTable( cz_StrSPA );
						lo_Lines.BDCData	= this._RfcDestination.CreateRfcTable( cz_StrBDC );
						lo_Lines.MSGData	= this._RfcDestination.CreateRfcTable( cz_StrMSG );
						//.............................................
						return	lo_Lines;
					}

			#endregion

			//===========================================================================================
			#region "Function indicies"

				#region "Function Parameters"

					[SAPFncAttribute(Name = "IF_TCODE"							)]				public	int ParIdx_TCode	{ get; set;	}
					[SAPFncAttribute(Name = "IF_SKIP_FIRST_SCREEN"	)]				public	int ParIdx_Skip1	{ get; set;	}
					[SAPFncAttribute(Name = "IS_OPTIONS"						)]				public	int ParIdx_CTUOpt	{ get; set;	}
					[SAPFncAttribute(Name = "IT_BDCDATA"						)]				public	int ParIdx_TabBDC	{ get; set;	}
					[SAPFncAttribute(Name = "ET_MSG"								)]				public	int	ParIdx_TabMSG	{ get; set;	}
					[SAPFncAttribute(Name = "CT_SETGET_PARAMETER"		)]				public	int ParIdx_TabSPA	{ get; set;	}

				#endregion
				//.................................................
				#region "CTUOptions"

					[SAPFncAttribute(Stru	= cz_StrCTU	,	Name = "DISMODE"	)]	public	int CTUOpt_DspMde	{ get; set;	}
					[SAPFncAttribute(Stru	= cz_StrCTU	,	Name = "UPDMODE"	)]	public	int CTUOpt_UpdMde	{ get; set;	}
					[SAPFncAttribute(Stru	= cz_StrCTU	,	Name = "CATTMODE"	)]	public	int CTUOpt_CATMde	{ get; set;	}
					[SAPFncAttribute(Stru	= cz_StrCTU	,	Name = "DEFSIZE"	)]	public	int CTUOpt_DefSze	{ get; set;	}
					[SAPFncAttribute(Stru	= cz_StrCTU	,	Name = "RACOMMIT"	)]	public	int CTUOpt_NoComm	{ get; set;	}
					[SAPFncAttribute(Stru	= cz_StrCTU	,	Name = "NOBINPT"	)]	public	int CTUOpt_NoBtcI	{ get; set;	}
					[SAPFncAttribute(Stru	= cz_StrCTU	,	Name = "NOBIEND"	)]	public	int CTUOpt_NoBtcE	{ get; set;	}

				#endregion
				//.................................................
				#region "SPA Data"

					[SAPFncAttribute(Stru	= cz_StrSPA	,	Name = "PARID"	)]		public	int SPADat_MID	{ get; set;	}
					[SAPFncAttribute(Stru	= cz_StrSPA	,	Name = "PARVAL"	)]		public	int SPADat_Val	{ get; set;	}

				#endregion
				//.................................................
				#region "BDC Data"

					[SAPFncAttribute(Stru	= cz_StrBDC	,	Name = "PROGRAM"	)]	public	int BDCDat_Prg	{ get; set;	}
					[SAPFncAttribute(Stru	= cz_StrBDC	,	Name = "DYNPRO"		)]	public	int BDCDat_Dyn	{ get; set;	}
					[SAPFncAttribute(Stru	= cz_StrBDC	,	Name = "DYNBEGIN"	)]	public	int BDCDat_Bgn	{ get; set;	}
					[SAPFncAttribute(Stru	= cz_StrBDC	,	Name = "FNAM"			)]	public	int BDCDat_Fld	{ get; set;	}
					[SAPFncAttribute(Stru	= cz_StrBDC	,	Name = "FVAL"			)]	public	int BDCDat_Val	{ get; set;	}

				#endregion
				//.................................................
				#region "Messages"

					[SAPFncAttribute(Stru	= cz_StrMSG	,	Name = "TCODE"		)]	public	int TabMsg_TCode	{ get; set;	}
					[SAPFncAttribute(Stru	= cz_StrMSG	,	Name = "DYNAME"		)]	public	int TabMsg_DynNm	{ get; set;	}
					[SAPFncAttribute(Stru	= cz_StrMSG	,	Name = "DYNUMB"		)]	public	int TabMsg_DynNo	{ get; set;	}
					[SAPFncAttribute(Stru	= cz_StrMSG	,	Name = "MSGTYP"		)]	public	int TabMsg_MsgTp	{ get; set;	}
					[SAPFncAttribute(Stru	= cz_StrMSG	,	Name = "MSGSPRA"	)]	public	int TabMsg_Lang		{ get; set;	}
					[SAPFncAttribute(Stru	= cz_StrMSG	,	Name = "MSGID"		)]	public	int TabMsg_MsgID	{ get; set;	}
					[SAPFncAttribute(Stru	= cz_StrMSG	,	Name = "MSGNR"		)]	public	int TabMsg_MsgNo	{ get; set;	}
					[SAPFncAttribute(Stru	= cz_StrMSG	,	Name = "MSGV1"		)]	public	int TabMsg_MsgV1	{ get; set;	}
					[SAPFncAttribute(Stru	= cz_StrMSG	,	Name = "MSGV2"		)]	public	int TabMsg_MsgV2	{ get; set;	}
					[SAPFncAttribute(Stru	= cz_StrMSG	,	Name = "MSGV3"		)]	public	int TabMsg_MsgV3	{ get; set;	}
					[SAPFncAttribute(Stru	= cz_StrMSG	,	Name = "MSGV4"		)]	public	int TabMsg_MsgV4	{ get; set;	}
					[SAPFncAttribute(Stru	= cz_StrMSG	,	Name = "ENV"			)]	public	int TabMsg_Envir	{ get; set;	}
					[SAPFncAttribute(Stru	= cz_StrMSG	,	Name = "FLDNAME"	)]	public	int TabMsg_Fldnm	{ get; set;	}

				#endregion

			#endregion

		}
}
