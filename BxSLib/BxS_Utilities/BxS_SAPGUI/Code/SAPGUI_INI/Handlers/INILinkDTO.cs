using System;
using System.Runtime.Serialization;
//•••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••
namespace BxS_SAPGUI.INI
{
	[DataContract]

	internal sealed class INILinkDTO
		{
			#region "Properties"

				[DataMember]	internal string	INIItemDesc	{ get; set; }
				[DataMember]	internal Guid		ServiceID		{ get; set; }
											internal bool   Used				{ get; set; }

			#endregion
		}
}