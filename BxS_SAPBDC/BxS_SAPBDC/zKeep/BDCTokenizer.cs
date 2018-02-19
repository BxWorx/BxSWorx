using System;
//•••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••
namespace BxS_SAPBDC.Parser
{
	internal class BDCTokenizer
		{
			#region "Constructors"

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				internal BDCTokenizer( Func<DTO_TokenReference> factory )
					{
						//. DI ........................................
						this._Factory	= factory;
					}

			#endregion

			//===========================================================================================
			#region "Declarations"

				private readonly	Func<DTO_TokenReference>	_Factory;

			#endregion

			//===========================================================================================
			#region "Properties"
			#endregion

			//===========================================================================================
			#region "Methods: Exposed"

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				internal void LoadTokens( BDCMain BDCMain )
					{
						const string t1	= "<@@PROGRAM>"				;
						const string t2	= "<@@DYNNO>"					;
						const string t3	= "<@@DYNSTART>"			;
						const string t4	= "<@@CURSORBEFORE>	"	;
						const string t5	= "<@@SPECIALINST>	"	;
						const string t6	= "<@@OKCODE>"				;

						BDCMain.Tokens.Add( t1, this.CreateToken(	t1, 0, 0 ) );
						BDCMain.Tokens.Add( t2, this.CreateToken(	t2, 2, 1 ) );
						BDCMain.Tokens.Add( t3, this.CreateToken(	t3, 3, 1 ) );
						BDCMain.Tokens.Add( t4, this.CreateToken(	t4, 4, 1 ) );
						BDCMain.Tokens.Add( t5, this.CreateToken(	t5, 5, 1 ) );
						BDCMain.Tokens.Add( t6, this.CreateToken(	t6, 6, 1 ) );
					}

			#endregion

			//===========================================================================================
			#region "Methods: Private"

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				private DTO_TokenReference CreateToken( string token, int row, int col )
					{
						DTO_TokenReference lo_DTO		= this._Factory();

						lo_DTO.Token	= token;
						lo_DTO.Row		= row;
						lo_DTO.Col		= col;
						lo_DTO.Value	=	token;

						return	lo_DTO;
					}

			#endregion

		}
}
