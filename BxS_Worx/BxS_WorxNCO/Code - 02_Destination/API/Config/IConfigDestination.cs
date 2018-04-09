using BxS_WorxNCO.Destination.Config;
//•••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••
namespace BxS_WorxNCO.Destination.API
{
	public interface IConfigDestination : IConfigBase
		{
			#region "Properties"

				int	IdleTimeout				{ set; }
				int	IdleCheckTime			{ set; }
				int	MaxPoolWaitTime		{ set; }
				int	PeakConnLimit			{ set; }
				int	PoolIdleTimeout		{ set; }
				int	PoolSize					{ set; }
				int	UseSAPGUI					{ set; }
				//.................................................
				bool	DoLogonCheck		{ set; }
				//.................................................
				int	SAPGUINotUse			{ get; }
				int	SAPGUIUse					{ get; }
				int	SAPGUIHidden			{ get; }

			#endregion

		}
}