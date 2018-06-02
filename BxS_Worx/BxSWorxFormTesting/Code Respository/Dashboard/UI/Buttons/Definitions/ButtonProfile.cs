using System;
using System.Collections.Generic;
using System.Linq;
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
						this.Children		= new	Dictionary<string , IButtonProfile>()	;
					}

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				public	static	IButtonProfile	Create()						=>	new	ButtonProfile();
				public	static	IButtonProfile	Create( string id )	=>	new	ButtonProfile( id );

			#endregion

			//===========================================================================================
			#region "Declarations"

				private readonly Dictionary<string , IButtonProfile>	Children;
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
				public	int					ChildCount	{	get	=>	this.Children.Count		;	}
				public	string			ButtonType	{ get	=>	this.Spec.ButtonType	; }

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				public	IUC_Button	Button			{ get	=>		this._Button;
																					set			{	this._Button	= value	;
																										this.ApplyProfile()		;	} }

			#endregion

			//===========================================================================================
			#region "Methods: Exposed"

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				public void	ApplyProfile()
					{
						if ( this._Button	== null )		{	return; }
						//...
						this._Button.SetFocusColour		=	this.Config.ColourFocus	;
						this._Button.Index						=	item.TabIndex						;
						this._Button.SetName					=	item.ID									;
						this._Button.SetTag						= item.ID									;

						if ( ! buttonType.Equals( ButtonType.Standard ) && ! string.IsNullOrEmpty( item.Text ) )
							{
								lo_Btn.SetText	=	item.Text	;
							}

						if ( ! string.IsNullOrEmpty( item.ImageID	) )
							{
								lo_Btn.SetImage		=	(Image)Resources.ResourceManager.GetObject( item.ImageID );
							}
						//...
						if ( IsRootNode )
							{
								lo_Btn.SetClickEventHandler		= this.OnMenuButton_Click		;
							}
						else
							{
								lo_Btn.SetClickEventHandler		=	this.OnSliderButton_Click	;
							}



						this._Button.SetImage		= this.Spec.ImageID	;

						//buttonProfile.Button.SetImage		=	buttonProfile

						//int			TabIndex		{ get;  set; }
						//string	ID					{ get;  set; }
						//string	ImageID			{ get;  set; }
						//string	Text				{ get;  set; }
						//string	ButtonType	{ get;  set; }

					}

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				public void AddChild( IButtonProfile profile )	=>	this.Children.Add( profile.Spec.ID , profile );

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				public IList<IButtonProfile> GetSubList()	=>	this.Children.Values
																												.OrderByDescending( x	=> x.Spec.TabIndex )
																													.ToList();

			#endregion

		}
}
