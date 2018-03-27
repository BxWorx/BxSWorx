using System;
//.........................................................
using SMC	= SAP.Middleware.Connector;
//.........................................................
using BxS_WorxNCO.BDCSession.DTO;

using static	BxS_WorxNCO.RfcFunction.BDCTran	.BDCCall_Constants;
using	static	BxS_WorxNCO.Main								.NCO_Constants;
//•••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••
namespace BxS_WorxNCO.RfcFunction.BDCTran
{
	internal class BDCCall_Header
		{
			#region "Constructors"

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				internal BDCCall_Header(	SMC.IRfcStructure ctuParms
																, BDCCall_IndexCTU	ctuIndex
																,	bool							withDefaults = true	)
					{
						this.CTUParms		= ctuParms	??	throw		new	ArgumentException( $"{typeof(BDCCall_Header).Namespace}:- CTUParms null" );
						this.IndexCTU		= ctuIndex	??	throw		new	ArgumentException( $"{typeof(BDCCall_Header).Namespace}:- CTUIndex null" );
						//.............................................
						if ( withDefaults )		this.SetupDefaults();
					}

			#endregion

			//===========================================================================================
			#region "Properties"

				internal	bool		ShowSAPGui	{ get;	set; }
				//.................................................
				internal	string	SAPTCode		{ get;	set; }
				internal	bool		Skip1st			{ get;	set; }
				//.................................................
				internal	SMC.IRfcStructure		CTUParms	{ get; }

				private		BDCCall_IndexCTU		IndexCTU	{ get; }

			#endregion

			//===========================================================================================
			#region "Methods: Exposed"

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				internal void Load( DTO_BDC_Header dtoHead )
					{
						this.SAPTCode		= dtoHead.SAPTCode;
						this.Skip1st		= dtoHead.Skip1st	;
						//.............................................
						this.CTUParms[ this.IndexCTU.CTUOpt_DspMde ].SetValue( dtoHead.CTUParms.DisplayMode		);
						this.CTUParms[ this.IndexCTU.CTUOpt_UpdMde ].SetValue( dtoHead.CTUParms.UpdateMode		);
						this.CTUParms[ this.IndexCTU.CTUOpt_CATMde ].SetValue( dtoHead.CTUParms.CATTMode			);
						this.CTUParms[ this.IndexCTU.CTUOpt_DefSze ].SetValue( dtoHead.CTUParms.DefaultSize		);
						this.CTUParms[ this.IndexCTU.CTUOpt_NoComm ].SetValue( dtoHead.CTUParms.NoCommit			);
						this.CTUParms[ this.IndexCTU.CTUOpt_NoBtcI ].SetValue( dtoHead.CTUParms.NoBatchInpFor	);
						this.CTUParms[ this.IndexCTU.CTUOpt_NoBtcE ].SetValue( dtoHead.CTUParms.NoBatchInpAft	);
					}

			#endregion

			//===========================================================================================
			#region "Methods: Private"

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				private void SetupDefaults()
					{
						this.ShowSAPGui	= false;
						this.Skip1st		= false;
						//.............................................
						this.CTUParms[ this.IndexCTU.CTUOpt_DspMde ].SetValue( cz_CTU_A );
						this.CTUParms[ this.IndexCTU.CTUOpt_UpdMde ].SetValue( cz_CTU_A	);
						this.CTUParms[ this.IndexCTU.CTUOpt_DefSze ].SetValue( cz_False	);
						this.CTUParms[ this.IndexCTU.CTUOpt_CATMde ].SetValue( cz_False	);
					}

			#endregion

		}
}
