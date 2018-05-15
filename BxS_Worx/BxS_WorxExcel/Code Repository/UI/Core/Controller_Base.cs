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
				internal Controller_Base( string id )
					{
						this.ID	= id;
					}

			#endregion

			//===========================================================================================
			#region "Declarations"

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

				public	string	ID	{ get; }
				//...
				protected	IIPX_Controller	IPXCntlr		{ get	=> IPX_Controller.Instance;	}

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
				public	virtual	void	ToggleView()	{	}

			#endregion

		}
}
