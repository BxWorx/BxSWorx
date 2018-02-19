using System;
using System.Collections.Generic;
//•••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••
namespace BxS_SAPBDC.Parser
{
	internal class BDCTokens
		{
			#region "Constructors"

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				internal BDCTokens()
					{
						//. Initialisers ..............................
						this.Tokens		= new Dictionary<string, DTO_TokenReference>();
					}

			#endregion

			//===========================================================================================
			#region "Declarations"
			#endregion

			//===========================================================================================
			#region "Properties"

				internal	int		Count	{ get { return this.Tokens.Count; } }
				//.................................................
				internal	Dictionary<	string , DTO_TokenReference >		Tokens	{ get; }

			#endregion

			//===========================================================================================
			#region "Methods: Exposed"
			#endregion

		}
}
