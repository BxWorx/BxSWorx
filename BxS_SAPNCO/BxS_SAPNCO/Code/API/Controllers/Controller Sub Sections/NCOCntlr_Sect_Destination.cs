using System;
using System.Threading;
//.........................................................
using SMC	= SAP.Middleware.Connector;
//.........................................................
using BxS_SAPNCO.Destination;
using BxS_SAPNCO.API.DL;
//•••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••
namespace BxS_SAPNCO.API
{
	public partial class NCOController
		{
			#region "Section: Destination"

				//=========================================================================================
				#region "Declarations"

					private readonly
						Lazy<IDTOConfigSetupGlobal>		_GlobalSetup
							= new Lazy<IDTOConfigSetupGlobal>	(	() => new DTOConfigSetupGlobal()
																									, LazyThreadSafetyMode.ExecutionAndPublication );
					//...............................................
					private readonly
						Lazy<DestinationRepository>		_DestRepos
							= new Lazy<DestinationRepository>	(	() => new DestinationRepository()
																									, LazyThreadSafetyMode.ExecutionAndPublication );

				#endregion

				//===========================================================================================
				#region "Properties"

					public DestinationRepository	Repository	{ get {	return	this._DestRepos		.Value; } }
					public IDTOConfigSetupGlobal	GlobalSetup	{ get {	return	this._GlobalSetup	.Value; } }

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
							this.Startup();
							//.............................................
							SMC.RfcConfigParameters	lo_rfcConfig	=	this._DestRepos.Value.GetParameters(ID);

							return	new DestinationRfc(	lo_rfcConfig						,
																					this._GlobalSetup.Value		)	{	SAPGUIID = ID	}	;
						}

					//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
					public DestinationRfc CreateDestinationRFC(string ID)
						{
							this.Startup();
							//.............................................
							Guid lg = this._DestRepos.Value.GetAddIDFor(ID);
							//.............................................
							return	CreateDestinationRFC(lg);
						}

				#endregion

			#endregion

		}
}
