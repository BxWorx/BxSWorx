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
			private const string	cz_SchemaName	= "SAPGUI_USR_Schema.xml";
			private const string	cz_FileName		= "SAPGUI_USR_Repos.xml";
			private const string	cz_DSName			= "SAPGUI_USR";
			private const string	cz_TestDir		= "Test Resources";

			//...................................................
			private	static readonly string	_Path				= Directory.GetParent( Directory.GetCurrentDirectory() ).Parent.Parent.FullName;
			private	static readonly string	_PathTest		= Path.Combine(_Path,	cz_TestDir);
			//private	static readonly string	cc_SchemaName	= Path.Combine(_Path,	cz_TestDir, cz_SchemaName);
			//private	static readonly string	cc_FullName		= Path.Combine(_Path,	cz_TestDir, cz_FileName);

			//-------------------------------------------------------------------------------------------
			[TestMethod]
			public void UT_SapGuiUsr_Schema()
				{
					int	ln_Cnt;
					var	lo_DS	= new Schema();
					//...............................................
					ln_Cnt	= 1;
					DataSet lo_DSx	= lo_DS.Create();
					Assert.IsNotNull	(lo_DSx										,	$"Cntlr: {ln_Cnt}: DS-Schema-Nul: Error");
					Assert.AreEqual		(lo_DSx.Tables.Count	, 3	,	$"Cntlr: {ln_Cnt}: DS-Schema-Tbl: Error");
				}

			//-------------------------------------------------------------------------------------------
			[TestMethod]
			public void UT_SapGuiUsr_DataSetHandler()
				{
					int		ln_Cnt;
					bool	lb_Ok;

					var		lo_DSH	= new DataSetHandler(_PathTest);
					//...............................................
					ln_Cnt	= 0;
					string lc_Nme	= Path.Combine(_PathTest,	lo_DSH.SchemaName);

					if (File.Exists(lc_Nme))	File.Delete(lc_Nme);
					Assert.IsFalse	(File.Exists(lc_Nme),	$"Cntlr: {ln_Cnt}: DS Ready: Error");
					//...............................................
					ln_Cnt	= 1;
					var			lo_DSH1	= new DataSetHandler(_PathTest);
					DataSet lo_DS1	= null;

					try
						{
							lo_DS1	= lo_DSH1.GetDataSet();
							lb_Ok	= true;
						}
						catch (Exception)	{	lb_Ok	= false; }

					Assert.IsTrue		(lb_Ok,												$"Cntlr: {ln_Cnt}: DS-NoPath: Error");
					Assert.AreEqual	(3		,	lo_DS1?.Tables.Count,	$"Cntlr: {ln_Cnt}: DS-NoPath: Error");
					//...............................................
					ln_Cnt	= 3;
					var	lo_DSH2			= new DataSetHandler("ZZZZ");
					DataSet lo_DS2	= null;

					try
						{
							lo_DS2	= lo_DSH2.GetDataSet();
							lb_Ok		= false;
						}
						catch (System.IO.DirectoryNotFoundException)	{	lb_Ok	= true; }
						catch (Exception)															{	lb_Ok	= false; }

						Assert.IsTrue(lb_Ok,	$"Cntlr: {ln_Cnt}: DS-NoPath: Error");
				}

			//-------------------------------------------------------------------------------------------
			[TestMethod]
			public void UT_SapGuiUsr_DataSet()
				{
					//int	ln_Cnt	= 0;
					//var	lo_DSH	= new UsrDataSet(cc_PathTest);
					//Assert.IsTrue(lo_DSH.IsReady	,	$"Cntlr: {ln_Cnt}: DS Ready: Error");
					////...............................................
					//var lo_DTOSrv = new DTOService	{	UUID = Guid.NewGuid().ToString() };
					//var x = lo_DTOSrv.UUID.Length;
					//ln_Cnt	= 1;
					//lo_DSH.AddUpdateService(lo_DTOSrv);
					//Assert.IsTrue(lo_DSH.AddUpdateService(lo_DTOSrv)	,	$"Cntlr: {ln_Cnt}: DS Ready: Error");
				}
		}
}
