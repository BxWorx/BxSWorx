using SMC	= SAP.Middleware.Connector;
//•••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••
namespace BxS_WorxNCO.RfcFunction.Main
{
	internal interface IRfcFunctionIndex
		{
			#region "Properties"

				string	Name	{ get; }
				SMC.RfcFunctionMetadata	Metadata	{ get; set; }

			#endregion

			//===========================================================================================
			#region "Methods: Exposed"

				SMC.IRfcFunction	CreateFunction();

			#endregion

		}
}
