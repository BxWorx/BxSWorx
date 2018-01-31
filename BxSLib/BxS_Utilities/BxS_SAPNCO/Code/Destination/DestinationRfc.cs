using System.Collections.Generic;
//.........................................................
using SMC	= SAP.Middleware.Connector;
using SDM = SAP.Middleware.Connector.RfcDestinationManager;
//•••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••
namespace BxS_SAPNCO.Destination
{
	public class DestinationRfc
		{
			#region "Constructors"

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				public DestinationRfc( SMC.RfcCustomDestination rfcCustomDestination )
					{
						this.RfcDestination	= rfcCustomDestination;
					}

			#endregion

			//===========================================================================================
			#region "Properties"

				public SMC.RfcCustomDestination	RfcDestination { get; }

				public string Client		{ set { this.RfcDestination.Client		= value; } }
				public string User			{ set { this.RfcDestination.User			= value; } }
				public string Password	{ set { this.RfcDestination.Password	= value; } }

			#endregion

			//===========================================================================================
			#region "Methods: Exposed"





				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				public bool Ping()
					{
						try
							{
								this.RfcDestination.Ping();
								return	true;
							}
						catch (System.Exception)
							{
								return	false;
							}
					}

			#endregion

		}
}
