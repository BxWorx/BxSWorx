using BXSDest	= BxS_WorxDestination.API.Main;
//•••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••
namespace BxS_Worx.API
{
	public interface IController
		{
			#region "Methods: Exposed: Destination"

				BXSDest.IController	BxSDestController	{ get; }

			#endregion

		}
}