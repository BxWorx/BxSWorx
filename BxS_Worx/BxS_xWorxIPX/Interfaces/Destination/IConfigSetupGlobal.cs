using BxS_WorxIPX.Destination;
//•••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••
namespace BxS_WorxIPX.API.Destination
{
	public interface IConfigSetupGlobal : IConfigSetupBase
		{
			#region "Properties"

				string	SNCLibPath	{ set; }

			#endregion

		}
}