using SMC	= SAP.Middleware.Connector;
//•••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••
namespace BxS_WorxNCO.RfcFunction.Main
{
	public interface IRfcFncProfile
		{
			#region "Properties"

				string	FunctionName	{	get; }
				bool		IsReady				{ get; set; }
				//.................................................
				//SMC.RfcCustomDestination	NCODestination	{ get; }

			#endregion

			//===========================================================================================
			#region "Methods"

				void ReadyProfile();
				//.................................................
				SMC.IRfcFunction	CreateRfcFunction();

			#endregion

		}
}
