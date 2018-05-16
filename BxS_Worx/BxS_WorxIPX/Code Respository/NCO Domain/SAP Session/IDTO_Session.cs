using System;
//•••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••
namespace BxS_WorxIPX.NCO
{
	public interface IDTO_Session
		{
			#region "Properties"

				string		QID						{ get; set; }
				string		UserID				{ get; set; }
				string		SessionName		{ get; set; }
				string		SAPTCode			{ get; set; }
				int				Count					{ get; set; }
				DateTime	CreationDate	{ get; set; }
				TimeSpan	CreationTime	{ get; set; }

			#endregion

		}
}