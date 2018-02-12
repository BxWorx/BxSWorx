using SMC	= SAP.Middleware.Connector;
//•••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••
namespace BxS_SAPNCO.RfcFunction
{
	internal interface IRfcFncProfile
		{
			#region "Properties"

				string	FunctionName	{	get; }
				//.................................................
				SMC.RfcDestination				RfcDestination	{ get; }
				SMC.RfcFunctionMetadata		Metadata				{ get; }

			#endregion

		}
}
