using System;
using System.Threading;
//.........................................................
using Microsoft.VisualStudio.TestTools.UnitTesting;
//.........................................................
using BxS_WorxNCO.BDCSession.Main;
using BxS_WorxNCO.BDCSession.Parser;
using BxS_WorxNCO.BDCSession.DTO;
using BxS_WorxIPX.Main;
using BxS_WorxIPX.BDC;
//•••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••
namespace BxS_zWorx_UT_Destination.Test_Units
{
	[TestClass]
	public class UT_410_BDCSessionParser
		{
			private	const LazyThreadSafetyMode	cz_LM		= LazyThreadSafetyMode.ExecutionAndPublication;

			private	const	string	_Nme	=  "Test-00"									;
			private	const	string	_Path	=  @"GitHub\BxSWorx\BxS_Worx\BxS_zWorxIPX_UT\Test Resources";

			private	readonly	string	_User	;
			private						string	_Full	;

			private	readonly	IPX_Controller			co_Cntlr	;
			private	readonly	IBDC_Controller			co_BC			;
			private	readonly	BDC_Session_Factory	co_BSFact	;
			//...
			private readonly	Lazy< BDC_Parser_Factory >	_ParserFactory	;
			private	readonly	BDC_Parser									_Parser					;

			//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
			public UT_410_BDCSessionParser()
				{
					this.co_Cntlr		= IPX_Controller.Instance	;
					this.co_BC			= this.co_Cntlr.Create_BDCController();
					//...
					this._ParserFactory		= new Lazy< BDC_Parser_Factory >(	()=>	BDC_Parser_Factory.Instance	, cz_LM	);
					this._Parser					=	new	BDC_Parser	( this._ParserFactory );
					this.co_BSFact				= BDC_Session_Factory.Instance;

					this._User						= Environment.GetFolderPath(Environment.SpecialFolder.UserProfile);
				}

			[TestMethod]
			//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
			public void UT_410_BCDParser_10_Instantiate()
				{
					Assert.IsNotNull	( this.co_Cntlr		, "C" );
					Assert.IsNotNull	( this.co_BC			, "C" );
					Assert.IsNotNull	( this.co_BSFact	, "C" );
					Assert.IsNotNull	( this._Parser		, "C" );
				}

			[TestMethod]
			//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
			public void UT_410_BCDParser_20_ParseSession()
				{
					this.SetFullPath(_Nme);
					//...
					DTO_BDC_Session lo_DTO	= this.co_BSFact.CreateSessionDTO();
					IRequest lo_R0					= this.co_BC.ReceiveRequest_FromFile( this._Full );
					Assert.IsNotNull	( lo_DTO	, "C" );
					Assert.IsNotNull	( lo_R0		, "C" );
					//...
					var				lt_Guids	= new Guid[lo_R0.Sessions.Keys.Count];
					ISession	lo_BS			= null;

					lo_R0.Sessions.Keys.CopyTo(lt_Guids,0);
					lo_BS	= lo_R0.Sessions[lt_Guids[0]];
					this._Parser.Parse( lo_BS , lo_DTO );
				}

			[TestMethod]
			//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
			public void UT_410_BCDParser_30_ParseRequest()
				{
					this.SetFullPath("DPB");
					//...
					IRequest lo_R0	= this.co_BC.ReceiveRequest_FromFile( this._Full );
					Assert.IsNotNull	( lo_R0	, "C" );
					//...
				}

		//.

			//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
			private void SetFullPath( string name )	=>	this._Full	= $@"{this._User}\{_Path}\{name}.xml" ;

		//.

		}
}
