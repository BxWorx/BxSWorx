using System.Collections.Generic;
//•••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••
namespace BxS_SAPNCO.API.DL
{
	public interface IDTOConfigSetupBase
		{
			#region "Properties"

				Dictionary<string,string> Settings				{ get;				}

			#endregion

			//===========================================================================================
			#region "Methods: Exposed"

				void Reset();

			#endregion

		}
}