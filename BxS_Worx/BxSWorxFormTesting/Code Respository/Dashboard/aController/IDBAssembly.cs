using System.Collections.Generic;
//.........................................................
using BxS_Worx.Dashboard.UI.Window;
//•••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••
namespace BxS_Worx.Dashboard.UI
{
	public interface IDBAssembly
		{
			#region "Properties"

				IDB_ViewConfig	FormConfig { get; }
				//...
				IList<IUC_TBarSetup>	ToolBarList	{ get; }
				IList<IButtonProfile>	ButtonList	{ get; }

			#endregion

			//===========================================================================================
			#region "Methods"

				void LoadFromSource();

			#endregion

		}
}
