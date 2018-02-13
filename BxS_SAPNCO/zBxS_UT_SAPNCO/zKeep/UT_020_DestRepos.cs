using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
//.........................................................
using SMC	= SAP.Middleware.Connector;
//.........................................................
using BxS_SAPNCO.Destination;
using BxS_SAPNCO.API.DL;
//•••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••
namespace zBxS_SAPNCO_UT
{
	[TestClass]

	public class UT_20_DestRepos
	{
		private const string	cz_A = "A";
		//...................................................

			//-------------------------------------------------------------------------------------------
			[TestMethod]
			public void UT_DestRepos_Instantiate()
				{
					int	ln_Cnt	= 0;
					//...............................................
					ln_Cnt	++;

					var lo_Repo = new DestinationRepository();

					Assert.IsNotNull	(lo_Repo	,	$"SAPNCO:Repos:GetAddID {ln_Cnt}: Not =");
				}

			//-------------------------------------------------------------------------------------------
			[TestMethod]
			public void UT_DestRepos_AddGetGUID()
				{
					int	ln_Cnt	= 0;
					//...............................................
					ln_Cnt	++;

					var lg_ID1	= Guid.NewGuid();
					var lo_Repo = new DestinationRepository();

					Guid lg_a00	= lo_Repo.GetAddIDFor(cz_A, false	);
					Guid lg_a01	= lo_Repo.GetAddIDFor(cz_A, true	);
					Guid lg_a02	= lo_Repo.GetAddIDFor(cz_A, true	);

					IList<IDTORefEntry> lt_Lst = lo_Repo.ReferenceList();

					Assert.AreNotEqual	(lg_a00, lg_a01	,	$"SAPNCO:Repos:GetAddID {ln_Cnt}: Not =	");
					Assert.AreEqual			(lg_a01, lg_a02	,	$"SAPNCO:Repos:GetAddID {ln_Cnt}: =			");
					Assert.AreEqual			(1, lt_Lst.Count,	$"SAPNCO:Repos:GetAddID {ln_Cnt}: Count	");
				}

			//-------------------------------------------------------------------------------------------
			[TestMethod]
			public void UT_DestRepos_AddConfig()
				{
					int	ln_Cnt	= 0;
					//...............................................
					ln_Cnt	++;

					var lg_ID1	= Guid.NewGuid();
					var lo_Repo = new DestinationRepository();
					var lo_Cnfg = new SMC.RfcConfigParameters();

					Guid	lg_b01 = lo_Repo.AddConfig(cz_A		, lo_Cnfg);
					Guid	lg_b02 = lo_Repo.AddConfig(lg_ID1	, lo_Cnfg);
					Guid	lg_b03 = lo_Repo.AddConfig(lg_b01	, lo_Cnfg);

					IList<IDTORefEntry> lt_Lst = lo_Repo.ReferenceList();

					Assert.AreEqual	(lg_ID1	, lg_b02				,	$"SAPNCO:Repos:Conf {ln_Cnt}: 1			");
					Assert.AreEqual	(lg_b01	, lg_b03				,	$"SAPNCO:Repos:Conf {ln_Cnt}: 2			");
					Assert.AreEqual	(1			, lt_Lst.Count	,	$"SAPNCO:Repos:Conf {ln_Cnt}: Count	");
					//...............................................
					ln_Cnt	++;

					IList<IDTORefEntry> lt = lo_Repo.ReferenceList();
					Assert.AreNotEqual	(0, lt.Count	,	$"SAPNCO:Repos:Conf {ln_Cnt}: Not =	");
				}
	}
}
