using System;
//•••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••
namespace BxS_WorxNCO.BDCSession.API
{
	public interface ISAPSession_Header
		{
			#region "Properties"

				string		UserID        { get; set; }
				string		SessionName   { get; set; }
				DateTime	CreationDate	{ get; set; }
			  TimeSpan	CreationTime  { get; set; }
				int				Count         { get; set; }
				string		QID           { get; set; }

			#endregion

		}
}