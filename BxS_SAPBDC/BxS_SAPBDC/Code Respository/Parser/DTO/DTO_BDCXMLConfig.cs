using System.Runtime.Serialization;
//•••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••
namespace BxS_SAPBDC.Parser
{
	[DataContract()]
	internal class DTO_BDCXMLConfig
		{
			#region "Constructors"

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				internal DTO_BDCXMLConfig()
					{
					}

			#endregion

			//===========================================================================================
			#region "Properties"

				[DataMember]	public string GUID						{ get; set; }
				[DataMember]	public string SessionID				{ get; set; }
				[DataMember]	public string IsActive        { get; set; }
				[DataMember]	public string SAPTCode        { get; set; }
				[DataMember]	public string PauseTime       { get; set; }
				[DataMember]	public string Active_Column   { get; set; }
				[DataMember]	public string Msg_Column			{ get; set; }
				[DataMember]	public string CTU_DisMode			{ get; set; }
				[DataMember]	public string CTU_UpdMode			{ get; set; }
				[DataMember]	public string CTU_DefSize			{ get; set; }

				[DataMember]	public string IsProtected			{ get; set; }
				[DataMember]	public string Password    		{ get; set; }

			#endregion
		}
}
