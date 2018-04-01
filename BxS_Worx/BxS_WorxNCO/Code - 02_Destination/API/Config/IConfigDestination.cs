using System.Security;
//.........................................................
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
				int	RepoIdleTimeout		{ set; }
				//.................................................
				int			Client				{ set; }
				string	Language			{ set; }
				string	User					{ set; }
				string	Password			{ set; }
				bool		DoLogonCheck	{ set; }

				SecureString	SecurePassword	{ get;	set;	}

			#endregion

			//===========================================================================================
			#region "Methods: Exposed"

				void SetSAPGUIasHidden	();
				void SetSAPGUIasUsed		();
				void SetSAPGUIasNotUsed	();

			#endregion

		}
}