using System;
using System.Collections.Generic;
//.........................................................
using SMC	= SAP.Middleware.Connector;
//.........................................................
using BxS_WorxNCO.BDCSession.DTO;
//•••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••
namespace BxS_WorxNCO.RfcFunction.BDCTran
{
	internal class BDCCall_Data
		{
			#region "Constructors"

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				internal BDCCall_Data(		SMC.IRfcTable	bdcData
																,	SMC.IRfcTable	spaData
																,	SMC.IRfcTable	msgData

																, BDCCall_IndexSPA	spaIndex
																,	BDCCall_IndexBDC	bdcIndex
																, BDCCall_IndexMSG	msgIndex )
					{
						this.BDCData	= bdcData		??	throw		new	ArgumentException( $"{typeof(BDCCall_Data).Namespace}:- BDCData null" );
						this.SPAData	= spaData		??	throw		new	ArgumentException( $"{typeof(BDCCall_Data).Namespace}:- SPAData null" );
						this.MSGData	= msgData		??	throw		new	ArgumentException( $"{typeof(BDCCall_Data).Namespace}:- MSGData null" );
						//.............................................
						this.IndexSPA	= spaIndex	??	throw		new	ArgumentException( $"{typeof(BDCCall_Data).Namespace}:- SPA index null" );
						this.IndexBDC	= bdcIndex	??	throw		new	ArgumentException( $"{typeof(BDCCall_Data).Namespace}:- BDC index null" );
						this.IndexMSG	= msgIndex	??	throw		new	ArgumentException( $"{typeof(BDCCall_Data).Namespace}:- BDC index null" );
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
				internal	SMC.IRfcTable			BDCData		{ get; }
				internal	SMC.IRfcTable			SPAData		{ get; }
				internal	SMC.IRfcTable			MSGData		{ get; }
				//.................................................
				internal	BDCCall_IndexSPA	IndexSPA	{ get; }
				internal	BDCCall_IndexBDC	IndexBDC	{ get; }
				internal	BDCCall_IndexMSG	IndexMSG	{ get; }

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
								this.SPAData.SetValue( this.IndexSPA.MID , SPASrce[i].MemoryID		);
								this.SPAData.SetValue( this.IndexSPA.Val , SPASrce[i].MemoryValue	);
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
								this.BDCData.SetValue( this.IndexBDC.Prg , BDCSrce[i].ProgramName	);
								this.BDCData.SetValue( this.IndexBDC.Dyn , BDCSrce[i].Dynpro				);
								this.BDCData.SetValue( this.IndexBDC.Bgn , BDCSrce[i].Begin				);
								this.BDCData.SetValue( this.IndexBDC.Fld , BDCSrce[i].FieldName		);
								this.BDCData.SetValue( this.IndexBDC.Val , BDCSrce[i].FieldValue		);
							}
					}

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				internal void LoadMsg( IList< DTO_BDC_Msg > MsgTrgt )
					{
						MsgTrgt.Clear();
						//.............................................
						for ( int i = 0; i < this.MSGData.Count; i++ )
							{
								this.MSGData.CurrentIndex	= i;

								MsgTrgt.Add( new DTO_BDC_Msg
															{
																TCode	= this.MSGData.GetString( this.IndexMSG.TCode )	,
																DynNm	= this.MSGData.GetString( this.IndexMSG.DynNm )	,
																DynNo	= this.MSGData.GetString( this.IndexMSG.DynNo )	,
																MsgTp	= this.MSGData.GetString( this.IndexMSG.MsgTp )	,
																MsgLg	= this.MSGData.GetString( this.IndexMSG.Lang	 )	,
																MsgID	= this.MSGData.GetString( this.IndexMSG.MsgID )	,
																MsgNr	= this.MSGData.GetString( this.IndexMSG.MsgNo )	,
																MsgV1	= this.MSGData.GetString( this.IndexMSG.MsgV1 )	,
																MsgV2	= this.MSGData.GetString( this.IndexMSG.MsgV2 )	,
																MsgV3	= this.MSGData.GetString( this.IndexMSG.MsgV3 )	,
																MsgV4	= this.MSGData.GetString( this.IndexMSG.MsgV4 )	,
																Envir	= this.MSGData.GetString( this.IndexMSG.Envir )	,
																FldNm	= this.MSGData.GetString( this.IndexMSG.Fldnm )
															}
														);
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
