using System;
//.........................................................
using BxS_WorxNCO.RfcFunction.Main;

using	static	BxS_WorxNCO.Main								.NCO_Constants;
using static	BxS_WorxNCO.RfcFunction.BDCTran	.BDC_Constants;
//•••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••
namespace BxS_WorxNCO.RfcFunction.BDCTran
{
	internal class BDC_IndexBDC : RfcStructureIndex
		{
			#region "Constructors"

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				internal BDC_IndexBDC( bool useAltVersion = false )
					{
						this.Name		=	useAltVersion	?	cz_BDCCall : cz_BDCTran	;
						//.............................................
						this._Prg	= new Lazy<int>( ()=> this.Metadata.TryNameToIndex( "PROGRAM"		) );
						this._Dyn	= new Lazy<int>( ()=> this.Metadata.TryNameToIndex( "DYNPRO"		) );
						this._Bgn	= new Lazy<int>( ()=> this.Metadata.TryNameToIndex( "DYNBEGIN"	) );
						this._Fld	= new Lazy<int>( ()=> this.Metadata.TryNameToIndex( "FNAM"			) );
						this._Val	= new Lazy<int>( ()=> this.Metadata.TryNameToIndex( "FVAL"			) );
					}

			#endregion

			//===========================================================================================
			#region "Declarations"

				private	readonly	Lazy<int>		_Prg;
				private	readonly	Lazy<int>		_Dyn;
				private	readonly	Lazy<int>		_Bgn;
				private	readonly	Lazy<int>		_Fld;
				private	readonly	Lazy<int>		_Val;

			#endregion

			//===========================================================================================
			#region "Properties"

				internal	int	Prg		{ get { return	this.IsLoaded	?	this._Prg.Value	:	cz_Neg1	; } }
				internal	int	Dyn		{ get { return	this.IsLoaded	?	this._Dyn.Value	:	cz_Neg1	; } }
				internal	int	Bgn		{ get { return	this.IsLoaded	?	this._Bgn.Value	:	cz_Neg1	; } }
				internal	int	Fld		{ get { return	this.IsLoaded	?	this._Fld.Value	:	cz_Neg1	; } }
				internal	int	Val		{ get { return	this.IsLoaded	?	this._Val.Value	:	cz_Neg1	; } }

			#endregion

		}
}
