using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
//•••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••
namespace BxS_SAPGUI.COM.DL
{
	[DataContract]
	internal sealed class DTONode : IDTONode
		{
			#region "Constructors"

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				internal DTONode()
					{
						this.Items	= new Dictionary<Guid, IDTOItem>();
					}

			#endregion

			//===========================================================================================
			#region "Properties"

				[DataMember]	public Guid	UUID				{ get; set; }
				//...................................................
				[DataMember]	public string	Description	{ get; set; }

				[DataMember]	public Dictionary<Guid, IDTOItem>	Items	{ get; set; }
				//...................................................
				[DataMember]	public Guid	WSID	{ get; set; }

			#endregion
		}
}