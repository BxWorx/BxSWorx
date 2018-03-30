using BxS_WorxNCO.RfcFunction.Main;

using		static	BxS_WorxNCO.RfcFunction.Main.SAPRfcFncConstants;
//•••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••
namespace BxS_WorxNCO.RfcFunction.BDCTran
{
//	[	SAP(	Name = cz_BDCCallTran	)	]

	internal class BDCCall_IndexFNC
		{
			#region "Properties"

				[ SAP(	Name = "IF_TCODE"							)	]		public	int TCode		{ get; set;	}
				[ SAP(	Name = "IF_SKIP_FIRST_SCREEN"	)	]		public	int Skip1		{ get; set;	}
				[ SAP(	Name = "IS_OPTIONS"						)	]		public	int CTUOpt	{ get; set;	}
				[ SAP(	Name = "IT_BDCDATA"						)	]		public	int TabBDC	{ get; set;	}
				[ SAP(	Name = "ET_MSG"								)	]		public	int	TabMSG	{ get; set;	}
				[ SAP(	Name = "CT_SETGET_PARAMETER"	)	]		public	int TabSPA	{ get; set;	}

			#endregion

		}
}
