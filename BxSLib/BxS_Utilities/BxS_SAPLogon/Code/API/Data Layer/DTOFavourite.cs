using System;
using System.Runtime.Serialization;
//.........................................................
using BxS_SAPConn.API;
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
				[DataMember]	public string	Client				{ get; set; }
				[DataMember]	public string	User					{ get; set; }
				[DataMember]	public string	Password			{ get; set; }
				//.................................................
				[DataMember]	public IDTOConnection	SAPConn	{ get; set; }

			#endregion

		}
}
