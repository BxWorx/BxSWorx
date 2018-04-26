using System;
using System.Collections.Generic;
//.........................................................
using BxS_WorxUtil.General;

using static	BxS_WorxIPX.Main.IPX_Constants;
//•••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••
namespace BxS_WorxIPX.BDC
{
	public partial class BDC_Controller : IBDC_Controller
		{
			#region "Constructors"

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				internal BDC_Controller(	Lazy<IPX_ToolSet>		toolSet
																,	Lazy<IPX_Factory>		factory	)
					{
						this._Toolset		= toolSet		??	throw		new ArgumentException( $"{typeof(BDC_Controller).Namespace}:- Toolset null" )	;
						this._Factory		= factory		??	throw		new ArgumentException( $"{typeof(BDC_Controller).Namespace}:- Factory null" )	;
						//...
						this._ReqTypes	= new Lazy< List<Type> >	( ()=>  new List<Type> {	typeof(	Request		)
																																							,	typeof(	Session		)
																																							, typeof(	SAP_Logon	)
																																							, typeof(	User			)
																																							,	typeof(	XMLConfig	)	}		, cz_LM );

						this._CfgTypes	= new Lazy< List<Type> >	( ()=>  new List<Type> {	typeof(	XMLConfig	)	}		, cz_LM );
					}

			#endregion

			//===========================================================================================
			#region "Declarations"

				private	readonly	Lazy<IPX_ToolSet>		_Toolset	;
				private	readonly	Lazy<IPX_Factory>		_Factory	;
				//...
				private	readonly	Lazy< List<Type> >	_ReqTypes	;
				private	readonly	Lazy< List<Type> >	_CfgTypes	;

			#endregion

			//===========================================================================================
			#region "Properties"

				private	IPX_Factory	Factory			{ get	{ return	this._Factory.Value							; } }
				private	IO					IO					{ get	{ return	this._Toolset.Value.IO					; } }
				private	Serializer	Serializer	{ get	{ return	this._Toolset.Value.Serializer	; } }

			#endregion

			//===========================================================================================
			#region "Methods: Exposed"

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				public	IXMLConfig	Create_XMLConfig	( bool withDefaults = true )	=> new XMLConfig( withDefaults );
				//...
				public	IUser			Create_User			()=>	new User();
				public	ISession	Create_Session	()=>	new	Session( this.Create_XMLConfig() )	;
				public	IRequest	Create_Request	()=>	new	Request( this.Create_User() , this.Factory.Create_SAPLogon() )	;

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				public void DispatchRequest_ToFile(		IRequest	request
																						, string		fullPath )
					{
						try
							{
								this.PreTransport( request );
								this.IO.WriteFile(	fullPath
																	, this.Serializer.Serialize( request , this._ReqTypes.Value ) );
							}
						catch (Exception ex)
							{
								throw	new	Exception( "Request to XML file failure" , ex );
							}
					}

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				public IRequest ReceiveRequest_FromFile( string fullPath )
					{
						try
							{
								IRequest	lo_Req	= this.Serializer.DeSerialize<IRequest>(	this.IO.ReadFile( fullPath )
																																					, this._ReqTypes.Value					);
								this.PostTransport( lo_Req );
								return	lo_Req;
							}
						catch (Exception ex)
							{
								throw	new	Exception( "Request from XML file failure" , ex );
							}
					}

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				public	string			SerializeXMLConfig	( IXMLConfig config )	=>	this.Serializer.Serialize								( config ).Replace("\n","").Replace("\r","")	;
				public	IXMLConfig	DeserializeXMLConfig( string config )			=>	this.Serializer.DeSerialize<IXMLConfig>	( config , this._CfgTypes.Value )							;

			#endregion

		}
}