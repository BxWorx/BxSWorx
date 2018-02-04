using System;
using System.Collections.Generic;
using System.Security;
//.........................................................
using SMC	= SAP.Middleware.Connector;
//.........................................................
using BxS_SAPNCO.API.DL;
using BxS_SAPNCO.API.Function;
//•••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••
namespace BxS_SAPNCO.Destination
{
	public class DestinationRfc
		{
			#region "Constructors"

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				public DestinationRfc( SMC.RfcConfigParameters RfcConfig )
					{
						this.RfcConfig	= RfcConfig;
					}

			#endregion

			//===========================================================================================
			#region "Properties"

				public Guid											SAPGUIID				{ get; set; }
				public SMC.RfcDestination				RfcDestination	{ get; set; }
				public SMC.RfcConfigParameters	RfcConfig				{ get;			}

				//public SMC.RfcRepository				RfcRepository		{	get {	return	this.RfcDestination.Repository; }	}
				//.................................................
				public string Client			{ set { this.RfcConfig	[ SMC.RfcConfigParameters.Client					]	= value; } }
				public string User				{ set { this.RfcConfig	[	SMC.RfcConfigParameters.User						]	= value; } }
				public string Password		{ set { this.RfcConfig	[	SMC.RfcConfigParameters.Password				]	= value; } }
				public string SNCLibPath	{ set { this.RfcConfig	[	SMC.RfcConfigParameters.SncLibraryPath	]	= value; } }

				public SecureString SecurePassword	{ set { this.RfcConfig.SecurePassword	= value; } }

			#endregion

			//===========================================================================================
			#region "Methods: Exposed: Configuration"

				internal void LoadMetadata(IRfcFncProfile profile)
					{
						profile.Metadata	= this.
					}

			#endregion

			//===========================================================================================
			#region "Methods: Exposed: Configuration"

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				public void LoadConfig(SMC.RfcConfigParameters RFCConfigParams)
					{
						foreach (KeyValuePair<string, string> ls_kvp in RFCConfigParams)
							{
								this.RfcConfig[ls_kvp.Key]	= ls_kvp.Value;
							}
						//.............................................
						this.SecurePassword = RFCConfigParams.SecurePassword;
					}

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				public void LoadConfig(IDTOConfigSetupBase DTOConfig)
					{
						foreach (KeyValuePair<string, string> ls_kvp in DTOConfig.Settings)
							{
								this.RfcConfig[ls_kvp.Key]	= ls_kvp.Value;
							}
					}

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				public void LoadConfig(DTOConfigSetupDestination DTOConfig)
					{
						foreach (KeyValuePair<string, string> ls_kvp in DTOConfig.Settings)
							{
								this.RfcConfig[ls_kvp.Key]	= ls_kvp.Value;
							}
						//.............................................
						this.SecurePassword = DTOConfig.SecurePassword;
					}

			#endregion

			//===========================================================================================
			#region "Methods: Exposed: General"

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
			#region "Methods: Internal: Repository"

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				internal void CreateRFCFunction(IRFCFunction function)
					{
						function.RfcFunction		= this.RfcDestination.Repository.CreateFunction(function.Name);
						function.RfcDestination	= this.RfcDestination;
					}

			#endregion

		}
}
