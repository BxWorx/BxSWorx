using BxS_WorxNCO.RfcFunction.Main;
//•••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••
namespace BxS_WorxNCO.RfcFunction.BDCTran
{
	[	SAP(	Name = "/ISDFPS/CALL_TRANSACTION"	)	]

	internal class BDCCall_IndexFNC
		{
			#region "Properties"

				[ SAP(	Name = "IF_TCODE"							)	]		internal	int ParIdx_TCode	{ get; set;	}
				[ SAP(	Name = "IF_SKIP_FIRST_SCREEN"	)	]		internal	int ParIdx_Skip1	{ get; set;	}
				[ SAP(	Name = "IS_OPTIONS"						)	]		internal	int ParIdx_CTUOpt	{ get; set;	}
				[ SAP(	Name = "IT_BDCDATA"						)	]		internal	int ParIdx_TabBDC	{ get; set;	}
				[ SAP(	Name = "ET_MSG"								)	]		internal	int	ParIdx_TabMSG	{ get; set;	}
				[ SAP(	Name = "CT_SETGET_PARAMETER"	)	]		internal	int ParIdx_TabSPA	{ get; set;	}

			#endregion

		}
}
