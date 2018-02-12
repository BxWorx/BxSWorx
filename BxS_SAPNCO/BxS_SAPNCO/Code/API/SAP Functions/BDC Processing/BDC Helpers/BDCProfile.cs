using SMC	= SAP.Middleware.Connector;
//.........................................................
using BxS_SAPNCO.Destination;
using BxS_SAPNCO.RfcFunction;
//•••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••
namespace BxS_SAPNCO.BDCProcess
{
	internal class BDCFncProfile	:	RfcFncProfileBase	 ,
																	IBDCProfile
		{
			#region "Constructors"

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				internal BDCFncProfile(	DestinationRfc	destination	,
																string					rfcFncName		)	: base( destination, rfcFncName)
					{
					}

			#endregion

			//===========================================================================================
			#region "Properties:  General"

				public	bool	Ready { get; set;	}

				public	SMC.IRfcFunction		RFCFnc	{	get	{ return	this.Metadata.CreateFunction()																												; } }
				public	SMC.IRfcStructure		CTUStr	{	get	{ return	this.Metadata[this.ParIdx_CTUOpt].ValueMetadataAsStructureMetadata.CreateStructure()	; } }
				public	SMC.IRfcTable				BDCTbl	{	get	{ return	this.Metadata[this.ParIdx_TabBDC].ValueMetadataAsTableMetadata.CreateTable()					; } }
				public	SMC.IRfcTable				SPATbl	{	get	{ return	this.Metadata[this.ParIdx_TabSPA].ValueMetadataAsTableMetadata.CreateTable()					; } }
				public	SMC.IRfcTable				MSGTbl	{	get	{ return	this.Metadata[this.ParIdx_TabMSG].ValueMetadataAsTableMetadata.CreateTable()					; } }

			#endregion

			//===========================================================================================
			#region "Properties:  Indicies"

				#region "Function Parameters"

					public	int ParIdx_TCode	{ get; set;	}
					public	int ParIdx_Skip1	{ get; set;	}
					public	int ParIdx_CTUOpt	{ get; set;	}
					public	int ParIdx_TabBDC	{ get; set;	}
					public	int	ParIdx_TabMSG	{ get; set;	}
					public	int ParIdx_TabSPA	{ get; set;	}

				#endregion
				//.................................................
				#region "CTUOptions"

					public	int CTUOpt_DspMde	{ get; set;	}
					public	int CTUOpt_UpdMde	{ get; set;	}
					public	int CTUOpt_CATMde	{ get; set;	}
					public	int CTUOpt_DefSze	{ get; set;	}
					public	int CTUOpt_NoComm	{ get; set;	}
					public	int CTUOpt_NoBtcI	{ get; set;	}
					public	int CTUOpt_NoBtcE	{ get; set;	}

				#endregion
				//.................................................
				#region "SPA Data"

					public	int SPADat_MID	{ get; set;	}
					public	int SPADat_Val	{ get; set;	}

				#endregion
				//.................................................
				#region "BDC Data"

					public	int BDCDat_Prg	{ get; set;	}
					public	int BDCDat_Dyn	{ get; set;	}
					public	int BDCDat_Bgn	{ get; set;	}
					public	int BDCDat_Fld	{ get; set;	}
					public	int BDCDat_Val	{ get; set;	}

				#endregion
				//.................................................
				#region "Messages"

					public	int TabMsg_TCode	{ get; set;	}
					public	int TabMsg_DynNm	{ get; set;	}
					public	int TabMsg_DynNo	{ get; set;	}
					public	int TabMsg_MsgTp	{ get; set;	}
					public	int TabMsg_Lang		{ get; set;	}
					public	int TabMsg_MsgID	{ get; set;	}
					public	int TabMsg_MsgNo	{ get; set;	}
					public	int TabMsg_MsgV1	{ get; set;	}
					public	int TabMsg_MsgV2	{ get; set;	}
					public	int TabMsg_MsgV3	{ get; set;	}
					public	int TabMsg_MsgV4	{ get; set;	}
					public	int TabMsg_Envir	{ get; set;	}
					public	int TabMsg_Fldnm	{ get; set;	}

				#endregion

			#endregion

			//===========================================================================================
			#region "Methods: Exposed"

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				public void Configure(DTO_RFCHeader	DTO)
					{
						DTO.CTUParms	= this.CTUStr;
					}

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				public void Configure(DTO_RFCTran DTO)
					{
						DTO.BDCData	= this.BDCTbl;
						DTO.SPAData	= this.SPATbl;
						DTO.MSGData	= this.MSGTbl;
					}

			#endregion

		}
}
