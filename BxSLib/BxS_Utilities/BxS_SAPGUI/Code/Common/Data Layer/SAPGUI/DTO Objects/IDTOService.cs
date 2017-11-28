using System;
//•••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••
namespace BxS_SAPGUI.COM.DL
{
	internal interface IDTOService
		{
			#region "Properties"

				Guid	UUID	{ get; set; }
				//...................................................
				string	Name				{ get; set; }
				string	Description	{ get; set; }
				string	SystemID		{ get; set; }
				string	SystemNo		{ get; set; }
				string	Type				{ get; set; }
				string	Server			{ get; set; }
				string	SNCName			{ get; set; }
				string	SAPCPG			{ get; set; }
				string	DCPG				{ get; set; }
				string	SNCOp				{ get; set; }
				string	Mode				{ get; set; }
				//...................................................
				Guid	MSID	{ get; set; }

			#endregion

		}
}