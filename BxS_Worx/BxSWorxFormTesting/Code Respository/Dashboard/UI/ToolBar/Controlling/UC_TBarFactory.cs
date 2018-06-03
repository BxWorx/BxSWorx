using System.Drawing;
using BxS_Worx.Dashboard.UI.Toolbar;
//•••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••
namespace BxS_Worx.Dashboard.UI
{
	internal static class UC_TBarFactory
		{
			#region "Properties"

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				internal	static	IUC_TBarViewConfig	lo_TBVCfg		=	UC_TBarViewConfig.CreateWithDefaults();
				internal	static	IUC_TBarModel				lo_TBModel	=	new	UC_TBarModel();




				internal	static	IUC_TBarView				lo_TBView		=	UC_TBarView.Create()	;

				internal	static	UC_TBarPresenter		CreatePresenter(	IUC_TBarViewConfig	config
																															,	IUC_TBarModel				model
																															,	IUC_TBarView				view		)		=>	new	UC_TBarPresenter(		config
																																																													,	model
																																																													,	view		)	;

			#endregion

		}
}
