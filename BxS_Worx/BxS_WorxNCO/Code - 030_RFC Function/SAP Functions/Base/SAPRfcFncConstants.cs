﻿//•••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••
namespace BxS_WorxNCO.RfcFunction.Main
{
	internal static class SAPRfcFncConstants
		{
			#region "Declarations: Function Names"

				internal	const	string	cz_TableReader			= "/BODS/RFC_READ_TABLE2"			;
				internal	const	string	cz_BDCAlt						= "/ISDFPS/CALL_TRANSACTION"	;
				internal	const	string	cz_BDCStd						= "ABAP4_CALL_TRANSACTION"		;
				internal	const	string	cz_SAPMsgCompiler		= "RPY_MESSAGE_COMPOSE"				;
				internal	const	string	cz_DDICInfo					= "DDIF_FIELDINFO_GET"				;			//ISDFPS/DD_FIELDINFO_GET (gets to muvh info)

			#endregion

		}
}
