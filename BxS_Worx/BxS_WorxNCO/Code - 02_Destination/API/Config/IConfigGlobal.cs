using BxS_WorxNCO.Destination.Config;
//•••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••
namespace BxS_WorxNCO.Destination.API
{
	public interface IConfigGlobal	: IConfigBase
		{
			#region "Properties"

				string	SNCLibPath	{ set; }

			#endregion

		}
}