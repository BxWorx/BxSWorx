using System;
using System.Threading;
using System.Threading.Tasks;
//.........................................................
using Microsoft.VisualStudio.TestTools.UnitTesting;
//.........................................................
using BxS_WorxNCO.BDCSession.Main;
using BxS_WorxIPX.Main;
using BxS_WorxNCO.BDCSession.API;
using BxS_WorxIPX.BDC;
using BxS_WorxIPX.Toolset;
using BxS_WorxUtil.Progress;
using BxS_WorxNCO.BDCSession.DTO;
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
					this.co_RMngr		= new	BDC_Request_Manager( this.co_NCO.GetSAPDestConfigured() );
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
					IRequest	lo_R0	= this.LoadRequest( _Nme , 500 );
					//...
					var																x = new CancellationTokenSource();
					ProgressHandler<DTO_BDC_Progress> p = this.co_RMngr.CreateProgressHandler();
					//...
					Task.Run( ()=> this.co_RMngr.ProcessAsync( lo_R0 , x.Token , p )).Wait();
					
				}

		//.

			//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
			public IRequest LoadRequest( string name , int qty = 100 )
				{
					this.SetFullPath(name);
					IRequest	lo_R0	= this.co_BC.ReceiveRequest_FromFile( this._Full );
					ISession	lo_BS	= null;
					int[] lt_Guids	= new int[lo_R0.Sessions.Keys.Count];
					lo_R0.Sessions.Keys.CopyTo(lt_Guids,0);
					lo_BS	= lo_R0.Sessions[lt_Guids[0]];

					for ( int i = 0; i < qty; i++ )
						{
							ISession lo_XX = this.co_BC.Create_Session();
							lo_XX.CopyPropertiesFrom( lo_BS );
							lo_R0.Add_Session( lo_XX );
						}
					//...
					return	lo_R0;
				}

			//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
			private void SetFullPath( string name )	=>	this._Full	= $@"{this._User}\{_Path}\{name}.xml" ;

		//.

		}
}
