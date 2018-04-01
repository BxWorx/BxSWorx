using SMC	= SAP.Middleware.Connector;
//•••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••
namespace BxS_WorxNCO.RfcFunction.Main
{
	internal interface IRfcFncManager
		{
			#region "Properties"

				SMC.RfcRepository	NCORepository { get; }

			#endregion

			//===========================================================================================
			#region "Methods: Exposed"

				void RegisterProfile	( IRfcFncProfile	rfcFncProfile );
				//.................................................
				bool							ProfileExists						( string rfcFncName );
				ProfileType				GetProfile<ProfileType>	( string rfcFncName );
				//.................................................
				void UpdateProfiles();

			#endregion

		}
}
