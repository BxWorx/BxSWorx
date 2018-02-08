using System;
using System.Threading;
//.........................................................
using BxS_SAPNCO.Destination;
using BxS_SAPNCO.API.DL;
using BxS_SAPNCO.Helpers;
//•••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••
namespace BxS_SAPNCO.API
{
	public partial class NCOController
		{
			#region "Constructors"

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				public NCOController(	bool	LoadSAPGUIConfig	= true	,
															bool	FirstReset				= false		)
					{
						this._LoadSAPGUICfg		= LoadSAPGUIConfig;
						this._FirstReset			= FirstReset;
						//.............................................
						this._Started		= false;
					}

			#endregion

			//===========================================================================================
			#region "Declarations"

				private bool						_Started;
				//.................................................
				private	readonly	bool	_LoadSAPGUICfg;
				private	readonly	bool	_FirstReset;
				//.................................................
				private readonly
					Lazy<DestinationRepository>	_DestRepos		= new Lazy<DestinationRepository>
																													(	() => new DestinationRepository()
																														, LazyThreadSafetyMode.ExecutionAndPublication );

				private readonly
					Lazy<IDTOConfigSetupGlobal>	_GlobalSetup	= new Lazy<IDTOConfigSetupGlobal>
																													(	() => new DTOConfigSetupGlobal()
																														, LazyThreadSafetyMode.ExecutionAndPublication );

			#endregion

			//===========================================================================================
			#region "Properties"

				public DestinationRepository	Repository	{ get {	return	this._DestRepos		.Value; } }
				public IDTOConfigSetupGlobal	GlobalSetup	{ get {	return	this._GlobalSetup	.Value; } }

			#endregion

			//===========================================================================================
			#region "Methods: Private"

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				private void Startup()
					{
						if (this._Started)	return;
						//.............................................
						if (this._LoadSAPGUICfg)	this.LoadRepositoryFromConfig(this._FirstReset);
						this._Started	= true;
					}

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				private void LoadRepositoryFromConfig(bool FirstReset = false)
					{
						if (FirstReset)	this._DestRepos.Value.Reset();
						//.............................................
						SAPLogonINI.LoadRepository(this._DestRepos.Value);
					}

			#endregion

		}
}
