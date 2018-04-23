using System;
using System.Collections.Generic;
//.........................................................
using BxS_WorxIPX.SAPBDCSession;

using BxS_WorxUtil.Main;
using BxS_WorxUtil.General;

using static	BxS_WorxIPX.Main.IPX_Constants;
//•••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••
namespace BxS_WorxIPX.BDC
{
	internal sealed class BDC_Factory
		{
			#region "Constructors: Singleton"

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				private		static readonly	Lazy< BDC_Factory >	_Instance		= new		Lazy< BDC_Factory >( ()=>	new BDC_Factory() , cz_LM );
				internal	static					BDC_Factory					Instance	{	get { return _Instance.Value; }	}

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				private BDC_Factory()
					{
						this._UtlCntlr	= new	Lazy< IUTL_Controller >	( ()=>	UTL_Controller.Instance										, cz_LM );
						this._Parser		= new	Lazy< Parser >					( ()=>	new	Parser( this.Create_SAPBDCSession )		, cz_LM	);
						this._SRTypes		= new	Lazy< List<Type> >			(	()=>	new List<Type> { typeof(SAP_BDCRequest)	}	, cz_LM	);
					}

			#endregion

			//===========================================================================================
			#region "Properties"

				private	IO					IO					{ get	{ return	this._UtlCntlr.Value.IO					; } }
				private	Serializer	Serializer	{ get	{ return	this._UtlCntlr.Value.Serializer	; } }

			#endregion

			//===========================================================================================
			#region "Declarations"

				private	readonly	Lazy< IUTL_Controller >		_UtlCntlr;
				private	readonly	Lazy< Parser >						_Parser;
				private readonly	Lazy< List<Type>	>				_SRTypes;

			#endregion

			//===========================================================================================
			#region "Methods: Exposed"

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				internal	ISAP_Logon		Create_SAPLogon	()=>	new	SAP_Logon()	;
				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				internal	IExcel_BDCRequest			Create_ExcelBDCRequest		()=>	new	Excel_BDCRequest( this.Create_SAPLogon() )	;
				internal	IExcel_BDCWorksheet		Create_ExcelBDCWorksheet	()=>	new Excel_BDCWorksheet	()	;

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				internal ISAP_BDCRequest ParseRequest( IExcel_BDCRequest request )
					{
						ISAP_BDCRequest	lo_Req	= this.Create_SAPBDCRequest();
						this._Parser.Value.ParseRequest( request , lo_Req );
						return	lo_Req;
					}

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				internal ISAP_BDCRequest ReadBDCRequest( string pathName )
					{
						try
							{
								return	this.DeSerialiseBDCRequest( this.ReadXMLFile( pathName ) );
							}
						catch (Exception ex)
							{
								throw	new	Exception( "BDC Request from XML file failure" , ex );
							}
					}

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				internal void WriteBDCRequest( ISAP_BDCRequest request , string pathName )
					{
						try
							{
								this.WriteXMLtoFile( this.SerialiseBDCRequest( request ) , pathName );
							}
						catch (Exception ex)
							{
								throw	new	Exception( "BDC Request to XML file failure" , ex );
							}
					}

			#endregion

			//===========================================================================================
			#region "Methods: Private"

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				private	ISAP_BDCRequest		Create_SAPBDCRequest	()=>	new	SAP_BDCRequest( this.Create_SAPLogon() )	;
				private	ISAP_BDCSession		Create_SAPBDCSession	()=>	new SAP_BDCSession()	;

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				private	string						SerialiseBDCRequest		( ISAP_BDCRequest request )	=>	this.Serializer.Serialize											( request	); //	, this._SRTypes.Value )	;
				private	ISAP_BDCRequest		DeSerialiseBDCRequest	( string serializedObj )		=>	this.Serializer.DeSerialize<ISAP_BDCRequest>	( serializedObj )										;
				//...
				private	void		WriteXMLtoFile( string serializedObj , string fullPath )		=>	this.IO.WriteFile	( fullPath	, serializedObj )	;
				private	string	ReadXMLFile		( string fullPath )														=>	this.IO.ReadFile	( fullPath )									;

			#endregion

			////===========================================================================================
			//#region "TO-DO"

			//	public	IExcelBDCSessionResult	CreateBDCSessionResult	()=> new ExcelBDCSessionResult	();
			//	public	IExcelBDC_Config				Create_BDCConfig	()	=>	new	ExcelBDC_Config		();

			//#endregion

		}
}