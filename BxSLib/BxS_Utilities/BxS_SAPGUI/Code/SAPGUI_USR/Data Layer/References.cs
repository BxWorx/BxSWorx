//•••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••
namespace SAPGUI.USR.DS
{
		internal class References
		{
			#region "Constructors"

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				internal References()
					{
					}

			#endregion

			//===========================================================================================
			#region "Declarations"

				private const	string	_UUID									= "UUID";
				private const	string	_DataSetName					= "SAPGUI_USR_Repository";
				private const	string	_ServiceTableName			= "Service";
				private const	string	_MsgServerTableName		= "MsgServer";
				private const	string	_WorkspaceTableName		= "Workspace";

			#endregion

			//===========================================================================================
			#region "Properties"

				internal string	UUID								{ get	{ return _UUID;								} }
				internal string DataSetName					{ get	{ return _DataSetName;				} }
				internal string ServiceTableName		{ get	{ return _ServiceTableName;		} }
				internal string MsgServerTableName	{ get	{ return _MsgServerTableName;	} }
				internal string WorkspaceTableName	{ get	{ return _WorkspaceTableName; } }

			#endregion

		}
}
