using System												;
using System.Collections.Generic		;
using System.Drawing								;
using System.Linq;
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
						this._CurButton	= null					;
					}

			#endregion

			//===========================================================================================
			#region "Declarations"

				private readonly	Dictionary<string , IUC_Button>		_Buttons	;
				//...
				private readonly	object		_Lock	;
				//...
				private	IUC_Button		_CurButton	;

			#endregion

			//===========================================================================================
			#region "Properties"

				internal	string	ID				{ get; }
				internal	bool		IsReady		{	get;	set; }

				private		


			#endregion

			//===========================================================================================
			#region "Methods: Exposed"

				internal	void ChangeFocus( IButtonTag	lo_Tag )
					{
						if ( this._CurButton != null )
							{
								if ( lo_Tag.ButtonID.Equals(	this._CurButton.ID ) )
									{	return ; }
								//...
								this._CurButton.HasFocus	= ! this._CurButton.HasFocus ;
							}
						//...
						if ( this._Buttons.TryGetValue( lo_Tag.ButtonID , out this._CurButton ) )
							{
								this._CurButton.HasFocus	= ! this._CurButton.HasFocus ;
							}

						if (		! string.IsNullOrEmpty( this._CurScenario )
								&&	! string.IsNullOrEmpty( this._CurButton		) )
							{
								if (		lo_Tag.ScenarioID.Equals( this._CurScenario )
										&&	lo_Tag.ButtonID.Equals	(	this._CurButton		) )
									{ return ; }
								//...
								IUC_Button lo_BtnSrc	= this.GetButton( this._CurScenario , this._CurButton );
								lo_BtnSrc.HasFocus		= ! lo_BtnSrc.HasFocus;
							}
						//...
						IUC_Button lo_BtnTrg	= this.GetButton( lo_Tag.ScenarioID , lo_Tag.ButtonID );
						lo_BtnTrg.HasFocus		= ! lo_BtnTrg.HasFocus;
						//...
						this._CurScenario	= lo_Tag.ScenarioID	;
						this._CurButton		= lo_Tag.ButtonID		;

					}

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				internal IList<IUC_Button> ScenarioButtons()
					{
						IList<IUC_Button> lt_List		= new	List<IUC_Button>();
						//...
						foreach ( IUC_Button lo_Btn in this._Buttons.Values.OrderByDescending( x => x.Index ).ToList() )
							{
								lt_List.Add( lo_Btn );
							}
						//...
						return	lt_List;
					}

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
