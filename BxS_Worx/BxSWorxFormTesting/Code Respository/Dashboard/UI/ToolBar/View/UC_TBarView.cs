using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
//.........................................................
using BxS_Worx.Dashboard.Utilities;
//•••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••
namespace BxS_Worx.Dashboard.UI.Toolbar
{
	internal sealed partial class UC_TBarView : UserControl , IUC_TBarView
		{
			#region "Constructors"

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				private UC_TBarView( bool	isHori	= false )
					{
						InitializeComponent()	;
						//...
						this.Dock		=	isHori	?	DockStyle.Top	: DockStyle.Left	;

						if ( isHori )
							{
								this.Dock				=	DockStyle.Top				;
								//...
								this._SpanGet		=	this.GetSpanAsHeight	;
								this._SpanSet		=	this.SetSpanAsHeight	;
							}
						else
							{
								this.Dock				=	DockStyle.Left			;
								//...
								this._SpanGet		=	this.GetSpanAsWidth	;
								this._SpanSet		=	this.SetSpanAsWidth	;
							}
					}

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				internal	static	UC_TBarView Create(		bool								isHorizontal
																							,	IUC_TBarViewConfig	config				)
					{
						return	new UC_TBarView(isHorizontal)	{	_Config = config };
					}

			#endregion

			//===========================================================================================
			#region "Declarations"

				private	IUC_TBarViewConfig	_Config	;
				//...
				private	delegate	int		GetSpan()						;
				private	delegate	void	SetSpan( int size )	;
				//...
				private readonly GetSpan	_SpanGet;
				private readonly SetSpan	_SpanSet;
				//...
				private	bool	IsClosed;

			#endregion

			//===========================================================================================
			#region "Properties"

				public	UserControl		ViewUC				{ get	=>	this					; }
				public	Control				ButtonCanvas	{ get	=>	this.xpnl_Bar	; }
				//...
				public	IUC_TBarViewConfig	Config	{ set	=> this.ApplyConfig( value )	; }


				////¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				//// *** Public
				////¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				//public IToolBarConfig	Config	{ get	=>		this._Config	;
				//																set			{	this._Config	= value	;
				//																					this.ApplyConfig()		;	}	}

				////¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				//// *** Private
				////¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				//private	bool		Horizontal			{ get	=>		this._Config.IsHorizontal								;	}
				//private	Color		ColourBack			{ get	=>		this._Config.ColourBack									; }
				//private	Color		ColourFocus			{ get	=>		this._Config.ColourFocus								; }
				//private	bool		CanTransition		{ get	=>	!	this._Config.TransitionSpeed.Equals(0)	; }

				////¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				//private	int			Span	{	get	=>		this.Horizontal	?	this.Size.Height :	this.Size.Width ;

				//												set			{	if ( this.Horizontal )	{	this.Size =	new Size( -1 , value )	;	}
				//																	else										{	this.Size =	new Size( value , -1 )	;	} }	}

			#endregion

			//===========================================================================================
			#region "Routines: Exposed"

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				public void InvokeTransition()
					{
						if ( ! this._Config.CanTransition )		{ return;	}
						//...
						int ln_Span	=	0;
						//...
						if ( this.IsClosed )
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
						this.IsClosed	= !this.IsClosed;
					}

			#endregion

			//===========================================================================================
			#region "Routines: Private"

				Color			ColourBack					{ get;  set; }
				Color			ColourFocus					{ get;  set; }
				//...
				bool			CanTransition				{ get;  set; }

				int				TransitionSpanMin		{ get;  set; }
				int				TransitionSpanMax		{ get;  set; }
				int				TransitionSpeed			{ get;  set; }

				
				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				private void ApplyConfig()
					{
						this.SuspendLayout();
						//...
						this._Config.ColourBack		= config.ColourBack				;
						this.BackColor						= this._Config.ColourBack	;
						//this.Span				=	this._Config.ShowOnstartup	?	this._Config.TransitionSpanMax
						//																							: this._Config.TransitionSpanMin ;
						//this.Dock				=	this._Config.Dock	;
						//...
						this.ResumeLayout(false);
					}

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				private void UpdateConfig( IUC_TBarViewConfig	config )
					{
						this._Config.ColourBack		= config.ColourBack				;
				Color			ColourBack					{ get;  set; }
				Color			ColourFocus					{ get;  set; }
				//...
				bool			CanTransition				{ get;  set; }

				int				TransitionSpanMin		{ get;  set; }
				int				TransitionSpanMax		{ get;  set; }
				int				TransitionSpeed			{ get;  set; }
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
