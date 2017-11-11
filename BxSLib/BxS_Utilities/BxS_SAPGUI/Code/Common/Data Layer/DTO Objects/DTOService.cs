using System;
//•••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••
namespace SAPGUI.COM.DL
{
	internal sealed class DTOService
		{
			internal Guid		UUID				{ get; set; }
			//...................................................
			internal string Name				{ get; set; }
			internal string Description	{ get; set; }
			internal string SystemID		{ get; set; }
			internal string Type				{ get; set; }
			internal string Server			{ get; set; }
			internal string SNCName			{ get; set; }
			internal string SAPCPG			{ get; set; }
			internal string DCPG				{ get; set; }
			internal string SNCOp				{ get; set; }
			internal string Mode				{ get; set; }
			//...................................................
			internal Guid		MSID				{ get; set; }
		}
}