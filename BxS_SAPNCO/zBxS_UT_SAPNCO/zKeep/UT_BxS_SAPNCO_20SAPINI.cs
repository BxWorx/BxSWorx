using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
//.........................................................
using SMC	= SAP.Middleware.Connector;
//.........................................................
using BxS_SAPNCO.Destination;
using BxS_SAPNCO.Helpers;
using BxS_SAPNCO.API.DL;
//•••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••
namespace zBxS_SAPNCO_UT
{
	[TestClass]
	public class UT_BxS_SAPNCO_20SAPINI
	{
			//-------------------------------------------------------------------------------------------
			[TestMethod]
			public void UT_SAPNCO_SAPIni()
				{
					//int						ln_Cnt	= 0;
					//IList<IDTORefEntry> lt_Lst;
					////...............................................
					//ln_Cnt	++;

					//var	lo_Ini	= new SAPLogonINI();
					//var	lo_Rep	= new DestinationRepository();

					//Assert.IsNotNull	(	lo_Ini	,	$"SAPNCO:Repos:SAPINI {ln_Cnt}: INI"		);
					//Assert.IsNotNull	(	lo_Rep	,	$"SAPNCO:Repos:SAPINI {ln_Cnt}: Rep"		);
					////...............................................
					//ln_Cnt	++;

					//lo_Ini.LoadRepository(lo_Rep);
					//lt_Lst	= lo_Rep.ReferenceList();

					//Assert.AreNotEqual	(0	, lo_Rep.Count	,	$"SAPNCO:Repos:SAPINI  {ln_Cnt}: List1");
					////...............................................
					//ln_Cnt	++;

					//SMC.RfcConfigParameters lo_Cn1	= lo_Rep.GetParameters(lt_Lst[0].Name);

					//Assert.AreNotEqual	(0	, lo_Cn1.Count	,	$"SAPNCO:Repos:SAPINI  {ln_Cnt}: List"	);
				}
	}
}
