using System.Security;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
//.........................................................
using BxS_SAPNCO.Destination;
using BxS_SAPNCO.API.DL;
using BxS_SAPNCO.API;
using BxS_SAPNCO.API.Function;
//•••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••
namespace zBxS_SAPNCO_UT
{
	[TestClass]
	public class UT_BxS_SAPNCO_40RfcFnc
		{
			private	NCOController								co_Cntlr			;
			private	DestinationRfc							co_Dest				;
			private	IDTOConfigSetupDestination	co_Setup			;
			private	SecureString								co_SecurePwd	;

			//-------------------------------------------------------------------------------------------
			[TestMethod]
			public void UT_SAPNCO_RfcFnc_Many()
				{
								int		ln_Cnt	=  0;
								bool	lb_Ok		= true;
					const int		ln_Max	= 10;
					//...............................................
					ln_Cnt	++;

					this.Setup();
					Assert.IsTrue	( this.co_Dest.Ping(),	$"SAPNCO:RfcFnc:Inst {ln_Cnt}: Many" );
					//...............................................
					ln_Cnt	++;

					const string	lc_FncNme	= "MSS_GET_SY_DATE_TIME";

					IList<IRFCFunction>	lt_Fnc	= new List<IRFCFunction>	(ln_Max);
					IList<string>				lt_Res	= new List<string>				(ln_Max);

					for (int i = 0; i < ln_Max; i++)
						{
							lt_Fnc.Add(this.co_Cntlr.CreateRfcFunction(lc_FncNme));
							this.co_Dest.CreateRFCFunction(lt_Fnc[i]);
						}

					for (int i = 0; i < ln_Max; i++)
						{
							if (lt_Fnc[i].Invoke())
								{
									lt_Res.Add( lt_Fnc[i].RfcFunction.GetValue(1).ToString() );
								}
							else
								{
									lt_Res.Add("F");
									lb_Ok	=false;
								}
							System.Threading.Thread.Sleep(1000);
						}

					Assert.IsTrue	( lb_Ok	,	$"SAPNCO:RfcFnc:Invoke {ln_Cnt}: Many" );
				}

			//-------------------------------------------------------------------------------------------
			[TestMethod]
			public void UT_SAPNCO_RfcFnc_Single()
				{
					int	ln_Cnt	= 0;
					//...............................................
					ln_Cnt	++;

					this.Setup();
					Assert.IsTrue	( this.co_Dest.Ping(),	$"SAPNCO:RfcFnc:Inst {ln_Cnt}: Single" );
					//...............................................
					ln_Cnt	++;

					const string	lc_FncNme	= "MSS_GET_SY_DATE_TIME";
					IRFCFunction	lo_RfcFnc	= this.co_Cntlr.CreateRfcFunction(lc_FncNme);
					this.co_Dest.CreateRFCFunction(lo_RfcFnc);

					Assert.IsTrue	( lo_RfcFnc.Invoke()	,	$"SAPNCO:RfcFnc:Invoke {ln_Cnt}: Single" );
				}

			//-------------------------------------------------------------------------------------------
			private void Setup()
				{
					this.co_Cntlr		= new NCOController();
					//...............................................
					this.co_SecurePwd						= new SecureString();
					const string	lz_PWrd				= "M@@n1234"				;
					var						lo_SecurePwd	= new SecureString();

					foreach( char c in lz_PWrd) { lo_SecurePwd.AppendChar(c) ; }
					//...............................................
					IList<string>	lt1			= this.co_Cntlr.GetSAPGUIConfigEntries();
					string				lc_ID0	= lt1.FirstOrDefault(s => s.Contains("PWD"));

					this.co_Dest	= this.co_Cntlr.CreateDestinationRFC(lc_ID0);
					//...............................................
					this.co_Setup					= this.co_Cntlr.CreateConfigSetupDestination();
					this.co_Setup.Client	= 700;
					this.co_Setup.User		= "DERRICKBINGH";

					this.co_Dest.LoadConfig(this.co_Setup);
					this.co_Dest.SecurePassword	= lo_SecurePwd;
					//...............................................
					this.co_Cntlr.ProcureDestination(this.co_Dest);
				}
		}
}
