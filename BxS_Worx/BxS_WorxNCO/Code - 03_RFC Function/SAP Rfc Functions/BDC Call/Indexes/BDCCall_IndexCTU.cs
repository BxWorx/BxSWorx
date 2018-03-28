using BxS_WorxNCO.RfcFunction.Main;
//•••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••
namespace BxS_WorxNCO.RfcFunction.BDCTran
{
	[ SAP( Name = "CTU_PARAMS" ) ]

	internal class BDCCall_IndexCTU
		{
			#region "Properties"

				[	SAP(	Name = "DISMODE"	)	]		public	int CTUOpt_DspMde	{ get; set;	}
				[	SAP(	Name = "UPDMODE"	)	]		public	int CTUOpt_UpdMde	{ get; set;	}
				[	SAP(	Name = "CATTMODE"	)	]		public	int CTUOpt_CATMde	{ get; set;	}
				[	SAP(	Name = "DEFSIZE"	)	]		public	int CTUOpt_DefSze	{ get; set;	}
				[	SAP(	Name = "RACOMMIT"	)	]		public	int CTUOpt_NoComm	{ get; set;	}
				[	SAP(	Name = "NOBINPT"	)	]		public	int CTUOpt_NoBtcI	{ get; set;	}
				[	SAP(	Name = "NOBIEND"	)	]		public	int CTUOpt_NoBtcE	{ get; set;	}

			#endregion

		}
}
