using BxS_WorxNCO.RfcFunction.Main;

using		static	BxS_WorxNCO.RfcFunction.Main.SAPRfcFncConstants;
//•••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••
namespace BxS_WorxNCO.RfcFunction.SAPMsg
{
	[	SAP(	Name = cz_SAPMsgCompiler	)	]

	internal class SAPMsg_IndexFNC
		{
			#region "Properties"

				[ SAP(	Name = "LANGUAGE"				)	]		public	int ParIdx_Langu	{ get; set;	}
				[ SAP(	Name = "MESSAGE_ID"			)	]		public	int ParIdx_MsgID	{ get; set;	}
				[ SAP(	Name = "MESSAGE_NUMBER"	)	]		public	int ParIdx_MsgNo	{ get; set;	}
				[ SAP(	Name = "MESSAGE_VAR1"		)	]		public	int ParIdx_MsgV1	{ get; set;	}
				[ SAP(	Name = "MESSAGE_VAR2"		)	]		public	int ParIdx_MsgV2	{ get; set;	}
				[ SAP(	Name = "MESSAGE_VAR3"		)	]		public	int ParIdx_MsgV3	{ get; set;	}
				[ SAP(	Name = "MESSAGE_VAR4"		)	]		public	int ParIdx_MsgV4	{ get; set;	}
				[ SAP(	Name = "MESSAGE_TEXT"		)	]		public	int ParIdx_MsgST	{ get; set;	}
				[ SAP(	Name = "LONGTEXT"				)	]		public	int ParIdx_MsgLT	{ get; set;	}

			#endregion

		}
}
