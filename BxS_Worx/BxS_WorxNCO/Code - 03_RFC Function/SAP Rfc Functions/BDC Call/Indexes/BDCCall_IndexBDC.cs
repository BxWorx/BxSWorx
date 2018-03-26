using BxS_WorxNCO.RfcFunction.Main;
//•••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••
namespace BxS_WorxNCO.RfcFunction.BDCTran
{
	[	SAP( Name = "BDCDATA" ) ]

	internal class BDCCall_IndexBDC
		{
			#region "Properties"

				[ SAP(	Name = "PROGRAM"	) ]		internal	int BDCDat_Prg	{ get; set;	}
				[ SAP(	Name = "DYNPRO"		) ]		internal	int BDCDat_Dyn	{ get; set;	}
				[ SAP(	Name = "DYNBEGIN"	) ]		internal	int BDCDat_Bgn	{ get; set;	}
				[ SAP(	Name = "FNAM"			) ]		internal	int BDCDat_Fld	{ get; set;	}
				[ SAP(	Name = "FVAL"			) ]		internal	int BDCDat_Val	{ get; set;	}

			#endregion

		}
}
