using System;
//.........................................................
using BxS_WorxNCO.BDCSession.DTO;

using static BxS_WorxNCO.Main.NCO_Constants;
//•••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••
namespace BxS_WorxNCO.BDCSession.Parser
{
	internal class BDC_Parser_Session : BDC_Parser_Base
		{
			#region "Constructors"

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				internal	BDC_Parser_Session(	Lazy< BDC_Parser_Factory > factory ) : base( factory )
					{
					}

			#endregion

			//===========================================================================================
			#region "Methods: Exposed: Columns"

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				internal void	Process(	DTO_ParserProfile	dtoProfile
															, DTO_BDC_Header		sessionCfg )
					{
						sessionCfg.SAPTCode	= dtoProfile.XMLConfig.SAPTCode	;
						sessionCfg.Skip1st	= dtoProfile.XMLConfig.Skip1st.ToUpper().Contains( cz_True );
						//.............................................
						sessionCfg.CTUParms.DefaultSize		= this.GetChar( dtoProfile.XMLConfig.CTU_DefSize );
						sessionCfg.CTUParms.DisplayMode		= this.GetChar( dtoProfile.XMLConfig.CTU_DisMode );
						sessionCfg.CTUParms.UpdateMode		= this.GetChar( dtoProfile.XMLConfig.CTU_UpdMode );
					}

			#endregion

			//===========================================================================================
			#region "Methods: Private"

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				private char GetChar( string s )
					{
						return !String.IsNullOrWhiteSpace(s) ? Convert.ToChar(s.Substring(0, 1).ToUpper()) : ' ';
					}

			#endregion

		}
}
