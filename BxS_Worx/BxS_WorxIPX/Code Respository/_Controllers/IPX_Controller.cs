using System;
using System.Collections.Generic;
//.........................................................
using BxS_WorxIPX.BDC;
using BxS_WorxIPX.SAPBDCSession;

using BxS_WorxUtil.Main;
using BxS_WorxUtil.General;

using static	BxS_WorxIPX.Main.IPX_Constants;
//•••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••
namespace BxS_WorxIPX.Main
{
	public sealed class IPX_Controller : IIPX_Controller
		{
			#region "Constructors: Singleton"

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				private	static readonly	Lazy< IPX_Controller >	_Instance		= new		Lazy< IPX_Controller >( ()=>	new IPX_Controller() , cz_LM );
				public	static					IPX_Controller					Instance	{	get { return _Instance.Value; }	}

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				private IPX_Controller()
					{
						this._WSParser	= new	Lazy< ExcelBDC_Parser >	( ()=>	new	ExcelBDC_Parser()		, cz_LM	);
						this._UtlCntlr	= new	Lazy< IUTL_Controller >	( ()=>	UTL_Controller.Instance , cz_LM );
						//.............................................
						this._SRTypes		= new	Lazy< List<Type> >			(	()=>	new List<Type>	{	typeof(Excel_BDCRequest) });
					}

			#endregion

			//===========================================================================================
			#region "Properties"

				private	IO					IO					{ get	{ return	this._UtlCntlr.Value.IO					; } }
				private	Serializer	Serializer	{ get	{ return	this._UtlCntlr.Value.Serializer	; } }

			#endregion

			//===========================================================================================
			#region "Declarations"

				private	readonly	Lazy< ExcelBDC_Parser >		_WSParser;
				private	readonly	Lazy< IUTL_Controller >		_UtlCntlr;
				//.................................................
				private readonly	Lazy< List<Type>	>		_SRTypes;

			#endregion

			//===========================================================================================
			#region "Methods: Exposed"

				public	IExcelBDCSessionResult	CreateBDCSessionResult	()=> new ExcelBDCSessionResult	();
				public	IExcelBDC_Config				Create_BDCConfig	()	=>	new	ExcelBDC_Config		();

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨

				public	ISAP_Logon					Create_SAPLogon					()	=>	new	SAP_Logon()					;
				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				public	ISAP_BDCRequest			Create_SAPBDCRequest		()	=>	new	SAP_BDCRequest()	;
				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				public	IExcel_WSSource			Create_ExcelWSSource		()	=>	new Excel_WSSource	()	;
				public	IExcel_WSRequest		Create_ExcelWSRequest		()	=>	new Excel_WSRequest	()	;
				//...
				public	IExcel_BDCRequest		Create_ExcelBDCRequest	()	=>	new	Excel_BDCRequest( this.Create_SAPLogon() )	;

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				public	IExcel_BDCRequestManager	Create_ExcelBDCRequestManager()
					{
						return	new	Excel_BDCRequestManager(	this.Create_ExcelBDCRequest()
																								, this.Create_SAPLogon
																								, this.Create_ExcelWSSource			);
					}

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				public IExcel_WSRequest ParseWStoRequest( IExcel_WSSource worksheet )
					{
						IExcel_WSRequest	lo_Req	= this.Create_ExcelWSRequest();
						this._WSParser.Value.ParseWStoRequest( worksheet , lo_Req );
						return	lo_Req;
					}

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				public IExcel_BDCRequest ReadExcelBDCRequest( string pathName )
					{
						try
							{
								return	this.DeSerialiseExcelBDCRequest( this.ReadXMLFile( pathName ) );
							}
						catch (Exception ex)
							{
								throw	new	Exception( "Excel BDC Request from XML file failure" , ex );
							}
					}

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				public void WriteExcelBDCRequest( IExcel_BDCRequest request , string pathName )
					{
						try
							{
								this.WriteXMLtoFile( this.SerialiseExcelBDCRequest( request ) , pathName );
							}
						catch (Exception ex)
							{
								throw	new	Exception( "Excel BDC Request to XML file failure" , ex );
							}
					}

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				private	string						SerialiseExcelBDCRequest	( IExcel_BDCRequest request )	=>	this.Serializer.Serialize											( request		, this._SRTypes.Value )	;
				private	IExcel_BDCRequest	DeSerialiseExcelBDCRequest( string serializedObj )			=>	this.Serializer.DeSerialize<IExcel_BDCRequest>( serializedObj )	;
				//...
				private	void		WriteXMLtoFile( string serializedObj , string fullPath )	=>	this.IO.WriteFile	( fullPath	, serializedObj )	;
				private	string	ReadXMLFile		( string fullPath )													=>	this.IO.ReadFile	( fullPath )									;

			#endregion

		}
}