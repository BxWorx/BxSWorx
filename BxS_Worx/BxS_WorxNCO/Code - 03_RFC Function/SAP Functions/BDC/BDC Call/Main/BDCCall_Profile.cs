using System;
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
																	, BDC_Factory				bdcFactory
																	, BDCCall_Factory		callFactory	)	: base(		fncName )
					{
						this._BDCFactory		=	bdcFactory	??	throw		new	ArgumentException( $"{typeof(BDCCall_Profile).Namespace}:- BDC Factory null" );
						this._CallFactory		= callFactory	??	throw		new	ArgumentException( $"{typeof(BDCCall_Profile).Namespace}:- CALL Factory null" );
						//.............................................
						this._FNCIndex	=	new	Lazy<	BDCCall_IndexFNC >( ()=>	this._CallFactory.CreateIndexFNC() );
						this._CTUIndex	= new	Lazy< BDCCall_IndexCTU >( ()=>	this._CallFactory.CreateIndexCTU() );

						this._SPAIndex	=	new Lazy<	BDC_IndexSPA >( ()=>	this._BDCFactory.CreateIndexSPA() );
						this._BDCIndex	= new	Lazy< BDC_IndexBDC >( ()=>	this._BDCFactory.CreateIndexBDC() );
						this._MSGIndex	= new	Lazy< BDC_IndexMSG >( ()=>	this._BDCFactory.CreateIndexMSG() );
					}

			#endregion

			//===========================================================================================
			#region "Declarations"

				private		readonly	BDC_Factory				_BDCFactory;
				private		readonly	BDCCall_Factory		_CallFactory;
				//.................................................
				internal	readonly	Lazy<	BDCCall_IndexFNC >	_FNCIndex;
				internal	readonly	Lazy<	BDCCall_IndexCTU >	_CTUIndex;

				internal	readonly	Lazy<	BDC_IndexSPA >	_SPAIndex;
				internal	readonly	Lazy<	BDC_IndexBDC >	_BDCIndex;
				internal	readonly	Lazy<	BDC_IndexMSG >	_MSGIndex;

			#endregion

			//===========================================================================================
			#region "Methods: Exposed"

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				internal BDCCall_Header CreateBDCCallHeader	( bool withDefaults = true )	=>	this._CallFactory	.CreateBDCHeader	( this._CTUIndex , withDefaults );
				internal BDC_Data				CreateBDCCallData		()														=>	this._BDCFactory	.CreateBDCData		( this._SPAIndex , this._BDCIndex , this._MSGIndex );

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				public override void ReadyProfile()
					{
						this.LoadFunctionIndex	( this._FNCIndex.Value );

						this.LoadStructureIndex	( this._CTUIndex.Value );
						this.LoadStructureIndex	( this._SPAIndex.Value );
						this.LoadStructureIndex	( this._BDCIndex.Value );
						this.LoadStructureIndex	( this._MSGIndex.Value );
						//.............................................
						base.ReadyProfile();
					}

			#endregion

		}
}
