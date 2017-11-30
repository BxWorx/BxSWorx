using System;
using System.Runtime.Serialization;
//•••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••
namespace BxS_SAPGUI.COM.DL
{
	[DataContract]

	internal sealed class DTONode : IDTONode
		{
			#region "Properties"

				[DataMember]	public Guid	UUID	{ get; set; }
				//...................................................
				[DataMember]	public string	Description	{ get; set; }
				[DataMember]	public Guid		WSID				{ get; set; }

			#endregion
		}
}