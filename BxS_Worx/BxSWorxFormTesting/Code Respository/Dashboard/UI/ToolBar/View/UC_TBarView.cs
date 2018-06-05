using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
//•••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••
namespace BxS_Worx.Dashboard.UI.Toolbar
{
	internal sealed partial class UC_TBarView : UserControl , IUC_TBarView
		{
			#region "Constructors"

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				internal UC_TBarView()
					{
						InitializeComponent()	;
					}

			#endregion

			//===========================================================================================
			#region "Declarations"

				private	delegate	int		GetSpan()						;
				private	delegate	void	SetSpan( int size )	;
				//...
				private	IUC_TBarViewConfig	_Config		;
				private GetSpan							_SpanGet	;
				private SetSpan							_SpanSet	;
				//...
				private	bool								_IsClosed	;

			#endregion

			//===========================================================================================
			#region "Properties"

				public	IUC_TBarViewConfig	Config	{ set	{	this._Config	= value ;
																										this.ApplyConfig()		;	} }
				//...
				public	UserControl					ViewUC	{ get	=>	this	; }

			#endregion

			//===========================================================================================
			#region "Routines: Exposed"

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
				public void InvokeTransition()
					{
						if ( ! this._Config.CanTransition )		{ return;	}
						//...
						int ln_Span		=	0;
						//...
						if ( this._IsClosed )
							{
								do	{	this._SpanSet( this._SpanGet() + this._Config.TransitionSpeed );	}
								while ( this._SpanGet()	< this._Config.TransitionSpanMax );
								//...
								ln_Span	=	this._Config.TransitionSpanMax	;
							}
						else
							{
								do	{	this._SpanSet( this._SpanGet() - this._Config.TransitionSpeed );	}
								while ( this._SpanGet()	> this._Config.TransitionSpanMin );
								//...
								ln_Span	=	this._Config.TransitionSpanMin	;
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
				private void ApplyConfig()
					{
						this.Prepare();
						//...
						this.BackColor	= this._Config.ColourBack	;
					}

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				private void Prepare()
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
						this._IsClosed	= true;
						this._SpanSet( this._Config.TransitionSpanMin );

					}

			#endregion

			//===========================================================================================
			#region "Delegates: Private"

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				private int GetSpanAsWidth	()	=> this.Size.Width	;
				private int GetSpanAsHeight	()	=> this.Size.Height	;

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				private void SetSpanAsWidth		( int size )	=> this.Size =	new Size( size	, -1		)	;
				private void SetSpanAsHeight	( int size )	=> this.Size =	new Size( -1		,	size	)	;

			#endregion

		}
}
