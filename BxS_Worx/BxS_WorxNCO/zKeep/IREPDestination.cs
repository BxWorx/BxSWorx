using System.Threading.Tasks;
//.........................................................
using SMC	= SAP.Middleware.Connector;
//•••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••
namespace BxS_WorxNCO.Destination.API
{
	public interface IREPDestination : IRfcDestination
		{
			#region "Methods: Exposed"

				void	RegisterRfcFunctionForMetadata( string fncName );
				//.................................................
				Task<	SMC.RfcFunctionMetadata >	FetchFunctionMetadata	( string	fncName );
				Task< bool >										FetchMetadataAsync		( bool		optimiseMetadataFetch = true );

			#endregion

		}
}