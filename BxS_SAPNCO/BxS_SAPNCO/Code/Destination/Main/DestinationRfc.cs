using System;
using System.Collections.Generic;
using System.Security;
using System.Collections.Concurrent;
//.........................................................
using SMC	= SAP.Middleware.Connector;
using SDM = SAP.Middleware.Connector.RfcDestinationManager;
//.........................................................
using BxS_SAPNCO.API.Function;
//•••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••
namespace BxS_SAPNCO.Destination
{
	public class DestinationRfc
		{
			#region "Constructors"

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				public DestinationRfc(	SMC.RfcConfigParameters RfcConfig		,
																IDTOConfigSetupGlobal		globalSetup	= null	)
					{
						this.RfcConfig	= RfcConfig		;
						this._GlbSetup	= globalSetup	;
						//.............................................
						this._Profiles	= new	ConcurrentDictionary<string, object>();
					}

			#endregion

			//===========================================================================================
			#region "Declarations"

				private readonly IDTOConfigSetupGlobal	_GlbSetup;

			#endregion

			//===========================================================================================
			#region "Properties"

				public Guid											SAPGUIID				{ get; set; }
				public SMC.RfcDestination				RfcDestination	{ get; set; }
				public SMC.RfcConfigParameters	RfcConfig				{ get;			}
				//.................................................
				public string Client			{ set { this.RfcConfig	[ SMC.RfcConfigParameters.Client					]	= value; } }
				public string User				{ set { this.RfcConfig	[	SMC.RfcConfigParameters.User						]	= value; } }
				public string Password		{ set { this.RfcConfig	[	SMC.RfcConfigParameters.Password				]	= value; } }
				public string SNCLibPath	{ set { this.RfcConfig	[	SMC.RfcConfigParameters.SncLibraryPath	]	= value; } }

				public SecureString SecurePassword	{ set { this.RfcConfig.SecurePassword	= value; } }

			#endregion

			//===========================================================================================
			#region "Methods: Exposed: Configuration"

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				public void LoadConfig(SMC.RfcConfigParameters Config)
					{
						foreach (KeyValuePair<string, string> ls_kvp in Config)
							{
								this.RfcConfig[ls_kvp.Key]	= ls_kvp.Value;
							}
						//.............................................
						this.SecurePassword = Config.SecurePassword;
					}

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				public void LoadConfig(IDTOConfigSetupBase Config)
					{
						foreach (KeyValuePair<string, string> ls_kvp in Config.Settings)
							{
								this.RfcConfig[ls_kvp.Key]	= ls_kvp.Value;
							}
					}

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				public void LoadConfig(IDTOConfigSetupDestination Config)
					{
						foreach (KeyValuePair<string, string> ls_kvp in Config.Settings)
							{
								this.RfcConfig[ls_kvp.Key]	= ls_kvp.Value;
							}
						//.............................................
						this.SecurePassword = Config.SecurePassword;
					}

			#endregion

			//===========================================================================================
			#region "Methods: Exposed: General"

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				public bool Procure()
					{
						bool lb_Ret	= true;
						//.............................................
						if (this._GlbSetup != null)	this.LoadConfig(this._GlbSetup);

						try
							{
								this.RfcDestination	= SDM.GetDestination(this.RfcConfig);
							}
						catch (Exception)
							{
								lb_Ret	= false;
							}
						//.............................................
						return	lb_Ret;
					}

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

			//===========================================================================================
			#region "Methods: Internal: Profiles"

				private readonly ConcurrentDictionary<string, object>	_Profiles;

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				internal void	TryGetProfile(string name, out Object profile)
					{
						this._Profiles.TryGetValue(name, out profile);
					}

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				internal bool RegisterProfile(IRfcFncProfile profile)
					{
						return	this._Profiles.TryAdd(profile.FunctionName, profile);
					}

			#endregion

		}
}
