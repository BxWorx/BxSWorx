using Microsoft.VisualStudio.TestTools.UnitTesting;

using BxS_WorxIPX.Main;
using BxS_WorxIPX.BDC;
using BxS_WorxIPX.BDCSAP;
using System;

namespace BxS_zWorx_UT_Destination.Test_Units
{
	[TestClass]
	public class UT_200_BDCXMLConfig
		{
			//private	const	string	_Nme	=  "Test-00"									;
			//private	const	string	_Path	=  @"C:\Users\BMA\GitHub\BxSWorx\BxS_Worx\BxS_zWorxIPX_UT\Test Resources";
			//private				string	_Full	;

			private	readonly	IPX_Controller			co_Cntlr	;
			private	readonly	IBDCRequestManager	co_RM			;
			private	readonly	BDCXMLConfig				co_Cfg		;
			//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
			public UT_200_BDCXMLConfig()
				{
					this.co_Cntlr		= IPX_Controller.Instance	;
					this.co_RM			= this.co_Cntlr.Create_BDCRequestManager();
					this.co_Cfg			= this.co_RM.Create_BDCXmlConfig( true );
				}

			[TestMethod]
			//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
			public void UT_100_BDCXmlCfg_10_Instantiate()
				{
					Assert.IsNotNull( this.co_Cntlr	, "" );
					Assert.IsNotNull( this.co_RM		, "" );
					Assert.IsNotNull( this.co_Cfg		, "" );
				}

			[TestMethod]
			//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
			public void UT_100_BDCXmlCfg_20_ToXML()
				{
					try
						{
							string x = this.co_RM.SerializeXMLConfig( this.co_Cfg );
						}
					catch
						{
							Assert.Fail( "A" );
						}
				}

			[TestMethod]
			//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
			public void UT_100_BDCXmlCfg_30_ToObj()
				{
					BDCXMLConfig lo_R0	= null;
					//...
					try
						{
							string x	= this.co_RM.SerializeXMLConfig( this.co_Cfg );
							lo_R0			= this.co_RM.DeserializeXMLConfig( x );
						}
					catch
						{
							Assert.Fail( "A" );
						}

					Assert.IsNotNull	(								lo_R0							, "" );
					Assert.AreNotEqual( Guid.Empty ,	lo_R0.GUID				, "" );
					Assert.AreEqual		( this.co_Cfg.GUID ,	lo_R0.GUID	, "" );
				}

			//.

			////¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
			//private void SetFullPath( string name )	=>	this._Full	= $@"{_Path}\{name}.xml" ;

			//.

		}

	//.

	}
