using System;
using System.Collections.Generic;
//.........................................................
using SMC	= SAP.Middleware.Connector;

using	static	BxS_WorxNCO.Main	.NCO_Constants;
//•••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••
namespace BxS_WorxNCO.RfcFunction.TableReader
{
	internal class TblRdr_Data
		{
			#region "Constructors"

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				internal TblRdr_Data(		Lazy< TblRdr_IndexOPT >	optIndex
															,	Lazy< TblRdr_IndexFLD >	fldIndex	)
					{
						this._IndexOPT	= optIndex	??	throw		new	ArgumentException( $"{typeof(TblRdr_Data).Namespace}:- OPT index null" );
						this._IndexFLD	= fldIndex	??	throw		new	ArgumentException( $"{typeof(TblRdr_Data).Namespace}:- FLD index null" );
						//.............................................
						this._OPTData		= new	Lazy< SMC.IRfcTable >( ()=> this._IndexOPT.Value.CreateTable() , cz_LM )	;
						this._FLDData		=	new	Lazy< SMC.IRfcTable >( ()=> this._IndexFLD.Value.CreateTable() , cz_LM )	;
				}

			#endregion

			//===========================================================================================
			#region "Declarations"

				private readonly	Lazy< TblRdr_IndexOPT >		_IndexOPT	;
				private readonly	Lazy< TblRdr_IndexFLD >		_IndexFLD	;
				//.................................................
				private	readonly	Lazy< SMC.IRfcTable >		_OPTData	;
				private	readonly	Lazy< SMC.IRfcTable >		_FLDData	;

			#endregion

			//===========================================================================================
			#region "Properties"

				public	string	QueryTable		{ get; set; }
				public	char		Delimeter			{ get; set; }
				public	bool		NoData				{ get; set; }
				public	int			SkipRows			{ get; set; }
				public	int			ReturnRows		{ get; set; }
				//.................................................
				public	SMC.IRfcTable		OutData		{ get	; set; }
				//.................................................
				public	SMC.IRfcTable		Options		{ get	{	return	this._OPTData.Value; } }
				public	SMC.IRfcTable		Fields		{ get	{	return	this._FLDData.Value; } }

			#endregion

			//===========================================================================================
			#region "Methods: Exposed"

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				internal void LoadField( IList<string> fields )
					{
						foreach ( string lc_Fld in fields )
							{
								this.LoadField( lc_Fld );
							}
					}

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				internal void LoadField( string	fieldName )
					{
						SMC.IRfcStructure	ls_Str	=	this._IndexFLD.Value.CreateStructure();
						ls_Str.SetValue( this._IndexFLD.Value.FldNme	, fieldName );
						this.Fields.Append( ls_Str );
					}

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				internal void LoadOption( IList<string> options )
					{
						foreach ( string lc_Fld in options )
							{
								this.LoadOption( lc_Fld );
							}
					}

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				internal void LoadOption( string option )
					{
						SMC.IRfcStructure	ls_Str	=	this._IndexOPT.Value.CreateStructure();
						ls_Str.SetValue( this._IndexOPT.Value.Text , option );
						this.Options.Append( ls_Str );
					}

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				internal void PostProcess()
					{
						this._OPTData.Value.Clear();
						this._FLDData.Value.Clear();
					}

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				internal void Reset()
					{
						this._OPTData.Value.Clear();
						this._FLDData.Value.Clear();
					}

			#endregion

		}
}
