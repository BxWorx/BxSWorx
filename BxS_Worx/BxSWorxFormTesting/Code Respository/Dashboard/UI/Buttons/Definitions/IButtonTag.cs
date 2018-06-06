//•••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••
namespace BxS_Worx.Dashboard.UI.Button
{
	public interface IButtonTag
		{
			#region "Properties"

				string	ScenarioID			{ get; set; }
				string	ButtonID				{ get; set; }
				//...
				string	TargetScenario	{ get; set; }
				string	TargetToolBar		{ get; set; }

			#endregion

		}
}
