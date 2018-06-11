using System.Collections.Generic		;
using System.Drawing								;
using System.Windows.Forms					;
//.........................................................
using BxS_Worx.Dashboard.UI.Button	;
//•••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••
namespace BxS_Worx.Dashboard.UI.Toolbar
{
	internal sealed partial class UC_TBarView : UserControl , IUC_TBarView
		{
			#region "Constructors"

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				internal UC_TBarView(	IUC_TBarSetup	setup )
					{
						InitializeComponent()	;
						//...
						this._Config	= setup	;
					}

			#endregion

			//===========================================================================================
			#region "Declarations"

				private	delegate	int		GetSpan()						;
				private	delegate	void	SetSpan( int size )	;
				//...
				private	readonly	IUC_TBarSetup		_Config		;
				//...
				private GetSpan		_SpanGet	;
				private SetSpan		_SpanSet	;
				//...
				private	bool	_IsClosed	;

			#endregion

			//===========================================================================================
			#region "Properties"

				public	UserControl		ViewUC		{ get	=>	this						; }
				public	Panel					ViewBar		{ get	=>	this.xpnl_Bar		; }
				public	bool					IsClosed	{ get	=>	this._IsClosed	; }

			#endregion

			//===========================================================================================
			#region "Routines: Exposed"


				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				public void Startup()
					{
						this.Initialize()		;
						this.ApplyConfig()	;
					}

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				public void	LoadButtons( IList<IUC_Button> buttonList , bool doLayout = false )
					{
						this.SuspendLayout();
						//...
						this.xpnl_Bar.Controls.Clear();

						foreach ( IUC_Button lo_Btn in buttonList )
							{
								this.xpnl_Bar.Controls.Add( (Control)lo_Btn );
							}
						//...
						this.ResumeLayout( doLayout );
					}

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				public void InvokeTransition( bool quickview = false )
					{
						if ( this._Config.TransitionSpeed.Equals(0) )		{ return;	}
						//...
						int ln_Span		=	this._SpanGet() ;
						//...
						if ( this._IsClosed )
							{
								do	{	this._SpanSet( ln_Span	+= this._Config.TransitionSpeed ) ; }
								while ( this._SpanGet()	< this._Config.TransitionSpanMax );
								//...
								ln_Span		=	this._Config.TransitionSpanMax	;
								this.ShowChildControls( true , ! quickview );
							}
						else
							{
								this.ShowChildControls( false , false );
								do	{	this._SpanSet( ln_Span	-= this._Config.TransitionSpeed ) ;	}
								while ( this._SpanGet()	> this._Config.TransitionSpanMin );
								//...
								ln_Span		=	this._Config.TransitionSpanMin	;
							}
						//...
						if ( ! this._SpanGet().Equals( ln_Span ) )
							{ this._SpanSet( ln_Span); }
						//...
						this._IsClosed		= !this._IsClosed;
					}

			#endregion

			//===========================================================================================
			#region "Routines: Private"

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				private void OnPanelClick(	object sender , System.EventArgs e )	=>	this.InvokeTransition();

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				private void Initialize()
					{
						if ( this._Config.IsHorizontal )
							{
								this.Dock				=	DockStyle.Top					;
								//...
								this._SpanGet		=	this.GetSpanAsHeight	;
								this._SpanSet		=	this.SetSpanAsHeight	;
							}
						else
							{
								this.Dock				=	DockStyle.Left				;
								//...
								this._SpanGet		=	this.GetSpanAsWidth		;
								this._SpanSet		=	this.SetSpanAsWidth		;
							}
						//...
						if (		this._Config.TransitionSpeed.Equals(0)
								||	this._Config.IsStartupSpanMax					 )
							{
								this._SpanSet( this._Config.TransitionSpanMax )	;
								this._IsClosed	= false;
							}
						else
							{
								this._SpanSet( this._Config.TransitionSpanMin )	;
								this._IsClosed	= true;
							}
					}

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				private void ApplyConfig()
					{
						this.BackColor	= this._Config.ColourBack	;
					}

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				private void ShowChildControls( bool visible , bool enabled )
					{
						foreach ( Control lo_Btn in this.xpnl_Bar.Controls )
							{
								lo_Btn.Enabled	=	enabled;
								lo_Btn.Visible	= visible;
							}
					}

			#endregion

			//===========================================================================================
			#region "Delegates: Private"

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				private int GetSpanAsWidth	()	=> this.Size.Width	;
				private int GetSpanAsHeight	()	=> this.Size.Height	;

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				private void SetSpanAsWidth		( int size )	=> this.Size	=	new Size( size	, -1		)	;
				private void SetSpanAsHeight	( int size )	=> this.Size	=	new Size( -1		,	size	)	;

			#endregion

		}
}
