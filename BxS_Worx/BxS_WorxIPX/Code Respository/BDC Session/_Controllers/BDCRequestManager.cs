using System;
//.........................................................
using BxS_WorxIPX.BDCExcel;
using BxS_WorxIPX.BDCSAP;

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
						this._ExcelBDCRequest		= new	Lazy<IExcel_BDCRequest>( ()=>	this._Factory.Value.Create_ExcelBDCRequest() , cz_LM );
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
				//...
				private BDC_Factory		Factory		{ get { return	this._Factory.Value; }	}

			#endregion

			//===========================================================================================
			#region "Methods: Exposed"

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				public ISAP_Logon						Create_SAPLogon			()	=> this.Factory.Create_SAPLogon()						;
				public IExcel_BDCWorksheet	Create_BDCWorksheet	()	=> this.Factory.Create_ExcelBDCWorksheet()	;
				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				public BDCXMLConfig		Create_BDCXmlConfig	( bool withDefaults = true )	=> this.Factory.Create_BDCXmlConfig( withDefaults)	;

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				public void	Set_SAPLogon			( ISAP_Logon					sapLogon	)	=>	this._ExcelBDCRequest.Value.SAPLogon.Transfer( sapLogon );
				public void Add_BDCWorksheet	( IExcel_BDCWorksheet bdcWS			)	=>	this._ExcelBDCRequest.Value.Worksheets.Add( bdcWS.WSGuid , bdcWS );

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				public void							Write_BDCRequest( string pathName )	=>	this.Factory.WriteBDCRequest( this.Factory.ParseRequest( this._ExcelBDCRequest.Value ) , pathName )	;
				public ISAP_BDCRequest	Read_BDCRequest	( string pathName )	=>	this.Factory.ReadBDCRequest	( pathName );

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				public string				SerializeXMLConfig		( BDCXMLConfig config )	=>	this.Factory.SerialiseXMLConfig		( config ).Replace("\n","").Replace("\r","")	;
				public BDCXMLConfig	DeserializeXMLConfig	( string config  )			=>	this.Factory.DeSerialiseXMLConfig	( config )	;

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				public void Clear() => this._ExcelBDCRequest.Value.Clear();

			#endregion

		}
}