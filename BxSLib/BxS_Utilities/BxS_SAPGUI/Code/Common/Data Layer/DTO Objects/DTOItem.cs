using System;
using System.Runtime.Serialization;
//•••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••
namespace BxS_SAPGUI.COM.DL
{
	[DataContract]

	internal sealed class DTOItem : IDTOItem
		{
			#region "Properties"

				[DataMember]	public Guid	UUID			{ get; set; }
				//.................................................
				[DataMember]	public Guid	ServiceID	{ get; set; }
				//.................................................
				[DataMember]	public Guid	WSID			{ get; set; }
				[DataMember]	public Guid	NodeID		{ get; set; }

			#endregion
		}
}