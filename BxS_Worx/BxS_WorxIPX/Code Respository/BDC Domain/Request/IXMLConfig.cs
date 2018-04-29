using System;
//•••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••
namespace BxS_WorxIPX.BDC
{
	public interface IXMLConfig
		{
			#region "Properties"

				Guid			GUID						{ get; set; }

				string		IsActive        { get; set; }

				string		SAPBDCSessionID	{ get; set; }
				string		SAPTCode        { get; set; }

				string		PauseTime       { get; set; }
				string		Skip1st					{ get; set; }

				string		Col_ID					{ get; set; }
				string		Col_Active			{ get; set; }
				string		Col_Exec				{ get; set; }
				string		Col_Msg					{ get; set; }
				string		Col_DataStart		{ get; set; }
				string		Row_DataStart		{ get; set; }

				string		CTU_DisMode			{ get; set; }
				string		CTU_UpdMode			{ get; set; }
				string		CTU_DefSize			{ get; set; }

				string		IsProtected			{ get; set; }
				string		Password    		{ get; set; }

			#endregion

		}
}
