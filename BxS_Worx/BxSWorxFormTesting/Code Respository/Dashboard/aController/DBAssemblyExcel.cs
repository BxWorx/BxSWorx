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
						IButtonSpec b1	= ButtonSpec.CreateWith( 1 , ButtonTypes.TypeStd , "ID1" , "icons8_Settings_25px" , "Settings" )	;
						IButtonSpec b2	= ButtonSpec.CreateWith( 2 , ButtonTypes.TypeStd , "ID2" , "icons8_Settings_25px" , "Settings" )	;
						IButtonSpec b3	= ButtonSpec.CreateWith( 3 , ButtonTypes.TypeStd , "ID1" , "icons8_Settings_25px" , "Settings" )	;

						IButtonProfile	x1	= ButtonProfile.Create( "x1" );

						x1.Spec				=	b1		;
						x1.ToolbarID	=	"X1"	;
						x1.ScenarioID	= "SC1"	;

						this._BtnProf.Add( x1.ID , x1 );
					}

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				private	void	LoadToolbarsFromSource()
					{
						IToolBarConfig TB1	= ToolBarConfig.CreateWithDefaults();		TB1.ID	= "X1";		TB1.SeqNo	= 1	;
						IToolBarConfig TB2	= ToolBarConfig.CreateWithDefaults();		TB2.ID	= "X2";		TB2.SeqNo	= 2	;
						//...
						TB2.ColourBack					= System.Drawing.Color.Aquamarine;
						TB2.IsHorizontal				= true	;
						TB2.TransitionSpeed		=	10		;
						TB2.TransitionSpanMin	= 03		;

						TB1.IsStartupToolBar		= true	;
						TB1.StartupScenario			=	"SC1"	;
						TB1.TransitionSpanMin		= 03		;
						TB1.ButtonType					= ButtonTypes.TypeStd	;

						//...
						this._ToolBars.Add(	TB1.ID	,	TB1 )	;
						this._ToolBars.Add(	TB2.ID	,	TB2 )	;
					}

			#endregion

		}
}
