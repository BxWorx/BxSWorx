using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.IO;
//.........................................................
using SAPGUI.API;
using SAPGUI.API.DL;
using SAPGUI.COM.DL;
//••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••
namespace zBxS_SAPGUI_UT
	{
	[TestClass]
	public class UT_BxS_SAPGUI_Repository
		{
			private const string	cz_FileName		= "SAPUILandscapeS2A.xml";
			private const string	cz_TestDir		= "Test Resources";
			private const string	cz_TestConnID	=	"dbb1aab6-c82f-4762-bf2b-c525dc55191b";

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
					bool					lb_Ok		= lo_Repos.AddUpdateMsgServer(lg_ID, "Name", "Host", "Port", "Desc");
					IDTOMsgServer	lo_Get	= lo_Repos.GetMsgServer(lg_ID);

					Assert.IsTrue			(	lb_Ok									,	$"Repos-MsgSvr: {ln_Cnt}: AddUpd: Error");
					Assert.IsNotNull	(	lo_Get								, $"Repos-MsgSvr: {ln_Cnt}: Get		: Error");
					Assert.AreEqual		(	lg_ID		, lo_Get.UUID	, $"Repos-MsgSvr: {ln_Cnt}: Equal	: Error");
					Assert.AreEqual		(	"Name"	, lo_Get.Name	, $"Repos-MsgSvr: {ln_Cnt}: Prop	: Error");
				}

			//-------------------------------------------------------------------------------------------
			[TestMethod]
			public void UT_SapGui_Repos_Service()
				{
					const string lz_Name	= "Name";

					int					ln_Cnt;
					IRepository lo_Repos	= this.CreateRepository();
					//...............................................
					ln_Cnt	= 1;

					var					lg_ID		= Guid.NewGuid();
					var					lg_MID	= Guid.NewGuid();
					bool				lb_Ok		= lo_Repos.AddUpdateService(lg_ID, lz_Name, "Desc", "SysID", "Type", "Server", "SAPCPG", "DCPG", "SNCNme", "SNCOp", lg_MID, "Mode");
					IDTOService	lo_Get	= lo_Repos.GetService(lg_ID);

					Assert.IsTrue			(	lb_Ok									,	$"Repos-Srv: {ln_Cnt}: AddUpd: Error");
					Assert.IsNotNull	(	lo_Get								, $"Repos-Srv: {ln_Cnt}: Get		: Error");
					Assert.AreEqual		(	lg_ID		, lo_Get.UUID	, $"Repos-Srv: {ln_Cnt}: Equal	: Error");
					Assert.AreEqual		(	lz_Name	, lo_Get.Name	, $"Repos-Srv: {ln_Cnt}: Prop	: Error");
				}

			//-------------------------------------------------------------------------------------------
			[TestMethod]
			public void UT_SapGui_Repos_Workspace()
				{
					const string lz_Desc	= "Desc";

					int					ln_Cnt;
					IRepository lo_Repos	= this.CreateRepository();
					//...............................................
					ln_Cnt	= 1;

					var						lg_ID		= Guid.NewGuid();
					var						lg_ErID	= Guid.NewGuid();
					bool					lb_Ok		= lo_Repos.AddUpdateWorkspace(lg_ID, lz_Desc);
					IDTOWorkspace	lo_Get	= lo_Repos.GetWorkspace(lg_ID);
					IDTOWorkspace	lo_Err	= lo_Repos.GetWorkspace(lg_ErID);

					Assert.IsTrue			(	lb_Ok													,	$"Repos-Wsp: {ln_Cnt}: AddUpd		: Error");
					Assert.IsNotNull	(	lo_Get												, $"Repos-Wsp: {ln_Cnt}: GetID		: Error");
					Assert.AreEqual		(	lg_ID		, lo_Get.UUID					, $"Repos-Wsp: {ln_Cnt}: Equal		: Error");
					Assert.AreEqual		(	lz_Desc	, lo_Get.Description	, $"Repos-Wsp: {ln_Cnt}: PropID		: Error");
					Assert.IsNull			(	lo_Err												, $"Repos-Wsp: {ln_Cnt}: Error		: Error");
				}

			//-------------------------------------------------------------------------------------------
			[TestMethod]
			public void UT_SapGui_Repos_Node()
				{
					const string lz_Desc	= "Desc";

					int					ln_Cnt;
					IRepository lo_Repos	= this.CreateRepository();
					//...............................................
					ln_Cnt	= 1;

					var				lg_WSID	= Guid.NewGuid();
					var				lg_ErID	= Guid.NewGuid();
					var				lg_ID		= Guid.NewGuid();

					bool			lb_WSOk	= lo_Repos.AddUpdateWorkspace	(lg_WSID, lz_Desc);
					bool			lb_NdOk	= lo_Repos.AddUpdateNode			(lg_WSID, lg_ID, lz_Desc);
					bool			lb_NdEr	= lo_Repos.AddUpdateNode			(lg_ErID, lg_ID, lz_Desc);

					IDTONode	lo_Get	= lo_Repos.GetNode						(lg_ID	, lg_WSID);
					IDTONode	lo_Er1	= lo_Repos.GetNode						(lg_ID	, lg_ErID);
					IDTONode	lo_Er2	= lo_Repos.GetNode						(lg_ErID, lg_WSID);

					Assert.IsTrue			(	lb_WSOk												,	$"Repos-Nde: {ln_Cnt}: AddUpdWS	: Error");
					Assert.IsTrue			(	lb_NdOk												,	$"Repos-Nde: {ln_Cnt}: AddUpdNd	: Error");
					Assert.IsNotNull	(	lo_Get												, $"Repos-Nde: {ln_Cnt}: GetID		: Error");
					Assert.AreEqual		(	lg_ID		, lo_Get.UUID					, $"Repos-Nde: {ln_Cnt}: Equal		: Error");
					Assert.AreEqual		(	lz_Desc	, lo_Get.Description	, $"Repos-Nde: {ln_Cnt}: PropID		: Error");
					Assert.IsNull			(	lo_Er1												, $"Repos-Nde: {ln_Cnt}: GetID		: Error");
					Assert.IsNull			(	lo_Er2												, $"Repos-Nde: {ln_Cnt}: GetID		: Error");
				}

			//-------------------------------------------------------------------------------------------
			[TestMethod]
			public void UT_SapGui_Repos_Item()
				{
					const string lz_Desc	= "Desc";

					int					ln_Cnt;
					IRepository lo_Repos	= this.CreateRepository();
					//...............................................
					ln_Cnt	= 1;

					var				lg_WSID	= Guid.NewGuid();
					var				lg_NdID	= Guid.NewGuid();
					var				lg_ID		= Guid.NewGuid();
					var				lg_SvID	= Guid.NewGuid();

					bool			lb_WSOk	= lo_Repos.AddUpdateWorkspace	(lg_WSID, lz_Desc);
					bool			lb_NdOk	= lo_Repos.AddUpdateNode			(lg_WSID, lg_ID, lz_Desc);
					bool			lb_ItOk	= lo_Repos.AddUpdateItem			(lg_WSID, lg_NdID, lg_ID, lg_SvID);
					IDTOItem	lo_Get	= lo_Repos.GetItem						(lg_ID, lg_WSID, lg_NdID);

					Assert.IsTrue			(	lb_WSOk											,	$"Repos-Itm: {ln_Cnt}: AddUpdWS	: Error");
					Assert.IsTrue			(	lb_NdOk											,	$"Repos-Itm: {ln_Cnt}: AddUpdNd	: Error");
					Assert.IsTrue			(	lb_ItOk											,	$"Repos-Itm: {ln_Cnt}: AddUpdIt	: Error");
					Assert.IsNotNull	(	lo_Get											, $"Repos-Itm: {ln_Cnt}: GetID		: Error");
					Assert.AreEqual		(	lg_ID		, lo_Get.UUID				, $"Repos-Itm: {ln_Cnt}: Equal		: Error");
					Assert.AreEqual		(	lg_SvID	, lo_Get.ServiceID	, $"Repos-Itm: {ln_Cnt}: PropID		: Error");
				}













			////-------------------------------------------------------------------------------------------
			//[TestMethod]
			//public void UT_SapGui_ControllerINI()
			//	{
			//		int								ln_Cnt;
			//		IController				lo_Cntlr;
			//		IDTOConnection		lo_DTOConn;
			//		//...............................................
			//		lo_Cntlr	= ControllerFactory.CreateControllerForSAPINI(cc_XMLFullName);
			//		//...............................................
			//		ln_Cnt			= 1;
			//		lo_DTOConn	= lo_Cntlr.CreateConnection();
			//		Assert.IsNotNull	(lo_DTOConn					,	$"Cntlr: {ln_Cnt}: DTO a: Error");
			//		Assert.IsFalse		(lo_DTOConn.IsValid	,	$"Cntlr: {ln_Cnt}: DTO b: Error");
			//	}

			////-------------------------------------------------------------------------------------------
			//[TestMethod]
			//public void UT_SapGui_ControllerUsr()
			//	{
			//		int								ln_Cnt;
			//		IController				lo_Cntlr;
			//		IDTOConnection		lo_DTOConn;
			//		//...............................................
			//		lo_Cntlr	= ControllerFactory.CreateControllerForSAPUSR(cc_FullPath);
			//		//...............................................
			//		ln_Cnt			= 1;
			//		lo_DTOConn	= lo_Cntlr.CreateConnection();
			//		Assert.IsNotNull	(lo_DTOConn					,	$"Cntlr: {ln_Cnt}: DTO a: Error");
			//		Assert.IsFalse		(lo_DTOConn.IsValid	,	$"Cntlr: {ln_Cnt}: DTO b: Error");
			//	}




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
