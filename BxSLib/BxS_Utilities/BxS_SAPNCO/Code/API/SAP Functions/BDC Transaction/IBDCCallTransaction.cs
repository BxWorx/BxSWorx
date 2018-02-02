//.........................................................
using SMC	= SAP.Middleware.Connector;
//•••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••
namespace BxS_SAPNCO.API.Function
{
	public interface IBDCCallTransaction
		{
			#region "Properties"



				string							Name						{ get; }
				SMC.IRfcFunction		RfcFunction			{ get; set; }
				SMC.RfcDestination	RfcDestination	{ get; set; }

			#endregion

			//===========================================================================================
			#region "Methods: Exposed"

				bool	Invoke();

			#endregion

			//===========================================================================================
			#region "Methods: Private"
			#endregion

		}
}
