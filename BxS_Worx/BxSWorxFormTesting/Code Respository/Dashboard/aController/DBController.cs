using System;
using System.Collections.Generic;
using System.Threading;
using System.Linq;
//.........................................................
using BxS_Worx.Dashboard.UI.Buttons;
//•••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••
namespace BxS_Worx.Dashboard.UI
{
	//***********************************************************************************************
	public sealed class DBController
		{
			#region "Constructors"

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				private DBController()
					{
						this._DBForm		=	new	Lazy<BxS_DashboardForm>	(	()=>		BxS_DashboardForm.Create()
																																	, LazyThreadSafetyMode.ExecutionAndPublication	);
						//...
						this._ToolBars	= new	Dictionary<string, UC_ToolBar>()	;
						this._BtnSpecs	= new	Dictionary<string, IButtonSpec>()	;
					}

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				public static DBController Create()	=>	new DBController();

			#endregion

			//===========================================================================================
			#region "Declarations"

				private readonly	Lazy<BxS_DashboardForm>		_DBForm		;
				//...
				private	IDBAssembly		_Assembly	;
				//...
				private readonly Dictionary<string , UC_ToolBar>		_ToolBars	;
				private readonly Dictionary<string , IButtonSpec>		_BtnSpecs	;

			#endregion

			//===========================================================================================
			#region "Properties"

				public	BxS_DashboardForm		Form	{	get => this._DBForm.Value; }
				//...
				public	IDBAssembly		Assembly	{ set	=>	this._Assembly	= value;	}

			#endregion

			//===========================================================================================
			#region "Methods: Exposed"

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				internal void	AssembleDashboard()
					{
						if ( this._Assembly	== null	)		return;
						//...
						this._DBForm.Value.Config		=	this._Assembly.FormConfig	;
						//...
						this.AssembleToolbars()	;
						this.AssembleButtons()	;
						//...
						this.LoadToolBarsOntoForm();
						//var b1	= ButtonFactory.CreateButton( ButtonTypes.TypeStandard );
						//var b2	= ButtonFactory.CreateButton( ButtonTypes.TypeFlipFlop );
					}

			#endregion

			//===========================================================================================
			#region "Methods: Private"

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				private	void	LoadToolBarsOntoForm()
					{
						foreach ( UC_ToolBar lo_TBar in this._ToolBars.Values )
							{
								this._DBForm.Value.LoadToolbar( lo_TBar );
							}
					}

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				private	void	AssembleButtons()
					{
						IToolBarConfig	lo_TBCfg		=	null					;
						string					lc_BtnType	=	string.Empty	;
						//...
						foreach ( IButtonProfile lo_Btn in this._Assembly.ButtonList )
							{
								lo_TBCfg	=	this._Assembly.GetToolbarConfig( lo_Btn.ToolbarID )	;
								//...
								if ( lo_TBCfg == null )
									{	lc_BtnType	=	lo_Btn.Spec.ButtonType;	}
								else
									{
										lc_BtnType	=	lo_TBCfg.Equals(ButtonTypes.TypeAll)	?	lo_Btn.Spec.ButtonType
																																				:	lo_TBCfg.ButtonType			;
									}
								//...
								lo_Btn.Button		=	ButtonFactory.CreateButton( lo_Btn.Spec.ButtonType );
							}
					}

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				private	void	AssembleToolbars()
					{
						foreach ( IToolBarConfig lo_TBCfg in	this._Assembly.ToolBarList )
							{
								this._ToolBars.Add( lo_TBCfg.ID , UC_ToolBar.CreateWithConfig( lo_TBCfg ) )	;
							}
					}

			#endregion

		}
}
