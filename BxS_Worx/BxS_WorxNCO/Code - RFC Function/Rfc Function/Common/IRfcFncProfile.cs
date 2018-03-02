using SMC	= SAP.Middleware.Connector;
//•••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••
namespace BxS_WorxNCO.RfcFunction.Common
{
	internal interface IRfcFncProfile
		{
			#region "Properties"

				string	FunctionName	{	get; }
				bool		IsReady				{ get; }

			#endregion

			//===========================================================================================
			#region "Methods: Exposed"
			#endregion

		}
}
