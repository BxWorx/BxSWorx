using BxS_WorxNCO.RfcFunction.Main;

using		static	BxS_WorxNCO.RfcFunction.Main.SAPRfcFncConstants;
//•••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••
namespace BxS_WorxNCO.RfcFunction.SAPMsg
{
	[	SAP(	Name = cz_SAPMsgCompiler	)	]

	internal class SAPMsg_IndexFNC
		{
			#region "Properties"

				[ SAP(	Name = "LANGUAGE"				)	]		public	int Langu	{ get; set;	}
				[ SAP(	Name = "MESSAGE_ID"			)	]		public	int MsgID	{ get; set;	}
				[ SAP(	Name = "MESSAGE_NUMBER"	)	]		public	int MsgNo	{ get; set;	}
				[ SAP(	Name = "MESSAGE_VAR1"		)	]		public	int MsgV1	{ get; set;	}
				[ SAP(	Name = "MESSAGE_VAR2"		)	]		public	int MsgV2	{ get; set;	}
				[ SAP(	Name = "MESSAGE_VAR3"		)	]		public	int MsgV3	{ get; set;	}
				[ SAP(	Name = "MESSAGE_VAR4"		)	]		public	int MsgV4	{ get; set;	}
				[ SAP(	Name = "MESSAGE_TEXT"		)	]		public	int MsgST	{ get; set;	}
				[ SAP(	Name = "LONGTEXT"				)	]		public	int MsgLT	{ get; set;	}

			#endregion

		}
}
