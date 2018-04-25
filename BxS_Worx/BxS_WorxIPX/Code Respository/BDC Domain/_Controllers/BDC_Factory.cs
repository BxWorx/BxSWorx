using System;
using System.Collections.Generic;
//.........................................................
using BxS_WorxIPX.BDC;

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
						this._UtlCntlr	= new	Lazy< IUTL_Controller >	( ()=>	UTL_Controller.Instance											, cz_LM );

						this._STypes		= new Lazy< List<Type> >			( ()=>  new List<Type> {	typeof(Request)
																																									,	typeof(Session)
																																									, typeof(SAP_Logon)
																																									, typeof(User)
																																									,	typeof(XMLConfig)	}		, cz_LM );
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

				internal	ISession		Create_Session	()=>	new	Session()	;
				internal	IRequest		Create_Request	()=>	new	Request( this.Create_User() , this.Create_SAPLogon() )	;










				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				internal	XMLConfig	Create_BDCXmlConfig	( bool withDefaults = true )=> new XMLConfig( withDefaults );
				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				internal	string				SerialiseXMLConfig		( XMLConfig config )		=>	this.Serializer.Serialize									( config )				;
				internal	IXMLConfig	DeSerialiseXMLConfig	( string serializedObj )	=>	this.Serializer.DeSerialize<IXMLConfig>	( serializedObj , this._STypes.Value )	;


				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				internal IRequest ReadBDCRequest( string pathName )
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
				internal void WriteBDCRequest( IRequest request , string pathName )
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
				private	ISAP_Logon	Create_SAPLogon	()=>	new	SAP_Logon()	;
				//...
				private	IUser		Create_User			()=>	new	User()			;








				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				private	string				SerialiseBDCRequest		( IRequest request )	=>	this.Serializer.Serialize										( request	)				;
				private	IRequest		DeSerialiseBDCRequest	( string serializedObj )		=>	this.Serializer.DeSerialize<IRequest>( serializedObj , this._STypes.Value )	;
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