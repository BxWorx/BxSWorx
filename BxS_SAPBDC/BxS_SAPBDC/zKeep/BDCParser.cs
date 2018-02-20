using System;
//•••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••
namespace BxS_SAPBDC.Parser
{
	internal partial class BDCParser
		{
			#region "Constructors"

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				internal BDCParser(		Func<DTO_TokenReference>	createToken
														, Func<DTO_BDCColumn>				createColumn )
					{
						this._CreateToken			= createToken;
						this._CreateColumn		= createColumn;
					}

			#endregion

			//===========================================================================================
			#region "Declarations"

				private readonly	Func<DTO_TokenReference>	_CreateToken;
				private readonly	Func<DTO_BDCColumn>				_CreateColumn;

			#endregion

			//===========================================================================================
			#region "Properties"
			#endregion

			//===========================================================================================
			#region "Methods: Exposed"
			#endregion

			//===========================================================================================
			#region "Methods: Private"
			#endregion

		}
}
