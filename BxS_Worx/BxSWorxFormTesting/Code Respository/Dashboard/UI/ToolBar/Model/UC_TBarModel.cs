using System.Linq;
using System.Collections.Generic;
//•••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••
namespace BxS_Worx.Dashboard.UI.Toolbar
{
	internal class UC_TBarModel : IUC_TBarModel
		{
			#region "Constructors"

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				internal UC_TBarModel()
					{
						this._Scenarios		= new	Dictionary<string , Dictionary<string , IUC_Button>>();
					}

			#endregion

			//===========================================================================================
			#region "Declarations"

				private readonly Dictionary<string , Dictionary<string , IUC_Button>>	_Scenarios	;

			#endregion

			//===========================================================================================
			#region "Methods: Exposed"

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				public IList<IUC_Button>	ScenarioButtons( string scenarioID )
					{
						IList<IUC_Button> lt_List		= new	List<IUC_Button>();
						//...
						if ( this._Scenarios.TryGetValue( scenarioID , out Dictionary<string , IUC_Button> lt_Btns ) )
							{
								foreach ( IUC_Button lo_Btn in lt_Btns.Values.OrderByDescending( x => x.Index ).ToList() )
									{
										lt_List.Add( lo_Btn );
									}
							}
						//...
						return	lt_List;
					}

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				public IUC_Button	GetButton	( string scenarioID , string buttonID )
					{
						IUC_Button lo_Btn	= null ;
						//...
						if ( this._Scenarios.TryGetValue( scenarioID , out Dictionary<string , IUC_Button> lt_Btns ) )
							{
								lt_Btns.TryGetValue( buttonID , out lo_Btn )	;
							}
						//...
						return	lo_Btn ;
					}

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				public	void	LoadButton( string scenarioID , IUC_Button button )
					{
						if ( ! this._Scenarios.TryGetValue( scenarioID , out Dictionary<string , IUC_Button> lt_Btns ) )
							{
								this.AddScenario( scenarioID );
								if ( ! this._Scenarios.TryGetValue( scenarioID , out lt_Btns ) )
									{	return ; }
							}
						//...
						lt_Btns.Add( button.ID , button );
					}

			#endregion

			//===========================================================================================
			#region "Routines: Private"

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				private void	AddScenario( string id )	=>	this._Scenarios.Add(	id
																																				, new	Dictionary<string , IUC_Button>() );

			#endregion

		}
}
