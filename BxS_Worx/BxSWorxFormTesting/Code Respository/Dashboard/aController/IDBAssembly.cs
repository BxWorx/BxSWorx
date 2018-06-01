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

			#endregion

			void Load();


		}
}
