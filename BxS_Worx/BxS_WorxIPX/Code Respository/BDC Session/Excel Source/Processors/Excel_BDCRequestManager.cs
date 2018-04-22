//•••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••
namespace BxS_WorxIPX.BDC
{
	public class Excel_BDCRequestManager : IExcel_BDCRequestManager
		{
			#region "Constructors"

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				internal Excel_BDCRequestManager( IExcel_BDCRequest	excelBDCSession )
					{
						this._Session		= excelBDCSession;
					}

			#endregion

			//===========================================================================================
			#region "Declarations"

				private	readonly	IExcel_BDCRequest		_Session;

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
				public void Add_ExcelWS( IExcel_WSSource ws )
					{
						this._Session.Worksheets.Add( ws.WSGuid , ws );
					}

			#endregion

		}
}