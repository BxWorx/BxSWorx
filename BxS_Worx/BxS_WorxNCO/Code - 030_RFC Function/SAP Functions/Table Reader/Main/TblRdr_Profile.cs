using System;
//.........................................................
using BxS_WorxNCO.RfcFunction.Main;
//•••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••
namespace BxS_WorxNCO.RfcFunction.TableReader
{
	internal class TblRdr_Profile : RfcFncProfile
		{
			#region "Constructors"

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				internal TblRdr_Profile(		string					fncName
																	, TblRdr_Factory	factory	)	: base( fncName )
					{
						this._Factory		= factory	??	throw		new	ArgumentException( $"{typeof(TblRdr_Profile).Namespace}:- Factory null" );
						//.............................................
						this._FNCIndex	=	new	Lazy< TblRdr_IndexFNC >( ()=>	this._Factory.CreateIndexFNC() );
						this._OPTIndex	= new	Lazy< TblRdr_IndexOPT >( ()=>	this._Factory.CreateIndexOPT() );
						this._FLDIndex	= new	Lazy< TblRdr_IndexFLD >( ()=>	this._Factory.CreateIndexFLD() );
					}

			#endregion

			//===========================================================================================
			#region "Properties"

				internal	string	OutTableName	{ get; set ;}

			#endregion

			//===========================================================================================
			#region "Declarations"

				private		readonly	TblRdr_Factory		_Factory;
				//.................................................
				internal	readonly	Lazy<	TblRdr_IndexFNC	>		_FNCIndex;
				internal	readonly	Lazy<	TblRdr_IndexOPT	>		_OPTIndex;
				internal	readonly	Lazy<	TblRdr_IndexFLD	>		_FLDIndex;

			#endregion

			//===========================================================================================
			#region "Methods: Exposed"

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				internal	TblRdr_IndexOUT		CreateOutIndex		( string name )	=>	this._Factory.CreateIndexOUT( name );

				internal	TblRdr_Data				CreateTblRdrData	()	=>	this._Factory.CreateTblRdrData( this._OPTIndex , this._FLDIndex ) ;

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				public override void ReadyProfile()
					{
						this.LoadFunctionIndex	( this._FNCIndex.Value );

						this.LoadStructureIndex	( this._OPTIndex.Value );
						this.LoadStructureIndex	( this._FLDIndex.Value );
						//.............................................
						base.ReadyProfile();
					}

			#endregion

		}
}
