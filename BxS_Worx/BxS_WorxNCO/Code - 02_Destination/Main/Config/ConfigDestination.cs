using BxS_WorxNCO.Destination.API;
//•••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••
namespace BxS_WorxNCO.Destination.Config
{
	internal class ConfigDestination : IConfigDestination
		{
			#region "Properties"

				public	int IdleTimeout				{ get;	set; }
				public	int IdleCheckTime			{ get;	set; }
				public	int MaxPoolWaitTime		{ get;	set; }
				public	int PeakConnLimit			{ get;	set; }
				public	int PoolIdleTimeout		{ get;	set; }
				public	int PoolSize					{ get;	set; }
				public	int UseSAPGUI					{ get;	set; }
				//.................................................
				public	int RepoIdleTimeout		{ get;	set; }
				//.................................................
				public	bool	DoLogonCheck		{ get;	set; }

			#endregion

		}
}
