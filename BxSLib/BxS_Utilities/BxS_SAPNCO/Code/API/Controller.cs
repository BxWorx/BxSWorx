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
	public class Controller
		{
			#region "Constructors"

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				public Controller(DestinationManager destinationManager)
					{
						this._DestMngrx	= destinationManager;
						this._LoadSAPIni	= LoadSAPIni;
						//if (LoadSAPIni)	this._SAPIni.Value.LoadRepository(this._DestRep.Value);
					}

			#endregion

			//===========================================================================================
			#region "Declarations"

				private readonly DestinationManager _DestMngrx;

				private bool _LoadSAPIni;

				private	readonly	ConnFactory				_ConnFac	= new ConnFactory();

				private readonly	Lazy<DestinationManager>		_DestMngr
														= new Lazy<DestinationManager>		(	() => new DestinationManager()							,
																																LazyThreadSafetyMode.ExecutionAndPublication		);

				private readonly	Lazy<DestinationRepository>	_DestRep
														= new Lazy<DestinationRepository>	(	() => new DestinationRepository()							,
																																LazyThreadSafetyMode.ExecutionAndPublication		);

				private readonly	Lazy<SAPLogonINI>						_SAPIni
														= new Lazy<SAPLogonINI>						(	() => new SAPLogonINI()												,
																																LazyThreadSafetyMode.ExecutionAndPublication		);

				private readonly	Lazy<Parser>								_Parser
														= new Lazy<Parser>								(	() => new Parser()														,
																																LazyThreadSafetyMode.ExecutionAndPublication		);

			#endregion

			//===========================================================================================
			#region "Methods: Exposed"

				public Guid GetDestination(SMC.RfcConfigParameters rfcConfig)
					{
						return	this._DestMngr.Value.GetRfcDestination(rfcConfig);
					}

				public Guid GetDestination(string destinationName)
					{
						return	this._DestMngr.Value.GetRfcDestination(destinationName);
					}

				public bool AddConfig(string ID, SMC.RfcConfigParameters rfcConfig)
					{
						return	this._DestRep.Value.AddConfig(ID, rfcConfig);
					}







				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				public IDTOConnParameters FetchParameters(string ID)
					{
						IDTOConnParameters lo_DTO	= this._ConnFac.CreateParameterDTO();
						this._SAPIni.Value.LoadParameters(ID, lo_DTO);
						return	lo_DTO;
					}

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				public SMC.RfcConfigParameters	Parse(IDTOConnParameters DTO)
					{
						return	this._Parser.Value.Parse(DTO);
					}







				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				public IList<IDTORefEntry> ConnectionReferenceList()
					{
						return	this._DestRep.Value.ReferenceList();
					}

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				public SMC.RfcConfigParameters GetSAPIniConfig(string ID)
					{
						return	this._SAPIni.Value.GetConfig(ID);
					}

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				public IList<string>	GetSAPIniList()
					{
						return	this._SAPIni.Value.GetEntries();
					}

			#endregion

		}
}
