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
						this._Map		= new Dictionary<string	, Guid>											();
						this._Ref		= new Dictionary<Guid		,	string>										();
						this._Des		= new Dictionary<Guid		, SMC.RfcConfigParameters>	();
						//.............................................
						this.ToggleChangeEvent	= true;
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

				internal bool ToggleChangeEvent	{ get; set; }

			#endregion

			//===========================================================================================
			#region "Methods: Exposed"

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				internal IList<IDTORefEntry> ReferenceList()
					{
						IList<IDTORefEntry>	lt_List	= new List<IDTORefEntry>(this._Map.Count);
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
				internal Guid AddConfig(string ID, SMC.RfcConfigParameters rfcConfig)
					{
						return	this.AddConfig(	this.GetAddIDFor(ID, true)	,
																		rfcConfig											);
					}

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				internal Guid AddConfig(Guid ID, SMC.RfcConfigParameters rfcConfig)
					{
						this._Des[ID]	= rfcConfig;
						var lo_E = new SMC.RfcConfigurationEventArgs(SMC.RfcConfigParameters.EventType.CHANGED);
						this.OnConfigurationChanged("A",lo_E);
						return	ID;
					}

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				internal string GetNameFor(Guid ID)
					{
						return	this._Ref[ID] ?? string.Empty;
					}

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				internal Guid GetAddIDFor(string ID, bool Add = false)
					{
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

			//===========================================================================================
			#region "Methods: Interface"

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				public event SDM.ConfigurationChangeHandler		ConfigurationChanged;

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				public SMC.RfcConfigParameters GetParameters(string destinationName)
					{
						Guid lg = this.GetAddIDFor(destinationName);

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
						return	this.ToggleChangeEvent;
					}


				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				private void OnConfigurationChanged(string name, SMC.RfcConfigurationEventArgs e)
				{
					//SDM.ConfigurationChangeHandler lo_EvntHndlr	= this.ConfigurationChanged;
					//lo_EvntHndlr(name, e);
				}

			#endregion

		}
}
