using System												;
using System.Collections.Generic		;
using System.Drawing								;
using System.Threading							;
using System.Threading.Tasks				;
using System.Windows.Forms					;
//.........................................................
using BxS_Worx.Dashboard.UI.Button	;
//•••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••
namespace BxS_Worx.Dashboard.UI.Toolbar
{
	internal class UC_TBarScenario
		{
			#region "Constructors"

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				internal UC_TBarScenario( string	id )
					{
						if ( string.IsNullOrEmpty( id ) )		{	throw	new	Exception("")	; }
						//...
						this._Buttons		=	new	Dictionary<string , IUC_Button>()	;
						this._Lock			=	new	object()	;
					}

			#endregion

			//===========================================================================================
			#region "Declarations"

				private readonly Dictionary<string , IUC_Button>	_Buttons	;
				private readonly object														_Lock	;

			#endregion

			//===========================================================================================
			#region "Properties"

				internal	string	ID				{ get; }
				internal	bool		IsReady		{	get;	set; }

			#endregion

			//===========================================================================================
			#region "Methods: Exposed"

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				internal void	CreateButtons(	IUC_TBarModel	model
																		,	EventHandler	onButtonClick	)
					{
						if (		this.IsReady )										{	return ; }
						if ( !	Monitor.TryEnter( this._Lock ) )	{ return ; }
						//...
						try
							{
								Task.Run(
									()=>	{
													foreach ( IButtonProfile lo_BtnProf in model.ScenarioButtons( this.ID ) )
														{
															if (		lo_BtnProf.OnClickHandler == null									)		lo_BtnProf.OnClickHandler		= onButtonClick							;
															if (	! model.Setup.FocusDocking	.Equals(DockStyle.None)	)		lo_BtnProf.FocusDocking			= model.Setup.FocusDocking	;
															if (	! model.Setup.ColourFocus		.Equals(Color.Empty)		)		lo_BtnProf.ColourFocus			= model.Setup.ColourFocus		;
															//...
															IUC_Button lo_Btn		= DB_Factory.CreateButton( lo_BtnProf ) ;
															lo_Btn.ApplyProfile()		;
															lo_Btn.CompileButton()	;
															//...
															this._Buttons.Add( lo_BtnProf.ID , lo_Btn ) ;
														}
													//...
													this.IsReady	= true ;
												}	);
							}
						finally
							{
								Monitor.Exit( this._Lock ) ;
							}
					}

			#endregion

		}
}
