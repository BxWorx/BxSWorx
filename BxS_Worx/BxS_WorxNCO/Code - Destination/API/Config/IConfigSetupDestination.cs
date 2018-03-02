﻿using System.Security;
//.........................................................
using BxS_WorxNCO.Destination.Config;
//•••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••
namespace BxS_WorxNCO.Destination.API.Config
{
	public interface IConfigSetupDestination : IConfigSetupBase
		{
			#region "Properties"

				int	Client					{ set; }
				int	IdleTimeout			{ set; }
				int	IdleCheckTime		{ set; }
				int	MaxPoolWaitTime	{ set; }
				int	PeakConnLimit		{ set; }
				int	PoolIdleTimeout	{ set; }
				int	PoolSize				{ set; }

				string	Language	{ set; }
				string	User			{ set; }
				string	Password	{ set; }

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