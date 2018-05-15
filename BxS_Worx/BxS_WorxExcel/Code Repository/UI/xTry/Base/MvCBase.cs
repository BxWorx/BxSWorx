using System;
using System.Windows.Forms;
//.........................................................
using BxS_WorxIPX.Main;
//•••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••
namespace BxS_WorxExcel.MVVM
{
	internal abstract class MvCBase : IMvC
		{
			#region "Constructors"

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				internal MvCBase( string id )
					{
						this.ID	= id;
					}

			#endregion

			//===========================================================================================
			#region "Properties"

				public	string	ID	{ get ;}
				//.................................................
				protected	IIPX_Controller	IPXCntlr		{ get	{	return	IPX_Controller.Instance;	}	}

			#endregion

			//===========================================================================================
			#region "Declarations"

				private const DataSourceUpdateMode DSMODE	= DataSourceUpdateMode.OnPropertyChanged;
				//.................................................
				protected	const	string	PNME_VAL		= "Value"		;
				protected	const	string	PNME_CHECK	= "Checked"	;
				protected	const	string	PNME_TEXT		= "Text"		;
				//.................................................
				protected ViewModelBase _VMBase;

				public	event EventHandler	FormClosing;

			#endregion

			//===========================================================================================
			#region "Methods: Protected"

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				protected void BindControl( Control control , string cntrlPropName , object dataSource , string vmPropName )
					{
						control.DataBindings.Add( new	Binding( cntrlPropName , dataSource	, vmPropName , true , DSMODE ) );
					}

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				protected virtual void OnFormClosing(object sender , FormClosedEventArgs e)
					{
						FormClosing?.Invoke( this , e );
					}

			#endregion

			//===========================================================================================
			#region "Methods: Virtual"

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				public	virtual void	Shutdown()		{	}
				public	virtual	void	ToggleView()	{	}

			#endregion

		}
}
