using System;
//.........................................................
using BxS_WorxNCO.API;
using	UTL = BxS_WorxUtil.General;

using static	BxS_WorxNCO.Main.NCO_Constants;
//•••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••
namespace BxS_WorxNCO.BDCSession.Parser
{
	internal sealed class BDC_Parser_Factory
		{
			#region "Constructors: Singleton"

				private	static readonly	Lazy< BDC_Parser_Factory >	_Instance		= new Lazy< BDC_Parser_Factory >(	()=>	new BDC_Parser_Factory() , cz_LM );

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				internal static BDC_Parser_Factory Instance		{	get { return _Instance.Value	; }	}

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				private BDC_Parser_Factory()
					{
						this._NCO_Cntlr		=	new Lazy< INCO_Controller					>	(	()=>	NCO_Controller.Instance										, cz_LM );
						//.............................................
						this._Tkns		=	new Lazy< BDC_Parser_Tokens				>	(	()=>	new BDC_Parser_Tokens				( _Instance ) , cz_LM );
						this._Cols		=	new Lazy< BDC_Parser_Columns			>	(	()=>	new BDC_Parser_Columns			(	_Instance ) , cz_LM );
						this._Grps		=	new Lazy< BDC_Parser_Groups				>	(	()=>	new BDC_Parser_Groups				(	_Instance ) , cz_LM );
						this._Tran		=	new Lazy< BDC_Parser_Transaction	>	(	()=>	new BDC_Parser_Transaction	( _Instance ) , cz_LM );
						this._Sesn		=	new Lazy< BDC_Parser_Session			>	(	()=>	new BDC_Parser_Session			( _Instance ) , cz_LM );
						//this._Proc_Dest		=	new Lazy< BDC_Parser_Destination	>	(	()=>	new BDC_Parser_Destination	( _Instance ) , cz_LM );
						//.............................................
						this._Serializer	= new Lazy< UTL.Serializer					>	(	()=>	this._NCO_Cntlr.Value.UTL_Cntlr.Serializer , cz_LM );
					}

			#endregion

			//===========================================================================================
			#region "Declarations"

				private readonly	Lazy< INCO_Controller					>	_NCO_Cntlr;
				//.................................................
				private readonly	Lazy< BDC_Parser_Tokens				>	_Tkns;
				private readonly	Lazy< BDC_Parser_Columns			>	_Cols;
				private readonly	Lazy< BDC_Parser_Groups				>	_Grps;
				private readonly	Lazy< BDC_Parser_Transaction	>	_Tran;
				private readonly	Lazy< BDC_Parser_Session			>	_Sesn;
				//private readonly	Lazy< BDC_Parser_Destination	>	_Proc_Dest;
				//.................................................
				private	readonly	Lazy<	UTL.Serializer >	_Serializer;

			#endregion

			//===========================================================================================
			#region "Properties"

				internal	UTL.Serializer Serializer	{ get { return	this._Serializer.Value; } }

				internal	BDC_Parser_Tokens				TokenParser				{ get	{	return	this._Tkns.Value	; }	}
				internal	BDC_Parser_Columns			ColumnParser			{ get	{	return	this._Cols.Value	; }	}
				internal	BDC_Parser_Groups				GroupParser				{ get	{	return	this._Grps.Value	; }	}
				internal	BDC_Parser_Transaction	TransactionParser	{ get	{	return	this._Tran.Value	; }	}
				internal	BDC_Parser_Session			SessionParser			{ get	{	return	this._Sesn.Value	; }	}

			#endregion

			//===========================================================================================
			#region "Methods: Exposed"

				//.................................................
				//.................................................
				// Parser objects
				//.................................................
				internal	DTO_ParserSession			CreateDTOParserRequest	()=>	new DTO_ParserSession	();
				internal	DTO_ParserColumn			CreateDTOColumn					()=>	new DTO_ParserColumn	();

				internal	DTO_ParserProfile			CreateDTOProfile				()=>	new DTO_ParserProfile( new DTO_ParserHeaderRowRef() )	;

				internal	DTO_ParserToken				CreateDTOToken					( string ID = "" )=>	new DTO_ParserToken		( ID )					;
				//internal	DTO_ParserXMLConfig		CreateDTOXMLCfg					( bool SetDefaults = false	)=>	new DTO_ParserXMLConfig( SetDefaults )	;

			#endregion

		}
}