using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
//.........................................................
using BxS_SAPNCO.Destination;
using BxS_SAPConn.API;
using BxS_SAPNCO.API;
using BxS_SAPNCO.Helpers;
//•••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••
namespace zBxS_SAPNCO_UT
{
	[TestClass]
	public class UT_BxS_SAPNCO
	{
		//private const string	cz_TestDir	= "Test Resources";
		//private const string	cz_TestNme	= "Saplogon_Fav.xml";
		//...................................................
		//private	static readonly string	_Path					= Directory.GetParent( Directory.GetCurrentDirectory() ).Parent.Parent.FullName;
		//private	static readonly string	_PathTest			= Path.Combine(_Path		,	cz_TestDir	);
		//private	static readonly string	_TestFullNme	= Path.Combine(_PathTest,	cz_TestNme	);

		private readonly ConnFactory	_Fac	= new ConnFactory();
		private readonly NCOController		_Cnt	= new NCOController();

			//-------------------------------------------------------------------------------------------
			[TestMethod]
			public void UT_SAPNCO_Controller()
				{
					int	ln_Cnt	= 0;
					//...............................................
					ln_Cnt	++;

					IDTOConnParameters	lo_DTO		= this._Fac.CreateParameterDTO();
					IDTOConnParameters	lo_DTO1		= this._Fac.CreateParameterDTO();

					IList<string> y = this._Cnt.GetSAPIniList();
					//this._Cnt.LoadParameters(y[0]	, lo_DTO);
					//this._Cnt.LoadParameters(y[1]	, lo_DTO1);

					Assert.AreNotEqual		(0, y.Count										,	$"SAPNCO:INI: {ln_Cnt}: List");
					Assert.AreNotEqual		(0, lo_DTO.Parameters.Count		,	$"SAPNCO:INI: {ln_Cnt}: Parms 0");
					Assert.AreNotEqual		(0, lo_DTO1.Parameters.Count	,	$"SAPNCO:INI: {ln_Cnt}: Parms 1");
				}

			//-------------------------------------------------------------------------------------------
			[TestMethod]
			public void UT_SAPNCO_Dest_INI()
				{
				//	int	ln_Cnt	= 0;
				//	//...............................................
				//	ln_Cnt	++;

				//	IDTOConnParameters	lo_DTO		= this._Fac.CreateParameterDTO();
				//	IDTOConnParameters	lo_DTO1		= this._Fac.CreateParameterDTO();
				//	var									lo_SAPini	= new SAPLogonINI();

				//	IList<string> y = lo_SAPini.GetEntries();
				//	lo_SAPini.LoadParameters(y[0]	, lo_DTO);
				//	lo_SAPini.LoadParameters("XXX"	, lo_DTO1);

				//	Assert.AreNotEqual		(0, y.Count										,	$"SAPNCO:INI: {ln_Cnt}: List");
				//	Assert.AreNotEqual		(0, lo_DTO.Parameters.Count		,	$"SAPNCO:INI: {ln_Cnt}: Parms");
				//	Assert.AreEqual				(0, lo_DTO1.Parameters.Count	,	$"SAPNCO:INI: {ln_Cnt}: Parms Err");
				}

			//-------------------------------------------------------------------------------------------
			[TestMethod]
			public void UT_SAPNCO_Dest_Mngr()
				{
					int	ln_Cnt	= 0;
					//...............................................
					ln_Cnt	++;
					//IFavourites lo_Fav	= this._Fac.CreateFavourite(	fullPathName: _TestFullNme	,
					//																									AutoLoad		: false					,
					//																									AutoSave		: false						);
					//Assert.IsNotNull	(	lo_Fav,	$"DCTable: {ln_Cnt}: Instantiate");

					////...............................................
					//ln_Cnt	++;

					//IDTOFavourite lo_DTO1 = lo_Fav.Create(Guid.NewGuid());
					//IDTOFavourite lo_DTO2 = lo_Fav.Create(Guid.NewGuid());
					//IDTOFavourite lo_DTO3 = lo_Fav.Create(Guid.NewGuid());
					//IDTOFavourite lo_DTO4 = lo_Fav.Create(Guid.NewGuid());

					//lo_Fav.Add(lo_DTO1);
					//Assert.AreEqual		(1, lo_Fav.Count	,	$"Fav: {ln_Cnt}: Cnt1");
					//lo_Fav.Add(lo_DTO2);
					//Assert.AreEqual		(2, lo_Fav.Count	,	$"Fav: {ln_Cnt}: Cnt2");
					//lo_Fav.Add(lo_DTO3);
					//Assert.AreEqual		(3, lo_Fav.Count	,	$"Fav: {ln_Cnt}: Cnt3");
					//lo_Fav.Add(lo_DTO4);
					//Assert.AreEqual		(3, lo_Fav.Count	,	$"Fav: {ln_Cnt}: Cnt4");

					//lo_Fav.Save(true);

					////...............................................
					//ln_Cnt	++;
					//IFavourites lo_Fav1	= this._Fac.CreateFavourite(	fullPathName: _TestFullNme	,
					//																									AutoLoad		: true					,
					//																									AutoSave		: false						);

					//Assert.IsNotNull	(		lo_Fav1				,	$"Fav: {ln_Cnt}: Instantiate"	);
					//Assert.AreEqual		(3, lo_Fav1.Count	,	$"Fav: {ln_Cnt}: Cnt4"				);
				}
	}
}
