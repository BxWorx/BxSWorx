using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
//.........................................................
using SMC	= SAP.Middleware.Connector;
using BxS_SAPNCO.Destination;
using BxS_SAPConn.API;
using BxS_SAPNCO.Helpers;
//•••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••
namespace zBxS_SAPNCO_UT
{
	[TestClass]
	public class UT_BxS_SAPNCO_2SAPINI
	{
		private const string	cz_TestDir	= "Test Resources";
		private const string	cz_TestNme	= "Saplogon_Fav.xml";
		//...................................................
		//private	static readonly string	_Path					= Directory.GetParent( Directory.GetCurrentDirectory() ).Parent.Parent.FullName;
		//private	static readonly string	_PathTest			= Path.Combine(_Path		,	cz_TestDir	);
		//private	static readonly string	_TestFullNme	= Path.Combine(_PathTest,	cz_TestNme	);

		private readonly SMC.SapLogonIniConfiguration	_SAPIni	= SMC.SapLogonIniConfiguration.Create();

			//-------------------------------------------------------------------------------------------
			[TestMethod]
			public void UT_SAPNCO_Repos()
				{
					int	ln_Cnt	= 0;
					//...............................................
					ln_Cnt	++;

					var lo_Ini	= new SAPLogonINI(this._SAPIni);


					//IDTOConnParameters	lo_DTO		= this._Fac.CreateParameterDTO();
					//IDTOConnParameters	lo_DTO1		= this._Fac.CreateParameterDTO();

					//IList<string> y = this._Cnt.GetSAPIniList();
					//this._Cnt.LoadParameters(y[0]	, lo_DTO);
					//this._Cnt.LoadParameters(y[1]	, lo_DTO1);

					//Assert.AreNotEqual		(0, y.Count										,	$"SAPNCO:INI: {ln_Cnt}: List");
					//Assert.AreNotEqual		(0, lo_DTO.Parameters.Count		,	$"SAPNCO:INI: {ln_Cnt}: Parms 0");
					//Assert.AreNotEqual		(0, lo_DTO1.Parameters.Count	,	$"SAPNCO:INI: {ln_Cnt}: Parms 1");
				}
	}
}
