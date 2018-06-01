using System.Collections.Generic;
//•••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••
namespace BxS_Worx.Dashboard.UI
{
	public interface IDBAssembly
		{
			#region "Properties"

				IDBFormConfig	FormConfig { get; }
				//...
				IList<IToolBarConfig>	ToolBarList	{ get; }
				IList<IButtonProfile>	ButtonList	{ get; }

			#endregion

			//===========================================================================================
			#region "Methods"

				IToolBarConfig	GetToolbarConfig( string ID);
				void Load();

			#endregion

		}
}
