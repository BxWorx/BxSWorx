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
				internal TblRdr_Data(		TblRdr_IndexOPT		optIndex
															,	TblRdr_IndexFLD		fldIndex
															,	TblRdr_IndexOUT		outIndex	)
					{
						this._IndexOPT	= optIndex	??	throw		new	ArgumentException( $"{typeof(TblRdr_Data).Namespace}:- OPT index null" );
						this._IndexFLD	= fldIndex	??	throw		new	ArgumentException( $"{typeof(TblRdr_Data).Namespace}:- FLD index null" );
						this._IndexOUT	= outIndex	??	throw		new	ArgumentException( $"{typeof(TblRdr_Data).Namespace}:- OUT index null" );
						//.............................................
						this._OPTData		= new	Lazy< SMC.IRfcTable >( ()=> this._IndexOPT.CreateTable() , cz_LM )	;
						this._FLDData		=	new	Lazy< SMC.IRfcTable >( ()=> this._IndexFLD.CreateTable() , cz_LM )	;
						this._OUTData		=	new	Lazy< SMC.IRfcTable >( ()=> this._IndexOUT.CreateTable() , cz_LM )	;
				}

			#endregion

			//===========================================================================================
			#region "Declarations"

				private readonly	TblRdr_IndexOPT		_IndexOPT	;
				private readonly	TblRdr_IndexFLD		_IndexFLD	;
				private readonly	TblRdr_IndexOUT		_IndexOUT	;
				//.................................................
				private	readonly	Lazy< SMC.IRfcTable >		_OPTData	;
				private	readonly	Lazy< SMC.IRfcTable >		_FLDData	;
				private	readonly	Lazy< SMC.IRfcTable >		_OUTData	;

			#endregion

			//===========================================================================================
			#region "Properties"

				public	string	QueryTable		{ get; set; }
				public	char		Delimeter			{ get; set; }
				public	bool		NoData				{ get; set; }
				public	int			SkipRows			{ get; set; }
				public	int			ReturnRows		{ get; set; }
				//.................................................
				public	SMC.IRfcTable		Options		{ get	{	return	this._OPTData.Value; } }
				public	SMC.IRfcTable		Fields		{ get	{	return	this._FLDData.Value; } }
				public	SMC.IRfcTable		OutData		{ get	{	return	this._OUTData.Value; } }

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
						SMC.IRfcStructure	ls_Str	=	this._IndexFLD.CreateStructure();
						ls_Str.SetValue( this._IndexFLD.FldNme	, fieldName );
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
						SMC.IRfcStructure	ls_Str	=	this._IndexOPT.CreateStructure();
						ls_Str.SetValue( this._IndexOPT.Text , option );
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
						this._OUTData.Value.Clear();
					}

			#endregion

		}
}
