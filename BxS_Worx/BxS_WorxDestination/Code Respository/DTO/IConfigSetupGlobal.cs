using BxS_WorxIPX.Destination;
//•••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••
namespace BxS_WorxDestination.Config
{
	public interface IConfigSetupGlobal : IConfigSetupBase
		{
			#region "Properties"

				string	SNCLibPath	{ set; }

			#endregion

		}
}