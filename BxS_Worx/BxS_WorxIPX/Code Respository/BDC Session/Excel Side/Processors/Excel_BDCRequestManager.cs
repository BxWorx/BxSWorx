using System;
//•••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••
namespace BxS_WorxIPX.BDC
{
	public class Excel_BDCRequestManager : IExcel_BDCRequestManager
		{
			#region "Constructors"

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				internal Excel_BDCRequestManager(		IExcel_BDCRequest				excelBDCSession
																					, Func<ISAP_Logon>				sapLogonFactory
																					, Func<IExcel_WSSource>		wsSourceFactory )
					{
						this._Session						= excelBDCSession	;
						this._SAPLogonFactory		= sapLogonFactory	;
						this._WSSourceFactory		= wsSourceFactory	;
					}

			#endregion

			//===========================================================================================
			#region "Declarations"

				private	readonly	IExcel_BDCRequest				_Session					;
				private	readonly	Func<ISAP_Logon>				_SAPLogonFactory	;
				private	readonly	Func<IExcel_WSSource>		_WSSourceFactory	;

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
				public ISAP_Logon				Create_SAPLogon()	=> this._SAPLogonFactory()	;
				public IExcel_WSSource	Create_WSSource()	=> this._WSSourceFactory()	;

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				public void Set_SAPLogon( ISAP_Logon sapLogon )
					{
						this.SessionLogon.Client			= sapLogon.Client			;
						this.SessionLogon.SAPSysID		= sapLogon.SAPSysID		;
						this.SessionLogon.Client			= sapLogon.Client			;
						this.SessionLogon.User				= sapLogon.User				;
						this.SessionLogon.Lang				= sapLogon.Lang				;
						this.SessionLogon.Pwrd				= sapLogon.Pwrd				;
						this.SessionLogon.SecurePwrd	= sapLogon.SecurePwrd	;
					}

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				public void Add_ExcelWS( IExcel_WSSource wsSrce )
					{
						this._Session.Worksheets.Add( wsSrce.WSGuid , wsSrce );
					}

			#endregion

		}
}