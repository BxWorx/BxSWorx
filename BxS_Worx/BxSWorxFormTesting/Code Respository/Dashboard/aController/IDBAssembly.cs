using System.Collections.Generic		;
//.........................................................
using BxS_Worx.Dashboard.UI.Window	;
using BxS_Worx.Dashboard.UI.Button	;
//•••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••
namespace BxS_Worx.Dashboard.UI
{
	public interface IDBAssembly
		{
			#region "Properties"

				IDB_Config	ViewConfig	{ get; set; }
				//...
				IList<IUC_TBarSetup>	ToolBarList	{ get; }

			#endregion

			//===========================================================================================
			#region "Methods"

				void Save()	;
				void LoadFromSource();
				//...
				IList<IButtonProfile>	ToolbarButtonList( string tbarID );

			#endregion

		}
}
