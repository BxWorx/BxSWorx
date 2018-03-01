using BxS_WorxDestination.Config;
//•••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••
namespace BxS_WorxDestination.API.Config
{
	public interface IConfigSetupGlobal : IConfigSetupBase
		{
			#region "Properties"

				string	SNCLibPath	{ set; }

			#endregion

		}
}