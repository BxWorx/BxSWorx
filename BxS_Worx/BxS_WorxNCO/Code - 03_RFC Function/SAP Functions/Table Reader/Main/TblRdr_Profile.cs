using System;
//.........................................................
using SMC	= SAP.Middleware.Connector;
//.........................................................
using BxS_WorxNCO.RfcFunction.Main;
//•••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••
namespace BxS_WorxNCO.RfcFunction.TableReader
{
	internal class TblRdr_Profile : RfcFncProfile
		{
			#region "Constructors"

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				internal TblRdr_Profile(		string						fncName
																	, TblRdr_Factory		factory	)	: base( fncName )
					{
						this._Factory		= factory	??	throw		new	ArgumentException( $"{typeof(TblRdr_Profile).Namespace}:- Factory null" );
						//.............................................
						this.FNCIndex		= this._Factory.CreateIndexFNC( this );
						this.OPTIndex		= this._Factory.CreateIndexOPT( this );
						this.FLDIndex		= this._Factory.CreateIndexFLD( this );
						this.TBLIndex		= this._Factory.CreateIndexTBL( this );
					}

			#endregion

			//===========================================================================================
			#region "Declarations"

				private		readonly	TblRdr_Factory		_Factory;
				//.................................................
				internal	readonly	TblRdr_IndexFNC		FNCIndex;
				internal	readonly	TblRdr_IndexOPT		OPTIndex;
				internal	readonly	TblRdr_IndexFLD		FLDIndex;
				internal	readonly	TblRdr_IndexTBL		TBLIndex;

			#endregion

			//===========================================================================================
			#region "Properties"

				internal	SMC.RfcStructureMetadata	OptionsStructure		{ get	{	return	this.Metadata[this.FNCIndex.Options]		.ValueMetadataAsTableMetadata.LineType	; } }
				internal	SMC.RfcStructureMetadata	FieldsStructure			{ get	{	return	this.Metadata[this.FNCIndex.Fields]			.ValueMetadataAsTableMetadata.LineType	; } }
				internal	SMC.RfcStructureMetadata	OutTableStructure		{ get	{	return	this.Metadata[this.FNCIndex.OutTab128]	.ValueMetadataAsTableMetadata.LineType	; } }

			#endregion

			//===========================================================================================
			#region "Methods: Exposed"

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				//internal BDCCall_Header CreateBDCCallHeader	( bool withDefaults = true )	=>	this._Factory.CreateBDCHeader	( this.CTUIndex , withDefaults );
				//internal BDCCall_Data		CreateBDCCallData		()														=>	this._Factory.CreateBDCData		( this.SPAIndex , this.BDCIndex , this.MSGIndex );

			#endregion

		}
}
