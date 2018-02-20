using System.Runtime.Serialization;
//•••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••
namespace BxS_SAPBDC.Parser
{
	[DataContract()]
	internal class DTO_SessionXMLConfig
		{
			#region "Constructors"

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				internal DTO_SessionXMLConfig()
					{
					}

			#endregion

			//===========================================================================================
			#region "Properties"

				[DataMember]	public string IsActive        { get; set; }
				[DataMember]	public string Active_Column   { get; set; }
				[DataMember]	public string Msg_Column			{ get; set; }

				[DataMember]	public string IsProtected			{ get; set; }
				[DataMember]	public string Password    		{ get; set; }

			#endregion
		}
}
