using BxS_WorxNCO.RfcFunction.Main;
//•••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••
namespace BxS_WorxNCO.RfcFunction.BDCTran
{
	[ SAP( Name = "BDCMSGCOLL" ) ]

	internal class BDCCall_IndexMSG
		{
			#region "Properties"

				[	SAP( Name = "TCODE"		)	]		public	int TCode	{ get; set;	}
				[	SAP( Name = "DYNAME"	)	]		public	int DynNm	{ get; set;	}
				[	SAP( Name = "DYNUMB"	)	]		public	int DynNo	{ get; set;	}
				[	SAP( Name = "MSGTYP"	)	]		public	int MsgTp	{ get; set;	}
				[	SAP( Name = "MSGSPRA"	)	]		public	int Lang	{ get; set;	}
				[	SAP( Name = "MSGID"		)	]		public	int MsgID	{ get; set;	}
				[	SAP( Name = "MSGNR"		)	]		public	int MsgNo	{ get; set;	}
				[	SAP( Name = "MSGV1"		)	]		public	int MsgV1	{ get; set;	}
				[	SAP( Name = "MSGV2"		)	]		public	int MsgV2	{ get; set;	}
				[	SAP( Name = "MSGV3"		)	]		public	int MsgV3	{ get; set;	}
				[	SAP( Name = "MSGV4"		)	]		public	int MsgV4	{ get; set;	}
				[	SAP( Name = "ENV"			)	]		public	int Envir	{ get; set;	}
				[	SAP( Name = "FLDNAME"	)	]		public	int Fldnm	{ get; set;	}

			#endregion

		}
}
