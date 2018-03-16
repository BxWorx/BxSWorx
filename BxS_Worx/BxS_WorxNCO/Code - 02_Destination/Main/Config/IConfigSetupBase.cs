using System.Collections.Generic;
//•••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••
namespace BxS_WorxNCO.Destination.Config
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