using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.IO;
//.........................................................
using SAPGUI.API;
using SAPGUI.API.DL;
using SAPGUI.COM.DL;
//��������������������������������������������������������������������������������������������������
namespace zBxS_SAPGUI_UT
	{
	[TestClass]
	public class UT_BxS_SAPGUI_Repository
		{
			private const string	cz_FileName		= "SAPUILandscapeS2A.xml";
			private const string	cz_TestDir		= "Test Resources";
			private const string	cz_TestConnID	=	"dbb1aab6-c82f-4762-bf2b-c525dc55191b";
			private const string	cz_Desc				= "Desc";
			//...................................................
			private	static readonly string	cc_Path					= Directory.GetParent( Directory.GetCurrentDirectory() ).Parent.Parent.FullName;
			private	static readonly string	cc_FullPath			= Path.Combine(cc_Path,	cz_TestDir);
			private	static readonly string	cc_XMLFullName	= Path.Combine(cc_FullPath, cz_FileName);

			//-------------------------------------------------------------------------------------------
			[TestMethod]
			public void UT_SapGui_Repos_Create()
				{
					int					ln_Cnt;
					IRepository lo_Repos	= this.CreateRepository();
					//...............................................
					ln_Cnt	= 1;

					Assert.IsNotNull(	lo_Repos.CreateMsgServerDTO	()	, $"Repos-Create: {ln_Cnt}: DTO-Msg: Error");
					Assert.IsNotNull(	lo_Repos.CreateServiceDTO		()	, $"Repos-Create: {ln_Cnt}: DTO-Srv: Error");
					Assert.IsNotNull(	lo_Repos.CreateWorkspaceDTO	()	, $"Repos-Create: {ln_Cnt}: DTO-Wsp: Error");
					Assert.IsNotNull(	lo_Repos.CreateNodeDTO			()	, $"Repos-Create: {ln_Cnt}: DTO-Nde: Error");
					Assert.IsNotNull(	lo_Repos.CreateItemDTO			()	, $"Repos-Create: {ln_Cnt}: DTO-Itm: Error");
				}

			//-------------------------------------------------------------------------------------------
			[TestMethod]
			public void UT_SapGui_Repos_MsgSrv()
				{
					int					ln_Cnt;
					IRepository lo_Repos	= this.CreateRepository();
					//...............................................
					ln_Cnt	= 1;

					var						lg_ID		= Guid.NewGuid();
					bool					lb_Ok		= lo_Repos.AddUpdateMsgServer(lg_ID, "Name", "Host", "Port", cz_Desc);
					IDTOMsgServer	lo_Get	= lo_Repos.GetMsgServer(lg_ID);

					Assert.IsTrue			(	lb_Ok													,	$"Repos-MsgSvr: {ln_Cnt}: AddUpd: Error");
					Assert.IsNotNull	(	lo_Get												, $"Repos-MsgSvr: {ln_Cnt}: Get		: Error");
					Assert.AreEqual		(	lg_ID		, lo_Get.UUID					, $"Repos-MsgSvr: {ln_Cnt}: Equal	: Error");
					Assert.AreEqual		(	cz_Desc	, lo_Get.Description	, $"Repos-MsgSvr: {ln_Cnt}: Prop	: Error");
				}

			//-------------------------------------------------------------------------------------------
			[TestMethod]
			public void UT_SapGui_Repos_Service()
				{
					int					ln_Cnt;
					IRepository lo_Repos	= this.CreateRepository();
					//...............................................
					ln_Cnt	= 1;

					var					lg_ID		= Guid.NewGuid();
					var					lg_MID	= Guid.NewGuid();
					bool				lb_MOk	= lo_Repos.AddUpdateMsgServer(lg_MID, "Name", "Host", "Port", cz_Desc);
					bool				lb_Ok		= lo_Repos.AddUpdateService(lg_ID, cz_Desc, "XXXXX", "SysID", "Type", "Server", "SAPCPG", "DCPG", "SNCNme", "SNCOp", lg_MID, "Mode");
					IDTOService	lo_Get	= lo_Repos.GetService(lg_ID);

					Assert.IsTrue			(	lb_Ok									,	$"Repos-Srv: {ln_Cnt}: AddUpd: Error");
					Assert.IsNotNull	(	lo_Get								, $"Repos-Srv: {ln_Cnt}: Get		: Error");
					Assert.AreEqual		(	lg_ID		, lo_Get.UUID	, $"Repos-Srv: {ln_Cnt}: Equal	: Error");
					Assert.AreEqual		(	cz_Desc	, lo_Get.Name	, $"Repos-Srv: {ln_Cnt}: Prop	: Error");
				}

			//-------------------------------------------------------------------------------------------
			[TestMethod]
			public void UT_SapGui_Repos_Workspace()
				{
					int					ln_Cnt;
					IRepository lo_Repos	= this.CreateRepository();
					//...............................................
					ln_Cnt	= 1;

					var						lg_ID		= Guid.NewGuid();
					var						lg_ErID	= Guid.NewGuid();
					bool					lb_Ok		= lo_Repos.AddUpdateWorkspace(lg_ID, cz_Desc);
					IDTOWorkspace	lo_Get	= lo_Repos.GetWorkspace(lg_ID);
					IDTOWorkspace	lo_Err	= lo_Repos.GetWorkspace(lg_ErID);

					Assert.IsTrue			(	lb_Ok													,	$"Repos-Wsp: {ln_Cnt}: AddUpd		: Error");
					Assert.IsNotNull	(	lo_Get												, $"Repos-Wsp: {ln_Cnt}: GetID		: Error");
					Assert.AreEqual		(	lg_ID		, lo_Get.UUID					, $"Repos-Wsp: {ln_Cnt}: Equal		: Error");
					Assert.AreEqual		(	cz_Desc	, lo_Get.Description	, $"Repos-Wsp: {ln_Cnt}: PropID		: Error");
					Assert.IsNull			(	lo_Err												, $"Repos-Wsp: {ln_Cnt}: Error		: Error");
				}

			//-------------------------------------------------------------------------------------------
			[TestMethod]
			public void UT_SapGui_Repos_Node()
				{
					int					ln_Cnt;
					IRepository lo_Repos	= this.CreateRepository();
					//...............................................
					ln_Cnt	= 1;

					var	lg_WSID	= Guid.NewGuid();
					var	lg_ErID	= Guid.NewGuid();
					var	lg_ID		= Guid.NewGuid();

					bool	lb_WSOk	= lo_Repos.AddUpdateWorkspace	(lg_WSID, cz_Desc);
					bool	lb_NdOk	= lo_Repos.AddUpdateNode			(lg_WSID, lg_ID, cz_Desc);
					bool	lb_NdEr	= lo_Repos.AddUpdateNode			(lg_ErID, lg_ID, cz_Desc);

					IDTONode	lo_Get	= lo_Repos.GetNode						(lg_ID	, lg_WSID);
					IDTONode	lo_Er1	= lo_Repos.GetNode						(lg_ID	, lg_ErID);
					IDTONode	lo_Er2	= lo_Repos.GetNode						(lg_ErID, lg_WSID);

					Assert.IsTrue			(	lb_WSOk												,	$"Repos-Nde: {ln_Cnt}: AddUpdWS	: Error");
					Assert.IsTrue			(	lb_NdOk												,	$"Repos-Nde: {ln_Cnt}: AddUpdNd	: Error");
					Assert.IsNotNull	(	lo_Get												, $"Repos-Nde: {ln_Cnt}: GetID		: Error");
					Assert.AreEqual		(	lg_ID		, lo_Get.UUID					, $"Repos-Nde: {ln_Cnt}: Equal		: Error");
					Assert.AreEqual		(	cz_Desc	, lo_Get.Description	, $"Repos-Nde: {ln_Cnt}: PropID		: Error");
					Assert.IsNull			(	lo_Er1												, $"Repos-Nde: {ln_Cnt}: GetID		: Error");
					Assert.IsNull			(	lo_Er2												, $"Repos-Nde: {ln_Cnt}: GetID		: Error");
				}

			//-------------------------------------------------------------------------------------------
			[TestMethod]
			public void UT_SapGui_Repos_Item()
				{
					int					ln_Cnt;
					IRepository lo_Repos	= this.CreateRepository();
					//...............................................
					ln_Cnt	= 1;

					var	lg_MID	= Guid.NewGuid();
					var	lg_WSID	= Guid.NewGuid();
					var	lg_NdID	= Guid.NewGuid();
					var	lg_IDn	= Guid.NewGuid();
					var	lg_IDw	= Guid.NewGuid();
					var	lg_SvID	= Guid.NewGuid();

					bool	lb_MOk	= lo_Repos.AddUpdateMsgServer	(lg_MID, "Name", "Host", "Port", cz_Desc);
					bool	lb_SOk	= lo_Repos.AddUpdateService		(lg_SvID, cz_Desc, "XXXXX", "SysID", "Type", "Server", "SAPCPG", "DCPG", "SNCNme", "SNCOp", lg_MID, "Mode");
					bool	lb_WSOk	= lo_Repos.AddUpdateWorkspace	(lg_WSID, cz_Desc);
					bool	lb_NdOk	= lo_Repos.AddUpdateNode			(lg_WSID, lg_NdID, cz_Desc);
					bool	lb_ItON	= lo_Repos.AddUpdateItem			(lg_IDn	, lg_SvID, lg_WSID, lg_NdID);
					bool	lb_ItOW	= lo_Repos.AddUpdateItem			(lg_IDw	, lg_SvID, lg_WSID);

					IDTOItem	lo_GetN	= lo_Repos.GetItem	(lg_IDn	, lg_WSID, lg_NdID);
					IDTOItem	lo_GetW	= lo_Repos.GetItem	(lg_IDw	, lg_WSID);

					Assert.IsTrue			(	lb_WSOk											,	$"Repos-Itm: {ln_Cnt}: AddUpdWS	: Error");
					Assert.IsTrue			(	lb_NdOk											,	$"Repos-Itm: {ln_Cnt}: AddUpdNd	: Error");
					Assert.IsTrue			(	lb_ItON											,	$"Repos-Itm: {ln_Cnt}: AddUpdIt	: Error");
					Assert.IsNotNull	(	lo_GetN											, $"Repos-Itm: {ln_Cnt}: GetID		: Error");
					Assert.IsNotNull	(	lo_GetW											, $"Repos-Itm: {ln_Cnt}: GetID		: Error");
					Assert.AreEqual		(	lg_IDn	, lo_GetN.UUID			, $"Repos-Itm: {ln_Cnt}: Equal		: Error");
					Assert.AreEqual		(	lg_IDw	, lo_GetW.UUID			, $"Repos-Itm: {ln_Cnt}: Equal		: Error");
					Assert.AreEqual		(	lg_SvID	, lo_GetN.ServiceID	, $"Repos-Itm: {ln_Cnt}: PropID		: Error");
				}

			//-------------------------------------------------------------------------------------------
			//-------------------------------------------------------------------------------------------
			private IRepository	CreateRepository()
				{
					var	lo_DC			= new DataContainer();
					var	lo_Repos	= new Repository(lo_DC);
					return	lo_Repos;
				}
		}
}
