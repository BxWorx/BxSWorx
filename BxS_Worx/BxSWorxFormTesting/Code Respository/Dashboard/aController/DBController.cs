using System;
using System.Collections.Generic;
using System.Threading;
//•••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••
namespace BxS_Worx.UI.Dashboard
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
						this._ToolBars	= new	Dictionary<string, IToolBarConfig>()	;
						this._BtnSpecs	= new	Dictionary<string, IButtonSpec>()			;
					}

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				public static DBController Create()	=>	new DBController();

			#endregion

			//===========================================================================================
			#region "Declarations"

				private readonly Lazy<BxS_DashboardForm>	_DBForm		;
				//...
				private readonly Dictionary<string , IToolBarConfig>	_ToolBars	;
				private readonly Dictionary<string , IButtonSpec>			_BtnSpecs	;

			#endregion

			//===========================================================================================
			#region "Properties"

				public	BxS_DashboardForm		Form	{	get => this._DBForm.Value; }

			#endregion

			//===========================================================================================
			#region "Methods: Exposed"

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				internal void	AssembleDashboard( IDBAssembly assembly )
					{
						this._DBForm.Value.Config	= assembly.FormConfig	;
						//...
						this.AssembleToolbars( assembly.ToolBarList )	;

						var y = ButtonFactory.GetAllEntities();
						var x = ButtonFactory.Get( ButtonTypes.TypeStandard );
						var z = ButtonFactory.GetAll<IUC_BtnBase>();
						var t = ButtonFactory.GetButtonTypes();
					}

			#endregion

			//===========================================================================================
			#region "Methods: Private"

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				private	void	AssembleToolbars( IList<IToolBarConfig>	tbarList )
					{
						foreach ( IToolBarConfig lo_TBCfg in tbarList )
							{
								var lo_TBar		= UC_ToolBar.CreateWithConfig( lo_TBCfg );
								this.Form.LoadToolbar( lo_TBar )	;
							}
					}

			#endregion

		}
}
