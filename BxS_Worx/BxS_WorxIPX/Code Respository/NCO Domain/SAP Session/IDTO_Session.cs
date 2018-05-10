using System;
//•••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••
namespace BxS_WorxIPX.NCO
{
	public interface IDTO_Session
		{
			#region "Properties"

				int				Count					{ get; set; }
				DateTime	CreationDate	{ get; set; }
				TimeSpan	CreationTime	{ get; set; }
				string		QID						{ get; set; }
				string		SAPTCode			{ get; set; }
				string		SessionName		{ get; set; }
				string		UserID				{ get; set; }

			#endregion

		}
}