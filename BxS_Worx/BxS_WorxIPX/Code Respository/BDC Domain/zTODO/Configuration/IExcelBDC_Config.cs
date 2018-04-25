//•••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••
namespace BxS_WorxIPX.BDC
{
	public interface IExcelBDC_Config
		{
			#region "Properties"

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
