using System;
//.........................................................
using	static	BxS_WorxNCO.RfcFunction.Main	.SAPRfcFncConstants;
//•••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••
namespace BxS_WorxNCO.RfcFunction.TableReader
{
	internal class TblRdr_IndexFNC
		{
			#region "Constructors"

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				internal TblRdr_IndexFNC( TblRdr_Profile profile )
					{
						this._Profile		= profile;
						//.............................................
						this._QryTable     = new Lazy<int>( ()=> this._Profile.Metadata.TryNameToIndex( "QUERY_TABLE"	) );
						this._Delimeter    = new Lazy<int>( ()=> this._Profile.Metadata.TryNameToIndex( "DELIMITER"		) );
						this._NoData       = new Lazy<int>( ()=> this._Profile.Metadata.TryNameToIndex( "NO_DATA"			) );
						this._SkipRows     = new Lazy<int>( ()=> this._Profile.Metadata.TryNameToIndex( "ROWSKIPS"		) );
						this._RowsCount    = new Lazy<int>( ()=> this._Profile.Metadata.TryNameToIndex( "ROWCOUNT"		) );
						this._Options      = new Lazy<int>( ()=> this._Profile.Metadata.TryNameToIndex( "OPTIONS"			) );
						this._Fields       = new Lazy<int>( ()=> this._Profile.Metadata.TryNameToIndex( "FIELDS"			) );
						this._OutTable     = new Lazy<int>( ()=> this._Profile.Metadata.TryNameToIndex( "OUT_TABLE"		) );
						this._OutTab128    = new Lazy<int>( ()=> this._Profile.Metadata.TryNameToIndex( "TBLOUT128"		) );
						this._OutTab512    = new Lazy<int>( ()=> this._Profile.Metadata.TryNameToIndex( "TBLOUT512"		) );
						this._OutTab2048   = new Lazy<int>( ()=> this._Profile.Metadata.TryNameToIndex( "TBLOUT2048"	) );
						this._OutTab8192   = new Lazy<int>( ()=> this._Profile.Metadata.TryNameToIndex( "TBLOUT8192"	) );
						this._OutTab30000  = new Lazy<int>( ()=> this._Profile.Metadata.TryNameToIndex( "TBLOUT30000"	) );
					}

			#endregion

			//===========================================================================================
			#region "Declarations"

				private	readonly	TblRdr_Profile		_Profile;
				//.................................................
				private	readonly	Lazy<int>		_QryTable    ;
				private	readonly	Lazy<int>		_Delimeter   ;
				private	readonly	Lazy<int>		_NoData      ;
				private	readonly	Lazy<int>		_SkipRows    ;
				private	readonly	Lazy<int>		_RowsCount   ;
				private	readonly	Lazy<int>		_Options     ;
				private	readonly	Lazy<int>		_Fields      ;
				private	readonly	Lazy<int>		_OutTable    ;
				private	readonly	Lazy<int>		_OutTab128   ;
				private	readonly	Lazy<int>		_OutTab512   ;
				private	readonly	Lazy<int>		_OutTab2048  ;
				private	readonly	Lazy<int>		_OutTab8192  ;
				private	readonly	Lazy<int>		_OutTab30000 ;

			#endregion

			//===========================================================================================
			#region "Properties"

				internal	string	Name				{ get { return	cz_TableReader; } }
				//.................................................
				internal	int		QryTable			{ get { return	this._Profile.IsReady ?	this._QryTable   .Value : 0	; } }
				internal	int		Delimeter			{ get { return	this._Profile.IsReady ?	this._Delimeter  .Value : 0	; } }
				internal	int		NoData				{ get { return	this._Profile.IsReady ?	this._NoData     .Value : 0	; } }
				internal	int		SkipRows			{ get { return	this._Profile.IsReady ?	this._SkipRows   .Value : 0	; } }
				internal	int		RowsCount			{ get { return	this._Profile.IsReady ?	this._RowsCount  .Value : 0	; } }
				internal	int		Options				{ get { return	this._Profile.IsReady ?	this._Options    .Value : 0	; } }
				internal	int		Fields				{ get { return	this._Profile.IsReady ?	this._Fields     .Value : 0	; } }
				internal	int		OutTable			{ get { return	this._Profile.IsReady ?	this._OutTable   .Value : 0	; } }
				internal	int		OutTab128			{ get { return	this._Profile.IsReady ?	this._OutTab128  .Value : 0	; } }
				internal	int		OutTab512			{ get { return	this._Profile.IsReady ?	this._OutTab512  .Value : 0	; } }
				internal	int		OutTab2048		{ get { return	this._Profile.IsReady ?	this._OutTab2048 .Value : 0	; } }
				internal	int		OutTab8192		{ get { return	this._Profile.IsReady ?	this._OutTab8192 .Value : 0	; } }
				internal	int		OutTab30000		{ get { return	this._Profile.IsReady ?	this._OutTab30000.Value : 0	; } }

			#endregion

		}
}
