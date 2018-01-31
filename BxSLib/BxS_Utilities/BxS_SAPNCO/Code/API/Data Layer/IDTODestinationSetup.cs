using System.Collections.Generic;
//•••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••
namespace BxS_SAPNCO.API.DL
{
	public interface IDTODestinationSetup
		{
			#region "Properties"

				Dictionary<string,string> Settings { get; }

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

			#endregion

		//===========================================================================================
		#region "Methods: Exposed"

			void Reset();

			void SetSAPGUIasHidden();
			void SetSAPGUIasUsed();
			void SetSAPGUIasNotUsed();

			#endregion

		}
}