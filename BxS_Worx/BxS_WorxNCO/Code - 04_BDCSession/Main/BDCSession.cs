using System;
using System.Threading;
using System.Collections.Generic;
using System.Collections.Concurrent;
using System.Threading.Tasks;
//.........................................................
using BxS_WorxIPX.API.BDC;
using BxS_WorxNCO.RfcFunction.Main;
using BxS_WorxNCO.RfcFunction.BDCTran;
//•••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••
namespace BxS_WorxNCO.BDCSession.API
{
	internal class BDCSession : IBDCSession
		{
			#region "Constructors"

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				internal BDCSession( IRfcFncController fncCntlr )
					{
						this._Cntlr_Fnc	= fncCntlr;
						//.............................................
						this._IsReady		= false;

						this._Queue	= new	BlockingCollection< DTO_BDC_Trans >();
						this._Tasks	= new List< Task<int> >();
					}

			#endregion

			//===========================================================================================
			#region "Declarations"

				private	readonly	Guid	_DestID;
				//.................................................
				private readonly	IRfcFncController				_Cntlr_Fnc;
				private 	DTO_BDC_SessionConfig		_Config;
				//.................................................
				private bool							_IsReady;


				internal	CancellationTokenSource								_CTS;
				internal	BlockingCollection< DTO_BDC_Trans >		_Queue;

				private readonly IList< Task<int>	>	_Tasks;

			#endregion

			//===========================================================================================
			#region "Methods: Exposed: Session Handling"

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				// Configure the BDC session operating environment
				//
				public void ConfigureOperation( DTO_BDC_SessionConfig dto )
					{
						this._Config	= dto;
					}

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				// Process supplied BDC session
				//
				public async Task< int > Process_SessionAsync( DTO_BDC_Session dto )
					{
						this._CTS		=	new CancellationTokenSource();

						for (int i = 0; i < length; i++)
							{

							}

						


						this.LoadConsumers();
						this.LoadQueue( dto );


						this._CTS		=	null;
						return	await Task.Run( ()=> 1 ).ConfigureAwait(false);
					}

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				// Cancel processing of session
				//
				public void CancelProcessing()
					{
						if ( this._CTS != null )
							{
								this._CTS.Cancel();
							}
					}

			#endregion

			//===========================================================================================
			#region "Methods: Private"

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				private void LoadConsumers()
					{
					}

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				private void Consumer()
					{
						BDCCall_Function	lo_Func		= this._Cntlr_Fnc.CreateBDCCallFunction();
						BDCCall_Header		lo_Head		= lo_Func.CreateBDCCallHeader();
						BDCCall_Lines			lo_Line		= lo_Func.CreateBDCCallLines();

						foreach ( DTO_BDC_Trans lo_WorkItem in this._Queue.GetConsumingEnumerable( this._CTS.Token ) )
							{
								lo_Line.Reset();
								//.........................................
								lo_Line.BDCData.Append( lo_WorkItem.BDCData.Count );

								for (int i = 0; i < lo_WorkItem.BDCData.Count; i++)
									{
										lo_Line.BDCData.CurrentIndex	= i;

										lo_Line.BDCData.SetValue( lo_Func.MyProfile.Value.BDCDat_Prg , lo_WorkItem.BDCData[i].ProgramName );
										lo_Line.BDCData.SetValue( lo_Func.MyProfile.Value.BDCDat_Dyn , lo_WorkItem.BDCData[i].Dynpro			);
										lo_Line.BDCData.SetValue( lo_Func.MyProfile.Value.BDCDat_Bgn , lo_WorkItem.BDCData[i].Begin				);
										lo_Line.BDCData.SetValue( lo_Func.MyProfile.Value.BDCDat_Fld , lo_WorkItem.BDCData[i].FieldName		);
										lo_Line.BDCData.SetValue( lo_Func.MyProfile.Value.BDCDat_Val , lo_WorkItem.BDCData[i].FieldValue	);
									}
								//.........................................
								lo_Line.SPAData.Append( lo_WorkItem.SPAData.Count );

								for (int i = 0; i < lo_WorkItem.SPAData.Count; i++)
									{
										lo_Line.SPAData.CurrentIndex	= i;

										lo_Line.SPAData.SetValue( lo_Func.MyProfile.Value.SPADat_MID , lo_WorkItem.SPAData[i].MemoryID		);
										lo_Line.SPAData.SetValue( lo_Func.MyProfile.Value.SPADat_Val , lo_WorkItem.SPAData[i].MemoryValue	);
									}
								//.........................................
								lo_Func.Process( lo_Line );
								//.........................................
							}
					}

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				private void LoadQueue( DTO_BDC_Session dto )
					{
						foreach (KeyValuePair<int, DTO_BDC_Trans> item in dto.Transactions)
							{
								this._Queue.Add( item.Value );
							}
						this._Queue.CompleteAdding();
					}

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				private bool SetupDestination()
					{
						if ( this._IsReady )	return	this._IsReady;
						//.............................................
						//.............................................
						return	this._IsReady;
					}

			#endregion

		}
}
