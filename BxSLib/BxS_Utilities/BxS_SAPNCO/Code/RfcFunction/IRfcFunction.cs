//.........................................................
using SMC	= SAP.Middleware.Connector;
//•••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••
namespace BxS_SAPNCO.API.Function
{
	public interface IRFCFunction
		{
			#region "Methods: Exposed"

				string	Name	{ get; set; }

				SMC.IRfcFunction	RfcFunction { get; set; }

			#endregion

			//===========================================================================================
			#region "Methods: Private"
			#endregion

		}
}
