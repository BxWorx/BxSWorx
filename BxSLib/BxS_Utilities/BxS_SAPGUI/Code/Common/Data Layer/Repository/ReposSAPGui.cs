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
					}

			#endregion

			//===========================================================================================
			#region "Declarations"

				private DCSapGui	_DC;

			#endregion

			//===========================================================================================
			#region "Properties"

				public bool	IsDirty	{	get {	return	this._DC.IsDirty	; } }

				public int	MsgServerCount	{ get {	return	this._DC.MsgServers	.Count	; } }
				public int	ServiceCount		{ get {	return	this._DC.Services		.Count	; } }
				public int	WorkspaceCount	{ get {	return	this._DC.Workspaces	.Count	; } }
				public int	NodeCount				{ get {	return	this._DC.Nodes			.Count	; } }
				public int	ItemCount				{ get {	return	this._DC.Items			.Count	; } }
				//.................................................
				public DCSapGui	DataContainer	{ get {	return	this._DC; } }

			#endregion

			//===========================================================================================
			#region "Methods: Exposed"

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				public void Load(DCSapGui DC)
					{
						this._DC	= DC;
					}

			#endregion

		}
}