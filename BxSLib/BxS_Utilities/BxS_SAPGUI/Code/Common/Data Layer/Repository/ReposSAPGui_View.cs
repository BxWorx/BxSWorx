using System;
using System.Collections.Generic;
//.........................................................
using BxS_SAPGUI.API;
//•••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••
namespace BxS_SAPGUI.COM.DL
{
	internal partial class ReposSAPGui
		{
			#region "Methods: Exposed: View"

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				public IList<IDTOConnectionView> GetConnectionViewTree()
					{
						int			ln_WSNo;
						int			ln_NdNo;
						int			ln_ItNo;
						string	lc_WSID;
						string  lc_NDID;
						string	lc_ItID;

						IList<IDTOConnectionView>	lt_HierNodes = new List<IDTOConnectionView>();
						//.............................................
						ln_WSNo	= 0;

						foreach (KeyValuePair<Guid, IDTOWorkspace> ls_WS in this._DC.WorkSpaces)
							{
								ln_WSNo ++	;
								ln_NdNo	= 0	;
								ln_ItNo	= 0	;
								lc_WSID	= this.CreateHierID(ln_WSNo, ln_NdNo, ln_ItNo);

								lt_HierNodes.Add(new DTOConnectionView(lc_WSID, ls_WS.Value.Description));
								//.........................................
								foreach (KeyValuePair<Guid, IDTONode> ls_Node in ls_WS.Value.Nodes)
									{
										ln_NdNo	++	;
										ln_ItNo	= 0	;
										lc_NDID	= this.CreateHierID(ln_WSNo, ln_NdNo, ln_ItNo);

										lt_HierNodes.Add(new DTOConnectionView(lc_NDID, ls_Node.Value.Description, lc_WSID));

										foreach (KeyValuePair<Guid, IDTOItem> ls_Item in ls_Node.Value.Items)
											{
												ln_ItNo	++;
												lc_ItID	= this.CreateHierID(ln_WSNo, ln_NdNo, ln_ItNo);

												lt_HierNodes.Add(new DTOConnectionView(lc_ItID, this.GetItemDescription(ls_Item.Value.ServiceID), lc_NDID, ls_Item.Key));
											}

									}
								//.........................................
								ln_ItNo	= 0;

								foreach (KeyValuePair<Guid, IDTOItem> ls_Item in ls_WS.Value.Items)
									{
										ln_NdNo	++	;
										ln_ItNo	++	;
										lc_ItID	= this.CreateHierID(ln_WSNo, ln_NdNo, ln_ItNo);

										lt_HierNodes.Add(new DTOConnectionView(lc_ItID, this.GetItemDescription(ls_Item.Value.ServiceID), lc_WSID, ls_Item.Key));
									}
							}
						//.............................................
						return	lt_HierNodes;
					}

			#endregion

			//===========================================================================================
			#region "Methods: Private"

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				private string CreateHierID(int wsNo, int ndNo, int itNo)
					{
						return $"{wsNo:D2}.{ndNo:D2}.{itNo:D2}";
					}

			#endregion

		}
}