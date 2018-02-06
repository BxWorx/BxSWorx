using SMC	= SAP.Middleware.Connector;
//•••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••
namespace BxS_SAPNCO.API.Function
{
	internal interface IRFCFunction
		{
			#region "Properties"

				SMC.IRfcFunction		RfcFunction			{ get; set; }

			#endregion

			//===========================================================================================
			#region "Methods: Exposed"

				bool	Invoke(SMC.RfcDestination	rfcDestination);

			#endregion

		}
}
