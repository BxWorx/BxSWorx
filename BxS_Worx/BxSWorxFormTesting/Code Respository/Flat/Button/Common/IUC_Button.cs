using System;
using System.Drawing;
//•••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••
namespace BxS_WorxExcel.UI.UC
{
	internal interface IUC_Button
		{
			#region "Properties"

				bool					SetFocus							{	get;	set; }
				int						MyTabIndex						{	get;	set; }
				//...
				string				SetText								{	set; }
				string				SetName								{	set; }
				string				SetButtonTag					{	set; }
				Color					SetFocusColour				{	set; }
				Image					SetImage							{	set; }
				EventHandler	SetClickEventHandler	{ set; }

			#endregion

		}
}
