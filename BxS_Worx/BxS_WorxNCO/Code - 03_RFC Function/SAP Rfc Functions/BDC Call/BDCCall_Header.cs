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
				internal BDCCall_Header(	BDCCall_IndexCTU	ctuIndex
																,	bool							withDefaults = true	)
					{
						this._IndexCTU		= ctuIndex	??	throw		new	ArgumentException( $"{typeof(BDCCall_Header).Namespace}:- CTUIndex null" );
						//.............................................
						this._CTU				= new Lazy< SMC.IRfcStructure >( ()=> this._IndexCTU.Create() );
						//.............................................
						if ( withDefaults )
							{
								this.SetupDefaults();
							}
					}

			#endregion

			//===========================================================================================
			#region "Declarations"

				private	readonly	Lazy< SMC.IRfcStructure >		_CTU			;
				private readonly	BDCCall_IndexCTU						_IndexCTU	;

			#endregion

			//===========================================================================================
			#region "Properties"

				internal	string							SAPTCode	{ get;	set; }
				internal	bool								Skip1st		{ get;	set; }
				//.................................................
				internal	SMC.IRfcStructure		CTUParms	{ get { return	this._CTU.Value; } }

			#endregion

			//===========================================================================================
			#region "Methods: Exposed"

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				internal void Load( DTO_BDC_Header dtoHead )
					{
						this.SAPTCode		= dtoHead.SAPTCode;
						this.Skip1st		= dtoHead.Skip1st	;
						//.............................................
						this.CTUParms[ this._IndexCTU.DspMde ].SetValue( dtoHead.CTUParms.DisplayMode		);
						this.CTUParms[ this._IndexCTU.UpdMde ].SetValue( dtoHead.CTUParms.UpdateMode		);
						this.CTUParms[ this._IndexCTU.CATMde ].SetValue( dtoHead.CTUParms.CATTMode			);
						this.CTUParms[ this._IndexCTU.DefSze ].SetValue( dtoHead.CTUParms.DefaultSize		);
						this.CTUParms[ this._IndexCTU.NoComm ].SetValue( dtoHead.CTUParms.NoCommit			);
						this.CTUParms[ this._IndexCTU.NoBtcI ].SetValue( dtoHead.CTUParms.NoBatchInpFor );
						this.CTUParms[ this._IndexCTU.NoBtcE ].SetValue( dtoHead.CTUParms.NoBatchInpAft );
					}

			#endregion

			//===========================================================================================
			#region "Methods: Private"

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				private void SetupDefaults()
					{
						this.Skip1st		= false;
						//.............................................
						this.CTUParms[ this._IndexCTU.DspMde ].SetValue( cz_CTU_A );
						this.CTUParms[ this._IndexCTU.UpdMde ].SetValue( cz_CTU_A );
						this.CTUParms[ this._IndexCTU.DefSze ].SetValue( cz_False );
						this.CTUParms[ this._IndexCTU.CATMde ].SetValue( cz_False );
					}

			#endregion

		}
}
