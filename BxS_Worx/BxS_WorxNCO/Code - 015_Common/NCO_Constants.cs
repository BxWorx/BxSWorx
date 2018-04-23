using System.Threading;
//•••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••
namespace BxS_WorxNCO.Main
{
	internal static class NCO_Constants
		{
			#region "Declarations"

				internal	const string	cz_True		= "X"	;
				internal	const string	cz_False	= " "	;
				internal	const string	cz_Null		= ""	;
				//...
				internal	const int			cz_Neg1		= -1	;
				internal	const char		cz_Coma		= ','	;
				//.................................................
				internal	const	string	cz_DefDyn	= "0000"	;			// TO-DO: remove from BDC_Constants
				//.................................................
				internal	const LazyThreadSafetyMode	cz_LM		= LazyThreadSafetyMode.ExecutionAndPublication;

			#endregion

		}
}
