using SMC	= SAP.Middleware.Connector;
//•••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••
namespace BxS_WorxNCO.RfcFunction.Common
{
	internal interface IRfcFunctionPool
		{
			#region "Properties"

				SMC.RfcRepository	NCORepository { get; set; }

			#endregion

			//===========================================================================================
			#region "Methods: Exposed"

				void RegisterFunction( string name );
				bool FetchMetadata();

			#endregion

		}
}
