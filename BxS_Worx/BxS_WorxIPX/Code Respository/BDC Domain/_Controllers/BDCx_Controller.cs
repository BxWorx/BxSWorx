using System;
using System.Collections.Generic;
//.........................................................
using BxS_WorxIPX.NCO;
using BxS_WorxUtil.General;

using static	BxS_WorxIPX.Main.IPX_Constants;
//•••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••
namespace BxS_WorxIPX.BDC
{
	public partial class BDCx_Controller : IBDCx_Controller
		{
			#region "Constructors: Singleton"

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				private		static readonly	Lazy<IBDCx_Controller>	_Instance		= new	Lazy<IBDCx_Controller>(	()=>	new	BDCx_Controller()	, cz_LM );

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				internal static IBDCx_Controller Instance		{	get => _Instance.Value; }

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				private BDCx_Controller()
					{
						this._ReqTypes	= new Lazy< List<Type> >	( ()=>  new List<Type> {	typeof(	Request					)
																																							,	typeof(	Session					)
																																							, typeof(	SAP_Logon				)
																																							, typeof(	User						)
																																							,	typeof(	XMLConfig				)
																																							, typeof( Request_Config	) }	, cz_LM );

						this._CfgTypes	= new Lazy< List<Type> >	( ()=>  new List<Type> {	typeof(	XMLConfig	)	}	, cz_LM );
					}

			#endregion

			//===========================================================================================
			#region "Declarations"

				private	readonly	Lazy< List<Type> >	_ReqTypes	;
				private	readonly	Lazy< List<Type> >	_CfgTypes	;

			#endregion

			//===========================================================================================
			#region "Properties"

				public	string	XmlConfigTag	{ get	{	return	cz_XmlCfgTag	; } }
				//...
				private	INCOx_Controller	NCOx	{ get	{ return	NCOx_Controller.Instance	; } }
				//...
				private	IO					IO					{ get	{ return	IPX_ToolSet.Instance.IO					; } }
				private	Serializer	Serializer	{ get	{ return	IPX_ToolSet.Instance.Serializer	; } }

			#endregion

			//===========================================================================================
			#region "Methods: Exposed"

				////¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				//public	DTO_SAPSessionRequest	CreateSAPSessionListRequest()	=> new DTO_SAPSessionRequest();

				////¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				//public IList<IDTO_Session> RequestSAPSessionList( DTO_SAPSessionRequest Request )
				//	{
				//		IList<IDTO_Session>	lt	= new List<IDTO_Session>();
				//		//...
				//		if (Request.User.Equals(""))
				//			{
				//			}
				//		//...
				//		// Mockup: start
				//		//...
				//		for ( int i = 0; i < 10; i++ )
				//			{
				//				var d		= new DTO_Session	{
				//																		UserID       = $"User-{i.ToString()}" ,
				//																		SessionName  = $"Session-{i.ToString()}" ,
				//																		CreationDate = DateTime.Today ,
				//																		CreationTime = new TimeSpan(DateTime.Now.Hour , DateTime.Now.Minute , DateTime.Now.Second) ,
				//																		SAPTCode     = $"SAPTCde-{i.ToString()}"
				//																	};

				//				lt.Add( d );
				//			}
				//		//...
				//		// Mockup: end
				//		//...

				//		//...
				//		return	lt;
				//	}

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				//public	ISAP_Logon	Create_SAPLogon	()														=>	this.Factory.Create_SAPLogon();
				public	IXMLConfig	Create_XMLConfig( bool withDefaults = true )	=>	new XMLConfig( withDefaults );
				//...
				public	IUser			Create_User			()=>	new User();
				public	ISession	Create_Session	()=>	new	Session( this.Create_XMLConfig() )	;
				public	IRequest_Config	Create_RequestConfig()	=> new Request_Config()	;

				public	IRequest	Create_Request	()=>	new	Request( this.Create_User() , this.NCOx.NewSAPLogon()	, this.Create_RequestConfig() )	;

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				public void DispatchRequest_ToFile(		IRequest	request
																						, string		fullPath )
					{
						try
							{
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
								IRequest lo_Req	=	this.Serializer.DeSerialize<IRequest>(	this.IO.ReadFile( fullPath )
																																				, this._ReqTypes.Value					);
								lo_Req.Sync();
								return	lo_Req;
							}
						catch (Exception ex)
							{
								throw	new	Exception( "Request from XML file failure" , ex );
							}
					}

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				public	string			SerializeXMLConfig	( IXMLConfig config )	=>	this.Serializer.Serialize								( config , this._CfgTypes.Value , true );
				public	IXMLConfig	DeserializeXMLConfig( string config )			=>	this.Serializer.DeSerialize<XMLConfig>	( config , this._CfgTypes.Value )	;

			#endregion

		}
}