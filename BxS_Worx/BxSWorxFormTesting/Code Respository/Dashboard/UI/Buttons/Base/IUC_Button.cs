using System;
using System.Drawing;
using System.Windows.Forms;
//•••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••
namespace BxS_Worx.Dashboard.UI
{
	public interface IUC_Button
		{
			#region "Properties"

				string ID		{	get;	set; }
				int		Index	{	get;	set; }
				//...
				string	SetText		{	set; }
				string	SetName		{	set; }
				object	SetTag		{	set; }
				//...
				bool		HasFocus	{	get;	set; }
				//...
				Color		SetBackColour		{	set; }
				Color		SetFocusColour	{	set; }
				//...
				Image		SetImage	{	set; }
				//...
				EventHandler	SetClickEventHandler	{ set; }

				DockStyle	SetDockStyle	{	set; }

			#endregion

		}
}
