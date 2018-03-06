using SMC	= SAP.Middleware.Connector;
//•••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••
namespace BxS_WorxNCO.RfcFunction.Common
{
	internal interface IRfcFncManager
		{
			#region "Properties"

				SMC.RfcRepository	NCORepository { get; }

			#endregion

			//===========================================================================================
			#region "Methods: Exposed"

				void RegisterFunction	( IRfcFncProfile	rfcFncProfile );
				bool PrepareFunction	( IRfcFncBase			rfcFunc );

			#endregion

		}
}
