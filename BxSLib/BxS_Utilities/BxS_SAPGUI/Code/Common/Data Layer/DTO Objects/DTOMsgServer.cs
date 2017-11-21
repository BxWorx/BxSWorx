using System;
using System.Runtime.Serialization;
//.........................................................
using BxS_SAPGUI.API.DL;
//•••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••
namespace BxS_SAPGUI.COM.DL
{
	[DataContract]
	internal sealed class DTOMsgServer : IDTOMsgServer
		{
			#region "Properties"

				[DataMember]	public Guid		UUID				{ get; set; }
				//...................................................
				[DataMember]	public string Name				{ get; set; }
				[DataMember]	public string Description	{ get; set; }
				[DataMember]	public string Host				{ get; set; }
				[DataMember]	public string Port				{ get; set; }

			#endregion

		}
}