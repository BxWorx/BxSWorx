using System.Security;
//.........................................................
using SMC	= SAP.Middleware.Connector;
//.........................................................
using BxS_WorxNCO.Destination.API;
//•••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••
namespace BxS_WorxNCO.Destination.Config
{
	internal class ConfigDestination : ConfigBase , IConfigDestination
		{
			#region "Constructors"

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				internal ConfigDestination()
					{	}

			#endregion

			//===========================================================================================
			#region "Properties"

				public	int IdleTimeout				{ set { this.Settings[SMC.RfcConfigParameters.ConnectionIdleTimeout]						= value.ToString(); } }
				public	int IdleCheckTime			{ set { this.Settings[SMC.RfcConfigParameters.IdleCheckTime]										= value.ToString(); } }
				public	int MaxPoolWaitTime		{ set { this.Settings[SMC.RfcConfigParameters.MaxPoolWaitTime]									= value.ToString(); } }
				public	int PeakConnLimit			{ set { this.Settings[SMC.RfcConfigParameters.PeakConnectionsLimit]							= value.ToString(); } }
				public	int PoolIdleTimeout		{ set { this.Settings[SMC.RfcConfigParameters.PoolIdleTimeout]									= value.ToString(); } }
				public	int PoolSize					{ set { this.Settings[SMC.RfcConfigParameters.PoolSize]													= value.ToString(); } }
				public	int RepoIdleTimeout		{ set { this.Settings[SMC.RfcConfigParameters.RepositoryConnectionIdleTimeout]	= value.ToString(); } }
				//.................................................
				public	bool		DoLogonCheck	{ set { this.Settings[SMC.RfcConfigParameters.LogonCheck]	= value ? "1" : "0"	; } }
				public	int			Client				{ set { this.Settings[SMC.RfcConfigParameters.Client]			= value.ToString()	; } }
				public	string	Language			{ set { this.Settings[SMC.RfcConfigParameters.Language]		= value							; } }
				public	string	User					{ set { this.Settings[SMC.RfcConfigParameters.User]				= value							; } }
				public	string	Password			{ set { this.Settings[SMC.RfcConfigParameters.Password]		= value							; } }
				//.................................................
				public	SecureString	SecurePassword	{ get; set;	}

			#endregion

			//===========================================================================================
			#region "Properties"

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				public void SetSAPGUIasHidden()
					{
						this.Settings[SMC.RfcConfigParameters.UseSAPGui]	= SMC.RfcConfigParameters.RfcUseSAPGui.Hidden;
					}

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				public void SetSAPGUIasNotUsed()
					{
						this.Settings[SMC.RfcConfigParameters.UseSAPGui]	= SMC.RfcConfigParameters.RfcUseSAPGui.NotUse;
					}

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				public void SetSAPGUIasUsed()
					{
						this.Settings[SMC.RfcConfigParameters.UseSAPGui]	= SMC.RfcConfigParameters.RfcUseSAPGui.Use;
					}

		#endregion

		}
}
