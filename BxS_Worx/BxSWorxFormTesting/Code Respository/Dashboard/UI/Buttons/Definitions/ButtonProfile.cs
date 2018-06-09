using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
//.........
using BxSWorxFormTesting.Properties;
//•••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••
namespace BxS_Worx.Dashboard.UI.Button
{
	public sealed class ButtonProfile	: IButtonProfile
		{
			#region "Constructors"

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				internal ButtonProfile( string	id = "" )
					{
						this.ID		= id;
						//...
						//this._Children	= new	Dictionary<string , IButtonProfile>()	;
						this._IsReady		= false	;
					}

			#endregion

			//===========================================================================================
			#region "Declarations"

				//private readonly Dictionary<string , IButtonProfile>	_Children;
				//...
				private	bool	_IsReady;

			#endregion

			//===========================================================================================
			#region "Properties"

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				public	string	ID					{ get;	set; }
				public	int			SeqNo				{ get;  set; }
				//...
				public	string	ScenarioID	{ get;  set; }
				public	string	ToolbarID		{ get;  set; }
				public	string	ImageID			{ get;  set; }
				public	string	Text				{ get;  set; }

				public	IButtonTag	Tag					{ get;  set; }

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				//public	int			ChildCount	{	get	=>	this._Children.Count ;	}
				public	string	ButtonType	{ get;	set; }

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				public	Color					ColourBack			{ get;  set; }
				public	Color					ColourFocus			{ get;  set; }
				public	DockStyle			DockStyle				{ get;  set; }
				public	DockStyle			FocusDocking		{ get;  set; }
				public	IUC_Button		Button					{ get;	set; }
				public	EventHandler	OnClickHandler	{ get;  set; }

			#endregion

			//===========================================================================================
			#region "Methods: Exposed"

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				public void	ApplyProfile()
					{
						if ( this.Button	== null )		{	return	; }
						if ( this._IsReady )					{ return	; }
						//...
						this.Button.SetDockStyle					=	this.DockStyle			;
						this.Button.SetFocusDocking				=	this.FocusDocking		;
						this.Button.SetFocusColour				=	this.ColourFocus		;
						this.Button.SetBackColour					=	this.ColourBack			;
						this.Button.ID										= this.ID							;
						this.Button.SetName								= this.ID							;
						this.Button.SetTag								= this.Tag						;
						this.Button.SetText								=	this.Text						;
						//...
						this.Button.SetClickEventHandler	=	this.OnClickHandler	;
						//...
						if ( ! string.IsNullOrEmpty( this.ImageID	) )
							{
								this.Button.SetImage	=	(Image)Resources.ResourceManager.GetObject( this.ImageID );
							}
						//...
						this._IsReady	= true ;
					}

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				//public void AddChild( IButtonProfile profile )	=>	this._Children.Add( profile.ID , profile );

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				//public IList<IButtonProfile> GetSubList()				=>	this._Children.Values
				//																											.OrderByDescending( x	=> x.SeqNo )
				//																											.ToList();

			#endregion

		}
}
