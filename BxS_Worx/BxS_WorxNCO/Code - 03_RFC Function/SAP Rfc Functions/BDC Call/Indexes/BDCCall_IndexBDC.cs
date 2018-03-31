using System;
//.........................................................
using SMC	= SAP.Middleware.Connector;
//.........................................................
using	static	BxS_WorxNCO.RfcFunction.BDCTran.BDCCall_Constants;
//•••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••
namespace BxS_WorxNCO.RfcFunction.BDCTran
{
	internal class BDCCall_IndexBDC
		{
			#region "Constructors"

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				internal BDCCall_IndexBDC()
					{
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

				internal	string	Name { get { return	cz_StrBDC; } }

				internal	SMC.RfcStructureMetadata	Metadata	{ get; set;	}
				//.................................................
				internal	int		Prg	{ get { return	this.Metadata == null	?	0	:	this._Prg.Value; } }
				internal	int		Dyn	{ get { return	this.Metadata == null	?	0	:	this._Dyn.Value; } }
				internal	int		Bgn	{ get { return	this.Metadata == null	?	0	:	this._Bgn.Value; } }
				internal	int		Fld	{ get { return	this.Metadata == null	?	0	:	this._Fld.Value; } }
				internal	int		Val	{ get { return	this.Metadata == null	?	0	:	this._Val.Value; } }

			#endregion

			//===========================================================================================
			#region "Methods: Exposed"

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				internal SMC.IRfcStructure	Create()
					{
						return	this.Metadata.CreateStructure();
					}

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				internal SMC.IRfcTable	CreateTable()
					{
						return	this.Metadata.CreateTable();
					}

			#endregion

		}
}
