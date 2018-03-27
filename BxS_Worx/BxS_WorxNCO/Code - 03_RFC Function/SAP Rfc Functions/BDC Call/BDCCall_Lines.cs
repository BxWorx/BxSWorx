using System;
using System.Collections.Generic;
//.........................................................
using SMC	= SAP.Middleware.Connector;
//.........................................................
using BxS_WorxNCO.BDCSession.DTO;
//•••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••
namespace BxS_WorxNCO.RfcFunction.BDCTran
{
	internal class BDCCall_Lines
		{
			#region "Constructors"

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				internal BDCCall_Lines(		SMC.IRfcTable	bdcData
																,	SMC.IRfcTable	spaData
																,	SMC.IRfcTable	msgData
																, BDCCall_IndexSPA	spaIndex
																,	BDCCall_IndexBDC	bdcIndex	)
					{
						this.BDCData	= bdcData		??	throw		new	ArgumentException( $"{typeof(BDCCall_Lines).Namespace}:- BDCData null" );
						this.SPAData	= spaData		??	throw		new	ArgumentException( $"{typeof(BDCCall_Lines).Namespace}:- SPAData null" );
						this.MSGData	= msgData		??	throw		new	ArgumentException( $"{typeof(BDCCall_Lines).Namespace}:- MSGData null" );
						//.............................................
						this.IndexSPA	= spaIndex	??	throw		new	ArgumentException( $"{typeof(BDCCall_Lines).Namespace}:- SPA index null" );
						this.IndexBDC	= bdcIndex	??	throw		new	ArgumentException( $"{typeof(BDCCall_Lines).Namespace}:- BDC index null" );
						//.............................................
						this.ProcessedStatus	= false	;
						this.SuccesStatus			= false	;
					}

			#endregion

			//===========================================================================================
			#region "Properties"

				internal  int   Reference					{ get; set; }
				internal	bool	ProcessedStatus		{ get; set;	}
				internal	bool	SuccesStatus			{ get; set;	}
				//.................................................
				internal	SMC.IRfcTable		BDCData		{ get; }
				internal	SMC.IRfcTable		SPAData		{ get; }
				internal	SMC.IRfcTable		MSGData		{ get; }
				//.................................................
				internal	BDCCall_IndexSPA	IndexSPA	{ get; }
				internal	BDCCall_IndexBDC	IndexBDC	{ get; }

			#endregion

			//===========================================================================================
			#region "Methods: Exposed"

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				internal void LoadSPA( IList< DTO_BDC_SPA > SPASrce )
					{
						if ( SPASrce.Count.Equals(0) )	return;
						//.............................................
						this.SPAData.Append( SPASrce.Count );
						//.............................................
						for ( int i = 0; i < SPASrce.Count; i++ )
							{
								this.SPAData.CurrentIndex	= i;
								//.........................................
								this.SPAData.SetValue( this.IndexSPA.SPADat_MID , SPASrce[i].MemoryID		);
								this.SPAData.SetValue( this.IndexSPA.SPADat_Val , SPASrce[i].MemoryValue	);
							}
					}

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				internal void LoadBDC( IList< DTO_BDC_Data > BDCSrce )
					{
						if ( BDCSrce.Count.Equals(0) )	return;
						//.............................................
						this.BDCData.Append( BDCSrce.Count );
						//.............................................
						for ( int i = 0; i < BDCSrce.Count; i++ )
							{
								this.BDCData.CurrentIndex	= i;
								//.........................................
								this.BDCData.SetValue( this.IndexBDC.BDCDat_Prg , BDCSrce[i].ProgramName	);
								this.BDCData.SetValue( this.IndexBDC.BDCDat_Dyn , BDCSrce[i].Dynpro				);
								this.BDCData.SetValue( this.IndexBDC.BDCDat_Bgn , BDCSrce[i].Begin				);
								this.BDCData.SetValue( this.IndexBDC.BDCDat_Fld , BDCSrce[i].FieldName		);
								this.BDCData.SetValue( this.IndexBDC.BDCDat_Val , BDCSrce[i].FieldValue		);
							}
					}

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				internal void PostProcess()
					{
						this.BDCData.Clear();
						this.SPAData.Clear();
					}

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				internal void Reset()
					{
						this.ProcessedStatus	= false;
						this.SuccesStatus			= false;
						//.............................................
						this.BDCData.Clear();
						this.SPAData.Clear();
						this.MSGData.Clear();
					}

			#endregion

		}
}
