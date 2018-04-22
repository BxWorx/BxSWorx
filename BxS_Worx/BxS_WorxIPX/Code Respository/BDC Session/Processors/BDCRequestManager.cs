using System;
//•••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••
namespace BxS_WorxIPX.BDC
{
	public class BDCRequestManager : IBDCRequestManager
		{
			#region "Constructors"

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				internal BDCRequestManager(		IExcel_BDCRequest						excelBDCRequest
																		, Func<ISAP_Logon>						sapLogonFactory
																		, Func<IExcel_BDCWorksheet>		bdcWSFactory		)
					{
						this._ExcelBDCRequest		= excelBDCRequest	;
						this._SAPLogonFactory		= sapLogonFactory	;
						this._BDCWSFactory			= bdcWSFactory		;
					}

			#endregion

			//===========================================================================================
			#region "Declarations"

				private	readonly	IExcel_BDCRequest		_ExcelBDCRequest	;
				//...
				private	readonly	Func<ISAP_Logon>						_SAPLogonFactory	;
				private	readonly	Func<IExcel_BDCWorksheet>		_BDCWSFactory			;

			#endregion

			//===========================================================================================
			#region "Properties"

				public	int		WSCount		{ get { return	this._ExcelBDCRequest.Worksheets.Count; }	}
				//.................................................
				private	ISAP_Logon	SessionLogon	{ get	{	return	this._ExcelBDCRequest.SAPLogon; } }

			#endregion

			//===========================================================================================
			#region "Methods: Exposed"

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				public ISAP_Logon						Create_SAPLogon			()	=> this._SAPLogonFactory()	;
				public IExcel_BDCWorksheet	Create_BDCWorksheet	()	=> this._BDCWSFactory()			;

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				public void Set_SAPLogon			( ISAP_Logon sapLogon )				=>	this.SessionLogon.Transfer( sapLogon );
				public void Add_BDCWorksheet	( IExcel_BDCWorksheet bdcWS )	=>	this._ExcelBDCRequest.Worksheets.Add( bdcWS.WSGuid , bdcWS );

			#endregion

		}
}