using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.IO;
using System.Data;
//.........................................................
using SAPGUI.COM.DL;
using SAPGUI.USR.DL;
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
			private	readonly References		_Ref	= new References();

			private Guid lg_WspID;
			private Guid lg_MsgID;
			private Guid lg_SrvID;
			private Guid lg_NdeID;
			private Guid lg_ItmID;

			//-------------------------------------------------------------------------------------------
			[TestMethod]
			public void UT_SapGuiUsr_Schema()
				{
					int	ln_Cnt;
					var	lo_Schema	= new Schema(this._Ref);
					//...............................................
					ln_Cnt	= 1;
					DataSet lo_DS	= lo_Schema.Create();
					Assert.IsNotNull	(		lo_DS								,	$"Cntlr: {ln_Cnt}: DS-Schema-Nul: Error");
					Assert.AreEqual		(5,	lo_DS.Tables.Count	,	$"Cntlr: {ln_Cnt}: DS-Schema-Tbl: Error");
				}

			//-------------------------------------------------------------------------------------------
			[TestMethod]
			public void UT_SapGuiUsr_Parser()
				{
					int	ln_Cnt;
					var	lo_Par	= new Parser(this._Ref);

					DataSet			lo_DS		= new Schema(this._Ref).Create();
					Repository	lo_Rep	= this.Create_RepData();
					//...............................................

					//...............................................
					ln_Cnt	= 1;
					Assert.AreEqual	(1	,	lo_Rep.MsgServers.Count	,	$"Parser: {ln_Cnt}: DTO:MsgSvr: Error"		);
					Assert.AreEqual	(1	,	lo_Rep.Services.Count		,	$"Parser: {ln_Cnt}: DTO:Service: Error"		);
					Assert.AreEqual	(1	,	lo_Rep.WorkSpaces.Count	,	$"Parser: {ln_Cnt}: DTO:Workspace: Error"	);
					//...............................................
					ln_Cnt	= 2;
					lo_Par.ParseRep2DS(lo_Rep, lo_DS);

					Assert.AreEqual	(1	,	lo_DS.Tables[this._Ref.MsgServerTableName].Rows.Count			,	$"Parser: {ln_Cnt}: Parse:MsgSvr: Error"	);
					Assert.AreEqual	(1	,	lo_DS.Tables[this._Ref.ServiceTableName].Rows.Count				,	$"Parser: {ln_Cnt}: Parse:Service: Error"	);
					Assert.AreEqual	(1	,	lo_DS.Tables[this._Ref.WorkspaceTableName].Rows.Count			,	$"Parser: {ln_Cnt}: Parse:Workspace: Error"	);
					Assert.AreEqual	(1	,	lo_DS.Tables[this._Ref.WorkspaceNodeTableName].Rows.Count	,	$"Parser: {ln_Cnt}: Parse:WS-Node: Error"	);
					Assert.AreEqual	(2	,	lo_DS.Tables[this._Ref.WorkspaceItemTableName].Rows.Count	,	$"Parser: {ln_Cnt}: Parse:WS-Item: Error"	);
					//...............................................
					ln_Cnt	= 3;
					lo_Rep.Clear();
					lo_Par.ParseDS2Rep(lo_DS, lo_Rep);
					this.Validate_Rep(lo_Rep, ln_Cnt, "Parser");
			}

			//-------------------------------------------------------------------------------------------
			[TestMethod]
			public void UT_SapGuiUsr_DLController()
				{
					int					ln_Cnt;
					//...............................................
					var					lo_Parser		= new Parser(this._Ref);
					var					lo_Schema		= new Schema(this._Ref);
					var					lo_DLCntlr	= new DLController(_PathTest, lo_Schema, lo_Parser);
					var					lo_RepX			= new Repository();
					Repository	lo_Rep			= this.Create_RepData();
					//...............................................
					ln_Cnt	= 1;
					lo_DLCntlr.DeleteSchemaXMLFile();
					lo_DLCntlr.DeleteDatasetXMLFile();

					Assert.IsFalse	(lo_DLCntlr.SchemaXMLExists		,	$"DLCntlr: {ln_Cnt}: Del:Schema: Error");
					Assert.IsFalse	(lo_DLCntlr.DatasetXMLExists	,	$"DLCntlr: {ln_Cnt}: Del:Dataset: Error");
					//...............................................
					ln_Cnt	= 2;
					lo_DLCntlr.Save(lo_Rep);
					Assert.IsTrue	(lo_DLCntlr.SchemaXMLExists		,	$"DLCntlr: {ln_Cnt}: Save: File Schema: Error");
					Assert.IsTrue	(lo_DLCntlr.DatasetXMLExists	,	$"DLCntlr: {ln_Cnt}: Save: File DS: Error");
					//...............................................
					ln_Cnt	= 2;
					lo_DLCntlr.LoadRepository(lo_RepX);
					this.Validate_Rep(lo_RepX, ln_Cnt, "DLCntlr");
				}

			//===========================================================================================
			#region "Methods: Private"

				//-------------------------------------------------------------------------------------------
				private void Validate_Rep(Repository rep, int cnt, string utName)
					{
						Assert.AreEqual	(1	,	rep.MsgServers.Count	,	$"{utName}: {cnt}: Rep:MsgSvr: Error"	);
						Assert.AreEqual	(1	,	rep.Services.Count		,	$"{utName}: {cnt}: Rep:Service: Error"	);
						Assert.AreEqual	(1	,	rep.WorkSpaces.Count	,	$"{utName}: {cnt}: Rep:Workspace: Error"	);
						//...............................................
						Assert.IsTrue(rep.MsgServers.ContainsKey	(this.lg_MsgID)	,	$"{utName}: {cnt}: Msg:Check Key: Error"	);
						Assert.IsTrue(rep.Services.ContainsKey		(this.lg_SrvID)	,	$"{utName}: {cnt}: Srv:Check Key: Error"	);
						Assert.IsTrue(rep.WorkSpaces.ContainsKey	(this.lg_WspID)	,	$"{utName}: {cnt}: WSp:Check Key: Error"	);

						if (rep.WorkSpaces.TryGetValue(this.lg_WspID, out DTOWorkspace lo_WSx))
							{
								Assert.IsTrue(lo_WSx.Items.ContainsKey(this.lg_ItmID),	$"{utName}: {cnt}: WSpItem:Check Key: Error"	);
								Assert.IsTrue(lo_WSx.Nodes.ContainsKey(this.lg_NdeID),	$"{utName}: {cnt}: WSpNode:Check Key: Error"	);
							}
						else
							{	Assert.Fail($"{utName}: {cnt}: WS:Get: Error");	}
					}

				//-------------------------------------------------------------------------------------------
				private Repository Create_RepData()
					{
						var								lo_Rep		= new Repository();

						DTOMsgServer			lo_MsgDTO	= this.Create_MsgSvrDTO();
						DTOService				lo_SrvDTO	= this.Create_SrvDTO(lo_MsgDTO.UUID);
						DTOWorkspace			lo_WspDTO	= this.Create_WspDTO();
						DTOWorkspaceNode	lo_WSNDTO	= this.Create_WSNodeDTO();
						DTOWorkspaceItem	lo_WSIDTO	= this.Create_WSItemDTO(lo_SrvDTO.UUID);
						DTOWorkspaceItem	lo_WSxDTO	= this.Create_WSItemDTO(lo_SrvDTO.UUID);

						this.lg_WspID	= lo_WspDTO.UUID;
						this.lg_MsgID	= lo_MsgDTO.UUID;
						this.lg_SrvID	= lo_SrvDTO.UUID;
						this.lg_NdeID	= lo_WSNDTO.UUID;
						this.lg_ItmID	= lo_WSxDTO.UUID;

						lo_WSNDTO.Items.Add		(lo_WSIDTO.UUID, lo_WSIDTO);
						lo_WspDTO.Nodes.Add		(lo_WSNDTO.UUID, lo_WSNDTO);
						lo_WspDTO.Items.Add		(lo_WSxDTO.UUID, lo_WSxDTO);

						lo_Rep.MsgServers.Add	(lo_MsgDTO.UUID, lo_MsgDTO);
						lo_Rep.Services.Add		(lo_SrvDTO.UUID, lo_SrvDTO);
						lo_Rep.WorkSpaces.Add	(lo_WspDTO.UUID, lo_WspDTO);

						return	lo_Rep;
					}

				//-------------------------------------------------------------------------------------------
				private DTOMsgServer Create_MsgSvrDTO()
					{
						return	new DTOMsgServer	{	UUID				= Guid.NewGuid()	,
																				Name				= "name1"					,
																				Description = "desc1"					,
																				Host				= "host1"					,
																				Port				= "port1"						};
					}

				//-------------------------------------------------------------------------------------------
				private DTOService Create_SrvDTO(Guid	msgSvr)
					{
						return	new DTOService	{	UUID	= Guid.NewGuid()	,
																			Name	= "NameS"					,
																			MSID	= msgSvr						};
					}

				//-------------------------------------------------------------------------------------------
				private DTOWorkspace Create_WspDTO()
					{
						return	new DTOWorkspace	{	UUID				= Guid.NewGuid()	,
																				Description	= "DescWS"					};
					}

				//-------------------------------------------------------------------------------------------
				private DTOWorkspaceNode Create_WSNodeDTO()
					{
						return	new DTOWorkspaceNode	{	UUID				= Guid.NewGuid()	,
																						Description	= "DescNode"				};
					}

				//-------------------------------------------------------------------------------------------
				private DTOWorkspaceItem Create_WSItemDTO(Guid srvID)
					{
						return	new DTOWorkspaceItem	{	UUID			= Guid.NewGuid()	,
																						ServiceID	= srvID								};
					}

			#endregion

		}
}
