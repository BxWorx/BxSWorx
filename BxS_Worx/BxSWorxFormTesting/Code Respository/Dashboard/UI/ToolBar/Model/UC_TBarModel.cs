using System.Linq;
using System.Collections.Generic;
//.........................................................
using BxS_Worx.Dashboard.UI.Button;
//•••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••
namespace BxS_Worx.Dashboard.UI.Toolbar
{
	internal class UC_TBarModel : IUC_TBarModel
		{
			#region "Constructors"

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				internal UC_TBarModel()
					{
						this._Scenarios		= new	Dictionary<string , Dictionary<string , IButtonProfile>>();
					}

			#endregion

			//===========================================================================================
			#region "Declarations"

				private readonly Dictionary<string , Dictionary<string , IButtonProfile>>	_Scenarios	;

			#endregion

			//===========================================================================================
			#region "Properties"

				public	IUC_TBarSetup		Setup		{ get;	set; }

			#endregion

			//===========================================================================================
			#region "Methods: Exposed"

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				public IList<IButtonProfile>	ScenarioButtons( string scenarioID )
					{
						IList<IButtonProfile> lt_List		= new	List<IButtonProfile>();
						//...
						if ( this._Scenarios.TryGetValue( scenarioID , out Dictionary<string , IButtonProfile> lt_Btns ) )
							{
								foreach ( IButtonProfile lo_Btn in lt_Btns.Values.OrderByDescending( x => x.SeqNo ).ToList() )
									{
										lt_List.Add( lo_Btn );
									}
							}
						//...
						return	lt_List;
					}

				////¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				//public IUC_Button	GetButton	( string scenarioID , string buttonID )
				//	{
				//		IUC_Button lo_Btn	= null ;
				//		//...
				//		if ( this._Scenarios.TryGetValue( scenarioID , out Dictionary<string , IUC_Button> lt_Btns ) )
				//			{
				//				lt_Btns.TryGetValue( buttonID , out lo_Btn )	;
				//			}
				//		//...
				//		return	lo_Btn ;
				//	}

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				public	void	LoadButton( IButtonProfile buttonProfile )
					{
						if ( ! this._Scenarios.TryGetValue( buttonProfile.ScenarioID , out Dictionary<string , IButtonProfile> lt_Btns ) )
							{
								this.AddScenario( buttonProfile.ScenarioID );
								if ( ! this._Scenarios.TryGetValue( buttonProfile.ScenarioID , out lt_Btns ) )
									{	return ; }
							}
						//...
						lt_Btns.Add( buttonProfile.ID , buttonProfile );
					}

			#endregion

			//===========================================================================================
			#region "Routines: Private"

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				private void	AddScenario( string id )	=>	this._Scenarios.Add(	id
																																				, new	Dictionary<string , IButtonProfile>() );

			#endregion

		}
}
