using System;
using System.Collections.Generic;
using System.Threading;
//.........................................................
using SMC	= SAP.Middleware.Connector;
//.........................................................
using BxS_SAPConn.API;
using BxS_SAPNCO.Destination;
using BxS_SAPNCO.API.DL;
//•••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••
namespace BxS_SAPNCO.API
{
	public class DestinationController
		{
			#region "Constructors"

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				public DestinationController(	bool LoadSAPIni		= true	,
																			bool AutoRegister	= true		)
					{
						if (LoadSAPIni)		this.LoadSAPINIintoRepository();
						if (AutoRegister)	this.RegisterRepository();
					}

			#endregion

			//===========================================================================================
			#region "Declarations"

				private readonly	Lazy<DestinationRepository>					_DestRepos
														= new Lazy<DestinationRepository>
															(	() => new DestinationRepository()
																, LazyThreadSafetyMode.ExecutionAndPublication	);

				private readonly	Lazy<SMC.SapLogonIniConfiguration>	_SAPINI
														= new Lazy<SMC.SapLogonIniConfiguration>
															(	() => SMC.SapLogonIniConfiguration.Create()
																, LazyThreadSafetyMode.ExecutionAndPublication	);

				private readonly	Lazy<DestinationManager>						_DestMngr
														= new Lazy<DestinationManager>
															(	() => new DestinationManager()
																,	LazyThreadSafetyMode.ExecutionAndPublication	);

			#endregion

			//===========================================================================================
			#region "Methods: Exposed"

				//public Guid GetDestination(SMC.RfcConfigParameters rfcConfig)
				//	{
				//		return	this._DestMngr.Value.GetRfcDestination(rfcConfig);
				//	}

				//public Guid GetDestination(string destinationName)
				//	{
				//		return	this._DestMngr.Value.GetRfcDestination(destinationName);
				//	}

				//public bool AddConfig(string ID, SMC.RfcConfigParameters rfcConfig)
				//	{
				//		return	this._DestRep.Value.AddConfig(ID, rfcConfig);
				//	}







				////¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				//public IDTOConnParameters FetchParameters(string ID)
				//	{
				//		IDTOConnParameters lo_DTO	= this._ConnFac.CreateParameterDTO();
				//		this._SAPIni.Value.LoadParameters(ID, lo_DTO);
				//		return	lo_DTO;
				//	}











				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				public IList<IDTORefEntry> ConnectionReferenceList()
					{
						return	this._DestRepos.Value.ReferenceList();
					}

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				public SMC.RfcConfigParameters GetSAPIniConfig(string ID)
					{
						return	this._SAPINI.Value.GetParameters(ID);
					}

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				public IList<string>	GetSAPIniList()
					{
						return	this._SAPINI.Value.GetEntries();
					}

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				public void RegisterRepository()
					{
						this._DestMngr.Value.Register(this._DestRepos.Value);
					}

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				public void LoadSAPINIintoRepository()
					{
						foreach (string lc_ID in this._SAPINI.Value.GetEntries())
							{
								SMC.RfcConfigParameters lo = this._SAPINI.Value.GetParameters(lc_ID);
								if (lo is null)		continue;
								this._DestRepos.Value.AddConfig(lc_ID, lo);
							}
					}

			#endregion

			//===========================================================================================
			#region "Methods: Private"

			#endregion

		}
}
