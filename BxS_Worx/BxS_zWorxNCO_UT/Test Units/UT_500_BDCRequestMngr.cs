using System;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Linq;
//.........................................................
using Microsoft.VisualStudio.TestTools.UnitTesting;
//.........................................................
using BxS_WorxNCO.BDCSession.Main;
using BxS_WorxIPX.Main;
using BxS_WorxNCO.BDCSession.API;
using BxS_WorxIPX.BDC;
using BxS_WorxIPX.Toolset;
//•••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••
namespace BxS_zWorx_UT_Destination.Test_Units
{
	[TestClass]
	public class UT_500_BDCRequestMngr
		{
			private	const	string	_Nme	=  "Test-00"									;
			private	const	string	_Path	=  @"GitHub\BxSWorx\BxS_Worx\BxS_zWorxIPX_UT\Test Resources";

			private	readonly	string	_User	;
			private						string	_Full	;

			private readonly	UT_000_NCO						co_NCO;
			private	readonly	IBDC_Request_Manager	co_RMngr;
			private	readonly	IPX_Controller				co_Cntlr	;
			private	readonly	IBDC_Controller				co_BC			;
			private	readonly	BDC_Session_Factory		co_BSFact	;

			//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
			public UT_500_BDCRequestMngr()
				{
					this.co_NCO			= new	UT_000_NCO();
					this.co_RMngr	= new	BDC_Request_Manager( this.co_NCO.GetSAPDestConfigured() );
					//...............................................
					this.co_Cntlr		= IPX_Controller.Instance	;
					this.co_BC			= this.co_Cntlr.Create_BDCController();
					//...............................................
					this._User	= Environment.GetFolderPath(Environment.SpecialFolder.UserProfile);
				}

			[TestMethod]
			//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
			public void UT_500_BCDRMngr_10_Instantiate()
				{
					Assert.IsNotNull	( this.co_RMngr	, "C" );
				}

			[TestMethod]
			//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
			public void UT_500_BCDRMngr_20_Process()
				{
					this.SetFullPath(_Nme);
					IRequest	lo_R0	= this.co_BC.ReceiveRequest_FromFile( this._Full );
					ISession	lo_BS	= null;
					var				lt_Guids	= new int[lo_R0.Sessions.Keys.Count];
					lo_R0.Sessions.Keys.CopyTo(lt_Guids,0);
					lo_BS	= lo_R0.Sessions[lt_Guids[0]];

					for ( int i = 0; i < 100; i++ )
						{
							ISession lo_XX = this.co_BC.Create_Session();
							lo_XX.CopyPropertiesFrom( lo_BS );
							lo_XX.ID				= Guid.NewGuid();
							
							lo_R0.Add_Session( lo_XX );
						}

					//...
				}
		//.

		//.

			//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
			private void SetFullPath( string name )	=>	this._Full	= $@"{this._User}\{_Path}\{name}.xml" ;

		//.

		}
}
