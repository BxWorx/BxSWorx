using BxS_WorxNCO.RfcFunction.Main;
//•••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••
namespace BxS_WorxNCO.RfcFunction.BDCTran
{
	[ SAP( Name = "RFC_SPAGPA" ) ]

	internal class BDCCall_IndexSPA
		{
			#region "Properties"

				[ SAP( Name = "PARID"		) ]		public	int SPADat_MID	{ get; set;	}
				[ SAP( Name = "PARVAL"	) ]		public	int SPADat_Val	{ get; set;	}

			#endregion

		}
}
