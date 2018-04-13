using System;
//.........................................................
using SMC	= SAP.Middleware.Connector;
//.........................................................
using BxS_WorxNCO.BDCSession.DTO;

using static	BxS_WorxNCO.RfcFunction.BDCTran	.BDC_Constants;
using	static	BxS_WorxNCO.Main								.NCO_Constants;
//•••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••
namespace BxS_WorxNCO.RfcFunction.BDCTran
{
	internal class BDC_Header
		{
			#region "Constructors"

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				internal BDC_Header(	Lazy<	BDC_IndexCTU >	ctuIndex
														,	bool									withDefaults = true	)
					{
						this._IndexCTU	= ctuIndex;
						//.............................................
						this._CTU				= new Lazy< SMC.IRfcStructure >( ()=> this._IndexCTU.Value.CreateStructure() );
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
				private readonly	Lazy< BDC_IndexCTU	>				_IndexCTU	;

			#endregion

			//===========================================================================================
			#region "Properties"

				internal	string	SAPTCode	{ get;	set; }
				internal	bool		Skip1st		{ get;	set; }
				//.................................................
				internal	SMC.IRfcStructure		CTUParms	{ get { return	this._IndexCTU == null ? null :	this._CTU.Value; } }
				//.................................................
				internal	string	DispMode	{ get; set; }
				internal	string	UpdtMode	{ get; set; }

			#endregion

			//===========================================================================================
			#region "Methods: Exposed"

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				internal void Load( DTO_BDC_Header dtoHead )
					{
						this.SAPTCode		= dtoHead.SAPTCode;
						this.Skip1st		= dtoHead.Skip1st	;
						//.............................................
						if ( this._IndexCTU == null )
							{
								this.DispMode	= dtoHead.CTUParms.DisplayMode.ToString();
								this.UpdtMode	= dtoHead.CTUParms.UpdateMode	.ToString();
							}
						else
							{
								this.CTUParms[ this._IndexCTU.Value.DspMde ].SetValue( dtoHead.CTUParms.DisplayMode		);
								this.CTUParms[ this._IndexCTU.Value.UpdMde ].SetValue( dtoHead.CTUParms.UpdateMode		);
								this.CTUParms[ this._IndexCTU.Value.CATMde ].SetValue( dtoHead.CTUParms.CATTMode			);
								this.CTUParms[ this._IndexCTU.Value.DefSze ].SetValue( dtoHead.CTUParms.DefaultSize		);
								this.CTUParms[ this._IndexCTU.Value.NoComm ].SetValue( dtoHead.CTUParms.NoCommit			);
								this.CTUParms[ this._IndexCTU.Value.NoBtcI ].SetValue( dtoHead.CTUParms.NoBatchInpFor );
								this.CTUParms[ this._IndexCTU.Value.NoBtcE ].SetValue( dtoHead.CTUParms.NoBatchInpAft );
							}
					}

			#endregion

			//===========================================================================================
			#region "Methods: Private"

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				private void SetupDefaults()
					{
						this.Skip1st	= false;
						//.............................................
						if ( this._IndexCTU == null )
							{
								this.DispMode	= cz_CTU_A.ToString();
								this.UpdtMode	= cz_CTU_A.ToString();
							}
						else
							{
								this.CTUParms[ this._IndexCTU.Value.DspMde ].SetValue( cz_CTU_A );
								this.CTUParms[ this._IndexCTU.Value.UpdMde ].SetValue( cz_CTU_A );
								this.CTUParms[ this._IndexCTU.Value.DefSze ].SetValue( cz_False );
								this.CTUParms[ this._IndexCTU.Value.CATMde ].SetValue( cz_False );
							}
					}

			#endregion

		}
}
