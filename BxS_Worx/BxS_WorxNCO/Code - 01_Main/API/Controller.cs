using System;
using System.Threading;
using System.Collections.Generic;
//.........................................................
using BxS_WorxNCO.Main.API;
using BxS_WorxNCO.Destination.API;
using BxS_WorxNCO.Destination.Main;
using BxS_WorxNCO.RfcFunction.Main;
//•••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••
namespace BxS_WorxNCO.Main
{
	internal class Controller : IController
		{
			#region "Constructors"

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				internal Controller()
					{
						this._Cntlr_Dst		= new Lazy<IDestinationController>
																			(		()=>	new DestinationController()
																				, LazyThreadSafetyMode.ExecutionAndPublication	);
						//.............................................
						this._Cntlr_Rfc		= new Lazy<IRfcFncController>
																			(		()=>	new RfcFncController()
																				, LazyThreadSafetyMode.ExecutionAndPublication	);
					}

			#endregion

			//===========================================================================================
			#region "Declarations"

				private readonly Lazy<IDestinationController>		_Cntlr_Dst;
				private readonly Lazy<IRfcFncController>				_Cntlr_Rfc;

			#endregion

			//===========================================================================================
			#region "Properties"

				public int	LoadedSystemCount { get { return	this._DestRepos.Value.Count; } }

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
				// List of only requested SAP systems
				//
				public IList<ISAPSystemReference> GetSAPSystems()
					{
						IList<ISAPSystemReference>	lt_List	= new List<ISAPSystemReference>(this.LoadedSystemCount);
						//.............................................
						foreach ( KeyValuePair< Guid , string > ls_kvp in this._DestRepos.Value.SAPSystems )
							{
								lt_List.Add( this.CreateSAPSysRef( ls_kvp.Key , ls_kvp.Value	) );
							}
						//.............................................
						return	lt_List;
					}

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				public IRfcDestination GetDestination( string ID )
					{
						IRfcDestination lo	= this._DestRepos.Value.GetDestination( ID );
						lo.LoadConfig( this._GlobalSetup.Value );
						return	lo;
					}

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				public IRfcDestination GetDestination( Guid ID )
					{
						IRfcDestination lo	= this._DestRepos.Value.GetDestination( ID );
						lo.LoadConfig( this._GlobalSetup.Value );
						return	lo;
					}

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				public void LoadGlobalConfig( IConfigSetupGlobal config )
					{
						foreach (KeyValuePair<string, string> ls_kvp in config.Settings )
							{
								this._GlobalSetup.Value.Settings[ls_kvp.Key]	= ls_kvp.Value;
							}
					}

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				public void Reset()
					{
						this._DestRepos.Value.Reset();
						this._GlobalSetup.Value.Settings.Clear();
					}

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				public IConfigSetupDestination	CreateDestinationConfig	()=>	new ConfigSetupDestination()	;
				public IConfigSetupGlobal				CreateGlobalConfig			()=>	new ConfigSetupGlobal()				;

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
