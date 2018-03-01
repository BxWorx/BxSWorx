using BxS_WorxDestination.Main;
//•••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••
namespace BxS_WorxDestination.API.Main
{
	public static class Factory
		{
			#region "Properties"

				public static IController Controller()
					{
						return	new Controller();
					}

			#endregion

		}
}
