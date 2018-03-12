using SMC	= SAP.Middleware.Connector;
//•••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••
namespace BxS_WorxNCO.RfcFunction.Common
{
	internal interface IRfcFncBase
		{
			#region "Properties"

				string	SAPFunctionName	{ get; }

				IRfcFncProfile		Profile					{ get; }
				SMC.IRfcFunction	NCORfcFunction	{ get; set; }

			#endregion

			//===========================================================================================
			#region "Methods: Exposed"

				bool	Invoke( SMC.RfcCustomDestination rfcDest );

			#endregion

		}
}
