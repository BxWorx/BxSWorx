using System;
//.........................................................
using static	BxS_WorxIPX.Main.IPX_Constants;
//•••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••
namespace BxS_WorxIPX.BDC
{
	public class BDCRequestManager : IBDCRequestManager
		{
			#region "Constructors"

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				internal BDCRequestManager(	Lazy<BDC_Factory>	factory )
					{
						this._Factory	= factory;
						//...
						this._ExcelBDCRequest		= new	Lazy<IExcel_BDCRequest>( ()=> this._Factory.Value.Create_ExcelBDCRequest() , cz_LM );
					}

			#endregion

			//===========================================================================================
			#region "Declarations"

				private	readonly	Lazy<BDC_Factory>					_Factory					;
				private	readonly	Lazy<IExcel_BDCRequest>		_ExcelBDCRequest	;

			#endregion

			//===========================================================================================
			#region "Properties"

				public	int	WSCount		{ get { return	this._ExcelBDCRequest.Value.Worksheets.Count; }	}

			#endregion

			//===========================================================================================
			#region "Methods: Exposed"

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				public ISAP_Logon						Create_SAPLogon			()	=> this._Factory.Value.Create_SAPLogon()					;
				public IExcel_BDCWorksheet	Create_BDCWorksheet	()	=> this._Factory.Value.Create_ExcelBDCWorksheet()	;

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				public void Set_SAPLogon			( ISAP_Logon					sapLogon	)	=>	this._ExcelBDCRequest.Value.SAPLogon.Transfer( sapLogon );
				public void Add_BDCWorksheet	( IExcel_BDCWorksheet bdcWS			)	=>	this._ExcelBDCRequest.Value.Worksheets.Add( bdcWS.WSGuid , bdcWS );

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				public void Write_BDCRequest( string pathName )	=> this._Factory.Value.WriteBDCRequest(		this._Factory.Value.ParseRequest( this._ExcelBDCRequest.Value )
																																																, pathName )	;

				public void Clear() => this._ExcelBDCRequest.Value.Clear();

			#endregion

		}
}