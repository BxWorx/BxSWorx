using System;
using System.Collections.Generic;
//.........................................................
using SMC	= SAP.Middleware.Connector;
using SDM	= SAP.Middleware.Connector.RfcDestinationManager;
//.........................................................
using BxS_SAPNCO.API.DL;
//•••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••
namespace BxS_SAPNCO.Destination
{
	internal class DestinationRepository : SMC.IDestinationConfiguration
		{
			#region "Constructors"

				internal DestinationRepository()
					{
						this._Map		= new Dictionary<string	, System.Guid>							();
						this._Des		= new Dictionary<Guid		, SMC.RfcConfigParameters>	();
						this._Evt		= true;
					}

			#endregion

			//===========================================================================================
			#region "Declarations"

				private readonly bool																						_Evt;
				private readonly Dictionary<string,	Guid>												_Map;
				private readonly Dictionary<Guid	, SMC.RfcConfigParameters>		_Des;

			#endregion

			//===========================================================================================
			#region "Properties"

				internal bool ChangeEventSupported	{ get; set; }

			#endregion

			//===========================================================================================
			#region "Methods: Exposed"

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				internal IList<IDTORefEntry> ReferenceList()
					{
						IList<IDTORefEntry>	lt_List	= new List<IDTORefEntry>(this._Map.Count-1);
						//.............................................
						foreach (KeyValuePair<string, Guid> ls_kvp in this._Map)
							{
								lt_List.Add( new DTORefEntry{ ID		= ls_kvp.Value	,
																							Name	= ls_kvp.Key			} );
							}
						//.............................................
						return	lt_List;
					}

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				internal Guid AddConfig(string ID, SMC.RfcConfigParameters rfcConfig)
					{
						return	this.AddConfig(	this.GetAddIDFor(ID)	,
																		rfcConfig									);
					}

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				internal Guid AddConfig(Guid ID, SMC.RfcConfigParameters rfcConfig)
					{
						this._Des[ID]	= rfcConfig;
						return	ID;
					}

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				internal Guid GetAddIDFor(string ID, bool Add = true)
					{
						if (!this._Map.TryGetValue(ID, out Guid lg_Guid))
							{
								lg_Guid	= Guid.NewGuid();
								if (Add)	this._Map.Add(ID, lg_Guid);
							}
						//.............................................
						return	lg_Guid;
					}

			#endregion

			//===========================================================================================
			#region "Methods: Interface"

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				public event SDM.ConfigurationChangeHandler		ConfigurationChanged;

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				public SMC.RfcConfigParameters GetParameters(string destinationName)
					{
						Guid lg = this.GetAddIDFor(destinationName, false);

						if (			lg != Guid.Empty
									&&	this._Des.TryGetValue(lg, out SMC.RfcConfigParameters lo_Cnf)	)
							{
								return lo_Cnf;
							}
						//.............................................
						return	new SMC.RfcConfigParameters();
					}

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				public bool ChangeEventsSupported()
					{
						return	this._Evt;
					}

			#endregion

		}
}
