using System.Collections.Generic;
//•••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••
namespace BxS_SAPNCO.API.DL
{
	public interface IDTOGlobalSetup
		{
			#region "Properties"

				Dictionary<string,string> Settings { get; }

				string	SNCLibPath	{ set; }

			#endregion

		//===========================================================================================
		#region "Methods: Exposed"

			void Reset();

			#endregion

		}
}