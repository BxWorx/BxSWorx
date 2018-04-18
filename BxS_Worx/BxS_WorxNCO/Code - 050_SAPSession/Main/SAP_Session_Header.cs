using System;
//•••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••
namespace BxS_WorxNCO.SAPSession.API
{
	public class SAP_Session_Header : ISAP_Session_Header
		{
			#region "Properties"

				public	string		UserID        { get; set; }
				public	string		SessionName   { get; set; }
				public	DateTime	CreationDate	{ get; set; }
			  public	TimeSpan	CreationTime  { get; set; }
				public	int				Count         { get; set; }
				public	string		QID           { get; set; }
				public	string		SAPTCode			{ get; set; }

			#endregion

		}
}