using BxS_WorxNCO.Destination.Config;
//•••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••
namespace BxS_WorxNCO.Destination.API
{
	public interface IConfigSetupGlobal	: IConfigSetupBase
		{
			#region "Properties"

				string	SNCLibPath	{ set; }

			#endregion

		}
}