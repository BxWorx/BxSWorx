using System;
//.........................................................
using SMC	= SAP.Middleware.Connector;
//.........................................................
using BxS_WorxNCO.RfcFunction.Main;
//•••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••
namespace BxS_WorxNCO.RfcFunction.BDCTran
{
	internal class BDCTran_Profile : RfcFncProfile
		{
			#region "Constructors"

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				internal BDCTran_Profile(		string						fncName
																	, BDCTran_Factory		factory	)	: base(		fncName )
					{
						this._Factory		= factory	??	throw		new	ArgumentException( $"{typeof(BDCTran_Profile).Namespace}:- Factory null" );
						//.............................................
						this.FNCIndex		= this._Factory.CreateIndexFNC( this );
						this.CTUIndex		= this._Factory.CreateIndexCTU( this );
						this.SPAIndex		= this._Factory.CreateIndexSPA( this );
						this.BDCIndex		= this._Factory.CreateIndexBDC( this );
						this.MSGIndex		= this._Factory.CreateIndexMSG( this );
					}

			#endregion

			//===========================================================================================
			#region "Declarations"

				private		readonly	BDCTran_Factory		_Factory;
				//.................................................
				internal	readonly	BDCTran_IndexFNC	FNCIndex;
				internal	readonly	BDC_IndexSPA			SPAIndex;
				internal	readonly	BDC_IndexBDC			BDCIndex;
				internal	readonly	BDC_IndexMSG			MSGIndex;

			#endregion

			//===========================================================================================
			#region "Properties"

				internal	SMC.RfcStructureMetadata	SPAStructure	{ get	{	return	this.Metadata[this.FNCIndex.TabSPA].ValueMetadataAsTableMetadata.LineType	; } }
				internal	SMC.RfcStructureMetadata	BDCStructure	{ get	{	return	this.Metadata[this.FNCIndex.TabBDC].ValueMetadataAsTableMetadata.LineType	; } }
				internal	SMC.RfcStructureMetadata	MSGStructure	{ get	{	return	this.Metadata[this.FNCIndex.TabMSG].ValueMetadataAsTableMetadata.LineType	; } }

			#endregion

			//===========================================================================================
			#region "Methods: Exposed"

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				internal BDCTran_Header CreateBDCCallHeader	( bool withDefaults = true )	=>	this._Factory.CreateBDCHeader	( this.CTUIndex , withDefaults );
				internal BDCTran_Data		CreateBDCCallData		()														=>	this._Factory.CreateBDCData		( this.SPAIndex , this.BDCIndex , this.MSGIndex );

			#endregion

		}
}
