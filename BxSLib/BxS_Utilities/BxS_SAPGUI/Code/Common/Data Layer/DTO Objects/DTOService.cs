using System;
//.........................................................
using SAPGUI.API.DL;
//•••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••
namespace SAPGUI.COM.DL
{
	internal sealed class DTOService : IDTOService
		{
			public Guid		UUID				{ get; set; }
			//...................................................
			public string Name				{ get; set; }
			public string Description	{ get; set; }
			public string SystemID		{ get; set; }
			public string Type				{ get; set; }
			public string Server			{ get; set; }
			public string SNCName			{ get; set; }
			public string SAPCPG			{ get; set; }
			public string DCPG				{ get; set; }
			public string SNCOp				{ get; set; }
			public string Mode				{ get; set; }
			//...................................................
			public Guid		MSID				{ get; set; }
		}
}