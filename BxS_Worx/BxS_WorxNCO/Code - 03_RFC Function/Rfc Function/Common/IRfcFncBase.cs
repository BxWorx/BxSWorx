using System;
//.........................................................
using SMC	= SAP.Middleware.Connector;
//•••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••
namespace BxS_WorxNCO.RfcFunction.Main
{
	public interface IRfcFncBase
		{
			#region "Properties"

				Guid MyID	{	get; }
				//.................................................
				string						SAPFncName			{ get; }
				IRfcFncProfile		Profile					{ get; }
				SMC.IRfcFunction	NCORfcFunction	{ get; set; }

			#endregion

			//===========================================================================================
			#region "Methods: Exposed"

				bool	Invoke( SMC.RfcCustomDestination rfcDestination );

			#endregion

		}
}
