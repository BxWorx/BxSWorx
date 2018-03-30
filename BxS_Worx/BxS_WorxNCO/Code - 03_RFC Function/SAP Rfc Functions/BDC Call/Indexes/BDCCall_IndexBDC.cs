using BxS_WorxNCO.RfcFunction.Main;
//•••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••
namespace BxS_WorxNCO.RfcFunction.BDCTran
{
	[	SAP( Name = "BDCDATA" ) ]

	internal class BDCCall_IndexBDC
		{
			#region "Properties"

				[ SAP(	Name = "PROGRAM"	) ]		public	int Prg	{ get; set;	}
				[ SAP(	Name = "DYNPRO"		) ]		public	int Dyn	{ get; set;	}
				[ SAP(	Name = "DYNBEGIN"	) ]		public	int Bgn	{ get; set;	}
				[ SAP(	Name = "FNAM"			) ]		public	int Fld	{ get; set;	}
				[ SAP(	Name = "FVAL"			) ]		public	int Val	{ get; set;	}

			#endregion

		}
}
