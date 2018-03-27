using System;
using System.Threading;
//.........................................................
using BxS_WorxNCO.BDCSession.DTO			;
using BxS_WorxNCO.BDCSession.Parser		;
using BxS_WorxNCO.Destination.API			;
using BxS_WorxNCO.Helpers.ObjectPool	;
using BxS_WorxNCO.RfcFunction.BDCTran	;
//•••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••
namespace BxS_WorxNCO.BDCSession.Main
{
	internal sealed class BDCSession_Factory
		{
			#region "Constructors"

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				internal static BDCSession_Factory Instance
					{
						get { return _Instance.Value; }
					}

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				private BDCSession_Factory()
					{	}

			#endregion

			//===========================================================================================
			#region "Declarations"

				private const LazyThreadSafetyMode	_LM		= LazyThreadSafetyMode.ExecutionAndPublication;
				//.................................................
				private	static readonly	Lazy< BDCSession_Factory >	_Instance
						= new Lazy< BDCSession_Factory >(	()=>	new BDCSession_Factory() , _LM );

			#endregion

			//===========================================================================================
			#region "Methods: Exposed: Session"

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				public DTO_BDC_Session CreateSessionDTO()
					{
						var lo_CTU	= new DTO_BDC_CTU();
						var lo_Hed	= new DTO_BDC_Header( lo_CTU );
						//.............................................
						return	new DTO_BDC_Session( lo_Hed );
					}

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				internal ObjectPool< BDC_Session >		CreateSessionPool(	CancellationToken	CT
																																,	int		minPoolSize = 1
																																,	int		maxPoolSize	= 5
																																, bool	limiterOn						= false
																																, bool	activateDiagnostics	= false
																																, bool	autoStartMin				= false	)
					{
						return	new ObjectPool< BDC_Session >(	minPoolSize
																									, maxPoolSize
																									, limiterOn
																									, activateDiagnostics
																									, autoStartMin
																									, CT
																									, ()=> this.CreateSession()	);
					}

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				internal ObjectPool< BDCSessionConsumer >		CreateSessionConsumerPool( IRfcDestination	rfcDestination)
					{
						IRfcDestination X = rfcDestination;
						return	new ObjectPool< BDCSessionConsumer >( factory:  ()=> this.CreateSessionConsumer()	);
					}

			#endregion

			//===========================================================================================
			#region "Methods: Exposed: Parser"

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				internal ObjectPool< BDC_Parser >		CreateParserPool(		CancellationToken	CT
																															,	int		minPoolSize = 1
																															,	int		maxPoolSize	= 5
																															, bool	limiterOn						= false
																															, bool	activateDiagnostics	= false
																															, bool	autoStartMin				= false	)
					{
						return	new ObjectPool< BDC_Parser >(		minPoolSize
																									, maxPoolSize
																									, limiterOn
																									, activateDiagnostics
																									, autoStartMin
																									, CT
																									, ()=> this.CreateParser() );
					}

			#endregion

			//===========================================================================================
			#region "Methods: Exposed"

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				private BDCSessionConsumer	CreateSessionConsumer()
					{
						//BDCCall_Function	lo_Func			= this._FncCntlr.CreateBDCCallFunction();
						//BDCCall_Lines			lo_BDCData	=	this._Profile.CreateBDCCallLines();
						//lo_Func.Config( this._Header );
						//var X = new BDCSessionConsumer( this._Profile , lo_Func, lo_BDCData );
			
						BDCSessionConsumer		lo_S = null;
						return	lo_S;
					}

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				private BDC_Session	CreateSession()
					{
						BDCCall_Factory.Instance.cr


						BDC_Session		lo_S = null;
						return	lo_S;
					}

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				private BDC_Parser	CreateParser()
					{
						BDC_Parser	lo_P = null;
						return	lo_P;
					}

			#endregion

		}
}