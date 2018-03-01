using System.Collections.Generic;
//•••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••
namespace BxS_WorxNCO.API.Destination
{
	public interface IConfigSetupBase
		{
			#region "Properties"

				Dictionary<	string,	string>		Settings	{ get; }

			#endregion

			//===========================================================================================
			#region "Methods: Exposed"

				void Reset();

			#endregion

		}
}