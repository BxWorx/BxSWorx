using BxS_WorxNCO.Destination.Config;
//•••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••
namespace BxS_WorxNCO.Destination.API
{
	public interface IConfigDestination
		{
			#region "Properties"

				int	IdleTimeout				{ get; set; }
				int	IdleCheckTime			{ get; set; }
				int	MaxPoolWaitTime		{ get; set; }
				int	PeakConnLimit			{ get; set; }
				int	PoolIdleTimeout		{ get; set; }
				int	PoolSize					{ get; set; }
				int	UseSAPGUI					{ get; set; }
				//.................................................
				int	RepoIdleTimeout		{ get; set; }
				//.................................................
				bool	DoLogonCheck		{ get; set; }

			#endregion

		}
}