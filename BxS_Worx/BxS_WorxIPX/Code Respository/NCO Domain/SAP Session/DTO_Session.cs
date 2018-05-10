using System;
//•••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••
namespace BxS_WorxIPX.NCO
{
	public struct DTO_Session : IDTO_Session
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

			//===========================================================================================
			#region "Methods: Exposed: General"

				internal	static DTO_Session Create()	=>	new	DTO_Session();

			#endregion

		}
	}
