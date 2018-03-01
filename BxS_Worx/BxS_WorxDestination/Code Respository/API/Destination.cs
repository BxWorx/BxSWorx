using System;
using System.Collections.Generic;
using System.Security;
//.........................................................
using SMC	= SAP.Middleware.Connector;
using SDM = SAP.Middleware.Connector.RfcDestinationManager;
//.........................................................
using BxS_WorxIPX.API.Destination;
using BxS_WorxIPX.Destination;
//•••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••
namespace BxS_WorxDestination.API.Destination
{
	internal class Destination : IDestination
		{
			#region "Constructors"

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
//																,	IConfigSetupGlobal			globalSetup	= null )
//																,	SMC.RfcConfigParameters RfcConfig )
				internal Destination(	Guid ID )
					{
						this.SAPGUIID		= ID					;
						//this._RfcConfig	= RfcConfig		;
//						this._GlbSetup	= globalSetup	;
						//.............................................
						this._Lock	= new object();
					}

			#endregion

			//===========================================================================================
			#region "Declarations"

				private readonly object										_Lock;
				private readonly IConfigSetupGlobal				_GlbSetup;
				private readonly SMC.RfcConfigParameters	_RfcConfig;

			#endregion

			//===========================================================================================
			#region "Properties"

				public bool	IsConnected	{ get { return this.Ping(); } }
				public bool	IsProcured	{ get; private set; }
				//.................................................
				public Guid											SAPGUIID				{ get; }
				public SMC.RfcDestination				NCODestination	{ get; private set; }
				//.................................................
				public string Client			{ set { this._RfcConfig	[ SMC.RfcConfigParameters.Client					]	= value; } }
				public string Language		{ set { this._RfcConfig	[ SMC.RfcConfigParameters.Language				]	= value; } }
				public string User				{ set { this._RfcConfig	[	SMC.RfcConfigParameters.User						]	= value; } }
				public string Password		{ set { this._RfcConfig	[	SMC.RfcConfigParameters.Password				]	= value; } }

				public SecureString SecurePassword	{ set { this._RfcConfig.SecurePassword	= value; } }

			#endregion

			//===========================================================================================
			#region "Methods: Exposed: Configuration"






				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				public void LoadConfig( SMC.RfcConfigParameters Config )
					{
						foreach (KeyValuePair<string, string> ls_kvp in Config)
							{
								this._RfcConfig[ls_kvp.Key]	= ls_kvp.Value;
							}
						//.............................................
						this.SecurePassword = Config.SecurePassword;
					}

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				public void LoadConfig( IConfigSetupDestination Config )
					{
						foreach (KeyValuePair<string, string> ls_kvp in Config.Settings)
							{
								this._RfcConfig[ls_kvp.Key]	= ls_kvp.Value;
							}
						//.............................................
						this.SecurePassword = Config.SecurePassword;
					}

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				public void LoadConfig(IConfigSetupBase Config)
					{
						foreach (KeyValuePair<string, string> ls_kvp in Config.Settings)
							{
								this._RfcConfig[ls_kvp.Key]	= ls_kvp.Value;
							}
					}

			#endregion

			//===========================================================================================
			#region "Methods: Exposed: General"

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				public bool Procure()
					{
						if ( !this.IsProcured )
							{

							}
						//.............................................
						return	this.IsProcured;
						////.............................................
						//lock (this._Lock)
						//	{
						//		if (this.IsProcured)	return;
						//		if (this._GlbSetup != null)	this.LoadConfig(this._GlbSetup);

						//		try
						//			{
						//				this.NCODestination	= SDM.GetDestination(this.RfcConfig);
						//				this.IsProcured			= !this.IsProcured;
						//			}
						//		catch (Exception)
						//			{
						//				throw;
						//			}
						//	}
					}

		public void LoadConfig(IConfigSetupGlobal Config)
			{
			}




				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				public bool Ping()
					{
						bool	lb_Ret = false;
						//.............................................
						try
							{
								if ( this.Procure() )
									{
										this.NCODestination.Ping();
										lb_Ret	=	true;
									}
							}
						catch
							{	}
						//.............................................
						return	lb_Ret;
					}

		#endregion

		}
}
