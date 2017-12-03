using System;
using System.IO;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
//.........................................................
using BxS_Toolset;
using BxS_Toolset.DataContainer;
//•••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••
namespace zBxS_ToolSet_UT
{
	[TestClass]
	public class UT_BxS_ToolSet_DataTable
		{
			private const string	cz_TestDir			= "Test Resources";
			private const string	cz_TestFileName	= "Test DTCntlr.xml";
			private const string	cz_TestIniName	= "saplogon_Test.ini";
			//...................................................
			private	static readonly string	_Path					= Directory.GetParent( Directory.GetCurrentDirectory() ).Parent.Parent.FullName;
			private	static readonly string	_PathTest			= Path.Combine(_Path		,	cz_TestDir			);
			private	static readonly string	_TestFullNme	= Path.Combine(_PathTest,	cz_TestFileName	);
			//...................................................
			private readonly ToolSet _TS	= new ToolSet();

			//-------------------------------------------------------------------------------------------
			[TestMethod]
			public void UT_ToolSet_DTCntlr()
				{
					int	ln_Cnt	= 0;
					//...............................................
					ln_Cnt	++;

					DCController<IDTO, Guid> lo_DC = this._TS.CreateDCController<IDTO, Guid>(_TestFullNme,	(Guid ID) => new DTO() );
					Assert.IsNotNull	(	lo_DC	,	$"DTCntlr: {ln_Cnt}: Instantiate");
					//...............................................
					ln_Cnt	++;
					var lt_Types = new List<Type>	{	typeof(DTO)	};

					DCController<IDTO, Guid>	lo_DC1	= this._TS.CreateDCController<IDTO, Guid>(_TestFullNme,	(Guid ID) => new DTO() );
					IDTO											lo_DTO1	= lo_DC1.DataTable.Create(Guid.NewGuid());

					lo_DC1.DataTable.AddUpdate(lo_DTO1.Key, lo_DTO1);

					Assert.IsNotNull	(	lo_DC1																	,	$"DTCntlr1: {ln_Cnt}: Instantiate");
					Assert.AreEqual		(	1							, lo_DC1.DataTable.Count	,	$"DTCntlr1: {ln_Cnt}: Count"				);
					Assert.IsTrue			(	lo_DC1.Save()														,	$"DTCntlr1: {ln_Cnt}: Save"				);
					Assert.IsTrue			(	File.Exists(_TestFullNme)								,	$"DTCntlr1: {ln_Cnt}: Exists"	);
				}

			//-------------------------------------------------------------------------------------------
			[TestMethod]
			public void UT_ToolSet_DataTable()
				{
					int	ln_Cnt	= 0;
					//...............................................
					ln_Cnt	++;
					DCTable<IDTO, Guid> lo_DT = this._TS.CreateDCTable<IDTO, Guid>(	(Guid ID) => new DTO() );

					Assert.IsNotNull	(		lo_DT				,	$"DCTable: {ln_Cnt}: Ins: Error");
					Assert.AreEqual		(0, lo_DT.Count	,	$"DCTable: {ln_Cnt}: Cnt: Error");
					Assert.IsFalse		(lo_DT.IsDirty	, $"DCTable: {ln_Cnt}: Pro: Error");
					//...............................................
					ln_Cnt	++;
					IDTO lo_Obj	= new DTO() { Key = Guid.NewGuid(), Key1 = Guid.NewGuid(), IsValid	= true };

					Assert.IsTrue		(		lo_DT.AddUpdate(lo_Obj.Key, lo_Obj)	,	$"DCTable: {ln_Cnt}: Add: Error"		);
					Assert.AreEqual	(1, lo_DT.Count													,	$"DCTable: {ln_Cnt}: Write: Error"	);
					Assert.IsTrue		(		lo_DT.IsDirty												, $"DCTable: {ln_Cnt}: Prop: Error"		);
					Assert.IsTrue		(		lo_DT.Exists(lo_Obj.Key)						, $"DCTable: {ln_Cnt}: Prop: Error"		);
					//...............................................
					ln_Cnt	++;
					IDTO lo_Obj0	= lo_DT.Get(Guid.Empty);
					IDTO lo_Obj1	= lo_DT.Get(lo_Obj.Key);

					Assert.IsNotNull	(lo_Obj0										,	$"DCTable: {ln_Cnt}: Write: Error"	);
					Assert.AreEqual		(Guid.Empty	, lo_Obj0.Key		,	$"DCTable: {ln_Cnt}: Write: Error"	);
					Assert.AreEqual		(lo_Obj.Key	, lo_Obj1.Key		,	$"DCTable: {ln_Cnt}: Write: Error"	);

					IList<Guid> e = lo_DT.List<Guid>("XXX");
					IList<Guid> x = lo_DT.List<Guid>("Key");
					Assert.AreEqual	(0, e.Count	,	$"DCTable: {ln_Cnt}: Write: Error"	);
					Assert.AreEqual	(1, x.Count	,	$"DCTable: {ln_Cnt}: Write: Error"	);

					IList<IDTO> a = lo_DT.ValueListFor("Key", lo_Obj.Key);
					IList<IDTO> b = lo_DT.ValueListFor("Key", lo_Obj.Key, "Key1", lo_Obj.Key1);
					IList<IDTO> c = lo_DT.ValueListFor("Key", lo_Obj.Key, "Key1", Guid.Empty);
					IList<IDTO> d = lo_DT.ValueListFor("Key", lo_Obj.Key, "Key1", Guid.NewGuid());

					Assert.AreEqual	(1, a.Count	,	$"DCTable: {ln_Cnt}: Write: Error"	);
					Assert.AreEqual	(1, b.Count	,	$"DCTable: {ln_Cnt}: Write: Error"	);
					Assert.AreEqual	(0, c.Count	,	$"DCTable: {ln_Cnt}: Write: Error"	);
					Assert.AreEqual	(0, d.Count	,	$"DCTable: {ln_Cnt}: Write: Error"	);
				}

			//===========================================================================================
			#region "Local"

				private interface IDTO
					{
						Guid Key			{ get; set; }
						Guid Key1			{ get; set; }
						bool IsValid	{ get; set;	}
					}

				private class DTO : IDTO
					{
						public Guid Key     { get; set; }
						public Guid Key1    { get; set; }
						public bool IsValid { get; set; }
					}

			#endregion

		}
}
