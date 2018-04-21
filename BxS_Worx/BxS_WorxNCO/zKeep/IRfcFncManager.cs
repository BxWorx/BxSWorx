using System.Threading.Tasks;
//.........................................................
using SMC	= SAP.Middleware.Connector;
//•••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••
namespace BxS_WorxNCO.RfcFunction.Main
{
	internal interface IRfcFncController
		{
			#region "Properties"

				SMC.RfcRepository	SMCRepository { get; }

			#endregion

			//===========================================================================================
			#region "Methods: Exposed"

				void RegisterProfile	( IRfcFncProfile	rfcFncProfile );
				//.................................................
				bool							ProfileExists						( string rfcFncName );
				ProfileType				GetProfile<ProfileType>	( string rfcFncName );
				//.................................................
				Task	UpdateProfilesAsync( bool	optimiseMetadataFetch = true );

			#endregion

		}
}
