//•••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••
namespace BxS_SAPGUI.COM.DL
{
	internal partial class ReposSAPGui : IReposSAPGui
		{
			#region "Constructor"

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				internal ReposSAPGui(DCSapGui dataContainer)
					{
						this._DC	= dataContainer;
						////.............................................
						//this.IsDirty	= false;
					}

			#endregion

			//===========================================================================================
			#region "Declarations"

				private DCSapGui	_DC;

			#endregion

			//===========================================================================================
			#region "Properties"

				public bool	IsDirty	{	get {	return	this._DC.IsDirty	; } }
				//											private set { this._DC.IsDirty = value	; } }

				public int	MsgServerCount	{ get {	return	this._DC.XMsgServers.Count	; } }
				public int	ServiceCount		{ get {	return	this._DC.XServices.Count		; } }
				public int	WorkspaceCount	{ get {	return	this._DC.XWorkspaces.Count	; } }
				public int	NodeCount				{ get {	return	this._DC.XNodes.Count	; } }
				public int	ItemCount				{ get {	return	this._DC.XItems.Count	; } }

				public DCSapGui	DataContainerx	{ get {	return	this._DC; } }

			#endregion

			//===========================================================================================
			#region "Methods: Exposed: By Ref"



				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				public void Load(DCSapGui DC)
					{
						this._DC	= DC;
					}

			#endregion

		}
}