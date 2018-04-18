using System;
//•••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••
namespace BxS_WorxNCO.SAPSession.API
{
	public interface ISAP_Session_Header
		{
			#region "Properties"

				string		UserID        { get; set; }
				string		SessionName   { get; set; }
				DateTime	CreationDate	{ get; set; }
			  TimeSpan	CreationTime  { get; set; }
				int				Count         { get; set; }
				string		QID           { get; set; }
				string		SAPTCode			{ get; set; }

			#endregion

		}
}