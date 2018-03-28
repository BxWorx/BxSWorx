using BxS_WorxNCO.RfcFunction.Main;
//•••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••
namespace BxS_WorxNCO.RfcFunction.BDCTran
{
	[ SAP( Name = "BDCMSGCOLL" ) ]

	internal class BDCCall_IndexMSG
		{
			#region "Properties"

				[	SAP( Name = "TCODE"		)	]		public	int TabMsg_TCode	{ get; set;	}
				[	SAP( Name = "DYNAME"	)	]		public	int TabMsg_DynNm	{ get; set;	}
				[	SAP( Name = "DYNUMB"	)	]		public	int TabMsg_DynNo	{ get; set;	}
				[	SAP( Name = "MSGTYP"	)	]		public	int TabMsg_MsgTp	{ get; set;	}
				[	SAP( Name = "MSGSPRA"	)	]		public	int TabMsg_Lang		{ get; set;	}
				[	SAP( Name = "MSGID"		)	]		public	int TabMsg_MsgID	{ get; set;	}
				[	SAP( Name = "MSGNR"		)	]		public	int TabMsg_MsgNo	{ get; set;	}
				[	SAP( Name = "MSGV1"		)	]		public	int TabMsg_MsgV1	{ get; set;	}
				[	SAP( Name = "MSGV2"		)	]		public	int TabMsg_MsgV2	{ get; set;	}
				[	SAP( Name = "MSGV3"		)	]		public	int TabMsg_MsgV3	{ get; set;	}
				[	SAP( Name = "MSGV4"		)	]		public	int TabMsg_MsgV4	{ get; set;	}
				[	SAP( Name = "ENV"			)	]		public	int TabMsg_Envir	{ get; set;	}
				[	SAP( Name = "FLDNAME"	)	]		public	int TabMsg_Fldnm	{ get; set;	}

			#endregion

		}
}
