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
				internal BDCCall_Lines( BDCCall_Profile profile )
					{
						this.ProcessedStatus	= false	;
						this.SuccesStatus			= false	;
						//.............................................
						this.SPADat_MID		= profile.SPADat_MID	;
						this.SPADat_Val		= profile.SPADat_Val	;
						this.BDCDat_Prg		= profile.BDCDat_Prg	;
						this.BDCDat_Dyn		= profile.BDCDat_Dyn	;
						this.BDCDat_Bgn		= profile.BDCDat_Bgn	;
						this.BDCDat_Fld		= profile.BDCDat_Fld	;
						this.BDCDat_Val		= profile.BDCDat_Val	;
					}

			#endregion

			//===========================================================================================
			#region "Declarations"

				private	readonly	int		SPADat_MID	;
				private	readonly	int		SPADat_Val	;
				private	readonly	int		BDCDat_Prg	;
				private	readonly	int		BDCDat_Dyn	;
				private	readonly	int		BDCDat_Bgn	;
				private	readonly	int		BDCDat_Fld	;
				private	readonly	int		BDCDat_Val	;

			#endregion

			//===========================================================================================
			#region "Properties"

				internal  int   Reference					{ get; set; }
				internal	bool	ProcessedStatus		{ get; set;	}
				internal	bool	SuccesStatus			{ get; set;	}
				//.................................................
				internal	SMC.IRfcTable		BDCData	{ get; set; }
				internal	SMC.IRfcTable		SPAData	{ get; set; }
				internal	SMC.IRfcTable		MSGData	{ get; set; }

			#endregion

			//===========================================================================================
			#region "Methods: Exposed"

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				internal void LoadSPA( IList< DTO_BDC_SPA > SPASrce )
					{
						this.SPAData.Append( SPASrce.Count );
						//.............................................
						for ( int i = 0; i < SPASrce.Count; i++ )
							{
								this.SPAData.CurrentIndex	= i;
								//.........................................
								this.SPAData.SetValue( this.SPADat_MID , SPASrce[i].MemoryID		);
								this.SPAData.SetValue( this.SPADat_Val , SPASrce[i].MemoryValue	);
							}
					}

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				internal void LoadBDC( IList< DTO_BDC_Data > BDCSrce )
					{
						this.BDCData.Append( BDCSrce.Count );
						//.............................................
						for ( int i = 0; i < BDCSrce.Count; i++ )
							{
								this.BDCData.CurrentIndex	= i;
								//.........................................
								this.BDCData.SetValue( this.BDCDat_Prg , BDCSrce[i].ProgramName	);
								this.BDCData.SetValue( this.BDCDat_Dyn , BDCSrce[i].Dynpro			);
								this.BDCData.SetValue( this.BDCDat_Bgn , BDCSrce[i].Begin				);
								this.BDCData.SetValue( this.BDCDat_Fld , BDCSrce[i].FieldName		);
								this.BDCData.SetValue( this.BDCDat_Val , BDCSrce[i].FieldValue	);
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
