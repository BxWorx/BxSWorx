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
						//.............................................
						this.IsDirty	= false;
					}

			#endregion

			//===========================================================================================
			#region "Declarations"

				private DCSapGui	_DC;

			#endregion

			//===========================================================================================
			#region "Properties"

				public bool	IsDirty	{					get {	return	this._DC.IsDirty	; }
															private set { this._DC.IsDirty = value	; } }

				public int	MsgServerCount	{ get {	return	this._DC.MsgServers.Count	; } }
				public int	ServiceCount		{ get {	return	this._DC.Services.Count		; } }
				public int	WorkspaceCount	{ get {	return	this._DC.WorkSpaces.Count	; } }
				public int	NodeCount				{ get {	return	this._DC.Nodes.Count	; } }
				public int	ItemCount				{ get {	return	this._DC.Items.Count	; } }

			#endregion

			//===========================================================================================
			#region "Methods: Exposed: By Ref"

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				public ref DCSapGui GetDataContainer()
					{
						return ref this._DC;
					}

			#endregion

		}
}