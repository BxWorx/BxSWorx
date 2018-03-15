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

				void RegisterProfile	( IRfcFncProfile	rfcFncProfile  , bool loadMetadata = false );
				//.................................................
				bool							ProfileExists						( string rfcFncName );
				ProfileType				GetProfile<ProfileType>	( string rfcFncName );
				//.................................................
				bool UpdateProfiles();

			#endregion

		}
}
