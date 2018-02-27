using System;
//.........................................................
using					BxS_SAPBDC.BDC;
using static	BxS_SAPBDC.BDC.BDC_Constants;
//•••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••
namespace BxS_SAPBDC.Parser
{
	internal class BDC_Processor_Session
		{
			#region "Constructors"

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				internal	BDC_Processor_Session(	Lazy< BDC_Processor_Factory > factory )
					{
						this._Factory	= factory;
					}

			#endregion

			//===========================================================================================
			#region "Declaration"

				private	readonly Lazy< BDC_Processor_Factory > 	_Factory;

			#endregion

			//===========================================================================================
			#region "Methods: Exposed: Columns"

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				internal bool Process( BDC_Session	Session , DTO_BDCProfile dto )
					{
						bool	lb_Ret	= true;
						//.............................................
						return	lb_Ret;
					}

			#endregion

		}
}
