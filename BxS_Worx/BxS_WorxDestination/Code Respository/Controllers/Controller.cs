using System;
using System.Threading;
using System.Collections.Generic;
//.........................................................
using BxS_WorxIPX.API.Destination;
using BxS_WorxDestination.API.Destination;
using BxS_WorxDestination.Config;
//•••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••
namespace BxS_WorxDestination.Main
{
	internal class Controller : IController
		{
			#region "Constructors"

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				internal Controller()
					{
					}

				////¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				//internal Controller( bool autoLoadSAPINI = true )
				//	{
				//		if (autoLoadSAPINI)
				//			{
				//				this.LoadSAPINI();
				//			}
				//	}

			#endregion

			//===========================================================================================
			#region "Declarations"

				private readonly	Lazy<Repository>						_DestRepos
						= new Lazy<Repository>	(		() => new Repository(	( Guid ID )	=>	new Destination( ID ) )
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
						return	this._DestRepos.Value.GetDestination( ID );
					}

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				public IDestination GetDestination(Guid ID)
					{
						return	this._DestRepos.Value.GetDestination( ID );
					}

				////¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				//public bool LoadSAPINI()
				//	{
				//		try
				//			{
				//				this._SAPINI.Value.LoadRepository( this._DestRepos.Value );
				//				return	true;
				//			}
				//		catch
				//			{
				//				return	false;
				//			}
				//	}

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				public void Reset()
					{
						this._DestRepos.Value.Reset();
					}

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				public IConfigSetupDestination	CreateDestinationSetup	()=>	new ConfigSetupDestination()	;
				public IConfigSetupGlobal				CreateGlobalSetup				()=>	new ConfigSetupGlobal()				;

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
