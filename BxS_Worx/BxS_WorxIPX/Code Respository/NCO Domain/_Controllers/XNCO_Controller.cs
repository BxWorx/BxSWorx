using System;
using System.Collections.Generic;
//.........................................................
using BxS_WorxIPX.BDC;
using BxS_WorxUtil.General;
using static	BxS_WorxIPX.Main.IPX_Constants;
//•••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••
namespace BxS_WorxIPX.NCO
{
	internal sealed class NCOx_Controller : INCOx_Controller
		{
			#region "Constructors: Singleton"

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				private		static readonly	Lazy< INCOx_Controller >	_Instance		= new		Lazy< INCOx_Controller >( ()=>	new NCOx_Controller() , cz_LM );

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				internal static INCOx_Controller Instance		{	get => _Instance.Value; }

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				private NCOx_Controller()
					{
						this._CfgTypes	= new Lazy< List<Type> >	( ()=>  new List<Type> {	typeof(	DTO_SAPSessionRequest	)	}	, cz_LM );
					}

			#endregion

			//===========================================================================================
			#region "Declarations"

				private	readonly	Lazy< List<Type> >	_CfgTypes	;

			#endregion

			//===========================================================================================
			#region "Properties"

				private	Serializer	Serializer	{ get	{ return	IPX_ToolSet.Instance.Serializer	; } }

			#endregion

			//===========================================================================================
			#region "Methods: Exposed"

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				public	ISAP_Logon	NewSAPLogon	()=>	new	SAP_Logon()	;

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				public	DTO_SAPSessionRequest	NewSAPSessionRequest	()=>	new	DTO_SAPSessionRequest();

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				public IList<IDTO_Session> RequestSAPSessionList( DTO_SAPSessionRequest Request )
					{
						IList<IDTO_Session>	lt	= new List<IDTO_Session>();
						//...
						if (Request.User.Equals(""))
							{
							}
						//...
						// Mockup: start
						//...
						for ( int i = 0; i < 10; i++ )
							{
								var d		= new DTO_Session	{
																						UserID       = $"User-{i.ToString()}" ,
																						SessionName  = $"Session-{i.ToString()}" ,
																						CreationDate = DateTime.Today ,
																						CreationTime = new TimeSpan(DateTime.Now.Hour , DateTime.Now.Minute , DateTime.Now.Second) ,
																						SAPTCode     = $"SAPTCde-{i.ToString()}"
																					};

								lt.Add( d );
							}
						//...
						// Mockup: end
						//...

						//...
						return	lt;
					}

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				public	string								SerializeSAPSessionRequest		( DTO_SAPSessionRequest request )	=>	this.Serializer.Serialize	( request , this._CfgTypes.Value , true )											;
				public	DTO_SAPSessionRequest	DeserializeSAPSessionRequest	( string request )								=>	this.Serializer.DeSerialize<DTO_SAPSessionRequest>	( request , this._CfgTypes.Value )	;

			#endregion

		}
}
				//ISAP_Session_Profile	GetSAPSessionData(	string	sessionName
				//																				,	string	QID
				//																				, bool		onlyHeader	= false
				//																				, bool		inclDDIC		= true	);
