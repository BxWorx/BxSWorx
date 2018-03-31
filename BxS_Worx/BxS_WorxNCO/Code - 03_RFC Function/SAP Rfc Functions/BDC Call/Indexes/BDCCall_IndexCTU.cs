using System;
//.........................................................
using SMC	= SAP.Middleware.Connector;
//.........................................................
using	static	BxS_WorxNCO.RfcFunction.BDCTran.BDCCall_Constants;
//•••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••
namespace BxS_WorxNCO.RfcFunction.BDCTran
{
	internal class BDCCall_IndexCTU
		{
			#region "Constructors"

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				internal BDCCall_IndexCTU()
					{
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

				internal	string	Name { get { return	cz_StrCTU; } }

				internal	SMC.RfcStructureMetadata	Metadata	{ get; set;	}
				//.................................................
				internal	int		DspMde	{ get { return	this.Metadata == null	?	0	:	this._DspMde.Value; } }
				internal	int		UpdMde	{ get { return	this.Metadata == null	?	0	:	this._UpdMde.Value; } }
				internal	int		CATMde	{ get { return	this.Metadata == null	?	0	:	this._CATMde.Value; } }
				internal	int		DefSze	{ get { return	this.Metadata == null	?	0	:	this._DefSze.Value; } }
				internal	int		NoComm	{ get { return	this.Metadata == null	?	0	:	this._NoComm.Value; } }
				internal	int		NoBtcI	{ get { return	this.Metadata == null	?	0	:	this._NoBtcI.Value; } }
				internal	int		NoBtcE	{ get { return	this.Metadata == null	?	0	:	this._NoBtcE.Value; } }

			#endregion

			//===========================================================================================
			#region "Methods: Exposed"

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				internal SMC.IRfcStructure	Create()
					{
						return	this.Metadata.CreateStructure();
					}

			#endregion

		}
}
