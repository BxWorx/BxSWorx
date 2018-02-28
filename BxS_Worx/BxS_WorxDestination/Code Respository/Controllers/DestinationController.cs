using System;
using System.Threading;
//.........................................................
using SMC	= SAP.Middleware.Connector;
//.........................................................
using BxS_WorxDestination.DTO;
//•••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••
namespace BxS_WorxDestination.Main
{
	internal class DestinationController
		{
			#region "Constructors"

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				internal DestinationController( bool autoLoadSAPINI = true )
					{
						if (autoLoadSAPINI)
							{
								this._SAPINI.Value.LoadRepository( this._DestRepos.Value );
							}
					}

			#endregion

			//===========================================================================================
			#region "Declarations"

				private readonly	Lazy<DestinationRepository>		_DestRepos
						= new Lazy<DestinationRepository>	(	() => new DestinationRepository()
																								, LazyThreadSafetyMode.ExecutionAndPublication );
				//...............................................
				private readonly	Lazy<IDTOConfigSetupGlobal>		_GlobalSetup
						= new Lazy<IDTOConfigSetupGlobal>	(	() => new DTOConfigSetupGlobal()
																								, LazyThreadSafetyMode.ExecutionAndPublication );
				//...............................................
				private readonly	Lazy<SAPLogonINI>							_SAPINI
						= new Lazy<SAPLogonINI>	(	() => new SAPLogonINI()
																								, LazyThreadSafetyMode.ExecutionAndPublication );

			#endregion

			//===========================================================================================
			#region "Properties"

				internal DestinationRepository	Repository	{ get {	return	this._DestRepos		.Value; } }
				internal IDTOConfigSetupGlobal	GlobalSetup	{ get {	return	this._GlobalSetup	.Value; } }

			#endregion

			//===========================================================================================
			#region "Methods"

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				public IDTOConfigSetupDestination CreateConfigSetupDestination()
					{
						return	new	DTOConfigSetupDestination();
					}

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				public DestinationRfc CreateDestinationRFC(Guid ID)
					{
						SMC.RfcConfigParameters	lo_rfcConfig	=	this._DestRepos.Value.GetParameters(ID);
						//.............................................
						return	new DestinationRfc(	lo_rfcConfig						,
																				this._GlobalSetup.Value		)	{	SAPGUIID = ID	}	;
					}

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				public DestinationRfc CreateDestinationRFC(string ID)
					{
						Guid lg = this._DestRepos.Value.GetAddIDFor(ID);
						//.............................................
						return	CreateDestinationRFC(lg);
					}

			#endregion

		}
}
