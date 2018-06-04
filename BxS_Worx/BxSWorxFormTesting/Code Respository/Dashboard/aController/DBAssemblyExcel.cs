using System.Collections.Generic;
using System.Linq;
//•••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••
namespace BxS_Worx.Dashboard.UI
{
	public sealed class DBAssemblyExcel	: DBAssemblyBase
		{
			#region "Constructors"

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				private DBAssemblyExcel()	: base()
					{
					}

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				public	static	IDBAssembly	Create()	=>	new	DBAssemblyExcel();

			#endregion

			//===========================================================================================
			#region "Methods: Exposed"

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				public override void	LoadFromSource()
					{
						base.LoadFromSource();
						//...
						this.LoadToolbarsFromSource()	;
						this.LoadButtonsFromSource()	;
					}

			#endregion

			//===========================================================================================
			#region "Methods: Private"

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				private	void	LoadButtonsFromSource()
					{
						IButtonProfile	BP1	= ButtonProfile.Create( "BP1" );

						BP1.SeqNo						= 01		;
						BP1.ToolbarID				=	"TB1"	;
						BP1.ScenarioID			= "SC1"	;
						BP1.OnClickHandler	= this.OnButtonClick_RouteScenario	;
						BP1.Spec						=	ButtonSpec.CreateWith(	ButtonTypes.TypeStd
																												, "ID1"
																												, "<TB>TB2;<SC>SC1"
																												, ImageNames.Settings
																												, "Settings"					)	;

						this.LoadButton( BP1 );
						//...
						IButtonProfile	BP2	= ButtonProfile.Create( "BP2" );

						BP2.SeqNo						= 02		;
						BP2.ToolbarID				=	"TB1"	;
						BP2.ScenarioID			= "SC1"	;
						BP2.OnClickHandler	= this.OnButtonClick_RouteScenario	;
						BP2.Spec						=	ButtonSpec.CreateWith(	ButtonTypes.TypeFlp
																												, "ID2"
																												, "<TB>TB2;<SC>SC2"
																												, ImageNames.Logo
																												, "Settings"					)	;

						this.LoadButton( BP2 );
					}

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				private	void	LoadToolbarsFromSource()
					{
						IToolBarConfig TB1	= ToolBarConfig.CreateWithDefaults();		TB1.ID	= "TB1";		TB1.SeqNo	= 1	;
						IToolBarConfig TB2	= ToolBarConfig.CreateWithDefaults();		TB2.ID	= "TB2";		TB2.SeqNo	= 2	;
						//...
						TB2.ColourBack					= System.Drawing.Color.Aquamarine;
						TB2.IsHorizontal				= true	;
						TB2.TransitionSpeed			=	10		;
						TB2.TransitionSpanMin		= 03		;

						TB1.IsStartupToolBar		= true	;
						TB1.StartupScenario			=	"SC1"	;
						TB1.TransitionSpanMin		= 03		;
						TB1.ButtonType					= ButtonTypes.TypeStd	;
						//...
						this.LoadToolbar( TB1 );
						this.LoadToolbar( TB2 );
					}

			#endregion

		}
}
