using System;
using System.Collections.Generic;
//.........................................................
using BxS_WorxIPX.BDCExcel;
using BxS_WorxIPX.BDC;

using BxS_WorxUtil.Main;
using BxS_WorxUtil.General;

using static	BxS_WorxIPX.Main.IPX_Constants;
//•••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••
namespace BxS_WorxIPX.BDC
{
	internal sealed class BDCRequest_Factory
		{
			#region "Constructors: Singleton"

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				private		static readonly	Lazy< BDC_Factory >	_Instance		= new		Lazy< BDC_Factory >( ()=>	new BDC_Factory() , cz_LM );
				internal	static					BDC_Factory					Instance	{	get { return _Instance.Value; }	}

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				private BDCRequest_Factory()
					{
						this._UtlCntlr	= new	Lazy< IUTL_Controller >	( ()=>	UTL_Controller.Instance											, cz_LM );

						this._STypes		= new Lazy< List<Type> >			( ()=>  new List<Type> {	typeof(BDCRequest)
																																									,	typeof(BDCSession)
																																									, typeof(SAP_Logon)
																																									, typeof(BDCUser)
																																									,	typeof(BDCXMLConfig)	}		, cz_LM );
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
				private	readonly	Lazy< List<Type> >				_STypes;

			#endregion

			//===========================================================================================
			#region "Methods: Exposed"

				internal	IBDCRequest		Create_SAPBDCRequest	()=>	new	BDCRequest( this.Create_BDCUser() , this.Create_SAPLogon() )	;










				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				internal	BDCXMLConfig	Create_BDCXmlConfig	( bool withDefaults = true )=> new BDCXMLConfig( withDefaults );
				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				internal	IExcel_BDCRequest			Create_ExcelBDCRequest		()=>	new	Excel_BDCRequest		( this.Create_SAPLogon() )	;
				internal	IExcel_BDCWorksheet		Create_ExcelBDCWorksheet	()=>	new Excel_BDCWorksheet	()	;
				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				internal	string				SerialiseXMLConfig		( BDCXMLConfig config )		=>	this.Serializer.Serialize									( config )				;
				internal	IBDCXMLConfig	DeSerialiseXMLConfig	( string serializedObj )	=>	this.Serializer.DeSerialize<IBDCXMLConfig>	( serializedObj , this._STypes.Value )	;

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				internal IBDCRequest ParseRequest( IExcel_BDCRequest request )
					{
						IBDCRequest	lo_Req	= this.Create_SAPBDCRequest();
						return	lo_Req;
					}

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				internal IBDCRequest ReadBDCRequest( string pathName )
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
				internal void WriteBDCRequest( IBDCRequest request , string pathName )
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
				private	IBDCUser		Create_BDCUser	()=>	new	BDCUser()		;
				private	ISAP_Logon	Create_SAPLogon	()=>	new	SAP_Logon()	;







				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				private	IBDCSession		Create_SAPBDCSession	()=>	new BDCSession()	;

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				private	string						SerialiseBDCRequest		( IBDCRequest request )	=>	this.Serializer.Serialize										( request	)				;
				private	IBDCRequest		DeSerialiseBDCRequest	( string serializedObj )		=>	this.Serializer.DeSerialize<IBDCRequest>( serializedObj , this._STypes.Value )	;
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