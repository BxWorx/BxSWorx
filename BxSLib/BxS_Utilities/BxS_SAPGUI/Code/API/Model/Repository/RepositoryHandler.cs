using System;
using System.Linq;
//.........................................................
using SAPGUI.API;
using SAPGUI.API.DL;
//•••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••
namespace SAPGUI.COM.DL
{
	internal partial class RepositoryHandler : IRepositoryHandler
		{
			#region "Constructor"

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				internal RepositoryHandler(Datacontainer dataContainer)
					{
						this._DC	= dataContainer;
						//.............................................
						this.IsDirty	= false;
					}

			#endregion

			//===========================================================================================
			#region "Declarations"

				private readonly Datacontainer	_DC;

			#endregion

			//===========================================================================================
			#region "Proprties"

				internal bool IsDirty { get; private set; }

			#endregion

			//===========================================================================================
			#region "Methods: Exposed: General"

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				public void Clear()
					{
						if (			this._DC	.MsgServers	.Count.Equals(0)
									&&	this._DC	.Services		.Count.Equals(0)
									&&	this._DC	.WorkSpaces	.Count.Equals(0)	)
							{
								return;
							}
						//.............................................
						this.IsDirty	= true;
						this._DC.Clear();
					}

			#endregion

			//===========================================================================================
			#region "Methods: Private"







				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				private bool MsgServerInUse(Guid ID)
					{
						return	!this._DC.Services.Count(kvp => kvp.Value.MSID.Equals(ID)).Equals(0);
					}

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				private bool ServiceInUse(Guid ID)
					{
						bool lb_CntItm		= this._DC.WorkSpaces
																	.SelectMany( ws => ws.Value.Items
																		.Where( it => it.Key.Equals(ID) ) )
																			.Count()
																				.Equals(0);

						bool lb_CntNde		= this._DC.WorkSpaces
																	.SelectMany( ws => ws.Value.Nodes
																		.SelectMany( nd => nd.Value.Items
																			.Where( it => it.Key.Equals(ID) ) ) )
																				.Count()
																					.Equals(0);

						return	!lb_CntItm || !lb_CntNde;
				}

			#endregion

		}
}