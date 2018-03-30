using SMC	= SAP.Middleware.Connector;
//.........................................................
using BxS_WorxNCO.RfcFunction.Main;
using System;
//•••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••
namespace BxS_WorxNCO.RfcFunction.BDCTran
{
	//[ SAP( Name = "CTU_PARAMS" ) ]

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

				internal	string	Name { get { return	"CTU_PARAMS"; } }

				internal	SMC.RfcStructureMetadata	Metadata	{ get; set;	}
				//.................................................
				internal	int		DspMde	{ get { return	this._DspMde.Value; } }
				internal	int		UpdMde	{ get { return	this._UpdMde.Value; } }
				internal	int		CATMde	{ get { return	this._CATMde.Value; } }
				internal	int		DefSze	{ get { return	this._DefSze.Value; } }
				internal	int		NoComm	{ get { return	this._NoComm.Value; } }
				internal	int		NoBtcI	{ get { return	this._NoBtcI.Value; } }
				internal	int		NoBtcE	{ get { return	this._NoBtcE.Value; } }

				//[	SAP( Name = "DISMODE"	 ) ]		public	int	DspMde	{ get; set;	}
				//[	SAP( Name = "UPDMODE"	 ) ]		public	int UpdMde	{ get; set;	}
				//[	SAP( Name = "CATTMODE" ) ]		public	int CATMde	{ get; set;	}
				//[	SAP( Name = "DEFSIZE"	 ) ]		public	int DefSze	{ get; set;	}
				//[	SAP( Name = "RACOMMIT" ) ]		public	int NoComm	{ get; set;	}
				//[	SAP( Name = "NOBINPT"	 ) ]		public	int NoBtcI	{ get; set;	}
				//[	SAP( Name = "NOBIEND"	 ) ]		public	int NoBtcE	{ get; set;	}

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
