using System.Collections.Generic;
//.........................................................
using BxS_Worx.Dashboard.UI.Button;
//•••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••
namespace BxS_Worx.Dashboard.UI.Toolbar
{
	internal interface IUC_TBarModel
		{
			#region "Properties"

				IUC_TBarSetup		Setup	{ get;	set; }

			#endregion

			//===========================================================================================
			#region "Methods: Exposed"

				IList<string>					ScenarioIDs()													;
				IList<IButtonProfile>	ScenarioButtons( string scenarioID )	;
				//...
				void	LoadButton( IButtonProfile buttonProfile ) ;

			#endregion

		}
}
