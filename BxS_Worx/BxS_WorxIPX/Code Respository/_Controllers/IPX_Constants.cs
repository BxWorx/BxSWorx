using System.Threading;
//•••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••
namespace BxS_WorxIPX.Main
{
	internal static class IPX_Constants
		{
			#region "Declarations"

				//.................................................
				// Lazy Setup
				//.................................................
				internal	const LazyThreadSafetyMode	cz_LM		= LazyThreadSafetyMode.ExecutionAndPublication;

				//.................................................
				// Standard
				//.................................................
				internal	const string	cz_True		= "X"	;
				internal	const string	cz_False	= " "	;
				internal	const string	cz_Null		= ""	;

				//.................................................
				// CTU setup
				//.................................................
				internal	const char		cz_CTU_A		= 'A'	;
				internal	const char		cz_CTU_N		= 'N'	;

			#endregion

		}
}
