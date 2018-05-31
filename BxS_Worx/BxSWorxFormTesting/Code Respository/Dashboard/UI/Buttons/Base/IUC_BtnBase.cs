using System;
using System.Drawing;
//•••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••
namespace BxS_Worx.UI.Dashboard
{
	public interface IUC_BtnBase
		{
			#region "Properties"

				int		Index	{	get;	set; }
				//...
				string	SetText		{	set; }
				string	SetName		{	set; }
				string	SetTag		{	set; }
				//...
				bool		HasFocus	{	get;	set; }
				//...
				Color		SetBackColour		{	set; }
				Color		SetFocusColour	{	set; }
				//...
				Image		SetImage	{	set; }
				//...
				EventHandler	SetClickEventHandler	{ set; }

			#endregion

		}
}
