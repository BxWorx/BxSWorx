using System												;
using System.Collections.Generic		;
using System.Linq										;
using System.Windows.Forms					;
//.........................................................
using BxS_Worx.Dashboard.UI.Window	;
//•••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••
namespace BxS_Worx.Dashboard.UI
{
	public abstract class DBAssemblyBase	: IDBAssembly
		{
			#region "Constructors"

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				protected DBAssemblyBase()
					{
						this._ToolBars	= new	Dictionary<string, IUC_TBarSetup>	()	;
						this._BtnProf		= new	Dictionary<string, IButtonProfile>()	;
					}

			#endregion

			//===========================================================================================
			#region "Declarations"

				private	readonly	Dictionary<string , IUC_TBarSetup>		_ToolBars	;
				private	readonly	Dictionary<string , IButtonProfile>		_BtnProf	;

			#endregion

			//===========================================================================================
			#region "Properties"

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				public	IDB_ViewConfig	FormConfig	{ get;	private set; }

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				public	IList<IUC_TBarSetup>	ToolBarList	{ get	=>	this._ToolBars.Values
																															.OrderByDescending( x => x.SeqNo )
																																.ToList(); }

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				public	IList<IButtonProfile>	ButtonList	{ get	=>	this._BtnProf.Values
																															.OrderByDescending( x => x.SeqNo )
																																.ToList(); }

			#endregion

			//===========================================================================================
			#region "Methods: Exposed"

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				public virtual void	LoadFromSource()
					{
						this.FormConfig		=	DB_Factory.CreateDBViewConfigWithDefaults();
					}

			#endregion

			//===========================================================================================
			#region "Methods: Local"

				////¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				//protected	bool ValidateButtonSpec( IButtonSpec btnSpec )
				//	{
				//		bool	lb_Ret	= true;
				//		//...
				//		//...
				//		return	lb_Ret	;
				//	}

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				protected	bool LoadButton( IButtonProfile btnProfile )
					{
						bool	lb_Ret	= false;
						//...
						IUC_TBarSetup lo_TBarCfg		= this.GetTBarConfig( btnProfile.ToolbarID );

						if ( lo_TBarCfg != null )
							{
								btnProfile.ColourBack		=	lo_TBarCfg.ColourBack		;
								btnProfile.ColourFocus	=	lo_TBarCfg.ColourFocus	;

								btnProfile.DockStyle		=	lo_TBarCfg.IsHorizontal		?	DockStyle.Left
																																		: DockStyle.Top		;

								if ( ! lo_TBarCfg.ButtonType.Equals( ButtonTypes.TypeAll ) )
									{
										btnProfile.ButtonType		=	lo_TBarCfg.ButtonType ;
									}
								//...
								lb_Ret	= true;
							}
						//...
						if ( lb_Ret )
							{
								this._BtnProf.Add( btnProfile.ID , btnProfile );
							}
						//...
						return	lb_Ret	;
					}

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				protected	bool LoadToolbar( IUC_TBarSetup tbarConfig )
					{
						bool	lb_Ret	= true;
						//...
						//...
						if ( lb_Ret )
							{
								this._ToolBars.Add( tbarConfig.ID , tbarConfig );
							}
						//...
						return	lb_Ret	;
					}

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				protected void OnButtonClick_RouteScenario( object sender , EventArgs e	)
					{
						var	lo_Btn	= (Control)sender				;
						var	lo_Tag	= (TagData) lo_Btn.Tag	;
						//...
					}

			#endregion

			//===========================================================================================
			#region "Methods: Private"

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				private	IUC_TBarSetup GetTBarConfig( string	id )
					{
						if ( this._ToolBars.TryGetValue(id , out IUC_TBarSetup lo_Cfg) )
							{	}
						//...
						return	lo_Cfg;
					}

			#endregion

			//*******************************************************************************************
			protected	class TagData
				{
					//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
					public TagData()
						{	}
					//...
					public string	Scenario	{ get; set; }
					public string	ToolBar		{ get; set; }
				}
			//...
		}
}
