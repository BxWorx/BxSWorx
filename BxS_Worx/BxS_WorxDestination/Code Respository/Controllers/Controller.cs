using System;
using System.Threading;
using System.Collections.Generic;
//.........................................................
using SMC	= SAP.Middleware.Connector;
//.........................................................
using BxS_WorxDestination.DTO;
using BxS_WorxDestination.API;
using BxS_WorxIPX.API.Destination;
//•••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••
namespace BxS_WorxDestination.Main
{
	internal class Controller : IController
		{
			#region "Constructors"

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				internal Controller( bool autoLoadSAPINI = true )
					{
						if (autoLoadSAPINI)
							{
								this.LoadSAPINI();
							}
					}

			#endregion

			//===========================================================================================
			#region "Declarations"

				private readonly	Lazy<Repository>						_DestRepos
						= new Lazy<Repository>	(		() => new Repository()
																			, LazyThreadSafetyMode.ExecutionAndPublication );
				//...............................................
				private readonly	Lazy<IConfigSetupGlobal>		_GlobalSetup
						= new Lazy<IConfigSetupGlobal>	(	() => new ConfigSetupGlobal()
																								, LazyThreadSafetyMode.ExecutionAndPublication );
				//...............................................
				//private readonly	Lazy<SAPLogonINI>						_SAPINI
				//		= new Lazy<SAPLogonINI>	(	() => new SAPLogonINI()
				//																				, LazyThreadSafetyMode.ExecutionAndPublication );
				//private readonly
				//	Lazy<SMC.SapLogonIniConfiguration>	_SAPINI		= new Lazy<SMC.SapLogonIniConfiguration>
				//																											(	() => SMC.SapLogonIniConfiguration.Create()
				//																												, LazyThreadSafetyMode.ExecutionAndPublication	);

			#endregion

			//===========================================================================================
			#region "Properties"

				internal Repository	Repository	{ get {	return	this._DestRepos		.Value; } }
				internal IConfigSetupGlobal	GlobalSetup	{ get {	return	this._GlobalSetup	.Value; } }

				public int Count => this._DestRepos.Value.Count;

			#endregion

			//===========================================================================================
			#region "Methods"

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				// List as per SAP Logon GUI setup
				//
				public IList<string> GetSAPINIList()
					{
						return	this._DestRepos.Value.GetSAPINIList();
					}

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				// List of SAP systems requested only
				//
				public IList<ISAPSystemReference> GetSAPSystems()
					{
						IList<ISAPSystemReference>	lt_List	= new List<ISAPSystemReference>(this.Count);
						//.............................................
						foreach ( KeyValuePair< Guid , string > ls_kvp in this._DestRepos.Value.SAPSystems )
							{
								lt_List.Add( this.CreateSAPSysRef( ls_kvp.Key , ls_kvp.Value	) );
							}
						//.............................................
						return	lt_List;
					}

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				public IDestination GetDestination( string ID )
					{
						Guid										lg						= this._DestRepos.Value.GetAddIDFor( ID , true );
						SMC.RfcConfigParameters	lo_rfcConfig	=	this._DestRepos.Value.GetParameters(lg);
						//.............................................
						return	GetDestinationRFC(lg);

								SMC.RfcConfigParameters lo_RfcCfgParms	= _SAPINI.Value.GetParameters(lc_ID)	?? new SMC.RfcConfigParameters();
								destinationRepository.AddConfig(lc_ID, lo_RfcCfgParms);




					}







				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				public bool LoadSAPINI()
					{
						try
							{
								this._SAPINI.Value.LoadRepository( this._DestRepos.Value );
								return	true;
							}
						catch
							{
								return	false;
							}
					}

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				public void Reset()
					{
						this._DestRepos.Value.Reset();
					}


				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				public IDestination GetDestination(Guid ID)
					{
						SMC.RfcConfigParameters	lo_rfcConfig	=	this._DestRepos.Value.GetParameters(ID);
						//.............................................
						return	new DestinationRfc(		ID
																				,	lo_rfcConfig
																				,	this._GlobalSetup.Value	);
					}



				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				public IDTOConfigSetupDestination CreateConfigSetupDestination()
					{
						return	new	DTOConfigSetupDestination();
					}




			#endregion

			//===========================================================================================
			#region "Methods: Private"


				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				private ISAPSystemReference CreateSAPSysRef(	Guid		id
																										,	string	name )
					{
						ISAPSystemReference lo	= new SAPSystemReference{		ID			= id
																															,	SAPName	= name };
						return	lo;
					}

			#endregion

		}
}
