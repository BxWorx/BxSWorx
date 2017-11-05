using System;
using System.Collections.Generic;
//•••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••
namespace SAPGUI.USR.DL
{
	// Mapping always relates to KVP being: Key		:=	DS Table Field
	//																			Value	:=	DTO property
	//...............................................................................................
	internal class Mapping
		{
			#region "Constructors"

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				internal Mapping(References references)
					{
						this._Ref	= references;
						//.............................................
						this._SrvMap	=	new Lazy<Dictionary<string, string>>(	() => LoadServiceMap()				,
																																				System.Threading.LazyThreadSafetyMode.ExecutionAndPublication );

						this._MsgMap	=	new Lazy<Dictionary<string, string>>(	() => LoadMsgServerMap()			,
																																				System.Threading.LazyThreadSafetyMode.ExecutionAndPublication );

						this._WrkMap	=	new Lazy<Dictionary<string, string>>(	() => LoadWorkspaceMap()			,
																																				System.Threading.LazyThreadSafetyMode.ExecutionAndPublication );

						this._WSNMap	=	new Lazy<Dictionary<string, string>>(	() => LoadWorkspaceNodeMap()	,
																																				System.Threading.LazyThreadSafetyMode.ExecutionAndPublication );

						this._WSIMap	=	new Lazy<Dictionary<string, string>>(	() => LoadWorkspaceItemMap()	,
																																				System.Threading.LazyThreadSafetyMode.ExecutionAndPublication );
					}

			#endregion

			//===========================================================================================
			#region "Declarations"

				private	readonly References	_Ref;
				//.................................................
				private readonly Lazy<Dictionary<string, string>>	_SrvMap;
				private readonly Lazy<Dictionary<string, string>>	_MsgMap;
				private readonly Lazy<Dictionary<string, string>>	_WrkMap;
				private readonly Lazy<Dictionary<string, string>>	_WSNMap;
				private readonly Lazy<Dictionary<string, string>>	_WSIMap;

			#endregion

			//===========================================================================================
			#region "Properties"

				internal Dictionary<string, string>	Service				{ get { return this._SrvMap.Value;	} }
				internal Dictionary<string, string>	MsgServer			{ get { return this._MsgMap.Value;	} }
				internal Dictionary<string, string>	Workspace			{ get { return this._WrkMap.Value;	} }
				internal Dictionary<string, string>	WorkspaceNode	{ get { return this._WSNMap.Value;	} }
				internal Dictionary<string, string>	WorkspaceItem	{ get { return this._WSIMap.Value;	} }

			#endregion

			//===========================================================================================
			#region "Methods: Private"

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				private Dictionary<string, string> LoadServiceMap()
					{
						return	new Dictionary<string, string>
							{
								{ this._Ref.UUID						, "UUID" }	,
								{ this._Ref.Name						, "UUID" }	,
								{ this._Ref.Description			, "UUID" }	,
								{ this._Ref.ConnType				, "UUID" }	,
								{ this._Ref.Server					, "UUID" }	,
								{ this._Ref.SystemID				, "UUID" }	,
								{ this._Ref.SNCName					, "UUID" }	,
								{ this._Ref.SNCOp						, "UUID" }	,
								{ this._Ref.CodePage				, "UUID" }	,
								{ this._Ref.DownUpCodePage	, "UUID" }
							};
					}

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				private Dictionary<string, string> LoadMsgServerMap()
					{
						return	new Dictionary<string, string>
							{
								{ this._Ref.UUID				, this._Ref.UUID.ToUpper()				}	,
								{ this._Ref.Name				, this._Ref.Name.ToUpper()				}	,
								{ this._Ref.Description	, this._Ref.Description.ToUpper()	}	,
								{ this._Ref.Host				, this._Ref.Host.ToUpper()				}	,
								{ this._Ref.Port				, this._Ref.Port.ToUpper()				}
							};
					}

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				private Dictionary<string, string> LoadWorkspaceMap()
					{
						return	new Dictionary<string, string>
							{
								{ this._Ref.UUID				, "UUID" }	,
								{ this._Ref.Description	, "UUID" }	,
								{ this._Ref.SeqNo				, "UUID" }
							};
					}

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				private Dictionary<string, string> LoadWorkspaceNodeMap()
					{
						return	new Dictionary<string, string>
							{
								{ this._Ref.UUID				, "UUID" }	,
								{ this._Ref.Description	, "UUID" }	,
								{ this._Ref.ReferenceID	, "UUID" }
							};
					}

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				private Dictionary<string, string> LoadWorkspaceItemMap()
					{
						return	new Dictionary<string, string>
							{
								{ this._Ref.UUID				, "UUID" }	,
								{ this._Ref.WSItemType	, "UUID" }	,
								{ this._Ref.ReferenceID	, "UUID" }	,
								{ this._Ref.ServiceID		, "UUID" }
							};
					}

			#endregion

		}
}
