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
				public override void	Load()
					{
						base.Load();
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
						IButtonSpec b1	= ButtonSpec.CreateWith( ButtonTypes.TypeStd , "ID1" , ImageNames.Settings	, "Settings" )	;
						IButtonSpec b2	= ButtonSpec.CreateWith( ButtonTypes.TypeFlp , "ID2" , ImageNames.Logo			, "Logo" )	;
						IButtonSpec b3	= ButtonSpec.CreateWith( ButtonTypes.TypeStd , "ID1" , "icons8_Settings_25px" , "Settings" )	;
						//...
						IButtonProfile	BP1	= ButtonProfile.Create( "BP1" );

						BP1.Spec				=	b1		;
						BP1.SeqNo				= 01		;
						BP1.ToolbarID		=	"TB1"	;
						BP1.ScenarioID	= "SC1"	;

						this.LoadButton( BP1 );
						//...
						IButtonProfile	BP2	= ButtonProfile.Create( "BP2" );

						BP2.Spec				=	b2		;
						BP2.SeqNo				= 02		;
						BP2.ToolbarID		=	"TB1"	;
						BP2.ScenarioID	= "SC1"	;

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
