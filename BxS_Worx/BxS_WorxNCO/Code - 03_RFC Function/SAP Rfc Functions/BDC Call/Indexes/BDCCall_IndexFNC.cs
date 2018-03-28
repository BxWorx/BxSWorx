using BxS_WorxNCO.RfcFunction.Main;
//•••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••
namespace BxS_WorxNCO.RfcFunction.BDCTran
{
	[	SAP(	Name = "/ISDFPS/CALL_TRANSACTION"	)	]

	internal class BDCCall_IndexFNC
		{
			#region "Properties"

				[ SAP(	Name = "IF_TCODE"							)	]		public	int ParIdx_TCode	{ get; set;	}
				[ SAP(	Name = "IF_SKIP_FIRST_SCREEN"	)	]		public	int ParIdx_Skip1	{ get; set;	}
				[ SAP(	Name = "IS_OPTIONS"						)	]		public	int ParIdx_CTUOpt	{ get; set;	}
				[ SAP(	Name = "IT_BDCDATA"						)	]		public	int ParIdx_TabBDC	{ get; set;	}
				[ SAP(	Name = "ET_MSG"								)	]		public	int	ParIdx_TabMSG	{ get; set;	}
				[ SAP(	Name = "CT_SETGET_PARAMETER"	)	]		public	int ParIdx_TabSPA	{ get; set;	}

			#endregion

		}
}
