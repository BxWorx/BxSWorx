using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.IO;
//.........................................................
using BxS_SAPGUI.API;
using BxS_SAPGUI.API.DL;
using BxS_SAPGUI.COM.DL;
using BxS_SAPGUI.USR;
using BxS_Toolset.IO;
using BxS_Toolset.Serialize;
//•••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••
namespace zBxS_SAPGUI_UT
{
	[TestClass]
	public class UT_BxS_SAPGUI_USR
		{
			private const string	cz_TestDir	= "Test Resources";
			//...................................................
			private	static readonly string	_Path				= Directory.GetParent( Directory.GetCurrentDirectory() ).Parent.Parent.FullName;
			private	static readonly string	_PathTest		= Path.Combine(_Path,	cz_TestDir);
			//...................................................
			private Guid lg_WspID;
			private Guid lg_MsgID;
			private Guid lg_SrvID;
			private Guid lg_NdeID;
			private Guid lg_ItmID;

			//-------------------------------------------------------------------------------------------
			[TestMethod]
			public void UT_SapGuiUsr_UsrController()
				{
					int	ln_Cnt;
					//...............................................
					IRepository		lo_Rep	= this.CreateRepository();
					IRepository		lo_RepX	= this.CreateRepository(true);
					IRepository		lo_RepY	= this.CreateRepository(true);

					var	lo_IO				= new IO();
					var	lo_DCSer		= new DCSerializer();
					var	lo_UsrCntlr	= new USRController(lo_Rep, _PathTest, lo_IO, lo_DCSer, false);
					//...............................................
					ln_Cnt	= 1;
					lo_UsrCntlr.DeleteDCXMLFile();
					Assert.IsFalse	(lo_UsrCntlr.FileExists()	,	$"DLCntlr: {ln_Cnt}: Del:Dataset: Error");
					//...............................................
					ln_Cnt	= 2;
					lo_UsrCntlr.Save(true);
					Assert.IsTrue	(lo_UsrCntlr.FileExists(),	$"DLCntlr: {ln_Cnt}: Save: File DS: Error");
					//...............................................
					ln_Cnt	= 3;
					var	lo_UsrCntlrx	= new USRController(lo_RepX, _PathTest, lo_IO, lo_DCSer);
					this.Validate_Rep(lo_UsrCntlrx.Repository.GetDataContainer(), ln_Cnt, "UsrCntlr");
					//...............................................
					ln_Cnt	= 4;
					lo_UsrCntlr.Repository.AddUpdateMsgServer(this.Create_MsgSvrDTO());
					lo_UsrCntlr.Save(true);
					var	lo_UsrCntlry	= new USRController(lo_RepY, _PathTest, lo_IO, lo_DCSer);
					Assert.AreEqual	(2,	lo_UsrCntlry.Repository.MsgServerCount,	$"DLCntlr: {ln_Cnt}: Del:Dataset: Error");
				}

			//===========================================================================================
			#region "Methods: Private"

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				private IRepository CreateRepository(bool emptyOne = false)
					{
						return	emptyOne	? new Repository(new DataContainer()) :	new Repository(CreateAndFill_DC());
					}

				//-----------------------------------------------------------------------------------------
				private void Validate_Rep(DataContainer rep, int cnt, string utName)
					{
						Assert.AreEqual	(1	,	rep.MsgServers.Count	,	$"{utName}: {cnt}: Rep:MsgSvr: Error"	);
						Assert.AreEqual	(1	,	rep.Services.Count		,	$"{utName}: {cnt}: Rep:Service: Error"	);
						Assert.AreEqual	(1	,	rep.WorkSpaces.Count	,	$"{utName}: {cnt}: Rep:Workspace: Error"	);
						//...............................................
						Assert.IsTrue(rep.MsgServers.ContainsKey	(this.lg_MsgID)	,	$"{utName}: {cnt}: Msg:Check Key: Error"	);
						Assert.IsTrue(rep.Services.ContainsKey		(this.lg_SrvID)	,	$"{utName}: {cnt}: Srv:Check Key: Error"	);
						Assert.IsTrue(rep.WorkSpaces.ContainsKey	(this.lg_WspID)	,	$"{utName}: {cnt}: WSp:Check Key: Error"	);

						if (rep.WorkSpaces.TryGetValue(this.lg_WspID, out IDTOWorkspace lo_WSx))
							{
								Assert.IsTrue(lo_WSx.Items.ContainsKey(this.lg_ItmID),	$"{utName}: {cnt}: WSpItem:Check Key: Error"	);
								Assert.IsTrue(lo_WSx.Nodes.ContainsKey(this.lg_NdeID),	$"{utName}: {cnt}: WSpNode:Check Key: Error"	);
							}
						else
							{	Assert.Fail($"{utName}: {cnt}: WS:Get: Error");	}
					}

				//-----------------------------------------------------------------------------------------
				private DataContainer CreateAndFill_DC()
					{
						var	lo_DC	= new DataContainer();

						DTOMsgServer	lo_MsgDTO	= this.Create_MsgSvrDTO();
						DTOService		lo_SrvDTO	= this.Create_SrvDTO(lo_MsgDTO.UUID);
						DTOWorkspace	lo_WspDTO	= this.Create_WspDTO();

						DTONode	lo_WSNDTO	= this.Create_WSNodeDTO();
						DTOItem	lo_WSIDTO	= this.Create_WSItemDTO(lo_SrvDTO.UUID);
						DTOItem	lo_WSxDTO	= this.Create_WSItemDTO(lo_SrvDTO.UUID);

						this.lg_WspID	= lo_WspDTO.UUID;
						this.lg_MsgID	= lo_MsgDTO.UUID;
						this.lg_SrvID	= lo_SrvDTO.UUID;
						this.lg_NdeID	= lo_WSNDTO.UUID;
						this.lg_ItmID	= lo_WSxDTO.UUID;

						lo_WSNDTO.Items	.Add (lo_WSIDTO.UUID, lo_WSIDTO);
						lo_WspDTO.Nodes	.Add (lo_WSNDTO.UUID, lo_WSNDTO);
						lo_WspDTO.Items	.Add (lo_WSxDTO.UUID, lo_WSxDTO);

						lo_DC.MsgServers	.Add	(lo_MsgDTO.UUID, lo_MsgDTO);
						lo_DC.Services		.Add	(lo_SrvDTO.UUID, lo_SrvDTO);
						lo_DC.WorkSpaces	.Add	(lo_WspDTO.UUID, lo_WspDTO);

						return	lo_DC;
					}

				//-----------------------------------------------------------------------------------------
				private DTOMsgServer Create_MsgSvrDTO()
					{
						return	new DTOMsgServer	{	UUID				= Guid.NewGuid()	,
																				Name				= "name1"					,
																				Description = "desc1"					,
																				Host				= "host1"					,
																				Port				= "port1"						};
					}

				//-----------------------------------------------------------------------------------------
				private DTOService Create_SrvDTO(Guid	msgSvr)
					{
						return	new DTOService	{	UUID	= Guid.NewGuid()	,
																			Name	= "NameS"					,
																			MSID	= msgSvr						};
					}

				//-----------------------------------------------------------------------------------------
				private DTOWorkspace Create_WspDTO()
					{
						return	new DTOWorkspace	{	UUID				= Guid.NewGuid()	,
																				Description	= "DescWS"					};
					}

				//-----------------------------------------------------------------------------------------
				private DTONode Create_WSNodeDTO()
					{
						return	new DTONode	{	UUID				= Guid.NewGuid()	,
																	Description	= "DescNode"				};
					}

				//-----------------------------------------------------------------------------------------
				private DTOItem Create_WSItemDTO(Guid srvID)
					{
						return	new DTOItem	{	UUID			= Guid.NewGuid()	,
																	ServiceID	= srvID								};
					}

			#endregion

		}
}
