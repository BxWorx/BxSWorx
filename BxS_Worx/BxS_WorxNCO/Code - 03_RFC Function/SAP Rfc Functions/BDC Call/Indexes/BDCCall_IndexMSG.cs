using BxS_WorxNCO.RfcFunction.Main;
//•••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••
namespace BxS_WorxNCO.RfcFunction.BDCTran
{
	[ SAP( Name = "BDCMSGCOLL" ) ]

	internal class BDCCall_IndexMSG
		{
			#region "Properties"

				[	SAP( Name = "TCODE"		)	]		internal	int TabMsg_TCode	{ get; set;	}
				[	SAP( Name = "DYNAME"	)	]		internal	int TabMsg_DynNm	{ get; set;	}
				[	SAP( Name = "DYNUMB"	)	]		internal	int TabMsg_DynNo	{ get; set;	}
				[	SAP( Name = "MSGTYP"	)	]		internal	int TabMsg_MsgTp	{ get; set;	}
				[	SAP( Name = "MSGSPRA"	)	]		internal	int TabMsg_Lang		{ get; set;	}
				[	SAP( Name = "MSGID"		)	]		internal	int TabMsg_MsgID	{ get; set;	}
				[	SAP( Name = "MSGNR"		)	]		internal	int TabMsg_MsgNo	{ get; set;	}
				[	SAP( Name = "MSGV1"		)	]		internal	int TabMsg_MsgV1	{ get; set;	}
				[	SAP( Name = "MSGV2"		)	]		internal	int TabMsg_MsgV2	{ get; set;	}
				[	SAP( Name = "MSGV3"		)	]		internal	int TabMsg_MsgV3	{ get; set;	}
				[	SAP( Name = "MSGV4"		)	]		internal	int TabMsg_MsgV4	{ get; set;	}
				[	SAP( Name = "ENV"			)	]		internal	int TabMsg_Envir	{ get; set;	}
				[	SAP( Name = "FLDNAME"	)	]		internal	int TabMsg_Fldnm	{ get; set;	}

			#endregion

		}
}
