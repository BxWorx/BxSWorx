using					BxS_SAPBDC.Helpers;
using static	BxS_SAPBDC.Parser.BDC_Constants;
//•••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••
namespace BxS_SAPBDC.Parser
{
	internal class Parser_BDCConfig
		{
			#region "Constructors"

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				internal	Parser_BDCConfig(		BDCMain					BDCMain
																		,	ObjSerializer		serializer )
					{
						this._BDCMain				= BDCMain			;
						this._Serializer		= serializer	;
					}

			#endregion

			//===========================================================================================
			#region "Declarations"

				private readonly	BDCMain					_BDCMain		;
				private readonly	ObjSerializer		_Serializer	;

			#endregion

			//===========================================================================================
			#region "Methods: Exposed"

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				internal bool Parse()
					{
						if ( this._BDCMain.Tokens.TryGetValue( cz_Token_XCfg , out DTO_TokenReference token ) )
							{
								this._BDCMain.XMLConfig
									= this._Serializer
										.DeSerialize<DTO_BDCXMLConfig>(	token.Value.Replace( cz_Cmd_Prefix , ""	) );
								return	true;
							}
						//.............................................
						return	false;
					}

			#endregion

		}
}
