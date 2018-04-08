using System;
//.........................................................
using SMC	= SAP.Middleware.Connector;
//•••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••
namespace BxS_WorxNCO.RfcFunction.BDCTran
{
	internal class BDCCall_IndexBDC : BDCCall_IndexBase
		{
			#region "Constructors"

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				internal BDCCall_IndexBDC( BDCCall_Profile profile ) : base( profile )
					{
						this._Metadata	=	new	Lazy< SMC.RfcStructureMetadata >( ()=> this._Profile.BDCStructure	);
						//.............................................
						this._Prg	= new Lazy<int>( ()=> this._Metadata.Value.TryNameToIndex( "PROGRAM"	) );
						this._Dyn	= new Lazy<int>( ()=> this._Metadata.Value.TryNameToIndex( "DYNPRO"		) );
						this._Bgn	= new Lazy<int>( ()=> this._Metadata.Value.TryNameToIndex( "DYNBEGIN"	) );
						this._Fld	= new Lazy<int>( ()=> this._Metadata.Value.TryNameToIndex( "FNAM"			) );
						this._Val	= new Lazy<int>( ()=> this._Metadata.Value.TryNameToIndex( "FVAL"			) );
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

				internal	int		Prg	{ get { return	this._Profile.IsReady ?	this._Prg.Value	:	0	; } }
				internal	int		Dyn	{ get { return	this._Profile.IsReady ?	this._Dyn.Value	:	0	; } }
				internal	int		Bgn	{ get { return	this._Profile.IsReady ?	this._Bgn.Value	:	0	; } }
				internal	int		Fld	{ get { return	this._Profile.IsReady ?	this._Fld.Value	:	0	; } }
				internal	int		Val	{ get { return	this._Profile.IsReady ?	this._Val.Value	:	0	; } }

			#endregion

		}
}
