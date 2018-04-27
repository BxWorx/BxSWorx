using System;
using System.Runtime.Serialization;
//.........................................................
using static	BxS_WorxIPX.Main	.IPX_Constants;
//•••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••
namespace BxS_WorxIPX.BDC
{
	[DataContract()]

	public class XMLConfig : IXMLConfig
		{
			#region "Constructors"

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				internal XMLConfig( bool SetDefaults = true )
					{
						if ( SetDefaults )	this.SetDefaults();
					}

			#endregion

			//===========================================================================================
			#region "Properties"

				[DataMember]	public Guid			GUID						{ get; set; }

				[DataMember]	public string		IsActive        { get; set; }

				[DataMember]	public string		SAPBDCSessionID	{ get; set; }
				[DataMember]	public string		SAPTCode        { get; set; }

				[DataMember]	public string		PauseTime       { get; set; }
				[DataMember]	public string		Skip1st					{ get; set; }

				[DataMember]	public string		Col_ID					{ get; set; }
				[DataMember]	public string		Col_Active			{ get; set; }
				[DataMember]	public string		Col_Exec				{ get; set; }
				[DataMember]	public string		Col_Msg					{ get; set; }
				[DataMember]	public string		Col_DataStart		{ get; set; }
				[DataMember]	public string		Row_DataStart		{ get; set; }

				[DataMember]	public string		CTU_DisMode			{ get; set; }
				[DataMember]	public string		CTU_UpdMode			{ get; set; }
				[DataMember]	public string		CTU_DefSize			{ get; set; }

				[DataMember]	public string		IsProtected			{ get; set; }
				[DataMember]	public string		Password    		{ get; set; }

			#endregion

			//===========================================================================================
			#region "Methods: Exposed"

				public IXMLConfig	ShallowCopy()
					{
						return (IXMLConfig) this.MemberwiseClone();
					}

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				internal void SetDefaults()
					{
						this.GUID	= Guid.NewGuid();

						this.CTU_DefSize  = cz_True;
						this.CTU_DisMode  = cz_CTU_N.ToString();
						this.CTU_UpdMode	= cz_CTU_A.ToString();
					}

			#endregion

		}
}
