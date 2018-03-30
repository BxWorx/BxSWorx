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
						//this.CTUParms		= ctuParms	??	throw		new	ArgumentException( $"{typeof(BDCCall_Header).Namespace}:- CTUParms null" );
						this.IndexCTU		= ctuIndex	??	throw		new	ArgumentException( $"{typeof(BDCCall_Header).Namespace}:- CTUIndex null" );
						//.............................................
						if ( withDefaults )		this.SetupDefaults();

						this._CTU		= new Lazy<SMC.IRfcStructure>( ()=> this.IndexCTU.Metadata.CreateStructure() );
					}

			#endregion

			//===========================================================================================
			#region "Properties"

				private		BDCCall_IndexCTU	IndexCTU	{ get; }
				//.................................................
				internal	string	SAPTCode		{ get;	set; }
				internal	bool		Skip1st			{ get;	set; }
				//.................................................
				internal	SMC.IRfcStructure		CTUParms	{ get { return	this._CTU.Value; } }

				internal Lazy< SMC.IRfcStructure >	_CTU;

			#endregion

			//===========================================================================================
			#region "Methods: Exposed"

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				internal void Load( DTO_BDC_Header dtoHead )
					{
						this.SAPTCode		= dtoHead.SAPTCode;
						this.Skip1st		= dtoHead.Skip1st	;
						//.............................................
						this.CTUParms[ this.IndexCTU.DspMde ].SetValue( dtoHead.CTUParms.DisplayMode		);
						this.CTUParms[ this.IndexCTU.UpdMde ].SetValue( dtoHead.CTUParms.UpdateMode		);
						this.CTUParms[ this.IndexCTU.CATMde ].SetValue( dtoHead.CTUParms.CATTMode			);
						this.CTUParms[ this.IndexCTU.DefSze ].SetValue( dtoHead.CTUParms.DefaultSize		);
						this.CTUParms[ this.IndexCTU.NoComm ].SetValue( dtoHead.CTUParms.NoCommit			);
						this.CTUParms[ this.IndexCTU.NoBtcI ].SetValue( dtoHead.CTUParms.NoBatchInpFor	);
						this.CTUParms[ this.IndexCTU.NoBtcE ].SetValue( dtoHead.CTUParms.NoBatchInpAft	);
					}

			#endregion

			//===========================================================================================
			#region "Methods: Private"

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				private void SetupDefaults()
					{
						this.Skip1st		= false;
						//.............................................
						this.CTUParms[ this.IndexCTU.DspMde ].SetValue( cz_CTU_A );
						this.CTUParms[ this.IndexCTU.UpdMde ].SetValue( cz_CTU_A	);
						this.CTUParms[ this.IndexCTU.DefSze ].SetValue( cz_False	);
						this.CTUParms[ this.IndexCTU.CATMde ].SetValue( cz_False	);
					}

			#endregion

		}
}
