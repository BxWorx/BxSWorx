using System;
//.........................................................
using SMC	= SAP.Middleware.Connector;
//•••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••
namespace BxS_WorxNCO.RfcFunction.Common
{
	internal interface IRfcFncBase
		{
			#region "Properties"

				Guid		MyID						{	get; }
				//.................................................
				IRfcFncProfile		Profile					{ get; }
				SMC.IRfcFunction	NCORfcFunction	{ get; }

			#endregion

			//===========================================================================================
			#region "Methods: Exposed"

				bool	Invoke();

			#endregion

		}
}
