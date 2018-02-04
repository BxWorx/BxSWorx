using SMC	= SAP.Middleware.Connector;
//•••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••
namespace BxS_SAPNCO.API.Function
{
	internal interface IRfcFncProfile
		{
			#region "Properties"

				string	FunctionName	{	get; }
				//.................................................
				SMC.RfcFunctionMetadata		Metadata	{ get; }

			#endregion

		}
}
