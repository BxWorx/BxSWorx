using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
//.........................................................
using BxS_SAPNCO.Destination;
using BxS_SAPNCO.Helpers;
using BxS_SAPNCO.Pipeline;
//•••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••
namespace BxS_SAPNCO.BDCProcess
{
	public class BDCSession	: IBDCSession
		{
			#region "Constructors"

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				public BDCSession(	BDCCallTranParser		parser

													,	Func<	CancellationToken
																,	Pipeline< DTO_SessionTran , DTO_ProgressInfo > >	createBDCPipeline

													,	Func< BDCCallTranProfile , BDCCallTranProcessor >				createBDCCallTran

													,	Func<	ConsumerOpEnv<	DTO_SessionTran
																								, DTO_ProgressInfo >
																,	DTO_SessionHeader
																, BDCCallTranProcessor
																, BDCCallTranParser
																, BDCCallTranConsumer<	DTO_SessionTran
																											, DTO_ProgressInfo > >				createBDCCallConsumer	)
					{
						this._Parser	= parser	;
						//.............................................
						this._CreateBDCPipeline				= createBDCPipeline			;
						this._CreateBDCCallTran				= createBDCCallTran			;
						this._CreateBDCCallConsumer		= createBDCCallConsumer	;
					}



					//Lazy<BDCOpFnc>	opFnc	,
					//									BDCOpEnv				opEnv		)
					//{
					//	this._OpFnc	= opFnc	;
					//	this._OpEnv	= opEnv	;
					//	//.............................................
					//	this._Indexer		= 0												;
					//	this._Lock			= new object()						;
					//	//.............................................
					//	this.Transactions	= new	ConcurrentDictionary< int, DTO_SessionTran >();
					//}

			#endregion

			//===========================================================================================
			#region "Declarations"

				private	readonly	Func< BDCCallTranProfile	, BDCCallTranProcessor	>		_CreateBDCCallTran;

				private	readonly	Func<		ConsumerOpEnv<	DTO_SessionTran
																								, DTO_ProgressInfo >
																,	DTO_SessionHeader
																, BDCCallTranProcessor
																, BDCCallTranParser
																, BDCCallTranConsumer<	DTO_SessionTran
																											, DTO_ProgressInfo > >				_CreateBDCCallConsumer;

				private	readonly	Func<		CancellationToken
																,	Pipeline< DTO_SessionTran , DTO_ProgressInfo > >	_CreateBDCPipeline;

				private readonly	BDCCallTranProfile	_CallProfile		;
				private readonly	DTO_SessionHeader		_SessionHeader	;
				private	readonly	BDCCallTranParser		_Parser					;

				private readonly	ConsumerOpEnv< DTO_SessionTran , DTO_ProgressInfo >	_ConOpEnv	;
				private						Pipeline< DTO_SessionTran , DTO_ProgressInfo >			_Pipeline ;

				private	readonly	CancellationToken	_CT;
				private	readonly	int								_ConsumerCount;





				private	int	_Indexer;
				//.................................................
				private readonly	Lazy<BDCOpFnc>	_OpFnc	;
				private readonly	BDCOpEnv				_OpEnv	;

				private DTO_RFCHeader				_RfcHeader			;
				//private DTO_SessionHeader		_SessionHeader	;
				private DTO_SessionOptions	_SessionOptions	;

				private readonly	object	_Lock	;

			#endregion

			//===========================================================================================
			#region "Properties"

				public	bool	IsStarted							{ get { return	this._OpEnv.IsStarted		; } }
				public	int		TransactionCount			{ get { return	this.Transactions.Count	; } }
				public	int		RFCTransactionCount		{ get { return	this._OpEnv.PLOpEnv.Queue.Count	; } }
				//.................................................
				//public  DTO_SessionHeader		SessionHeader		{ get	{ return	this._SessionHeader		?? (this._SessionHeader		= this._OpFnc.Value.SessionHeader()		); } }
				public	DTO_SessionOptions	SessionOptions	{ get { return	this._SessionOptions	?? (this._SessionOptions	= this._OpFnc.Value.SessionOptions()	); } }
				//.................................................
				public	ConcurrentDictionary< int, DTO_SessionTran >	Transactions	{ get; }

			#endregion

			//===========================================================================================
			#region "Methods: Exposed"

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				public async Task<int> ProcessAsync()
					{
						if (this._ConsumerCount.Equals(0))	return	0;
						if (!this._CallProfile.Ready())			return	0;
						//.............................................
						this._Pipeline	= this._CreateBDCPipeline( this._CT );
						this.CreateAddConsumers();

						return	await this._Pipeline.StartAsync().ConfigureAwait(false);








						this._RfcHeader	=	this._OpFnc.Value.RFCHeader()	;
						if (!this._OpEnv.Start())		return 0;
						this.ParseBDCtoRFCHeader();

						for (int i = 0; i < this.SessionOptions.NoOfConsumers; i++)
							{
								BDCCallTranProcessor				lo_TP	= this._OpFnc.Value.TranProcessor	(this._OpEnv.Profile);
								lo_TP.Config(this._RfcHeader);
								//IConsumer<DTO_RFCTran>	lo_CS	= this._OpFnc.Value.Consumer			(this._OpEnv.PLOpEnv,lo_TP);
								//this._OpEnv.PLOpEnv.Consumers.Add(lo_CS);
							}
						//.............................................
						this.ParseBDCtoRFCTran();
						int ln_Cnt	= await	this._OpEnv.Pipeline.StartAsync().ConfigureAwait(false);
						this.ParseRFCtoBDC();
						return	ln_Cnt;
					}

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				public void	ConfigureUser(IDTOConfigSetupDestination config)
					{
						this._OpEnv.DestRFC.LoadConfig(config);
					}

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				public DTO_SessionTran CreateTran(Guid ID = default(Guid))
					{
						return	this._OpFnc.Value.SessionTran(ID);
					}

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				public void	AddTransaction(IList<DTO_SessionTran> transactions)
					{
						foreach (DTO_SessionTran lo_Tran in transactions)
							{
								this.AddTransaction(lo_Tran);
							}
					}

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				public void	AddTransaction(DTO_SessionTran transaction)
					{
						lock (this._Lock)
							{
								Interlocked.Increment(ref this._Indexer);
								if (!this.Transactions.TryAdd(this._Indexer, transaction))	Interlocked.Decrement(ref this._Indexer);
							}
					}

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				public void	Reset()
					{
						this.ClearRFC();
						this.Transactions.Clear();
					}

			#endregion

			//===========================================================================================
			#region "Methods: Private"

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				private void CreateAddConsumers()
					{
						for (int i = 0; i < this._ConsumerCount; i++)
							{
								BDCCallTranProcessor lo_Tran	=	this._CreateBDCCallTran(this._CallProfile);

								if (lo_Tran.Configure())
									{
										BDCCallTranConsumer<DTO_SessionTran, DTO_ProgressInfo> lo_Con
											=	this._CreateBDCCallConsumer(	this._ConOpEnv
																										, this._SessionHeader
																										, lo_Tran
																										, this._Parser				);

										this._Pipeline.AddConsumer(lo_Con);
									}
							}
					}




								//var	lo_Tran	= new BDCCallTranProcessor(this._CallProfile);

								//if (lo_Tran.Configure())
								//	{
								//		var	lo_Con	= new BDCCallTranConsumer<	DTO_SessionTran
								//																					, DTO_ProgressInfo >(		this._ConOpEnv
								//																																, this._SessionHeader
								//																																, lo_Tran
								//																																, this._Parser				);


				////¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				//private IConsumer<DTO_RFCTran>	CreateConsumer()
				//	{
				//		var p = new BDCCallTranProfile()
				//		var z = new BDCCallTranProcessor

				//		var x = new BDCConsumer<DTO_RFCTran,DTO_ProgressInfo>()

				//	}

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				private void ParseRFCtoBDC()
					{
						//this._OpEnv.Parser.ParseRFCtoBDC(  .PutBDCData(BDCTran.BDCData, lo_RFCTran.BDCData);
					}

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				private void ParseBDCtoRFCHeader()
					{
						this._OpEnv.Profile.Configure( this._RfcHeader );
						this._OpEnv.Parser.PutCTUOptions(	this.SessionHeader.CTUParms	,
																							this._RfcHeader		.CTUParms		);
						this._RfcHeader.SAPTCode	= this.SessionHeader.SAPTCode	;
						this._RfcHeader.Skip1st		= this.SessionHeader.Skip1st	;
					}

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				private void ParseBDCtoRFCTran()
					{
						this.ClearRFC();
						//.............................................
						this._OpEnv.Profile.Configure( this._RfcHeader );
						this._OpEnv.Parser.PutCTUOptions(	this.SessionHeader.CTUParms	,
																							this._RfcHeader		.CTUParms		);
						this._RfcHeader.SAPTCode	= this.SessionHeader.SAPTCode	;
						this._RfcHeader.Skip1st		= this.SessionHeader.Skip1st	;
						//.............................................
						ICollection<int> lt_Keys	= this.Transactions.Keys;

						if (this.SessionOptions.Sequential)
							{	AddSequential(lt_Keys); }
						else
							{	AddParallel(lt_Keys);		}

						//this._OpEnv.Pipeline.AddingCompleted();
					}

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				private void AddParallel(ICollection<int> lt_Keys)
					{
						ParallelLoopResult x = Parallel.ForEach
							(	lt_Keys	,
								(ln_Key) =>	{	if (this.Transactions.TryGetValue(ln_Key, out DTO_SessionTran lo_BDCTran))
																{
																	this.ParseTran(ln_Key, lo_BDCTran);
																}
														}
							);
					}

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				private void AddSequential(ICollection<int> lt_Keys)
					{
						foreach (int ln_Key in lt_Keys)
							{
								if (this.Transactions.TryGetValue(ln_Key, out DTO_SessionTran lo_BDCTran))
									{
										this.ParseTran(ln_Key, lo_BDCTran);
									}
							}
					}

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				private void ParseTran(int key, DTO_SessionTran BDCTran)
					{
						DTO_RFCTran lo_RFCTran	= this._OpFnc.Value.RFCTran();
						this._OpEnv.Profile.Configure(lo_RFCTran);
						this._OpEnv.Parser.ParseBDCtoRFC(BDCTran, lo_RFCTran);
						lo_RFCTran.Reference	= key;
						//this._OpEnv.Pipeline.Post(lo_RFCTran);
					}

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				private void ClearRFC()
					{
						//while(!this._RfcTran.IsEmpty)
						//	{
						//		this._RfcTran.TryDequeue( out DTO_RFCTran lo );
						//	}
					}

			#endregion

		}
}
