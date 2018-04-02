using System;
//.........................................................
using SMC	= SAP.Middleware.Connector;
//•••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••
namespace BxS_WorxNCO.Destination.API
{
	public interface ISTDDestination
		{
			#region "Properties"

				Guid	MyID			{ get; }
				Guid	SAPGUIID	{ get; }
				//.................................................
				SMC.RfcDestination	NCODestination	{ get; }

			#endregion

			//===========================================================================================
			#region "Methods: Exposed"

				void	LoadConfig	( SMC.RfcConfigParameters	config );
				void	LoadConfig	( IConfigLogon						config );
				void	LoadConfig	( IConfigDestination			config );
				void	LoadConfig	( IConfigGlobal						config );

			#endregion

		}
}