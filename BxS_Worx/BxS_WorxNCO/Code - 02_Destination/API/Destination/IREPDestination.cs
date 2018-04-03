using System.Threading.Tasks;
//.........................................................
using SMC	= SAP.Middleware.Connector;
//•••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••
namespace BxS_WorxNCO.Destination.API
{
	public interface IREPDestination : ISTDDestination
		{
			#region "Properties"

				bool	Optimise	{ get; set; }

			#endregion

			//===========================================================================================
			#region "Methods: Exposed"

				void	LoadConfig	( IConfigRepository config );
				//.................................................
				void	RegisterRfcFunctionForMetadata( string fncName );
				//.................................................
				Task<	SMC.RfcFunctionMetadata >	FetchFunctionMetadata( string fncName );
				Task< bool >										FetchMetadataAsync();

			#endregion

		}
}