using SMC	= SAP.Middleware.Connector;
//.........................................................
using BxS_WorxNCO.Destination.API;
using BxS_WorxNCO.Destination.Main;
//•••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••
namespace BxS_WorxNCO.Destination.Config
{
	internal class ConfigDestination : ConfigBase , IConfigDestination
		{
			#region "Properties"

				public	int IdleTimeout				{ set	{ this.Set( SMC.RfcConfigParameters.ConnectionIdleTimeout	, value.ToString() );	} }
				public	int IdleCheckTime			{ set	{ this.Set( SMC.RfcConfigParameters.IdleCheckTime					, value.ToString() );	} }
				public	int MaxPoolWaitTime		{ set	{ this.Set( SMC.RfcConfigParameters.MaxPoolWaitTime				, value.ToString() );	} }
				public	int PeakConnLimit			{ set	{ this.Set( SMC.RfcConfigParameters.PeakConnectionsLimit	, value.ToString() );	} }
				public	int PoolIdleTimeout		{ set	{ this.Set( SMC.RfcConfigParameters.PoolIdleTimeout				, value.ToString() );	} }
				public	int PoolSize					{ set	{ this.Set( SMC.RfcConfigParameters.PoolSize							, value.ToString() );	} }
				public	int UseSAPGUI					{ set	{ this.Set( SMC.RfcConfigParameters.UseSAPGui							, value.ToString() );	} }
				//.................................................
				public	bool	DoLogonCheck		{ set	{ this.Set( SMC.RfcConfigParameters.LogonCheck						, value ? "1":"0"	 );	} }
				//.................................................
				public	int	SAPGUINotUse			{ get { return	int.Parse( SAPSDM.UseSAPGUINotUse )	;	} }
				public	int	SAPGUIUse					{ get { return	int.Parse( SAPSDM.UseSAPGUIUse		)	;	} }
				public	int	SAPGUIHidden			{ get { return	int.Parse( SAPSDM.UseSAPGUIHidden	)	;	} }

			#endregion

		}
}
