using System;
using System.Runtime.Serialization;
using System.Collections.Generic;
//.........................................................
using BxS_SAPGUI.API.DL;
//•••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••
namespace BxS_SAPGUI.COM.DL
{
	[DataContract]
	internal sealed class DTOWorkspace :IDTOWorkspace
		{
			#region "Constructors"

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				internal DTOWorkspace()
					{
						this.Nodes	= new	Dictionary<Guid, IDTONode>();
						this.Items	= new Dictionary<Guid, IDTOItem>();
					}

			#endregion

			//===========================================================================================
			#region "Properties"

				[DataMember]	public Guid		UUID					{ get; set; }
				//...................................................
				[DataMember]	public string Description		{ get; set; }

				[DataMember]	public Dictionary<Guid, IDTONode> Nodes	{ get; set; }
				[DataMember]	public Dictionary<Guid, IDTOItem> Items	{ get; set; }

			#endregion
		}
}