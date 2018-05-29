using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
//.........................................................
using BxS_WorxExcel.UI.Menu;
using BxS_WorxExcel.UI.UC;

using BxSWorxFormTesting.Properties;
//•••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••
namespace BxS_WorxExcel.UI.Forms
{
	internal sealed class DBButton
		{
			#region "Constructors"

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				private	DBButton()
					{
						this._SubButtons		=	new	Dictionary<string , IUC_BtnBase>();
					}

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				internal DBButton Create()	=>	new	DBButton()	;

			#endregion
 
			//===========================================================================================
			#region "Declarations"

				internal	IUC_BtnBase												_Button			;
				internal	Dictionary<string , IUC_BtnBase>	_SubButtons	;

			#endregion

			//===========================================================================================
			#region "Properties"

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				internal	int	SubMenuCount	{	get	=>	this._SubButtons.Count	;	}

			#endregion

			//===========================================================================================
			#region "Methods: Exposed"

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				internal IList<IUC_BtnBase> GetSubList()	=>	this._SubButtons.Values
																												.OrderByDescending( x=> x.Index )
																													.ToList();

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				internal	void Configure( IMItem item )
					{
						this._Button	= this.CreateButton( item , true )	;
						//...
						Task.Run(	()	=>	{	foreach ( IMItem lo_Item in item.GetSubList() )
																	{
																		this._SubButtons.Add ( lo_Item.ID , this.CreateButton( lo_Item ) );
																	}
															} ).ConfigureAwait(false);
					}

			#endregion

			//===========================================================================================
			#region "Methods: Private"

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				private IUC_BtnBase	CreateButton( IMItem	item , bool IsRootNode = false )
					{
						IUC_BtnBase	lo_Btn	=	item.ButtonType.Equals(ButtonType.Flipflop)	?								new	UC_BtnFlipFlop()
																																							:	(IUC_BtnBase)	new	UC_BtnSelected() ;
						//...
						lo_Btn.SetFocusColour		=	this.Config.ColourFocus	;
						lo_Btn.Index						=	item.TabIndex						;
						lo_Btn.SetName					=	item.ID									;
						lo_Btn.SetTag						= item.ID									;

						if ( ! item.ButtonType.Equals( ButtonType.Standard ) && ! string.IsNullOrEmpty( item.Text ) )
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
						//...
						return	lo_Btn;
					}


			#endregion

		}
}
