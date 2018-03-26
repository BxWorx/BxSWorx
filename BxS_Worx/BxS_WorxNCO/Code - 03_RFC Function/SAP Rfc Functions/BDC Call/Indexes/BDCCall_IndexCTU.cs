using BxS_WorxNCO.RfcFunction.Main;
//•••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••
namespace BxS_WorxNCO.RfcFunction.BDCTran
{
	[ SAP( Name = "CTU_PARAMS" ) ]

	internal class BDCCall_IndexCTU
		{
			#region "Properties"

				[	SAP(	Name = "DISMODE"	)	]		internal	int CTUOpt_DspMde	{ get; set;	}
				[	SAP(	Name = "UPDMODE"	)	]		internal	int CTUOpt_UpdMde	{ get; set;	}
				[	SAP(	Name = "CATTMODE"	)	]		internal	int CTUOpt_CATMde	{ get; set;	}
				[	SAP(	Name = "DEFSIZE"	)	]		internal	int CTUOpt_DefSze	{ get; set;	}
				[	SAP(	Name = "RACOMMIT"	)	]		internal	int CTUOpt_NoComm	{ get; set;	}
				[	SAP(	Name = "NOBINPT"	)	]		internal	int CTUOpt_NoBtcI	{ get; set;	}
				[	SAP(	Name = "NOBIEND"	)	]		internal	int CTUOpt_NoBtcE	{ get; set;	}

			#endregion

		}
}
