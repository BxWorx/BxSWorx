using System;
using System.IO;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
//.........................................................
using BxS_Toolset;
using BxS_Toolset.IODisk;
using BxS_Toolset.Serialize;
using BxS_Toolset.DataContainer;
//•••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••
namespace zBxS_ToolSet_UT
{
	[TestClass]
	public class UT_BxS_ToolSet_Toolset
		{
			private const string	cz_TestDir			= "Test Resources";
			private const string	cz_TestFileName	= "Test_Data.XML";
			//...................................................
			private	static	readonly string		_Path					= Directory.GetParent( Directory.GetCurrentDirectory() ).Parent.Parent.FullName;
			private	static	readonly string		_PathTest			= Path.Combine(_Path		,	cz_TestDir			);
			private	static	readonly string		_TestFullNme	= Path.Combine(_PathTest,	cz_TestFileName	);
			//...................................................
			private	readonly ToolSet	_TS	= new BxS_Toolset.ToolSet();

			//-------------------------------------------------------------------------------------------
			[TestMethod]
			public void UT_ToolSet_Toolset()
				{
					int	ln_Cnt	= 0;
					//...............................................
					IO													x		= this._TS.GetIO();
					ObjSerializer								y		= this._TS.GetSerlizser();
					DTController	<TestDTO, Guid>	z		= this._TS.CreateDTController	<TestDTO, Guid>	(_TestFullNme, (Guid id) => new TestDTO() { ID = id } );
					DataTable				<TestDTO, Guid>	t		= this._TS.CreateDataTable			<TestDTO, Guid>	(	(Guid id) => new TestDTO() { ID = id } );
					//...............................................
					ln_Cnt ++;

					Assert.IsNotNull(x,	$"Toolset: {ln_Cnt}: x");
					Assert.IsNotNull(y,	$"Toolset: {ln_Cnt}: y");
					Assert.IsNotNull(z,	$"Toolset: {ln_Cnt}: z");
					Assert.IsNotNull(t,	$"Toolset: {ln_Cnt}: t");
					//...............................................
					ln_Cnt ++;

					var			i		= Guid.NewGuid();
					TestDTO o		= z.DataTable.Create(i);
					TestDTO p		= t.Create(i);

					Assert.IsNotNull	(		o		,	$"Toolset: {ln_Cnt}: o");
					Assert.IsNotNull	(		p		,	$"Toolset: {ln_Cnt}: o");
					Assert.AreEqual		(i, o.ID,	$"Toolset: {ln_Cnt}: z");
					Assert.AreEqual		(i, p.ID,	$"Toolset: {ln_Cnt}: z");
				}

			//===========================================================================================
			#region "Local"

				//-------------------------------------------------------------------------------------
				private class TestDTO
					{
						internal TestDTO()
							{
								this._Dict = new Dictionary<string, string>
									{	{ "1", "a" }	,
										{ "2", "b" }		};
							}

						internal Guid ID { get; set; }
						//.............................................
						internal Dictionary<string, string>  _Dict;
					}

			#endregion

		}
}
