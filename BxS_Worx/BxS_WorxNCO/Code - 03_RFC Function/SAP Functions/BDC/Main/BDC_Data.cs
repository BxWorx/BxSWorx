using System;
using System.Collections.Generic;
//.........................................................
using SMC	= SAP.Middleware.Connector;
//.........................................................
using BxS_WorxNCO.BDCSession.DTO;

using	static	BxS_WorxNCO.Main	.NCO_Constants;
//•••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••
namespace BxS_WorxNCO.RfcFunction.BDCTran
{
	internal class BDC_Data
		{
			#region "Constructors"

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				internal BDC_Data(	Lazy<	BDC_IndexSPA >	spaIndex
													,	Lazy<	BDC_IndexBDC >	bdcIndex
													, Lazy<	BDC_IndexMSG >	msgIndex )
					{
						this._IndexSPA	= spaIndex	??	throw		new	ArgumentException( $"{typeof(BDC_Data).Namespace}:- SPA index null" );
						this._IndexBDC	= bdcIndex	??	throw		new	ArgumentException( $"{typeof(BDC_Data).Namespace}:- BDC index null" );
						this._IndexMSG	= msgIndex	??	throw		new	ArgumentException( $"{typeof(BDC_Data).Namespace}:- BDC index null" );
						//.............................................
						this.ProcessedStatus	= false	;
						this.SuccesStatus			= false	;
						//.............................................
						this._SPAData		= new	Lazy< SMC.IRfcTable >( ()=> this._IndexSPA.Value.CreateTable() , cz_LM )	;
						this._BDCData		=	new	Lazy< SMC.IRfcTable >( ()=> this._IndexBDC.Value.CreateTable() , cz_LM )	;
						this._MSGData		= new	Lazy< SMC.IRfcTable >( ()=> this._IndexMSG.Value.CreateTable() , cz_LM )	;
				}

			#endregion

			//===========================================================================================
			#region "Declarations"

				private	readonly	Lazy< SMC.IRfcTable >		_SPAData	;
				private	readonly	Lazy< SMC.IRfcTable >		_BDCData	;
				private	readonly	Lazy< SMC.IRfcTable >		_MSGData	;
				//.................................................
				private readonly	Lazy< BDC_IndexSPA >	_IndexSPA	;
				private readonly	Lazy< BDC_IndexBDC >	_IndexBDC	;
				private readonly	Lazy< BDC_IndexMSG >	_IndexMSG	;

			#endregion

			//===========================================================================================
			#region "Properties"

				internal  int   Reference					{ get; set; }
				internal	bool	ProcessedStatus		{ get; set;	}
				internal	bool	SuccesStatus			{ get; set;	}
				//.................................................
				internal	SMC.IRfcTable	SPAData		{ get	{	return	this._SPAData.Value; } }
				internal	SMC.IRfcTable	BDCData		{ get	{	return	this._BDCData.Value; } }
				internal	SMC.IRfcTable	MSGData		{ get	{	return	this._MSGData.Value; } }

			#endregion

			//===========================================================================================
			#region "Methods: Exposed"

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				internal void LoadSPA( IList< DTO_BDC_SPA > SPASrce )
					{
						if ( SPASrce.Count.Equals(0) )	return;
						//.............................................
						this._SPAData.Value.Append( SPASrce.Count );
						//.............................................
						for ( int i = 0; i < SPASrce.Count; i++ )
							{
								this._SPAData.Value.CurrentIndex	= i;
								//.........................................
								this._SPAData.Value.SetValue( this._IndexSPA.Value.MID , SPASrce[i].MemoryID		);
								this._SPAData.Value.SetValue( this._IndexSPA.Value.Val , SPASrce[i].MemoryValue	);
							}
					}

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				internal void LoadBDC( IList< DTO_BDC_Data > BDCSrce )
					{
						if ( BDCSrce.Count.Equals(0) )	return;
						//.............................................
						this._BDCData.Value.Append( BDCSrce.Count );
						//.............................................
						for ( int i = 0; i < BDCSrce.Count; i++ )
							{
								this._BDCData.Value.CurrentIndex	= i;
								//.........................................
								this._BDCData.Value.SetValue( this._IndexBDC.Value.Prg , BDCSrce[i].ProgramName	);
								this._BDCData.Value.SetValue( this._IndexBDC.Value.Dyn , BDCSrce[i].Dynpro			);
								this._BDCData.Value.SetValue( this._IndexBDC.Value.Bgn , BDCSrce[i].Begin				);
								this._BDCData.Value.SetValue( this._IndexBDC.Value.Fld , BDCSrce[i].FieldName		);
								this._BDCData.Value.SetValue( this._IndexBDC.Value.Val , BDCSrce[i].FieldValue	);
							}
					}

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				internal void LoadMsg( IList< DTO_BDC_Msg > MsgTrgt )
					{
						MsgTrgt.Clear();
						//.............................................
						for ( int i = 0; i < this._MSGData.Value.Count; i++ )
							{
								this._MSGData.Value.CurrentIndex	= i;

								MsgTrgt.Add( new DTO_BDC_Msg
															{
																	TCode	= this._MSGData.Value.GetString( this._IndexMSG.Value.TCode )
																,	DynNm	= this._MSGData.Value.GetString( this._IndexMSG.Value.DynNm )
																,	DynNo	= this._MSGData.Value.GetString( this._IndexMSG.Value.DynNo )
																,	MsgTp	= this._MSGData.Value.GetString( this._IndexMSG.Value.MsgTp )
																,	MsgLg	= this._MSGData.Value.GetString( this._IndexMSG.Value.Lang	)
																,	MsgID	= this._MSGData.Value.GetString( this._IndexMSG.Value.MsgID )
																,	MsgNr	= this._MSGData.Value.GetString( this._IndexMSG.Value.MsgNo )
																,	MsgV1	= this._MSGData.Value.GetString( this._IndexMSG.Value.MsgV1 )
																,	MsgV2	= this._MSGData.Value.GetString( this._IndexMSG.Value.MsgV2 )
																,	MsgV3	= this._MSGData.Value.GetString( this._IndexMSG.Value.MsgV3 )
																,	MsgV4	= this._MSGData.Value.GetString( this._IndexMSG.Value.MsgV4 )
																,	Envir	= this._MSGData.Value.GetString( this._IndexMSG.Value.Envir )
																,	FldNm	= this._MSGData.Value.GetString( this._IndexMSG.Value.Fldnm )
															}
														);
							}
					}

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				internal void PostProcess()
					{
						this._SPAData.Value.Clear();
						this._BDCData.Value.Clear();
					}

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				internal void Reset()
					{
						this.ProcessedStatus	= false;
						this.SuccesStatus			= false;
						//.............................................
						this._SPAData.Value.Clear();
						this._BDCData.Value.Clear();
						this._MSGData.Value.Clear();
					}

			#endregion

		}
}
