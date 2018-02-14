using System;
using System.Collections.Generic;
using System.Security;
using System.Collections.Concurrent;
//.........................................................
using SMC	= SAP.Middleware.Connector;
using SDM = SAP.Middleware.Connector.RfcDestinationManager;
//.........................................................
using BxS_SAPNCO.RfcFunction;
//•••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••
namespace BxS_SAPNCO.Destination
{
	public class DestinationRfc
		{
			#region "Constructors"

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				public DestinationRfc(	SMC.RfcConfigParameters RfcConfig
															,	IDTOConfigSetupGlobal		globalSetup	= null )
					{
						this.RfcConfig	= RfcConfig		;
						this._GlbSetup	= globalSetup	;
						//.............................................
						this._Lock			= new object();
						this._Profiles	= new	ConcurrentDictionary<string, object>();
					}

			#endregion

			//===========================================================================================
			#region "Declarations"

				private readonly object									_Lock;
				private readonly IDTOConfigSetupGlobal	_GlbSetup;

			#endregion

			//===========================================================================================
			#region "Properties"

				public bool	IsConnected	{ get; private set; }
				public bool	IsProcured	{ get; private set; }
				//.................................................
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
				public void LoadConfig(IDTOConfigSetupDestination Config)
					{
						foreach (KeyValuePair<string, string> ls_kvp in Config.Settings)
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

			#endregion

			//===========================================================================================
			#region "Methods: Exposed: General"

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				public void Procure()
					{
						if (this.IsProcured)	return;
						//.............................................
						lock (this._Lock)
							{
								if (this.IsProcured)	return;
								if (this._GlbSetup != null)	this.LoadConfig(this._GlbSetup);

								try
									{
										this.RfcDestination	= SDM.GetDestination(this.RfcConfig);
										this.IsProcured	= !this.IsProcured;
									}
								catch (Exception)
									{
										throw;
									}
							}
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
				internal bool	TryGetProfile<T>(string name, out T profile)
					{
						bool lb_Ret	= this._Profiles.TryGetValue(name, out object lo);
						profile	= lb_Ret ? (T)lo : default(T);
						return	lb_Ret;
					}

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				internal bool RegisterProfile( IRfcFncProfile profile )
					{
						profile.DestinationRfc	= this;
						return	this._Profiles.TryAdd( profile.FunctionName , profile );
					}

			#endregion

		}
}
