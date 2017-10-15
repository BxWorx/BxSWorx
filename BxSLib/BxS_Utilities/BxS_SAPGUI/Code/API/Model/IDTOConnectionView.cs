//•••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••
namespace SAPGUI.API
{
	public interface IDTOConnectionView
		{
			#region "Properties"

				string	ID						{ get; set; }
				string	GroupID				{ get; set; }
				string	SAPID					{ get; set; }
				string	Description		{ get; set; }

			#endregion

		}
}
