using System;
using System.Collections.Generic;
//.........................................................
using BxS_WorxIPX.BDC;

using BxS_WorxUtil.General;
using BxS_WorxUtil.Main;

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
						this._UtlCntlr		= new	Lazy< IUTL_Controller >	( ()=>	UTL_Controller.Instance	, cz_LM );
						this._BDCFactory	= new	Lazy< BDC_Factory >			( ()=>	BDC_Factory		.Instance	, cz_LM );

						this._STypes			= new Lazy< List<Type> >			( ()=>  new List<Type> {	typeof(Request)
																																										,	typeof(Session)
																																										, typeof(SAP_Logon)
																																										, typeof(User)
																																										,	typeof(XMLConfig)	}		, cz_LM );
					}

			#endregion

			//===========================================================================================
			#region "Declarations"

				private	readonly	Lazy< IPX_ToolSet >				_IPXToolset	;

				private readonly	Lazy< BDC_Factory >				_BDCFactory;
				private	readonly	Lazy< IUTL_Controller >		_UtlCntlr;
				private	readonly	Lazy< List<Type> >				_STypes;

			#endregion

			//===========================================================================================
			#region "Properties"

				private	IO					IO					{ get	{ return	this._UtlCntlr.Value.IO					; } }
				private	Serializer	Serializer	{ get	{ return	this._UtlCntlr.Value.Serializer	; } }

			#endregion

			//===========================================================================================
			#region "Methods: Exposed"

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				public	ISAP_Logon	Create_SAPLogon()	=>	new	SAP_Logon();

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				public	IXMLConfig	Create_BDCXmlConfig	( bool withDefaults = true )=> new XMLConfig( withDefaults );
				//...
				public	string			SerialiseXMLConfig	( XMLConfig config	 )		=>	this.Serializer.Serialize( config ).Replace("\n","").Replace("\r","")				;
				public	IXMLConfig	DeSerialiseXMLConfig( string serializedObj )	=>	this.Serializer.DeSerialize<IXMLConfig>	( serializedObj , this._STypes.Value )	;

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				public	IBDC_Controller	Create_BDCController()	=>	new	BDC_Controller( this._BDCFactory );

			#endregion

		}
}