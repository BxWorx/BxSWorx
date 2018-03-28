using BxS_WorxNCO.RfcFunction.Main;
//•••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••
namespace BxS_WorxNCO.RfcFunction.BDCTran
{
	[	SAP( Name = "BDCDATA" ) ]

	internal class BDCCall_IndexBDC
		{
			#region "Properties"

				[ SAP(	Name = "PROGRAM"	) ]		public	int BDCDat_Prg	{ get; set;	}
				[ SAP(	Name = "DYNPRO"		) ]		public	int BDCDat_Dyn	{ get; set;	}
				[ SAP(	Name = "DYNBEGIN"	) ]		public	int BDCDat_Bgn	{ get; set;	}
				[ SAP(	Name = "FNAM"			) ]		public	int BDCDat_Fld	{ get; set;	}
				[ SAP(	Name = "FVAL"			) ]		public	int BDCDat_Val	{ get; set;	}

			#endregion

		}
}
