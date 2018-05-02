using System;
using System.Collections.Generic;
using System.Threading;
//.........................................................
using BxS_WorxNCO.Destination.API;
using BxS_WorxNCO.Destination.Main;
using BxS_WorxNCO.Destination.Main.Destination;
using BxS_WorxNCO.Destination.Config;

using BxS_WorxNCO.BDCSession.API;
using BxS_WorxNCO.BDCSession.DTO;
using BxS_WorxNCO.SAPSession.API;

using BxS_WorxIPX.Main;
using BxS_WorxIPX.NCO;

using BxS_WorxUtil.Main;
using BxS_WorxUtil.Progress;

using static	BxS_WorxNCO.Main.NCO_Constants;
//•••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••
namespace BxS_WorxNCO.API
{
	public sealed class NCO_Controller : INCO_Controller
		{
			#region "Constructors: Singleton"

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				private NCO_Controller()
					{
						this._DestRepos			= new Lazy<Repository>		(	()=>	new Repository	( this.CreateDestination )	, cz_LM );
						this._GlobalSetup		= new Lazy<IConfigGlobal>	( ()=>	new ConfigGlobal()													, cz_LM );
					}
				//.................................................
				private	static readonly		Lazy< NCO_Controller >	_Instance		= new	Lazy< NCO_Controller >( ()=> new NCO_Controller() , cz_LM );

				public	static	NCO_Controller	Instance	{	get { return _Instance.Value; }	}

			#endregion

			//===========================================================================================
			#region "Declarations"

				private readonly	Lazy< Repository >			_DestRepos;
				private readonly	Lazy< IConfigGlobal >		_GlobalSetup;

			#endregion

			//===========================================================================================
			#region "Properties"

				public	IIPX_Controller		IPX_Cntlr		{ get	{	return	IPX_Controller.Instance	; } }
				public	IUTL_Controller		UTL_Cntlr		{ get	{	return	UTL_Controller.Instance	; } }
				//.................................................
				private	int		LoadedSystemCount				{ get { return	this._DestRepos.Value.Count; } }

			#endregion

			//===========================================================================================
			#region "Methods: Exposed: Destination"

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				// List as per SAP Logon GUI setup
				//
				public IList<string> GetSAPINIList()	=>	this._DestRepos.Value.GetSAPINIList();

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				// List of requested SAP systems
				//
				public IList<ISAPSystemReference> GetSAPSystems()
					{
						IList<ISAPSystemReference>	lt_List		= new List<ISAPSystemReference>( this.LoadedSystemCount );
						//.............................................
						foreach ( KeyValuePair< Guid , string > ls_kvp in this._DestRepos.Value.SAPSystems )
							{
								lt_List.Add( this.CreateSAPSysRef( ls_kvp.Key , ls_kvp.Value	) );
							}
						//.............................................
						return	lt_List;
					}

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				public IBxSDestination GetDestination( string ID , bool useSAPINI = true )
					{
						IBxSDestination lo_BxSDest	= this._DestRepos.Value.GetDestination( ID , useSAPINI );

						if ( this._GlobalSetup.IsValueCreated )
							{
								lo_BxSDest.LoadConfig( this._GlobalSetup.Value );
							}
						//...
						return	lo_BxSDest;
					}

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				public IBxSDestination GetDestination( Guid ID , bool useSAPINI = true )
					{
						IBxSDestination lo_BxSDest	= this._DestRepos.Value.GetDestination( ID , useSAPINI );
						if ( this._GlobalSetup.IsValueCreated )
							{
								lo_BxSDest.LoadConfig( this._GlobalSetup.Value );
							}
						//...
						return	lo_BxSDest;
					}

			#endregion

			//===========================================================================================
			#region "Methods: Exposed: General"

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				public IConfigGlobal	CreateGlobalConfig()	=>	new ConfigGlobal();

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				public void LoadGlobalConfig( IConfigGlobal config )
					{
						foreach (KeyValuePair<string, string> ls_kvp in config.Settings )
							{
								this._GlobalSetup.Value.Settings[ls_kvp.Key]	= ls_kvp.Value;
							}
					}

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				public void Reset()
					{
						if ( this._DestRepos	.IsValueCreated )		this._DestRepos		.Value.Reset();
						if ( this._GlobalSetup.IsValueCreated )		this._GlobalSetup	.Value.Settings.Clear();
					}

			#endregion

			//===========================================================================================
			#region "Methods: Exposed: SAP Session Handling"

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				public ISAP_Session_Manager	CreateSAPSessionManager( IBxSDestination rfcDestination )
					{
						return	new SAP_Session_Manager( rfcDestination );
					}

			#endregion

			//===========================================================================================
			#region "Methods: Exposed: BDC Request Handling"

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				public IBDC_Request_Manager	CreateBDCRequestManager( ISAP_Logon	sapLogon )
					{
						IBxSDestination lo_BxSDest	= this.GetDestination(	sapLogon.SAPSysID
																															, sapLogon.IsSAPINI );
						//...
						IConfigLogon lo_Cfg = Destination_Factory.CreateLogonConfig();

						lo_Cfg.Client						= sapLogon.Client			;
						lo_Cfg.User							= sapLogon.User				;
						lo_Cfg.Language					= sapLogon.Lang				;
						lo_Cfg.Password					= sapLogon.Pwrd				;
						lo_Cfg.SecurePassword		= sapLogon.SecurePwrd	;

						lo_BxSDest.LoadConfig( lo_Cfg );
						//...
						return	new BDC_Request_Manager( lo_BxSDest );
					}

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				public ProgressHandler< DTO_BDC_Progress > CreateBDCSessionProgressHandler()
					{
						return	this.UTL_Cntlr.CreateProgressHandler< DTO_BDC_Progress >( this.CreateProgress );
					}

			#endregion

			//===========================================================================================
			#region "Methods: Private"

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				private	IBxSDestination		CreateDestination	( Guid ID )	=>	new BxSDestination( ID )	;
				private	DTO_BDC_Progress	CreateProgress		()					=>	new	DTO_BDC_Progress()		;

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				private ISAPSystemReference CreateSAPSysRef(	Guid		id
																										,	string	name
																										,	bool		isSSO	= false	)
					{
						return	Destination_Factory.CreateSAPSystemReference( id , name , isSSO );
					}

			#endregion

		}
}