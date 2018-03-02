using BxS_WorxNCO.Destination.Main;
//•••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••
namespace BxS_WorxNCO.Destination.API.Main
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
