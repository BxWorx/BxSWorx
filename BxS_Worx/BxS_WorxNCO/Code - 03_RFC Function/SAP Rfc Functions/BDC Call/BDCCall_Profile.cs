using System;
//.........................................................
using SMC	= SAP.Middleware.Connector;
//.........................................................
using BxS_WorxNCO.RfcFunction.Main;
//•••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••
namespace BxS_WorxNCO.RfcFunction.BDCTran
{
	internal class BDCCall_Profile : RfcFncProfile
		{
			#region "Constructors"

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				internal BDCCall_Profile(		string						fncName
																	, BDCCall_Factory		factory	)	: base(		fncName )
					{
						this._Factory		= factory	??	throw		new	ArgumentException( $"{typeof(BDCCall_Profile).Namespace}:- Factory null" );
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

				private		readonly	BDCCall_Factory		_Factory;
				//.................................................
				internal	readonly	BDCCall_IndexFNC	FNCIndex;
				internal	readonly	BDCCall_IndexCTU	CTUIndex;
				internal	readonly	BDCCall_IndexSPA	SPAIndex;
				internal	readonly	BDCCall_IndexBDC	BDCIndex;
				internal	readonly	BDCCall_IndexMSG	MSGIndex;

			#endregion

			//===========================================================================================
			#region "Properties"

				internal	SMC.RfcStructureMetadata	CTUStructure	{ get	{	return	this.Metadata[this.FNCIndex.CTUOpt].ValueMetadataAsStructureMetadata			; } }
				internal	SMC.RfcStructureMetadata	SPAStructure	{ get	{	return	this.Metadata[this.FNCIndex.TabSPA].ValueMetadataAsTableMetadata.LineType	; } }
				internal	SMC.RfcStructureMetadata	BDCStructure	{ get	{	return	this.Metadata[this.FNCIndex.TabBDC].ValueMetadataAsTableMetadata.LineType	; } }
				internal	SMC.RfcStructureMetadata	MSGStructure	{ get	{	return	this.Metadata[this.FNCIndex.TabMSG].ValueMetadataAsTableMetadata.LineType	; } }

			#endregion

			//===========================================================================================
			#region "Methods: Exposed"

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				internal BDCCall_Header CreateBDCCallHeader( bool withDefaults = true )
					{
						this.ReadyProfile();

						return	this._Factory.CreateBDCHeader(	this.CTUIndex
																									,	withDefaults	);
					}

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				internal BDCCall_Data CreateBDCCallData()
					{
						this.ReadyProfile();

						return	this._Factory.CreateBDCData(	this.SPAIndex
																								,	this.BDCIndex
																								,	this.MSGIndex	);
					}

			#endregion

		}
}
