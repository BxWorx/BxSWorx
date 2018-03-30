using SMC	= SAP.Middleware.Connector;
//•••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••
namespace BxS_WorxNCO.RfcFunction.Main
{
	public interface IRfcFncProfile
		{
			#region "Properties"

				string	FunctionName	{	get; }
				bool		IsReady				{ get; set; }

			#endregion

			//===========================================================================================
			#region "Methods"

				void ReadyProfile();

			#endregion

		}
}
