using System;
using BxS_WorxExcel.UI.Core;
//•••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••
namespace BxS_WorxExcel.UI.Forms
{
	internal class Controller_FormX : Controller_Base
		{
			#region "Constructors"

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				public Controller_FormX(	string id	)
					{
						this.ID	= id;
						//...
						this.ViewFactory	=	this.CreateView;

						//this._MD	=	new	Lazy<Model_FormX>			(	()=>	new	Model_FormX( )						);
						//this._VM	=	new Lazy<ViewModel_FormX>	(	()=>	new	ViewModel_FormX( this._MD.value )	);
					}

			#endregion

				private	readonly	Lazy<Model_FormX>			_MD;
				private	readonly	Lazy<ViewModel_FormX>	_VM;

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				protected	override void	PostCreate()
					{
						//var x = (FormX) this._View;

						//x.xdgv_Main.DataSource	= this._VM.Value.BDC_BS;
						//x.xdgv_Main.SelectionChanged	+= this.xdgv_Main_SelectionChanged;
						////x.xtbx_Test.DataBindings.Add("Text", this._VM.Value.BDCList, "UserID");
					}

				//private void xdgv_Main_SelectionChanged(object sender , System.EventArgs e)
				//	{
				//	}

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				private	FormX CreateView()	=>	new	FormX();

			//.

		}
}
