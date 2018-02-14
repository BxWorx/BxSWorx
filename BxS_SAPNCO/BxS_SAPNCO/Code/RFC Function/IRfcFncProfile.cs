using SMC	= SAP.Middleware.Connector;
//.........................................................
using BxS_SAPNCO.Destination;
//•••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••
namespace BxS_SAPNCO.RfcFunction
{
	internal interface IRfcFncProfile
		{
			#region "Properties"

				string	FunctionName	{	get; }
				bool		IsReady				{ get; }
				//.................................................
				DestinationRfc						DestinationRfc	{ get; set; }

				SMC.RfcDestination				RfcDestination	{ get; }
				SMC.RfcFunctionMetadata		Metadata				{ get; }

			#endregion

			//===========================================================================================
			#region "Methods: Exposed"

				bool	Ready();

			#endregion

		}
}
