using System;
using System.Runtime.Serialization;
//.........................................................
using BxS_SAPGUI.API;
//•••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••
namespace BxS_SAPGUI.COM.DL
{
	[DataContract]
	internal class DTOFavourite : IDTOFavourite
		{
			#region "Properties"

				[DataMember]	public int		SeqNo					{ get; set; }
				[DataMember]	public Guid		ServiceID			{ get; set; }
				[DataMember]	public string	Description		{ get; set; }
				[DataMember]	public string	Client				{ get; set; }
				[DataMember]	public string	SystemID			{ get; set; }

			#endregion

		}
}
