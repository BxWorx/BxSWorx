using System;
//.........................................................
using BxS_WorxNCO.RfcFunction.Main;

using	static	BxS_WorxNCO.Main								.NCO_Constants;
using static	BxS_WorxNCO.RfcFunction.BDCTran	.BDC_Constants;
//•••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••
namespace BxS_WorxNCO.RfcFunction.BDCTran
{
	internal class BDCCall_IndexCTU : RfcStructureIndex
		{
			#region "Constructors"

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				internal BDCCall_IndexCTU()
					{
						this.Name	=	cz_StrCTU;
						//.............................................
						this._DspMde	= new Lazy<int>( ()=> this.Metadata.TryNameToIndex( "DISMODE"	 ) );
						this._UpdMde	= new Lazy<int>( ()=> this.Metadata.TryNameToIndex( "UPDMODE"	 ) );
						this._CATMde	= new Lazy<int>( ()=> this.Metadata.TryNameToIndex( "CATTMODE" ) );
						this._DefSze	= new Lazy<int>( ()=> this.Metadata.TryNameToIndex( "DEFSIZE"	 ) );
						this._NoComm	= new Lazy<int>( ()=> this.Metadata.TryNameToIndex( "RACOMMIT" ) );
						this._NoBtcI	= new Lazy<int>( ()=> this.Metadata.TryNameToIndex( "NOBINPT"	 ) );
						this._NoBtcE	= new Lazy<int>( ()=> this.Metadata.TryNameToIndex( "NOBIEND"	 ) );
					}

			#endregion

			//===========================================================================================
			#region "Declarations"

				private	readonly	Lazy<int>		_DspMde;
				private	readonly	Lazy<int>		_UpdMde;
				private	readonly	Lazy<int>		_CATMde;
				private	readonly	Lazy<int>		_DefSze;
				private	readonly	Lazy<int>		_NoComm;
				private	readonly	Lazy<int>		_NoBtcI;
				private	readonly	Lazy<int>		_NoBtcE;

			#endregion

			//===========================================================================================
			#region "Properties"

				internal	int		DspMde	{ get { return	this.IsLoaded	?	this._DspMde.Value : cz_Neg	; } }
				internal	int		UpdMde	{ get { return	this.IsLoaded	?	this._UpdMde.Value : cz_Neg	; } }
				internal	int		CATMde	{ get { return	this.IsLoaded	?	this._CATMde.Value : cz_Neg	; } }
				internal	int		DefSze	{ get { return	this.IsLoaded	?	this._DefSze.Value : cz_Neg	; } }
				internal	int		NoComm	{ get { return	this.IsLoaded	?	this._NoComm.Value : cz_Neg	; } }
				internal	int		NoBtcI	{ get { return	this.IsLoaded	?	this._NoBtcI.Value : cz_Neg	; } }
				internal	int		NoBtcE	{ get { return	this.IsLoaded	?	this._NoBtcE.Value : cz_Neg	; } }

			#endregion

		}
}
