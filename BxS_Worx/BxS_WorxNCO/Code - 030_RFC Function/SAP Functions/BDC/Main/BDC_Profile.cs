using System;
//.........................................................
using BxS_WorxNCO.RfcFunction.Main;
//•••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••
namespace BxS_WorxNCO.RfcFunction.BDCTran
{
	internal class BDC_Profile : RfcFncProfile
		{
			#region "Constructors"

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				internal BDC_Profile(		string				fncName
															, BDC_Factory		bdcFactory
															, bool					useTranVersion	= false	)	: base(	fncName )
					{
						this._BDCFactory		=	bdcFactory	??	throw		new	ArgumentException( $"{typeof(BDC_Profile).Namespace}:- BDC Factory null" );
						this.IsTranVersion	= useTranVersion;
						//.............................................
						this._FNCIndex	=	new	Lazy<	BDC_IndexFNC >( ()=>	this._BDCFactory.CreateIndexFNC( this.IsTranVersion ) );
						this._SPAIndex	=	new Lazy<	BDC_IndexSPA >( ()=>	this._BDCFactory.CreateIndexSPA( this.IsTranVersion ) );
						this._BDCIndex	= new	Lazy< BDC_IndexBDC >( ()=>	this._BDCFactory.CreateIndexBDC( this.IsTranVersion ) );
						this._MSGIndex	= new	Lazy< BDC_IndexMSG >( ()=>	this._BDCFactory.CreateIndexMSG( this.IsTranVersion ) );

						this._CTUIndex	= new	Lazy< BDC_IndexCTU >( ()=>	this._BDCFactory.CreateIndexCTU() );
					}

			#endregion

			//===========================================================================================
			#region "Declarations"

				private		readonly	BDC_Factory		_BDCFactory	;
				//.................................................
				internal	readonly	Lazy<	BDC_IndexFNC >	_FNCIndex;
				internal	readonly	Lazy<	BDC_IndexSPA >	_SPAIndex;
				internal	readonly	Lazy<	BDC_IndexBDC >	_BDCIndex;
				internal	readonly	Lazy<	BDC_IndexMSG >	_MSGIndex;

				internal	readonly	Lazy<	BDC_IndexCTU >	_CTUIndex;

			#endregion

			//===========================================================================================
			#region "Properties"

				internal	bool	IsTranVersion	{ get; }

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

						this.LoadStructureIndex	( this._SPAIndex.Value );
						this.LoadStructureIndex	( this._BDCIndex.Value );
						this.LoadStructureIndex	( this._MSGIndex.Value );
						//.............................................
						if ( !this.IsTranVersion )		this.LoadStructureIndex	( this._CTUIndex.Value );
						//.............................................
						base.ReadyProfile();
					}

			#endregion

		}
}
