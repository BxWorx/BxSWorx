//•••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••
namespace BxS_Worx.UI.Dashboard
{
	public interface IButtonSpec
		{
			#region "Properties"

				int			TabIndex		{ get;  set; }
				string	ID					{ get;  set; }
				string	ImageID			{ get;  set; }
				string	Text				{ get;  set; }
				string	ButtonType	{ get;  set; }

			#endregion

		}
}
