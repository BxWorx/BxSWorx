using System;
using System.Runtime.Serialization;
//•••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••
namespace BxS_SAPLogon.API
{
	[DataContract]

	internal class DTOFavourite : IDTOFavourite
		{
			#region "Properties"

				[DataMember]	public Guid		UUID					{ get; set; }
				//.................................................
				[DataMember]	public int		SeqNo					{ get; set; }
				[DataMember]	public Guid		ServiceID			{ get; set; }
				[DataMember]	public string	Description		{ get; set; }
				[DataMember]	public string	Client				{ get; set; }
				[DataMember]	public string	SystemID			{ get; set; }

			#endregion

		}
}
