using System;
using System.Collections.Generic;
//.........................................................
using SMC	= SAP.Middleware.Connector;
//.........................................................
using BxS_WorxNCO.Destination.API;

using static	BxS_WorxNCO.Main.NCO_Constants;
//•••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••
namespace BxS_WorxNCO.Destination.Main.Destination
{
	public class STDDestination : ISTDDestination
		{
			#region "Constructors"

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				internal STDDestination( Guid ID )
					{
						this.SAPGUIID	= ID	;
						this.MyID			= Guid.NewGuid();
						//.............................................
						this._RfcConfig				=	new Lazy< SMC.RfcConfigParameters >	( ()=>	SAPSDM.Instance.CreateNCOConfig()												, cz_LM )	;
						this._NCODestination	= new Lazy< SMC.RfcDestination >			( ()=>	SAPSDM.Instance.GetDestination( this._RfcConfig.Value ) , cz_LM )	;
					}

			#endregion

			//===========================================================================================
			#region "Declarations"

				private		readonly Lazy< SMC.RfcDestination >				_NCODestination	;
				protected	readonly Lazy< SMC.RfcConfigParameters >	_RfcConfig			;

			#endregion

			//===========================================================================================
			#region "Properties"

				public Guid		MyID			{ get; }
				public Guid		SAPGUIID	{ get; }
				//.................................................
				public SMC.RfcDestination		NCODestination	{ get { return	this._NCODestination.Value	; } }

			#endregion

			//===========================================================================================
			#region "Methods: Exposed"

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				public void LoadConfig(SMC.RfcConfigParameters config )
					{
						foreach (KeyValuePair<string, string> ls_kvp in config)
							{
								this._RfcConfig.Value[ls_kvp.Key]	= ls_kvp.Value;
							}
						//.............................................
						this._RfcConfig.Value.SecureRepositoryPassword	= config.SecureRepositoryPassword	;
						this._RfcConfig.Value.SecurePassword						= config.SecurePassword						;
					}

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				public void LoadConfig( IConfigLogon config )
					{
						this._RfcConfig.Value[ SMC.RfcConfigParameters.Language	]	= config.Language	;
						this._RfcConfig.Value[ SMC.RfcConfigParameters.Client		]	= config.Client		;
						//.............................................
						if ( config.ForRepository )
							{
								this._RfcConfig.Value[ SMC.RfcConfigParameters.RepositoryUser			]	= config.User			;
								this._RfcConfig.Value[ SMC.RfcConfigParameters.RepositoryPassword ]	= config.Password	;
								//.........................................
								this._RfcConfig.Value.SecureRepositoryPassword	= config.SecurePassword;
							}
						else
							{
								this._RfcConfig.Value[ SMC.RfcConfigParameters.User			]	= config.User			;
								this._RfcConfig.Value[ SMC.RfcConfigParameters.Password ]	= config.Password	;
								//.........................................
								this._RfcConfig.Value.SecurePassword	= config.SecurePassword;
							}
					}

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				public void LoadConfig( IConfigGlobal config )
					{
						this._RfcConfig.Value[ SMC.RfcConfigParameters.SncLibraryPath ]	= config.SNCLibPath	;
					}

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				public void LoadConfig( IConfigDestination config )
					{
						this._RfcConfig.Value[ SMC.RfcConfigParameters.ConnectionIdleTimeout						]	=	config.IdleTimeout			.ToString()	;
						this._RfcConfig.Value[ SMC.RfcConfigParameters.IdleCheckTime										]	= config.IdleCheckTime		.ToString()	;
						this._RfcConfig.Value[ SMC.RfcConfigParameters.MaxPoolWaitTime									]	= config.MaxPoolWaitTime	.ToString()	;
						this._RfcConfig.Value[ SMC.RfcConfigParameters.PeakConnectionsLimit							]	= config.PeakConnLimit		.ToString()	;
						this._RfcConfig.Value[ SMC.RfcConfigParameters.PoolIdleTimeout									]	= config.PoolIdleTimeout	.ToString()	;
						this._RfcConfig.Value[ SMC.RfcConfigParameters.PoolSize													]	= config.PoolSize					.ToString()	;
						this._RfcConfig.Value[ SMC.RfcConfigParameters.UseSAPGui												]	= config.UseSAPGUI				.ToString()	;
						//.............................................
						this._RfcConfig.Value[ SMC.RfcConfigParameters.RepositoryConnectionIdleTimeout	]	= config.RepoIdleTimeout	.ToString()	;
						//.............................................
						this._RfcConfig.Value[ SMC.RfcConfigParameters.LogonCheck												]	= config.DoLogonCheck			? "1"	: "0"	;
					}

			#endregion

		}
}
