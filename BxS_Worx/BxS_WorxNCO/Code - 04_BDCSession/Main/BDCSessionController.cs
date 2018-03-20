using System;
using System.Threading;
using System.Collections.Generic;
//.........................................................
using BxS_WorxNCO.BDCSession.API;
using BxS_WorxNCO.BDCSession.DTO;
using BxS_WorxNCO.Destination.API;
using BxS_WorxNCO.Destination.Main;
using BxS_WorxNCO.RfcFunction.Main;
using BxS_WorxNCO.Destination.Config;
//•••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••
namespace BxS_WorxNCO.BDCSession.Main
{
	internal class BDCSessionController : IBDCSessionController
		{
			#region "Constructors"

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				internal BDCSessionController()
					{
						this._LazyMode		= LazyThreadSafetyMode.ExecutionAndPublication;
						//.............................................
						this._Cntlr_Dst		= new Lazy<IDestinationController>(	()=>		new DestinationController()
																																				, this._LazyMode							);
					}

			#endregion

			//===========================================================================================
			#region "Declarations"

				private readonly	LazyThreadSafetyMode						_LazyMode;
				private readonly	Lazy< IDestinationController >	_Cntlr_Dst;

			#endregion

			//===========================================================================================
			#region "Methods: Exposed: Destination Handling"

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				// List of SAP systems as per SAP INI/XML
				//
				public IList< string > GetSAPINIList()
					{
						return	this._Cntlr_Dst.Value.GetSAPINIList();
					}

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				// List of registered SAP systems
				//
				public IList< ISAPSystemReference > GetSAPSystems()
					{
						return	this._Cntlr_Dst.Value.GetSAPSystems();
					}

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				// Create BDC session config DTO to configure SAP Logon environment
				//
				public IConfigSetupDestination	CreateDestinationConfig()
					{
						IConfigSetupDestination lo_Config	= new ConfigSetupDestination {	DoLogonCheck	= true
																																						,	PeakConnLimit	= 10		};
						return	lo_Config;
					}

			#endregion

			//===========================================================================================
			#region "Methods: Exposed: Session Handling"

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				// Create BDC session config DTO to configure operating environment
				//
				public DTO_BDC_SessionConfig CreateSessionConfig()
					{
						var	lo_Config	= new DTO_BDC_SessionConfig {
																													IsSequential			= true
																												, ConsumersMax			= 1
																												,	ConsumersNo				= 1
																												,	PauseTime					= 0
																												, ConsumerThreshold	= 0
																												, QueueAddTimeout		= 0
																												, ProgressInterval	= 10
																																										};
						return	lo_Config;
					}

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				public DTO_BDC_Session CreateSessionDTO()
					{
						var lo_CTU	= new DTO_BDC_CTU();
						var lo_Hed	= new DTO_BDC_Header( lo_CTU );
						//.............................................
						return	new DTO_BDC_Session( lo_Hed );
					}

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				public	IBDCSession	CreateBDCSession( string destinationID )
					{
						IRfcDestination	lo_D	= this._Cntlr_Dst.Value.GetDestination( destinationID );
						return	this.CreateBDCSession( lo_D );
					}

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				public	IBDCSession	CreateBDCSession( Guid destinationID )
					{
						IRfcDestination	lo_D	= this._Cntlr_Dst.Value.GetDestination( destinationID );
						return	this.CreateBDCSession( lo_D );
					}

			#endregion

			//===========================================================================================
			#region "Methods: Private"

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				private	IBDCSession	CreateBDCSession( IRfcDestination rfcDestination )
					{
						IRfcFncController	lo_F	= new RfcFncController( rfcDestination );
						//.............................................
						return	new BDCSession( lo_F , this.CreateSessionConfig() );
					}

			#endregion

		}
}
