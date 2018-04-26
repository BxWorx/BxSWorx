using Microsoft.VisualStudio.TestTools.UnitTesting;

using BxS_WorxIPX.Main;
using BxS_WorxIPX.BDC;
using System;

namespace BxS_zWorx_UT_Destination.Test_Units
{
	[TestClass]
	public class UT_200_BDCXMLConfig
		{
			private	readonly	IPX_Controller		co_Cntlr	;
			private	readonly	IBDC_Controller		co_BC			;
			private	readonly	IXMLConfig				co_Cfg		;
			//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
			public UT_200_BDCXMLConfig()
				{
					this.co_Cntlr		= IPX_Controller.Instance	;
					this.co_BC			= this.co_Cntlr.Create_BDCController();
					this.co_Cfg			= this.co_BC.Create_XMLConfig( true );
				}

			[TestMethod]
			//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
			public void UT_100_BDCXmlCfg_10_Instantiate()
				{
					Assert.IsNotNull( this.co_Cntlr	, "" );
					Assert.IsNotNull( this.co_BC		, "" );
					Assert.IsNotNull( this.co_Cfg		, "" );
				}

			[TestMethod]
			//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
			public void UT_100_BDCXmlCfg_20_ToXML()
				{
					try
						{
							string x = this.co_BC.SerializeXMLConfig( this.co_Cfg );
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
					IXMLConfig lo_R0	= null;
					//...
					try
						{
							string x	= this.co_BC.SerializeXMLConfig( this.co_Cfg );
							lo_R0			= this.co_BC.DeserializeXMLConfig( x );
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

		}

	//.

	}
