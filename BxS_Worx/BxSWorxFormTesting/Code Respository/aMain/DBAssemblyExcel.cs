using System.Collections.Generic	;
using System.Linq									;
//•••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••
namespace BxS_Worx.Dashboard.UI
{
	public sealed class DBAssemblyExcel	: DBAssemblyBase
		{
			#region "Constructors"

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				private DBAssemblyExcel()	: base()
					{	}

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
						IButtonProfile	BP1	= DB_Factory.CreateButtonProfile(		"BP1"
																																	,	ButtonTypes.TypeStd
																																	, "<TB>TB2;<SC>SC1"
																																	, ImageNames.Settings
																																	, "Settings"					)	;

						BP1.SeqNo						= 01		;
						BP1.ToolbarID				=	"TB1"	;
						BP1.ScenarioID			= "SC1"	;
						BP1.OnClickHandler	= this.OnButtonClick_RouteScenario	;

						this.LoadButton( BP1 ) ;
						//...
						IButtonProfile	BP2	=	DB_Factory.CreateButtonProfile(		"BP2"
																																	,	ButtonTypes.TypeFlp
																																	, "<TB>TB2;<SC>SC2"
																																	, ImageNames.Logo
																																	, "Settings"					)	;

						BP2.SeqNo						= 02		;
						BP2.ToolbarID				=	"TB1"	;
						BP2.ScenarioID			= "SC1"	;
						BP2.OnClickHandler	= this.OnButtonClick_RouteScenario	;

						this.LoadButton( BP2 ) ;
					}

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				private	void	LoadToolbarsFromSource()
					{
						IUC_TBarSetup	TB1		=	DB_Factory.CreateTBSetup();		TB1.ID	= "TB1";	TB1.SeqNo	= 1	;	TB1.IsHorizontal	= true	;

						TB1.IsStartupToolBar	= true	;
						TB1.StartupScenario		=	"SC1"	;
						TB1.ButtonType				= ButtonTypes.TypeStd	;

						TB1.ViewConfig.ColourBack		= System.Drawing.Color.Aqua	;

						this.LoadToolbar( TB1 )	;
						//...
						IUC_TBarSetup	TB2		= DB_Factory.CreateTBSetup();		TB2.ID	= "TB2";	TB2.SeqNo	= 2	;	TB2.IsHorizontal	= false	;

						TB2.ViewConfig.ColourBack					= System.Drawing.Color.Aquamarine	;
						TB2.ViewConfig.TransitionSpeed		=	10	;

						this.LoadToolbar( TB2 )	;
					}

			#endregion

		}
}
