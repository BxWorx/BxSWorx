using System.Windows.Forms	;
//...
using BxS_Worx.Dashboard.UI.Button	;
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
				public override void LoadFromSource()
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
				private	void LoadButtonsFromSource()
					{
						IButtonProfile	BP1	= DB_Factory.CreateButtonProfile(		"BP1"
																																	,	"TB1"
																																	, "SC1"
																																	, 01
																																	,	ButtonTypes.TypeStd
																																	, ImageNames.Settings
																																	, "Settings"
																																	, "TB2"
																																	, "SC1"								)	;

						BP1.FocusDocking		=	DockStyle.Bottom	;

						this.LoadButton( BP1 ) ;
						//...
						IButtonProfile	BP2	=	DB_Factory.CreateButtonProfile(		"BP2"
																																	,	"TB1"
																																	, "SC1"
																																	, 03
																																	,	ButtonTypes.TypeFlp
																																	, ImageNames.Logo
																																	, "Settings"
																																	, "TB2"
																																	, "SC1"								)	;

						BP2.FocusDocking		=	DockStyle.Bottom	;

						this.LoadButton( BP2 ) ;
						//...
						IButtonProfile	BP3	=	DB_Factory.CreateButtonProfile(		"BP3"
																																	,	"TB1"
																																	, "SC1"
																																	, 02
																																	,	ButtonTypes.TypeFlp
																																	, ImageNames.Excel
																																	, "Settings"
																																	, "TB2"
																																	, "SC1"								)	;

						BP3.FocusDocking		=	DockStyle.Top	;

						this.LoadButton( BP3 ) ;
						//...
						//...
						IButtonProfile	BP4	= DB_Factory.CreateButtonProfile(		"BP2.1"
																																	,	"TB2"
																																	, "SC1"
																																	, 01
																																	,	ButtonTypes.TypeAny
																																	, ImageNames.Settings
																																	, "Settings"					)	;

						this.LoadButton( BP4 ) ;

					}

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				private	void LoadToolbarsFromSource()
					{
						IUC_TBarSetup	TB1			=	DB_Factory.CreateTBSetupWithDefaults();		TB1.ID	= "TB1";	TB1.SeqNo	= 1	;	TB1.IsHorizontal	= true	;

						TB1.IsStartupToolBar	= true	;
						TB1.StartupScenario		=	"SC1"	;
						TB1.TransitionSpeed		=	0			;
						TB1.IsStartupSpanMax	=	false	;
						TB1.FocusDocking			=	DockStyle.Top	;

						TB1.ButtonType				= ButtonTypes.TypeStd	;
						TB1.ColourBack				= System.Drawing.Color.Plum		;
						TB1.ColourFocus				= System.Drawing.Color.Black	;
						this.LoadToolbar( TB1 )	;
						//...
						IUC_TBarSetup	TB2			= DB_Factory.CreateTBSetupWithDefaults();		TB2.ID	= "TB2";	TB2.SeqNo	= 2	;	TB2.IsHorizontal	= false	;

						TB2.IsStartupToolBar	= true	;
						TB2.TransitionSpeed		=	10		;
						TB2.IsStartupSpanMax	=	false	;

						TB2.ButtonType				= ButtonTypes.TypeFlp	;
						TB2.ColourBack				= System.Drawing.Color.Aquamarine	;

						this.LoadToolbar( TB2 )	;
					}

			#endregion

		}
}
