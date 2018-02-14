using SMC	= SAP.Middleware.Connector;
//•••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••
namespace BxS_SAPNCO.BDCProcess
{
	internal class BDCProfileConfigurator
		{
			#region "Methods: Exposed"

				////¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				//public bool Configure( BDCCallTranProfile profile )
				//	{
				//		profile.ParIdx_TCode	= profile.Metadata.TryNameToIndex( "IF_TCODE"							);
				//		profile.ParIdx_Skip1	= profile.Metadata.TryNameToIndex( "IF_SKIP_FIRST_SCREEN"	);
				//		profile.ParIdx_TabBDC	= profile.Metadata.TryNameToIndex( "IT_BDCDATA"						);
				//		profile.ParIdx_CTUOpt	= profile.Metadata.TryNameToIndex( "IS_OPTIONS"						);
				//		profile.ParIdx_TabMSG	= profile.Metadata.TryNameToIndex( "ET_MSG"								);
				//		profile.ParIdx_TabSPA	= profile.Metadata.TryNameToIndex( "CT_SETGET_PARAMETER"	);

				//		return	true;

				//	}

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				public bool Configure( IBDCProfile profile )
					{
						bool	lb_Ret	= true;
						SMC.RfcStructureMetadata	ls_StruMetadata;
						//.............................................
						profile.ParIdx_TCode	= profile.Metadata.TryNameToIndex( "IF_TCODE"							);
						profile.ParIdx_Skip1	= profile.Metadata.TryNameToIndex( "IF_SKIP_FIRST_SCREEN"	);
						profile.ParIdx_TabBDC	= profile.Metadata.TryNameToIndex( "IT_BDCDATA"						);
						profile.ParIdx_CTUOpt	= profile.Metadata.TryNameToIndex( "IS_OPTIONS"						);
						profile.ParIdx_TabMSG	= profile.Metadata.TryNameToIndex( "ET_MSG"								);
						profile.ParIdx_TabSPA	= profile.Metadata.TryNameToIndex( "CT_SETGET_PARAMETER"	);
						//.............................................
						ls_StruMetadata = profile.Metadata[profile.ParIdx_CTUOpt].ValueMetadataAsStructureMetadata;

						if (ls_StruMetadata == null)
							{	lb_Ret	= false; }
						else
							{
								profile.CTUOpt_DspMde = ls_StruMetadata.TryNameToIndex( "DISMODE"		);
								profile.CTUOpt_UpdMde = ls_StruMetadata.TryNameToIndex( "UPDMODE"		);
								profile.CTUOpt_CATMde = ls_StruMetadata.TryNameToIndex( "CATTMODE"	);
								profile.CTUOpt_DefSze = ls_StruMetadata.TryNameToIndex( "DEFSIZE"		);
								profile.CTUOpt_NoComm = ls_StruMetadata.TryNameToIndex( "RACOMMIT"	);
								profile.CTUOpt_NoBtcI = ls_StruMetadata.TryNameToIndex( "NOBINPT"		);
								profile.CTUOpt_NoBtcE = ls_StruMetadata.TryNameToIndex( "NOBIEND"		);
							}
						//.............................................
						ls_StruMetadata = profile.Metadata[profile.ParIdx_TabSPA].ValueMetadataAsTableMetadata.LineType;

						if (ls_StruMetadata == null)
							{	lb_Ret	= false; }
						else
							{
								profile.SPADat_MID	= ls_StruMetadata.TryNameToIndex( "PARID"		);
								profile.SPADat_Val	= ls_StruMetadata.TryNameToIndex( "PARVAL"	);
							}
						//.............................................
						ls_StruMetadata = profile.Metadata[profile.ParIdx_TabBDC].ValueMetadataAsTableMetadata.LineType;

						if (ls_StruMetadata == null)
							{	lb_Ret	= false; }
						else
							{
								profile.BDCDat_Prg	= ls_StruMetadata.TryNameToIndex( "PROGRAM"		);
								profile.BDCDat_Dyn	= ls_StruMetadata.TryNameToIndex( "DYNPRO"		);
								profile.BDCDat_Bgn	= ls_StruMetadata.TryNameToIndex( "DYNBEGIN"  );
								profile.BDCDat_Fld	= ls_StruMetadata.TryNameToIndex( "FNAM"			);
								profile.BDCDat_Val	= ls_StruMetadata.TryNameToIndex( "FVAL"			);
							}
						//.............................................
						ls_StruMetadata = profile.Metadata[profile.ParIdx_TabMSG].ValueMetadataAsTableMetadata.LineType;

						if (ls_StruMetadata == null)
							{	lb_Ret	= false; }
						else
							{
								profile.TabMsg_TCode	= ls_StruMetadata.TryNameToIndex( "TCODE"		);
								profile.TabMsg_DynNm	= ls_StruMetadata.TryNameToIndex( "DYNAME"	);
								profile.TabMsg_DynNo	= ls_StruMetadata.TryNameToIndex( "DYNUMB"	);
								profile.TabMsg_MsgTp	= ls_StruMetadata.TryNameToIndex( "MSGTYP"	);
								profile.TabMsg_Lang		= ls_StruMetadata.TryNameToIndex( "MSGSPRA"	);
								profile.TabMsg_MsgID	= ls_StruMetadata.TryNameToIndex( "MSGID"		);
								profile.TabMsg_MsgNo	= ls_StruMetadata.TryNameToIndex( "MSGNR"		);
								profile.TabMsg_MsgV1	= ls_StruMetadata.TryNameToIndex( "MSGV1"		);
								profile.TabMsg_MsgV2	= ls_StruMetadata.TryNameToIndex( "MSGV2"		);
								profile.TabMsg_MsgV3	= ls_StruMetadata.TryNameToIndex( "MSGV3"		);
								profile.TabMsg_MsgV4	= ls_StruMetadata.TryNameToIndex( "MSGV4"		);
								profile.TabMsg_Envir	= ls_StruMetadata.TryNameToIndex( "ENV"			);
								profile.TabMsg_Fldnm	= ls_StruMetadata.TryNameToIndex( "FLDNAME"	);
							}
						//.............................................
						profile.IsReady	= lb_Ret;

						return	lb_Ret;
					}

			#endregion

		}
}
