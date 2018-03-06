using BxS_WorxNCO.RfcFunction.Common;
//•••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••
namespace BxS_WorxNCO.BDCCall
{
	internal class BDCCallParmIndex
		{
			#region "Function Parameters"

				[SAPFncParmNameAttribute(SAPName = "IF_TCODE"							)]	public	int ParIdx_TCode	{ get; set;	}
				[SAPFncParmNameAttribute(SAPName = "IF_SKIP_FIRST_SCREEN"	)]	public	int ParIdx_Skip1	{ get; set;	}
				[SAPFncParmNameAttribute(SAPName = "IS_OPTIONS"						)]	public	int ParIdx_CTUOpt	{ get; set;	}
				[SAPFncParmNameAttribute(SAPName = "IT_BDCDATA"						)]	public	int ParIdx_TabBDC	{ get; set;	}
				[SAPFncParmNameAttribute(SAPName = "ET_MSG"								)]	public	int	ParIdx_TabMSG	{ get; set;	}
				[SAPFncParmNameAttribute(SAPName = "CT_SETGET_PARAMETER"	)]	public	int ParIdx_TabSPA	{ get; set;	}

			#endregion

		}
}
