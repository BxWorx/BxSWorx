//•••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••
namespace BxS_WorxIPX.BDC
{
	public interface IRequest_Config
		{
			#region "Properties"

				bool	UseAltBDC			{ get; set; }
				bool	IsSequential	{ get; set; }

				int			IdleTimeout				{ get; set; }
				int			IdleCheckTime			{ get; set; }
				int			MaxPoolWaitTime		{ get; set; }
				int			PeakConnLimit			{ get; set; }
				int			PoolIdleTimeout		{ get; set; }
				int			PoolSize					{ get; set; }
				int			RepoIdleTimeout		{ get; set; }
				bool		DoLogonCheck			{ get; set; }

			#endregion

		}
}
