using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.IO;
using System.Data;
//.........................................................
using SAPGUI.USR.DS;
using SAPGUI.API.DTO;
//•••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••
namespace zBxS_SAPGUI_UT
	{
	[TestClass]
	public class UT_BxS_SAPGUI_USR
		{
			private const string	cz_TestDir		= "Test Resources";

			//...................................................
			private	static readonly string	_Path				= Directory.GetParent( Directory.GetCurrentDirectory() ).Parent.Parent.FullName;
			private	static readonly string	_PathTest		= Path.Combine(_Path,	cz_TestDir);

			//-------------------------------------------------------------------------------------------
			[TestMethod]
			public void UT_SapGuiUsr_Schema()
				{
					int	ln_Cnt;
					var	lo_DS	= new Schema(new References());
					//...............................................
					ln_Cnt	= 1;
					DataSet lo_DSx	= lo_DS.Create();
					Assert.IsNotNull	(lo_DSx										,	$"Cntlr: {ln_Cnt}: DS-Schema-Nul: Error");
					Assert.AreEqual		(lo_DSx.Tables.Count	, 3	,	$"Cntlr: {ln_Cnt}: DS-Schema-Tbl: Error");
				}

			//-------------------------------------------------------------------------------------------
			[TestMethod]
			public void UT_SapGuiUsr_Mapping()
				{
					int	ln_Cnt;
					var	lo_Map	= new Mapping();
					//...............................................
					ln_Cnt	= 1;
					Assert.AreNotEqual	(lo_Map.Service.Count		,	$"Cntlr: {ln_Cnt}: DS-Schema-Nul: Error");
					Assert.AreNotEqual	(lo_Map.MsgServer.Count	,	$"Cntlr: {ln_Cnt}: DS-Schema-Nul: Error");
					Assert.AreNotEqual	(lo_Map.Workspace.Count	,	$"Cntlr: {ln_Cnt}: DS-Schema-Nul: Error");

					Assert.AreNotEqual	(lo_Map.Service.Count		,	$"Cntlr: {ln_Cnt}: DS-Schema-Nul: Error");
					Assert.AreNotEqual	(lo_Map.MsgServer.Count	,	$"Cntlr: {ln_Cnt}: DS-Schema-Nul: Error");
					Assert.AreNotEqual	(lo_Map.Workspace.Count	,	$"Cntlr: {ln_Cnt}: DS-Schema-Nul: Error");
				}

			//-------------------------------------------------------------------------------------------
			[TestMethod]
			public void UT_SapGuiUsr_Table()
				{
					int	ln_Cnt	= 0;
					var	lo_Ref	= new References();
					var	lo_Tab	= new DSTable(lo_Ref, this.CreateDataTable(lo_Ref));
					Assert.IsNotNull	(lo_Tab,	$"{ln_Cnt}: DS Ready: Error");
					//...............................................
					var			lc_Key0		= Guid.NewGuid();
					var			lc_Key1		= Guid.NewGuid();
					DataRow lo_DSRow0	= lo_Tab.NewRow();
					DataRow lo_DSRow1	= lo_Tab.NewRow();
					//...............................................
					lo_DSRow0[lo_Ref.UUID]	= lc_Key0;
					lo_DSRow1[lo_Ref.UUID]	= lc_Key1;
					//...............................................
					ln_Cnt	= 1;
					lo_Tab.AddUpdate(lo_DSRow0);
					lo_Tab.AddUpdate(lo_DSRow1);
					Assert.AreEqual(2,	lo_Tab.Count,	$"{ln_Cnt}: DS AddUpd: Error");
					//...............................................
					ln_Cnt	= 2;
					DataRow lo_DSRow2	= lo_Tab.GetRow(lc_Key0);
					Assert.AreEqual(lc_Key0,	lo_DSRow2[lo_Ref.UUID],	$"Cntlr: {ln_Cnt}: DS Read: Error");
					//...............................................
					ln_Cnt	= 3;
					Assert.IsTrue(lo_Tab.Remove(lc_Key0)	,	$"{ln_Cnt}: DT-Remove: Error");
					Assert.AreEqual(1,	lo_Tab.Count			,	$"{ln_Cnt}: DT-Rem Count: Error");
					//...............................................
					ln_Cnt	= 4;
					lo_Tab.AddUpdate(lo_DSRow1);
					Assert.AreEqual(1,	lo_Tab.Count,	$"Table: {ln_Cnt}: Add Count: Error");
					lo_DSRow0								= lo_Tab.NewRow();
					lo_DSRow0[lo_Ref.UUID]	= lc_Key0;
					lo_Tab.AddUpdate(lo_DSRow0);
					Assert.AreEqual(2,	lo_Tab.Count,	$"Table: {ln_Cnt}: Add Count: Error");
					//...............................................
					ln_Cnt	= 4;

					lo_DSRow0["Name"]	= lc_Key0.ToString();
					lo_Tab.AddUpdate(lo_DSRow0);
					DataRow lo_DSRow3	= lo_Tab.GetRow(lc_Key0);
					Assert.AreEqual(lc_Key0						,	lo_DSRow3[lo_Ref.UUID],	$"Cntlr: {ln_Cnt}: DS Read: Error");
					Assert.AreEqual(lc_Key0.ToString(),	lo_DSRow3["Name"]			,	$"Cntlr: {ln_Cnt}: DS Read: Error");
				}

			//-------------------------------------------------------------------------------------------
			[TestMethod]
			public void UT_SapGuiUsr_UsrDS_Srv()
				{
					int			ln_Cnt	= 0;
					var			lo_Ref	= new References();
					DataSet lo_Sch	= new Schema(lo_Ref).Create();
					var			lo_UDS	= new UsrDataSet(lo_Ref, lo_Sch, _PathTest, false);
					//...............................................
					if (File.Exists(lo_UDS.DSFullName))	File.Delete(lo_UDS.DSFullName);
					Assert.IsFalse	(File.Exists(lo_UDS.DSFullName),	$"Cntlr: {ln_Cnt}: DS Ready: Error");
					//...............................................
					var			lc_Key0		= Guid.NewGuid();
					var			lc_Key1		= Guid.NewGuid();
					var			lc_Key2		= Guid.NewGuid();

					DataRow lo_DSRow0	= lo_UDS.NewSrvRow();
					DataRow lo_DSRow1	= lo_UDS.NewSrvRow();
					DataRow lo_DSRow2	= lo_UDS.NewSrvRow();

					lo_DSRow0[lo_Ref.UUID]	= lc_Key0;
					lo_DSRow1[lo_Ref.UUID]	= lc_Key1;
					lo_DSRow2[lo_Ref.UUID]	= lc_Key2;
					//...............................................
					ln_Cnt	= 1;
					lo_UDS.AddUpdateService(lo_DSRow0);
					lo_UDS.AddUpdateService(lo_DSRow1);
					Assert.AreEqual(2,	lo_UDS.ServiceCount,	$"Cntlr: {ln_Cnt}: DS Ready: Error");
					//...............................................
					ln_Cnt	= 2;
					DataRow lo_DSRowx	= lo_UDS.GetService(lc_Key0);
					Assert.AreEqual(lc_Key0,	lo_DSRowx[lo_Ref.UUID],	$"Cntlr: {ln_Cnt}: DS Read: Error");
					//...............................................
					ln_Cnt	= 3;
					Assert.IsTrue(lo_UDS.RemoveService(lc_Key0)	,	$"Cntlr: {ln_Cnt}: DT-Remove: Error");
					Assert.AreEqual(1,	lo_UDS.ServiceCount			,	$"Cntlr: {ln_Cnt}: DT-Rem Count: Error");
					//...............................................
					ln_Cnt	= 4;
					lo_UDS.AddUpdateService(lo_DSRow1);
					Assert.AreEqual(1,	lo_UDS.ServiceCount			,	$"Cntlr: {ln_Cnt}: DT-Rem Count: Error");
					lo_UDS.AddUpdateService(lo_DSRow2);
					Assert.AreEqual(2,	lo_UDS.ServiceCount			,	$"Cntlr: {ln_Cnt}: DT-Rem Count: Error");
					//...............................................
					ln_Cnt	= 5;
					lo_UDS.Save();
					var	lo_UDSx	= new UsrDataSet(lo_Ref, lo_Sch, _PathTest, true);
					Assert.AreEqual(lo_UDS.ServiceCount, lo_UDSx.ServiceCount,	$"DSSrv: {ln_Cnt}: Re-load compare: Error");
				}

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
