using System;

using Microsoft.VisualStudio.TestTools.UnitTesting;

using BxS_WorxIPX.Main;
using BxS_WorxIPX.BDC;

namespace BxS_zWorx_UT_Destination.Test_Units
{
	[TestClass]
	public class UT_100_IPX_BDC
		{
			private	const	string	_Nme	=  "UT_100_IPX-00"									;
			private	const	string	_Path	=  @"GitHub\BxSWorx\BxS_Worx\BxS_zWorxIPX_UT\Test Resources";

			private readonly	string	_User	;
			private						string	_Full	;

			private	readonly	IPX_Controller		co_Cntlr;
			private	readonly	IBDC_Controller		co_BC		;

			//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
			public UT_100_IPX_BDC()
				{
					this.co_Cntlr		= IPX_Controller.Instance	;
					this.co_BC			= this.co_Cntlr.Create_BDCController();
					this._User			= Environment.GetFolderPath(Environment.SpecialFolder.UserProfile);

					this.SetFullPath( _Nme );
				}

			[TestMethod]
			//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
			public void UT_100_IPXBDC_10_Instantiate()
				{
					IUser			u = this.co_BC.Create_User();
					ISession	s = this.co_BC.Create_Session();
					IRequest	r = this.co_BC.Create_Request();

					Assert.IsNotNull( u	, "" );
					Assert.IsNotNull( s	, "" );
					Assert.IsNotNull( r	, "" );
					//...
					Assert.IsNotNull( r.User			, "" );
					Assert.IsNotNull( r.SAPLogon	, "" );
					//...
					Assert.IsNotNull( s.XMLConfig	, "" );
				}

			[TestMethod]
			//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
			public void UT_100_IPXBDC_20_Request()
				{
					IRequest	r = this.co_BC.Create_Request();
					//...
					ISession	s = this.co_BC.Create_Session();

					r.Add_Session( s );
					Assert.AreNotEqual( 0, r.Count , "" );
					r.Clear();
					Assert.AreEqual		( 0, r.Count , "" );
					//...
					IUser u	=	this.co_BC.Create_User();
					u.Name	= "A";
					r.Set_User( u );
					Assert.AreEqual	( u.Name , r.User.Name , "" );
					//...
					ISAP_Logon l	= this.co_Cntlr.Create_SAPLogon();
					l.User	= "A";
					r.Set_Logon( l );
					Assert.AreEqual	( l.User , r.SAPLogon.User , "" );
				}

			[TestMethod]
			//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
			public void UT_100_IPXBDC_30_XMLConfig()
				{
					IXMLConfig	x = this.co_BC.Create_XMLConfig();
					x.SAPTCode		= "A";

					string			s = this.co_BC.SerializeXMLConfig( x );
					IXMLConfig	t = this.co_BC.DeserializeXMLConfig( s );

					Assert.AreEqual	( x.SAPTCode , t.SAPTCode , "" );
				}

			[TestMethod]
			//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
			public void UT_100_IPXBDC_40_RequestIO()
				{
					//IRequest	r = this.co_BC.Create_Request();
					////...
					//ISession	s = this.co_BC.Create_Session();

					//s.WSCells	= (object[,]) Array.CreateInstance( typeof( object ) , 5 , 5 );
					//s.WSCells[1,1] = "A";
					//r.Add_Session( s );
					//this.co_BC.DispatchRequest_ToFile( r , this._Full );

					//IRequest x = this.co_BC.ReceiveRequest_FromFile( this._Full );

					//Assert.AreEqual	( 1 , x.Count , "" );
				}

			//.

			//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
			private void SetFullPath( string name )	=>	this._Full	= $@"{this._User}\{_Path}\{name}.xml" ;

			//.

		}

	//.

	}
