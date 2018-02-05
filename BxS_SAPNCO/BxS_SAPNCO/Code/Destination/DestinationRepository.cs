using System;
using System.Collections.Generic;
//.........................................................
using SMC	= SAP.Middleware.Connector;
//.........................................................
using BxS_SAPNCO.API.DL;
//•••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••
namespace BxS_SAPNCO.Destination
{
	public class DestinationRepository
		{
			#region "Constructors"

				public DestinationRepository()
					{
						this._Map		= new Dictionary<string	, Guid>											();
						this._Ref		= new Dictionary<Guid		,	string>										();
						this._Des		= new Dictionary<Guid		, SMC.RfcConfigParameters>	();
					}

			#endregion

			//===========================================================================================
			#region "Declarations"

				private readonly Dictionary<string,	Guid>												_Map;
				private readonly Dictionary<Guid	,	string>											_Ref;
				private readonly Dictionary<Guid	, SMC.RfcConfigParameters>		_Des;

			#endregion

			//===========================================================================================
			#region "Properties"

				internal int	Count	{ get	{ return	this._Map.Count; }	}

			#endregion

			//===========================================================================================
			#region "Methods: Exposed"

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				public void Reset()
					{

					}
				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				public SMC.RfcConfigParameters GetParameters(Guid ID)
					{
						if (this._Des.TryGetValue(ID, out SMC.RfcConfigParameters lo_Cnf)	)
							{
								return lo_Cnf;
							}
						//.............................................
						return	new SMC.RfcConfigParameters();
					}

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				public SMC.RfcConfigParameters GetParameters(string ID)
					{
						Guid lg = this.GetAddIDFor(ID);
						return	this.GetParameters(lg);
					}

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				public IList<IDTORefEntry> ReferenceList()
					{
						IList<IDTORefEntry>	lt_List	= new List<IDTORefEntry>(this.Count);
						//.............................................
						foreach (KeyValuePair<Guid, string> ls_kvp in this._Ref)
							{
								lt_List.Add( new DTORefEntry{ ID		= ls_kvp.Key		,
																							Name	= ls_kvp.Value		} );
							}
						//.............................................
						return	lt_List;
					}

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				public Guid AddConfig(string ID, SMC.RfcConfigParameters rfcConfig)
					{
						return	this.AddConfig(	this.GetAddIDFor(ID, true)	,
																		rfcConfig											);
					}

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				public Guid AddConfig(Guid ID, SMC.RfcConfigParameters rfcConfig)
					{
						this._Des[ID]	= rfcConfig;
						var lo_E = new SMC.RfcConfigurationEventArgs(SMC.RfcConfigParameters.EventType.CHANGED);
						return	ID;
					}

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				public string GetNameFor(Guid ID)
					{
						return	this._Ref[ID] ?? string.Empty;
					}

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				public Guid GetAddIDFor(string ID, bool Add = false)
					{
						if (ID == null)	return	Guid.NewGuid();
						//.............................................
						if (!this._Map.TryGetValue(ID, out Guid lg_Guid))
							{
								lg_Guid	= Guid.NewGuid();
								if (Add)
									{
										this._Map[ID]				= lg_Guid;
										this._Ref[lg_Guid]	= ID;
									}
							}
						//.............................................
						return	lg_Guid;
					}

			#endregion

		}
}
