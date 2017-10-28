using System;
using System.Collections.Generic;
//•••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••
namespace SAPGUI.USR.DS
{
	internal class Mapping
		{
			#region "Declarations"

				private readonly Lazy<Dictionary<string, string>>	_SrvMap
									=	new Lazy<Dictionary<string, string>>(	() => LoadServiceMap()	,
																					System.Threading.LazyThreadSafetyMode.ExecutionAndPublication );

				private readonly Lazy<Dictionary<string, string>>	_MsgMap
									=	new Lazy<Dictionary<string, string>>(	() => LoadMsgServerMap()	,
																					System.Threading.LazyThreadSafetyMode.ExecutionAndPublication );

				private readonly Lazy<Dictionary<string, string>>	_WrkMap
									=	new Lazy<Dictionary<string, string>>(	() => LoadWorkspaceMap()	,
																					System.Threading.LazyThreadSafetyMode.ExecutionAndPublication );

			#endregion

			//===========================================================================================
			#region "Properties"

				internal Dictionary<string, string>	Service		{ get { return this._SrvMap.Value;	} }
				internal Dictionary<string, string>	MsgServer	{ get { return this._MsgMap.Value;	} }
				internal Dictionary<string, string>	Workspace	{ get { return this._WrkMap.Value;	} }

			#endregion

			//===========================================================================================
			#region "Methods: Private"

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				private static Dictionary<string, string> LoadServiceMap()
					{
						var lt_Map = new Dictionary<string, string>
							{
								{ "UUID", "UUID" }
							};

						return	lt_Map;
					}

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				private static Dictionary<string, string> LoadMsgServerMap()
					{
						var lt_Map = new Dictionary<string, string>
							{
								{ "UUID", "UUID" }	,
								{ "UUIDz", "UUID" }
							};

						return	lt_Map;
					}

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				private static Dictionary<string, string> LoadWorkspaceMap()
					{
						var lt_Map = new Dictionary<string, string>
							{
								{ "UUID0", "UUID" }	,
								{ "UUID1", "UUID" }	,
								{ "UUID2", "UUID" }
							};

						return	lt_Map;
					}

			#endregion

		}
}
