//•••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••
namespace BxS_WorxIPX.BDC
{
	public class ExcelBDC_SessionManager : IExcelBDC_SessionManager
		{
			#region "Constructors"

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				internal ExcelBDC_SessionManager( IExcelBDC_Session	excelBDCSession )
					{
						this._Session		= excelBDCSession;
					}

			#endregion

			//===========================================================================================
			#region "Declarations"

				private	readonly	IExcelBDC_Session		_Session;

			#endregion

			//===========================================================================================
			#region "Properties"

				public	int		WSCount		{ get { return	this._Session.Worksheets.Count; }	}
				//.................................................
				private	ISAP_Logon	SessionLogon	{ get	{	return	this._Session.SAPLogon; } }

			#endregion

			//===========================================================================================
			#region "Methods: Exposed"

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				public void Set_SAPLogon( ISAP_Logon logon )
					{
						this.SessionLogon.Client			= logon.Client			;
						this.SessionLogon.SAPSysID		= logon.SAPSysID		;
						this.SessionLogon.Client			= logon.Client			;
						this.SessionLogon.User				= logon.User				;
						this.SessionLogon.Lang				= logon.Lang				;
						this.SessionLogon.Pwrd				= logon.Pwrd				;
						this.SessionLogon.SecurePwrd	= logon.SecurePwrd	;
					}

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				public void Add_ExcelWS( IExcelBDC_WS ws )
					{
						this._Session.Worksheets.Add( ws.WSGuid , ws );
					}

			#endregion

		}
}