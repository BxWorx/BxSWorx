using System;
using System.Windows.Forms;
//.........................................................
using BxS_WorxIPX.Main;
//•••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••
namespace BxS_WorxExcel.UI.Core
{
	internal abstract class Controller_Base : IController_Base
		{
			#region "Constructors"

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				internal Controller_Base()
					{
					}

			#endregion

			//===========================================================================================
			#region "Declarations"

				private	Func<object>	_ViewFactory;
				internal	Func<object>	ViewFactory { set	=>	this._ViewFactory	= value ; }

				protected	Form _View;



				private		const DataSourceUpdateMode DSMODE		= DataSourceUpdateMode.OnPropertyChanged;
				//...
				protected	const	string	PNME_VAL		= "Value"		;
				protected	const	string	PNME_CHECK	= "Checked"	;
				protected	const	string	PNME_TEXT		= "Text"		;
				//.................................................
				protected ViewModel_Base _VMBase;
				//...
				public	event	EventHandler	FormClosed;

			#endregion

			//===========================================================================================
			#region "Properties"

				public	string	ID	{ get; set; }

			#endregion

			//===========================================================================================
			#region "Methods: Protected"

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				protected void BindControl( Control control , string cntrlPropName , object dataSource , string vmPropName )
					{
						control.DataBindings.Add( new	Binding(	cntrlPropName
																									, dataSource
																									, vmPropName
																									, true
																									, DSMODE				) );
					}

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				protected virtual void OnFormClosed( object	sender , FormClosedEventArgs e )
					{
						FormClosed?.Invoke( this , e );
					}

			#endregion

			//===========================================================================================
			#region "Methods: Virtual"

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				public	virtual void	Shutdown()		{	}

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				protected	virtual void	PostCreate()	{	}




				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				public	virtual	void	ToggleView()
					{
						if ( this._ViewFactory == null	)		{ return; }
						//...
						if ( this._View	== null )
							{
								this._View = (Form)	this._ViewFactory();
								this._View.FormClosed	+=	this.OnFormClosed	;		// need to know when then FORM closed by user
								this.PostCreate();
							}
						//...
						if ( this._View.Visible )
							{
								if ( this._View.WindowState.Equals( FormWindowState.Minimized ) )
									{	this._View.WindowState = FormWindowState.Normal	; }
								else
									{	this._View.Hide(); }
							}
						else
							{	this._View.Show(); }
					}

			#endregion

		}
}
