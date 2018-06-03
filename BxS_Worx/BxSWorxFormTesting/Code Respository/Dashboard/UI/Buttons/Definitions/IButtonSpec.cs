//•••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••
namespace BxS_Worx.Dashboard.UI
{
	public interface IButtonSpec
		{
			#region "Properties"

				string	ID					{ get;  set; }
				string	Tag					{ get;  set; }
				string	ImageID			{ get;  set; }
				string	Text				{ get;  set; }
				string	ButtonType	{ get;  set; }

			#endregion

		}
}
