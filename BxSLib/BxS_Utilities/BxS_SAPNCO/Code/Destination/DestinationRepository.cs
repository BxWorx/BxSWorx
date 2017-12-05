using System;
using System.Collections.Generic;
//.........................................................
using SMC	= SAP.Middleware.Connector;
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
						this._Map							= new Dictionary<string	, System.Guid>							();
						this._Dest						= new Dictionary<Guid		, SMC.RfcConfigParameters>	();
						this._SupportChgEvent	= true;
					}

			#endregion

			//===========================================================================================
			#region "Declarations"

				private readonly bool																						_SupportChgEvent;
				private readonly Dictionary<string,	Guid>												_Map;
				private readonly Dictionary<Guid	, SMC.RfcConfigParameters>		_Dest;
				//.................................................
				public event SMC.RfcDestinationManager.ConfigurationChangeHandler		ConfigurationChanged;

			#endregion

			//===========================================================================================
			#region "Properties"

				internal bool ChangeEventSupported	{ get; set; }

			#endregion

			//===========================================================================================
			#region "Methods: Exposed"

				internal IList<IDTOConnection>	List()
					{
						IList<IDTOConnection>	lt_List	= new List<IDTOConnection>(this._Map.Count-1);
						//.............................................
						foreach (KeyValuePair<string, Guid> ls_kvp in this._Map)
							{
								lt_List.Add( new DTOConnection{ ID		= ls_kvp.Value	,
																						Name	= ls_kvp.Key			} );
							}
						//.............................................
						return	lt_List;
					}

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				internal Guid AddConfig(string ID, SMC.RfcConfigParameters rfcConfig)
					{
						Guid lg_Ret	= this.GetAddGuidFor(ID);
						//.............................................
						this._Dest[lg_Ret]	= rfcConfig;
						//.............................................
						return	lg_Ret;
					}

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				internal bool AddConfig(Guid ID, SMC.RfcConfigParameters rfcConfig)
					{
						this._Dest[ID]	= rfcConfig;
						return	true;
					}

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				internal Guid GetAddGuidFor(string ID, bool Add = true)
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
				public SMC.RfcConfigParameters GetParameters(string destinationName)
					{
						Guid lg = this.GetAddGuidFor(destinationName, false);

						if (			lg != Guid.Empty
									&&	this._Dest.TryGetValue(lg, out SMC.RfcConfigParameters lo_Cnf)	)
							{
								return lo_Cnf;
							}
						//.............................................
						return	new SMC.RfcConfigParameters();
					}

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				public bool ChangeEventsSupported()
					{
						return	this._SupportChgEvent;
					}

			#endregion

		}
}
