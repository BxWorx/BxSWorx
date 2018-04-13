using System;
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
																	, BDC_Factory				bdcFactory
																	, BDCTran_Factory		trnFactory	)	: base(		fncName )
					{
						this._BDCFactory	=	bdcFactory	??	throw		new	ArgumentException( $"{typeof(BDCTran_Profile).Namespace}:- BDC Factory null" );
						this._TRNFactory	= trnFactory	??	throw		new	ArgumentException( $"{typeof(BDCTran_Profile).Namespace}:- Factory null" );
						//.............................................
						this._FNCIndex	=	new	Lazy<BDCTran_IndexFNC>(	()=>	this._TRNFactory.CreateIndexFNC()	);

						this._SPAIndex	=	new Lazy<	BDC_IndexSPA >( ()=>	this._BDCFactory.CreateIndexSPA() );
						this._BDCIndex	= new	Lazy< BDC_IndexBDC >( ()=>	this._BDCFactory.CreateIndexBDC() );
						this._MSGIndex	= new	Lazy< BDC_IndexMSG >( ()=>	this._BDCFactory.CreateIndexMSG() );
					}

			#endregion

			//===========================================================================================
			#region "Declarations"

				private		readonly	BDC_Factory				_BDCFactory;
				private		readonly	BDCTran_Factory		_TRNFactory;
				//.................................................
				internal	readonly	Lazy<	BDCTran_IndexFNC >	_FNCIndex;

				internal	readonly	Lazy<	BDC_IndexSPA >	_SPAIndex;
				internal	readonly	Lazy<	BDC_IndexBDC >	_BDCIndex;
				internal	readonly	Lazy<	BDC_IndexMSG >	_MSGIndex;

			#endregion

			//===========================================================================================
			#region "Properties"

				//internal	SMC.RfcStructureMetadata	SPAStructure	{ get	{	return	this.Metadata[this._FNCIndex.TabSPA].ValueMetadataAsTableMetadata.LineType	; } }
				//internal	SMC.RfcStructureMetadata	BDCStructure	{ get	{	return	this.Metadata[this._FNCIndex.TabBDC].ValueMetadataAsTableMetadata.LineType	; } }
				//internal	SMC.RfcStructureMetadata	MSGStructure	{ get	{	return	this.Metadata[this._FNCIndex.TabMSG].ValueMetadataAsTableMetadata.LineType	; } }

			#endregion

			//===========================================================================================
			#region "Methods: Exposed"

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				internal BDC_Data				CreateBDCCallData		()														=>	this._BDCFactory	.CreateBDCData		( this._SPAIndex , this._BDCIndex , this._MSGIndex );
				//internal BDCTran_Header CreateBDCCallHeader	( bool withDefaults = true )	=>	this._TRNFactory.CreateBDCHeader	( this.CTUIndex , withDefaults );

			#endregion

		}
}
