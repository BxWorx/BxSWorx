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
				internal BDCTran_Profile(		string			fncName
																	, BDC_Factory	bdcFactory )	: base( fncName )
					{
						this._BDCFactory	=	bdcFactory	??	throw		new	ArgumentException( $"{typeof(BDCTran_Profile).Namespace}:- BDC Factory null" );
						//.............................................
						this._FNCIndex	=	new	Lazy< BDCTran_IndexFNC >(	()=>	this._BDCFactory.CreateTRNIndexFNC()	);

						this._SPAIndex	=	new Lazy<	BDC_IndexSPA >( ()=>	this._BDCFactory.CreateIndexSPA( true ) );
						this._BDCIndex	= new	Lazy< BDC_IndexBDC >( ()=>	this._BDCFactory.CreateIndexBDC( true ) );
						this._MSGIndex	= new	Lazy< BDC_IndexMSG >( ()=>	this._BDCFactory.CreateIndexMSG( true ) );
					}

			#endregion

			//===========================================================================================
			#region "Declarations"

				private		readonly	BDC_Factory		_BDCFactory;
				//.................................................
				internal	readonly	Lazy<	BDCTran_IndexFNC >	_FNCIndex;
				//.................................................
				internal	readonly	Lazy<	BDC_IndexSPA >	_SPAIndex;
				internal	readonly	Lazy<	BDC_IndexBDC >	_BDCIndex;
				internal	readonly	Lazy<	BDC_IndexMSG >	_MSGIndex;

			#endregion

			//===========================================================================================
			#region "Methods: Exposed"

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				internal BDC_Data			CreateBDCData		()														=>	this._BDCFactory.CreateBDCData		( this._SPAIndex , this._BDCIndex , this._MSGIndex );
				internal BDC_Header		CreateBDCHeader	( bool withDefaults = true )	=>	this._BDCFactory.CreateBDCHeader	( null , withDefaults );

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				public override void ReadyProfile()
					{
						this.LoadFunctionIndex	( this._FNCIndex.Value );

						this.LoadStructureIndex	( this._SPAIndex.Value );
						this.LoadStructureIndex	( this._BDCIndex.Value );
						this.LoadStructureIndex	( this._MSGIndex.Value );
						//.............................................
						base.ReadyProfile();
					}

			#endregion

		}
}
