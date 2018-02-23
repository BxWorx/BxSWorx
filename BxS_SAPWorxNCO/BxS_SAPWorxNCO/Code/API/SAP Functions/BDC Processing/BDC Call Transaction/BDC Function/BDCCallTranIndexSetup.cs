using SMC	= SAP.Middleware.Connector;
//•••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••
namespace BxS_SAPNCO.BDCProcess
{
	internal class BDCCallTranIndexSetup
		{
			#region "Constructors"

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				internal BDCCallTranIndexSetup( SMC.RfcFunctionMetadata fncMetadata )
					{
						this._FncMetadata		= fncMetadata;
					}

			#endregion

			//===========================================================================================
			#region "Declarations"

				private readonly SMC.RfcFunctionMetadata	_FncMetadata;

			#endregion

			//===========================================================================================
			#region "Methods: Exposed"

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				public bool Configure( BDCCallTranIndex indexer )
					{
						bool	lb_Ret	= true;
						SMC.RfcStructureMetadata	ls_StruMetadata;
						//.............................................
						indexer.ParIdx_TCode	= this._FncMetadata.TryNameToIndex( "IF_TCODE"							);
						indexer.ParIdx_Skip1	= this._FncMetadata.TryNameToIndex( "IF_SKIP_FIRST_SCREEN"	);
						indexer.ParIdx_TabBDC	= this._FncMetadata.TryNameToIndex( "IT_BDCDATA"						);
						indexer.ParIdx_CTUOpt	= this._FncMetadata.TryNameToIndex( "IS_OPTIONS"						);
						indexer.ParIdx_TabMSG	= this._FncMetadata.TryNameToIndex( "ET_MSG"								);
						indexer.ParIdx_TabSPA	= this._FncMetadata.TryNameToIndex( "CT_SETGET_PARAMETER"	);
						//.............................................
						ls_StruMetadata = this._FncMetadata[indexer.ParIdx_CTUOpt].ValueMetadataAsStructureMetadata;

						if (ls_StruMetadata == null)
							{	lb_Ret	= false; }
						else
							{
								indexer.CTUOpt_DspMde = ls_StruMetadata.TryNameToIndex( "DISMODE"		);
								indexer.CTUOpt_UpdMde = ls_StruMetadata.TryNameToIndex( "UPDMODE"		);
								indexer.CTUOpt_CATMde = ls_StruMetadata.TryNameToIndex( "CATTMODE"	);
								indexer.CTUOpt_DefSze = ls_StruMetadata.TryNameToIndex( "DEFSIZE"		);
								indexer.CTUOpt_NoComm = ls_StruMetadata.TryNameToIndex( "RACOMMIT"	);
								indexer.CTUOpt_NoBtcI = ls_StruMetadata.TryNameToIndex( "NOBINPT"		);
								indexer.CTUOpt_NoBtcE = ls_StruMetadata.TryNameToIndex( "NOBIEND"		);
							}
						//.............................................
						ls_StruMetadata = this._FncMetadata[indexer.ParIdx_TabSPA].ValueMetadataAsTableMetadata.LineType;

						if (ls_StruMetadata == null)
							{	lb_Ret	= false; }
						else
							{
								indexer.SPADat_MID	= ls_StruMetadata.TryNameToIndex( "PARID"		);
								indexer.SPADat_Val	= ls_StruMetadata.TryNameToIndex( "PARVAL"	);
							}
						//.............................................
						ls_StruMetadata = this._FncMetadata[indexer.ParIdx_TabBDC].ValueMetadataAsTableMetadata.LineType;

						if (ls_StruMetadata == null)
							{	lb_Ret	= false; }
						else
							{
								indexer.BDCDat_Prg	= ls_StruMetadata.TryNameToIndex( "PROGRAM"		);
								indexer.BDCDat_Dyn	= ls_StruMetadata.TryNameToIndex( "DYNPRO"		);
								indexer.BDCDat_Bgn	= ls_StruMetadata.TryNameToIndex( "DYNBEGIN"  );
								indexer.BDCDat_Fld	= ls_StruMetadata.TryNameToIndex( "FNAM"			);
								indexer.BDCDat_Val	= ls_StruMetadata.TryNameToIndex( "FVAL"			);
							}
						//.............................................
						ls_StruMetadata = this._FncMetadata[indexer.ParIdx_TabMSG].ValueMetadataAsTableMetadata.LineType;

						if (ls_StruMetadata == null)
							{	lb_Ret	= false; }
						else
							{
								indexer.TabMsg_TCode	= ls_StruMetadata.TryNameToIndex( "TCODE"		);
								indexer.TabMsg_DynNm	= ls_StruMetadata.TryNameToIndex( "DYNAME"	);
								indexer.TabMsg_DynNo	= ls_StruMetadata.TryNameToIndex( "DYNUMB"	);
								indexer.TabMsg_MsgTp	= ls_StruMetadata.TryNameToIndex( "MSGTYP"	);
								indexer.TabMsg_Lang		= ls_StruMetadata.TryNameToIndex( "MSGSPRA"	);
								indexer.TabMsg_MsgID	= ls_StruMetadata.TryNameToIndex( "MSGID"		);
								indexer.TabMsg_MsgNo	= ls_StruMetadata.TryNameToIndex( "MSGNR"		);
								indexer.TabMsg_MsgV1	= ls_StruMetadata.TryNameToIndex( "MSGV1"		);
								indexer.TabMsg_MsgV2	= ls_StruMetadata.TryNameToIndex( "MSGV2"		);
								indexer.TabMsg_MsgV3	= ls_StruMetadata.TryNameToIndex( "MSGV3"		);
								indexer.TabMsg_MsgV4	= ls_StruMetadata.TryNameToIndex( "MSGV4"		);
								indexer.TabMsg_Envir	= ls_StruMetadata.TryNameToIndex( "ENV"			);
								indexer.TabMsg_Fldnm	= ls_StruMetadata.TryNameToIndex( "FLDNAME"	);
							}
						//.............................................
						return	lb_Ret;
					}

			#endregion

		}
}
