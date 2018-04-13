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
																	, BDC_Factory				bdcFactory	)	: base(		fncName )
					{
						this._BDCFactory		=	bdcFactory	??	throw		new	ArgumentException( $"{typeof(BDCCall_Profile).Namespace}:- BDC Factory null" );
						//.............................................
						this._FNCIndex	=	new	Lazy<	BDCCall_IndexFNC >( ()=>	this._BDCFactory.CreateBDCIndexFNC() );

						this._CTUIndex	= new	Lazy< BDC_IndexCTU >( ()=>	this._BDCFactory.CreateIndexCTU() );
						this._SPAIndex	=	new Lazy<	BDC_IndexSPA >( ()=>	this._BDCFactory.CreateIndexSPA() );
						this._BDCIndex	= new	Lazy< BDC_IndexBDC >( ()=>	this._BDCFactory.CreateIndexBDC() );
						this._MSGIndex	= new	Lazy< BDC_IndexMSG >( ()=>	this._BDCFactory.CreateIndexMSG() );
					}

			#endregion

			//===========================================================================================
			#region "Declarations"

				private		readonly	BDC_Factory		_BDCFactory;
				//.................................................
				internal	readonly	Lazy<	BDCCall_IndexFNC >	_FNCIndex;
				internal	readonly	Lazy<	BDC_IndexCTU >	_CTUIndex;

				internal	readonly	Lazy<	BDC_IndexSPA >	_SPAIndex;
				internal	readonly	Lazy<	BDC_IndexBDC >	_BDCIndex;
				internal	readonly	Lazy<	BDC_IndexMSG >	_MSGIndex;

			#endregion

			//===========================================================================================
			#region "Methods: Exposed"

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				internal BDC_Header CreateBDCHeader	( bool withDefaults = true )	=>	this._BDCFactory	.CreateBDCHeader	( this._CTUIndex , withDefaults );
				internal BDC_Data		CreateBDCData		()														=>	this._BDCFactory	.CreateBDCData		( this._SPAIndex , this._BDCIndex , this._MSGIndex );

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
