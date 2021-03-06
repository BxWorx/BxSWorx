﻿using System.Runtime.Serialization;
//.........................................................
using static	BxS_SAPBDC.BDC.BDC_Constants;
//•••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••
namespace BxS_SAPBDC.Parser
{
	[DataContract()]
	internal class DTO_ParserXMLConfig
		{
			#region "Constructors"

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				internal DTO_ParserXMLConfig( bool SetDefaults = false )
					{
						if (SetDefaults)	this.SetDefaults();
					}

			#endregion

			//===========================================================================================
			#region "Properties"

				[DataMember]	public string GUID						{ get; set; }

				[DataMember]	public string IsActive        { get; set; }

				[DataMember]	public string SAPBDCSessionID	{ get; set; }
				[DataMember]	public string SAPTCode        { get; set; }

				[DataMember]	public string PauseTime       { get; set; }
				[DataMember]	public string Skip1st					{ get; set; }

				[DataMember]	public string Col_ID					{ get; set; }
				[DataMember]	public string Col_Active			{ get; set; }
				[DataMember]	public string Col_Exec				{ get; set; }
				[DataMember]	public string Col_Msg					{ get; set; }
				[DataMember]	public string Col_DataStart		{ get; set; }
				[DataMember]	public string Row_DataStart		{ get; set; }

				[DataMember]	public string CTU_DisMode			{ get; set; }
				[DataMember]	public string CTU_UpdMode			{ get; set; }
				[DataMember]	public string CTU_DefSize			{ get; set; }

				[DataMember]	public string IsProtected			{ get; set; }
				[DataMember]	public string Password    		{ get; set; }

			#endregion

			//===========================================================================================
			#region "Methods: Private"

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				internal void SetDefaults()
					{
						this.CTU_DefSize  = cz_Val_True;
						this.CTU_DisMode  = "N";
						this.CTU_UpdMode	= "A";
					}

			#endregion

		}
}
