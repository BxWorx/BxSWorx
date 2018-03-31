using System;
//.........................................................
using SMC	= SAP.Middleware.Connector;
//•••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••
namespace BxS_WorxNCO.RfcFunction.BDCTran
{
	internal class BDCCall_IndexCTU : BDCCall_IndexBase
		{
			#region "Constructors"

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				internal BDCCall_IndexCTU( BDCCall_Profile profile ) : base( profile )
					{
						this._Metadata	=	new	Lazy< SMC.RfcStructureMetadata >( ()=> this._Profile.CTUStructure	);
						//.............................................
						this._DspMde	= new Lazy<int>( ()=> this._Metadata.Value.TryNameToIndex( "DISMODE"	) );
						this._UpdMde	= new Lazy<int>( ()=> this._Metadata.Value.TryNameToIndex( "UPDMODE"	) );
						this._CATMde	= new Lazy<int>( ()=> this._Metadata.Value.TryNameToIndex( "CATTMODE" ) );
						this._DefSze	= new Lazy<int>( ()=> this._Metadata.Value.TryNameToIndex( "DEFSIZE"	) );
						this._NoComm	= new Lazy<int>( ()=> this._Metadata.Value.TryNameToIndex( "RACOMMIT" ) );
						this._NoBtcI	= new Lazy<int>( ()=> this._Metadata.Value.TryNameToIndex( "NOBINPT"	) );
						this._NoBtcE	= new Lazy<int>( ()=> this._Metadata.Value.TryNameToIndex( "NOBIEND"	) );
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

				internal	int		DspMde	{ get { return	this._Profile.IsReady	?	this._DspMde.Value : 0 ; } }
				internal	int		UpdMde	{ get { return	this._Profile.IsReady	?	this._UpdMde.Value : 0 ; } }
				internal	int		CATMde	{ get { return	this._Profile.IsReady	?	this._CATMde.Value : 0 ; } }
				internal	int		DefSze	{ get { return	this._Profile.IsReady	?	this._DefSze.Value : 0 ; } }
				internal	int		NoComm	{ get { return	this._Profile.IsReady	?	this._NoComm.Value : 0 ; } }
				internal	int		NoBtcI	{ get { return	this._Profile.IsReady	?	this._NoBtcI.Value : 0 ; } }
				internal	int		NoBtcE	{ get { return	this._Profile.IsReady	?	this._NoBtcE.Value : 0 ; } }

			#endregion

		}
}
