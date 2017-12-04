using System;
using System.IO;
using Microsoft.VisualStudio.TestTools.UnitTesting;
//.........................................................
using BxS_SAPLogon.API;
//•••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••
namespace zBxS_SAPLogon_UT
{
	[TestClass]
	public class UT_BxS_SAPLogon
	{
		private const string	cz_TestDir	= "Test Resources";
		private const string	cz_TestNme	= "Saplogon_Fav.xml";
		//...................................................
		private	static readonly string	_Path					= Directory.GetParent( Directory.GetCurrentDirectory() ).Parent.Parent.FullName;
		private	static readonly string	_PathTest			= Path.Combine(_Path		,	cz_TestDir	);
		private	static readonly string	_TestFullNme	= Path.Combine(_PathTest,	cz_TestNme	);

		private readonly Factory _Fac	= new Factory();

		//-------------------------------------------------------------------------------------------
			[TestMethod]
			public void UT_SAPLogon_Favourites()
				{
					int	ln_Cnt	= 0;
					//...............................................
					ln_Cnt	++;
					IFavourites lo_Fav	= this._Fac.CreateFavourite(	fullPathName: _TestFullNme	,
																														AutoLoad		: false					,
																														AutoSave		: false						);
					Assert.IsNotNull	(	lo_Fav,	$"DCTable: {ln_Cnt}: Instantiate");

					//...............................................
					ln_Cnt	++;

					IDTOFavourite lo_DTO1 = lo_Fav.Create(Guid.NewGuid());
					IDTOFavourite lo_DTO2 = lo_Fav.Create(Guid.NewGuid());
					IDTOFavourite lo_DTO3 = lo_Fav.Create(Guid.NewGuid());
					IDTOFavourite lo_DTO4 = lo_Fav.Create(Guid.NewGuid());

					lo_Fav.Add(lo_DTO1);
					Assert.AreEqual		(1, lo_Fav.Count	,	$"Fav: {ln_Cnt}: Cnt1");
					lo_Fav.Add(lo_DTO2);
					Assert.AreEqual		(2, lo_Fav.Count	,	$"Fav: {ln_Cnt}: Cnt2");
					lo_Fav.Add(lo_DTO3);
					Assert.AreEqual		(3, lo_Fav.Count	,	$"Fav: {ln_Cnt}: Cnt3");
					lo_Fav.Add(lo_DTO4);
					Assert.AreEqual		(3, lo_Fav.Count	,	$"Fav: {ln_Cnt}: Cnt4");

					lo_Fav.Save(true);

					//...............................................
					ln_Cnt	++;
					IFavourites lo_Fav1	= this._Fac.CreateFavourite(	fullPathName: _TestFullNme	,
																														AutoLoad		: true					,
																														AutoSave		: false						);

					Assert.IsNotNull	(		lo_Fav1				,	$"Fav: {ln_Cnt}: Instantiate"	);
					Assert.AreEqual		(3, lo_Fav1.Count	,	$"Fav: {ln_Cnt}: Cnt4"				);
				}
	}
}
