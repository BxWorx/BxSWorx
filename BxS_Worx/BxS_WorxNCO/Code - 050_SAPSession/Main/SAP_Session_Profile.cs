using BxS_WorxNCO.BDCSession.DTO;
using BxS_WorxNCO.Common;
//•••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••
namespace BxS_WorxNCO.SAPSession.API
{
	public class SAP_Session_Profile : ISAP_Session_Profile
		{
			#region "Constructors"

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				internal SAP_Session_Profile(		DTO_BDC_CTU				dtoCTU
																			, BDC_Collection	bdcData )
					{
						this.CTUParams	= dtoCTU	;
						this.BDCData		= bdcData	;
					}

			#endregion

			//===========================================================================================
			#region "Properties"

				public	int			Count					{ get { return	this.BDCData.Count; } }
				//.............................................
				public	string	SessionName		{ get; set; }
				public	string	SAPTCode			{ get; set; }
				//.............................................
				public	DTO_BDC_CTU			CTUParams		{ get; }
				public	BDC_Collection	BDCData			{ get; }

			#endregion

			//===========================================================================================
			#region "Methods: Exposed"

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				public	DTO_BDC_Data	Create_BDC_DTO()	=> this.BDCData.CreateDataDTO();

			#endregion

		}
}