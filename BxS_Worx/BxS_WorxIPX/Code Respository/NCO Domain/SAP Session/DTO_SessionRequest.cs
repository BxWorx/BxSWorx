using System;
using System.Runtime.Serialization;
//•••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••
namespace BxS_WorxIPX.NCO
{
	[DataContract()]

	public class DTO_SessionRequest : IDTO_SessionRequest
		{
			#region "Properties"

				[DataMember]	public	string		User	{ get; set; }
				[DataMember]	public	string		Name	{ get; set; }
				[DataMember]	public	DateTime	From	{ get; set; }
				[DataMember]	public	DateTime	To		{ get; set; }
				[DataMember]	public	bool			FromX	{ get; set; }
				[DataMember]	public	bool			ToX		{ get; set; }

			#endregion

		}
}