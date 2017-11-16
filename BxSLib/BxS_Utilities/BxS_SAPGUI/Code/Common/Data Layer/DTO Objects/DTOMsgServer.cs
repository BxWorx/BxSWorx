using System;
using System.Runtime.Serialization;
//.........................................................
using SAPGUI.API.DL;
//•••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••
namespace SAPGUI.COM.DL
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