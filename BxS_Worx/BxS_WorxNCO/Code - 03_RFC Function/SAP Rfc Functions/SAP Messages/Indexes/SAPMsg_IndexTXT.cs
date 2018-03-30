﻿using BxS_WorxNCO.RfcFunction.Main;
//•••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••
namespace BxS_WorxNCO.RfcFunction.SAPMsg
{
	[ SAP( Name = "TLINE" ) ]

	internal class SAPMsg_IndexTXT
		{
			#region "Properties"

				[ SAP( Name = "TDFORMAT"	) ]		public	int Fmt		{ get; set;	}
				[ SAP( Name = "TDLINE"		) ]		public	int Lne		{ get; set;	}

			#endregion

		}
}
