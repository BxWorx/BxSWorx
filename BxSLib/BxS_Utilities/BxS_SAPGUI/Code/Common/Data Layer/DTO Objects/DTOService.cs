using System;
using System.Runtime.Serialization;
//•••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••
namespace BxS_SAPGUI.COM.DL
{
	[DataContract]

	internal sealed class DTOService : IDTOService
		{
			#region "Properties"

				[DataMember]	public Guid		UUID				{ get; set; }
				//...................................................
				[DataMember]	public string Name				{ get; set; }
				[DataMember]	public string Description	{ get; set; }
				[DataMember]	public string SystemID		{ get; set; }
				[DataMember]	public string SystemNo		{ get; set; }
				[DataMember]	public string Type				{ get; set; }
				[DataMember]	public string Server			{ get; set; }
				[DataMember]	public string SNCName			{ get; set; }
				[DataMember]	public string SAPCPG			{ get; set; }
				[DataMember]	public string DCPG				{ get; set; }
				[DataMember]	public string SNCOp				{ get; set; }
				[DataMember]	public string Mode				{ get; set; }
				//...................................................
				[DataMember]	public Guid		MSID				{ get; set; }

			#endregion

		}
}