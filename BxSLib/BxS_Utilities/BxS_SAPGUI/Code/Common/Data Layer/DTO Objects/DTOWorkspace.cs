using System;
using System.Runtime.Serialization;
//•••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••
namespace BxS_SAPGUI.COM.DL
{
	[DataContract]

	internal sealed class DTOWorkspace :IDTOWorkspace
		{
			#region "Properties"

				[DataMember]	public Guid		UUID					{ get; set; }
				//...................................................
				[DataMember]	public string Description		{ get; set; }

			#endregion
		}
}