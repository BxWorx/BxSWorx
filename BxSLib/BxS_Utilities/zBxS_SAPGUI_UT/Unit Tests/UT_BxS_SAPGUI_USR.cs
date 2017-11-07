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

			////-------------------------------------------------------------------------------------------
			//[TestMethod]
			//public void UT_SapGuiUsr_Mapping()
			//	{
			//		int	ln_Cnt;
			//		var	lo_Map	= new Mapping(this._Ref);
			//		//...............................................
			//		ln_Cnt	= 1;
			//		Assert.IsNotNull(lo_Map,	$"Cntlr: {ln_Cnt}: Map-Nul: Error");
			//	}

			//-------------------------------------------------------------------------------------------
			[TestMethod]
			public void UT_SapGuiUsr_Parser()
				{
					int			ln_Cnt;
					var			lo_Map	= new Mapping			(this._Ref);
					var			lo_Par	= new Parser			(this._Ref);
					var			lo_Rep	= new Repository	();
					DataSet lo_Sch	= new Schema			(this._Ref).Create();
					var			lo_UDS	= new UsrDataSet	(this._Ref, lo_Sch, _PathTest, false);
					//...............................................
					var lc_MsgID	= Guid.NewGuid();
					var lc_SrvID	= Guid.NewGuid();
					var lc_WspID	= Guid.NewGuid();
					var lc_WndID	= Guid.NewGuid();
					var lc_ItmID	= Guid.NewGuid();

					var lo_MsgDTO	= new DTOMsgServer			{	UUID = lc_MsgID	, Name = "name1", Description = "desc1", Host = "host1", Port = "port1" };
					var lo_SrvDTO	= new DTOService				{ UUID	= lc_SrvID, Name = "NameS", MSID = lc_MsgID };

					var lo_WspDTO	= new DTOWorkspace			{ UUID	= lc_SrvID, Description = "NameS"		};
					var lo_WndDTO	= new DTOWorkspaceNode	{ UUID	= lc_WndID, Description	=	"Desc1"		};
					var lo_ItmDTO	= new DTOWorkspaceItem	{	UUID	= lc_ItmID,	ServiceID		= lc_SrvID	};

					lo_WndDTO.Items.Add(lc_ItmID, lo_ItmDTO);
					lo_WspDTO.Nodes.Add(lc_WndID,	lo_WndDTO);

					lo_Rep.MsgServers.Add	(lc_MsgID, lo_MsgDTO);
					lo_Rep.Services.Add		(lc_SrvID, lo_SrvDTO);
					lo_Rep.WorkSpaces.Add (lc_WspID, lo_WspDTO);
					//...............................................
					ln_Cnt	= 1;
					lo_Par.ParseRep2DS(lo_Rep, lo_UDS);
					lo_Rep.Clear();
					lo_Par.ParseDS2Rep(lo_UDS, lo_Rep);
					lo_Rep.MsgServers.TryGetValue(lc_MsgID, out DTOMsgServer lo_DTOt);

					Assert.AreEqual	(1						,	lo_UDS.Services.Rows.Count	,	$"Parser: {ln_Cnt}: DTO-DS:MsgSvr: Error"	);
					Assert.AreEqual	(1						,	lo_Rep.MsgServers.Count			,	$"Parser: {ln_Cnt}: DS-DTO:MsgSvr: Error"	);
					Assert.AreEqual	(lo_MsgDTO.Name	,	lo_DTOt.Name								,	$"Parser: {ln_Cnt}: DS-MsgSvr: Error"			);
				}

			////-------------------------------------------------------------------------------------------
			//[TestMethod]
			//public void UT_SapGuiUsr_Table()
			//	{
			//		int	ln_Cnt	= 0;
			//		var	lo_Ref	= new References();
			//		var	lo_Tab	= new DSTable(this.CreateDataTable(lo_Ref));
			//		Assert.IsNotNull	(lo_Tab,	$"{ln_Cnt}: DS Ready: Error");
			//		//...............................................
			//		var			lc_Key0		= Guid.NewGuid();
			//		var			lc_Key1		= Guid.NewGuid();
			//		DataRow lo_DSRow0	= lo_Tab.NewRow();
			//		DataRow lo_DSRow1	= lo_Tab.NewRow();
			//		//...............................................
			//		lo_DSRow0[lo_Ref.UUID]	= lc_Key0;
			//		lo_DSRow1[lo_Ref.UUID]	= lc_Key1;
			//		//...............................................
			//		ln_Cnt	= 1;
			//		lo_Tab.AddUpdate(lo_DSRow0);
			//		lo_Tab.AddUpdate(lo_DSRow1);
			//		Assert.AreEqual(2,	lo_Tab.Count,	$"{ln_Cnt}: DS AddUpd: Error");
			//		//...............................................
			//		ln_Cnt	= 2;
			//		DataRow lo_DSRow2	= lo_Tab.GetRow(lc_Key0);
			//		Assert.AreEqual(lc_Key0,	lo_DSRow2[lo_Ref.UUID],	$"Cntlr: {ln_Cnt}: DS Read: Error");
			//		//...............................................
			//		ln_Cnt	= 3;
			//		Assert.IsTrue(lo_Tab.Remove(lc_Key0)	,	$"{ln_Cnt}: DT-Remove: Error");
			//		Assert.AreEqual(1,	lo_Tab.Count			,	$"{ln_Cnt}: DT-Rem Count: Error");
			//		//...............................................
			//		ln_Cnt	= 4;
			//		lo_Tab.AddUpdate(lo_DSRow1);
			//		Assert.AreEqual(1,	lo_Tab.Count,	$"Table: {ln_Cnt}: Add Count: Error");
			//		lo_DSRow0								= lo_Tab.NewRow();
			//		lo_DSRow0[lo_Ref.UUID]	= lc_Key0;
			//		lo_Tab.AddUpdate(lo_DSRow0);
			//		Assert.AreEqual(2,	lo_Tab.Count,	$"Table: {ln_Cnt}: Add Count: Error");
			//		//...............................................
			//		ln_Cnt	= 4;

			//		lo_DSRow0["Name"]	= lc_Key0.ToString();
			//		lo_Tab.AddUpdate(lo_DSRow0);
			//		DataRow lo_DSRow3	= lo_Tab.GetRow(lc_Key0);
			//		Assert.AreEqual(lc_Key0						,	lo_DSRow3[lo_Ref.UUID],	$"Cntlr: {ln_Cnt}: DS Read: Error");
			//		Assert.AreEqual(lc_Key0.ToString(),	lo_DSRow3["Name"]			,	$"Cntlr: {ln_Cnt}: DS Read: Error");
			//	}

			////-------------------------------------------------------------------------------------------
			//[TestMethod]
			//public void UT_SapGuiUsr_UsrDS_Srv()
			//	{
			//		int			ln_Cnt	= 0;
			//		var			lo_Ref	= new References();
			//		DataSet lo_Sch	= new Schema(lo_Ref).Create();
			//		var			lo_UDS	= new UsrDataSet(lo_Ref, lo_Sch, _PathTest, false);
			//		//...............................................
			//		if (File.Exists(lo_UDS.DSFullName))	File.Delete(lo_UDS.DSFullName);
			//		Assert.IsFalse	(File.Exists(lo_UDS.DSFullName),	$"Cntlr: {ln_Cnt}: DS Ready: Error");
			//		//...............................................
			//		var			lc_Key0		= Guid.NewGuid();
			//		var			lc_Key1		= Guid.NewGuid();
			//		var			lc_Key2		= Guid.NewGuid();

			//		DataRow lo_DSRow0	= lo_UDS.NewSrvRow();
			//		DataRow lo_DSRow1	= lo_UDS.NewSrvRow();
			//		DataRow lo_DSRow2	= lo_UDS.NewSrvRow();

			//		lo_DSRow0[lo_Ref.UUID]	= lc_Key0;
			//		lo_DSRow1[lo_Ref.UUID]	= lc_Key1;

			//		lo_DSRow2[lo_Ref.UUID]				= lc_Key2;
			//		lo_DSRow2[lo_Ref.Name]				= "Test Name";
			//		lo_DSRow2[lo_Ref.Description]	= "Test description";

			//		//...............................................
			//		ln_Cnt	= 1;
			//		lo_UDS.AddUpdateService(lo_DSRow0);
			//		lo_UDS.AddUpdateService(lo_DSRow1);
			//		Assert.AreEqual(2,	lo_UDS.ServiceCount,	$"Cntlr: {ln_Cnt}: DS Ready: Error");
			//		//...............................................
			//		ln_Cnt	= 2;
			//		DataRow lo_DSRowx	= lo_UDS.GetService(lc_Key0);
			//		Assert.AreEqual(lc_Key0,	lo_DSRowx[lo_Ref.UUID],	$"Cntlr: {ln_Cnt}: DS Read: Error");
			//		//...............................................
			//		ln_Cnt	= 3;
			//		Assert.IsTrue(lo_UDS.RemoveService(lc_Key0)	,	$"Cntlr: {ln_Cnt}: DT-Remove: Error");
			//		Assert.AreEqual(1,	lo_UDS.ServiceCount			,	$"Cntlr: {ln_Cnt}: DT-Rem Count: Error");
			//		//...............................................
			//		ln_Cnt	= 4;
			//		lo_UDS.AddUpdateService(lo_DSRow1);
			//		Assert.AreEqual(1,	lo_UDS.ServiceCount			,	$"Cntlr: {ln_Cnt}: DT-Rem Count: Error");
			//		lo_UDS.AddUpdateService(lo_DSRow2);
			//		Assert.AreEqual(2,	lo_UDS.ServiceCount			,	$"Cntlr: {ln_Cnt}: DT-Rem Count: Error");
			//		//...............................................
			//		ln_Cnt	= 5;
			//		lo_UDS.Save();
			//		DataSet lo_Schx	= new Schema(lo_Ref).Create();
			//		var			lo_UDSx	= new UsrDataSet(lo_Ref, lo_Schx, _PathTest, true);
			//		Assert.AreEqual(lo_UDS.ServiceCount, lo_UDSx.ServiceCount,	$"DSSrv: {ln_Cnt}: Re-load compare: Error");
			//	}

			////-------------------------------------------------------------------------------------------
			//[TestMethod]
			//public void UT_SapGuiUsr_DSStartup()
			//	{
			//		int		ln_Cnt;
			//		bool	lb_Ok;
			//		var		lo_Ref	= new References();
			//		var		lo_DSH	= new UsrDataSet(lo_Ref, _PathTest);
			//		//...............................................
			//		ln_Cnt	= 0;
			//		string lc_Nme	= Path.Combine(_PathTest,	lo_DSH.SchemaFileName);

			//		if (File.Exists(lc_Nme))	File.Delete(lc_Nme);
			//		Assert.IsFalse	(File.Exists(lc_Nme),	$"Cntlr: {ln_Cnt}: DS Ready: Error");
			//		//...............................................
			//		ln_Cnt	= 1;
			//		UsrDataSet	lo_DSH1 = null;

			//		try
			//			{
			//				lo_DSH1	= new UsrDataSet(lo_Ref, _PathTest);
			//				lb_Ok		= true;
			//			}
			//			catch (Exception)	{	lb_Ok	= false; }

			//		Assert.IsTrue		(lb_Ok,																		$"Cntlr: {ln_Cnt}: DS-NoPath: Error");
			//		Assert.AreEqual	(3		,	lo_DSH1?.Dataset.Tables.Count,	$"Cntlr: {ln_Cnt}: DS-NoPath: Error");
			//		//...............................................
			//		ln_Cnt	= 2;
			//		UsrDataSet	lo_DSH2 = null;

			//		try
			//			{
			//				lo_DSH2	= new UsrDataSet(lo_Ref, "ZZZZ");
			//				lb_Ok		= false;
			//			}
			//			catch (System.IO.DirectoryNotFoundException)	{	lb_Ok	= true; }
			//			catch (Exception)															{	lb_Ok	= false; }

			//			Assert.IsTrue(lb_Ok,	$"Cntlr: {ln_Cnt}: DS-NoPath: Error");
			//	}

			////-------------------------------------------------------------------------------------------
			//[TestMethod]
			//public void UT_SapGuiUsr_DSService()
			//	{
			//		//int	ln_Cnt	= 0;
			//		var	lo_Ref	= new References();
			//		var	lo_DSH	= new UsrDataSet(lo_Ref, _PathTest);
			//		//...............................................
			//		var			lc_Key		= Guid.NewGuid();
			//		DataRow lo_DSRow	= lo_DSH.NewSrvRow();
			//		lo_DSRow["UUID"]	= lc_Key;
			//		lo_DSH.AddUpdateService(lc_Key, lo_DSRow);

			//		//var	lo_DSH	= new UsrDataSet(cc_PathTest);
			//		//Assert.IsTrue(lo_DSH.IsReady	,	$"Cntlr: {ln_Cnt}: DS Ready: Error");
			//		////...............................................
			//		//var lo_DTOSrv = new DTOService	{	UUID = Guid.NewGuid().ToString() };
			//		//var x = lo_DTOSrv.UUID.Length;
			//		//ln_Cnt	= 1;
			//		//lo_DSH.AddUpdateService(lo_DTOSrv);
			//		//Assert.IsTrue(lo_DSH.AddUpdateService(lo_DTOSrv)	,	$"Cntlr: {ln_Cnt}: DS Ready: Error");
			//	}

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				private DataTable CreateDataTable(References refr)
					{
						Type lo_Str	= typeof(string);
						Type lo_Gui = typeof(Guid);
						var lo_Tbl	= new DataTable("TestTable");
						//.............................................
						lo_Tbl.Columns.Add(refr.UUID, lo_Gui);
						lo_Tbl.Columns.Add("Name"		, lo_Str);
						//.............................................
						DataColumn[]	Keys	= new DataColumn[1];
						Keys[0]							= lo_Tbl.Columns[refr.UUID];
						lo_Tbl.PrimaryKey		= Keys;
						//.............................................
						return	lo_Tbl;
					}
		}
}
