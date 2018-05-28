using System;
using System.Drawing;
//•••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••
namespace BxS_WorxExcel.UI.UC
{
	internal interface IUC_BtnBase
		{
			#region "Properties"

//				bool	SetFocus		{	get;	set; }
				int		Index	{	get;	set; }
				//...
//				string	SetText		{	set; }
//				string	SetName		{	set; }
//				string	SetTag		{	set; }
				//...
				Color		SetBackColour		{	set; }
				Color		SetFocusColour	{	set; }
				//...
				Image		SetImage	{	set; }
				//...
//				EventHandler	SetClickEventHandler	{ set; }

			#endregion

		}
}
