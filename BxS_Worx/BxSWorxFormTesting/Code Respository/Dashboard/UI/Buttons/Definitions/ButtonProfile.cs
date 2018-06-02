using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
//.........
using BxSWorxFormTesting.Properties;
//•••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••
namespace BxS_Worx.Dashboard.UI
{
	public sealed class ButtonProfile	: IButtonProfile
		{
			#region "Constructors"

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				private ButtonProfile( string	id = "" )
					{
						this.ID		= id;
						//...
						this._Children		= new	Dictionary<string , IButtonProfile>()	;
					}

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				public	static	IButtonProfile	Create()							=>	new	ButtonProfile();
				public	static	IButtonProfile	Create( string id )		=>	new	ButtonProfile( id );

			#endregion

			//===========================================================================================
			#region "Declarations"

				private readonly Dictionary<string , IButtonProfile>	_Children;
				//...
				private	IUC_Button _Button;

			#endregion

			//===========================================================================================
			#region "Properties"

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				public	string	ID			{ get;  set; }
				public	int			SeqNo		{ get;  set; }
				//...
				public	string	ScenarioID	{ get;  set; }
				public	string	ToolbarID		{ get;  set; }
				//...
				public	IButtonSpec		Spec		{ get;  set; }
				//...
				public	EventHandler	OnEventClick					{ get;  set; }

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				public	int					ChildCount	{	get	=>	this._Children.Count		;	}
				public	string			ButtonType	{ get; set; }

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				public	IUC_Button	Button			{ get	=>		this._Button;
																					set			{	this._Button	= value	;
																										this.ApplyProfile()		;	} }

				public	Color			ColourBack		{ get;  set; }
				public	Color			ColourFocus		{ get;  set; }
				public	DockStyle	DockStyle			{ get;  set; }

			#endregion

			//===========================================================================================
			#region "Methods: Exposed"

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				public void AddChild( IButtonProfile profile )	=>	this._Children.Add( profile.Spec.ID , profile );

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				public IList<IButtonProfile> GetSubList()	=>	this._Children.Values
																												.OrderByDescending( x	=> x.SeqNo )
																													.ToList();

			#endregion

			//===========================================================================================
			#region "Methods: Private"

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				private void	ApplyProfile()
					{
						if ( this._Button	== null )		{	return; }
						//...
						this._Button.SetDockStyle		=	this.DockStyle		;
						this._Button.SetFocusColour	=	this.ColourFocus	;
						this._Button.SetBackColour	=	this.ColourBack		;
						//...
						this._Button.SetName	= this.Spec.ID				;
						this._Button.SetTag		= this.Spec.ID				;
						this._Button.SetText	=	this.Spec.Text			;
						//...
						if ( ! string.IsNullOrEmpty( this.Spec.ImageID	) )
							{
								this._Button.SetImage		=	(Image)Resources.ResourceManager.GetObject( this.Spec.ImageID );
							}

						////...
						//if ( IsRootNode )
						//	{
						//		lo_Btn.SetClickEventHandler		= this.OnMenuButton_Click		;
						//	}
						//else
						//	{
						//		lo_Btn.SetClickEventHandler		=	this.OnSliderButton_Click	;
						//	}
					}

			#endregion

		}
}
