﻿//•••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••
namespace BxS_SAPNCO.BDCProcess
{
	internal class BDCCallTranIndex
		{
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

		}
}
