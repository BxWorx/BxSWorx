using System;
using System.Collections.Generic;
//.........................................................
using static	BxS_WorxIPX.Main.IPX_Constants;
//•••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••
namespace BxS_WorxIPX.BDC
{
	internal sealed class IPX_Factory
		{
			#region "Constructors: Singleton"

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				private		static readonly	Lazy< IPX_Factory >	_Instance		= new		Lazy< IPX_Factory >( ()=>	new IPX_Factory() , cz_LM );
				internal	static					IPX_Factory					Instance	{	get { return _Instance.Value; }	}

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				private IPX_Factory()
					{
						this._CfgTypes	= new Lazy< List<Type> >	( ()=>  new List<Type> {	typeof(	XMLConfig	)	}		, cz_LM );
					}

			#endregion

			//===========================================================================================
			#region "Declarations"

				private	readonly	Lazy< List<Type> >	_CfgTypes	;

			#endregion

			//===========================================================================================
			#region "Properties"

				internal	List<Type>	XMLConfigTypes	{ get	{ return	this._CfgTypes.Value;	} }

			#endregion

			//===========================================================================================
			#region "Methods: Exposed"

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				internal	ISAP_Logon	Create_SAPLogon	()=>	new	SAP_Logon()	;
				internal	IUser				Create_User			()=>	new	User()			;
				//...
				internal	IXMLConfig	Create_XmlConfig	( bool withDefaults = true )	=> new XMLConfig( withDefaults );

			#endregion

		}
}