﻿//•••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••
namespace BxS_WorxNCO.BDCSession.Parser
{
	internal class DTO_ParserSession
		{
			#region "Constructors"

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				internal DTO_ParserSession()
					{	}

			#endregion

			//===========================================================================================
			#region "Properties"

				internal	string			UsedAddress	{ get; set;	}
				internal	string[,]		WSData			{ get; set;	}
				//...
				internal	int	RowLB		{ get; set; }
				internal	int	RowUB		{ get; set; }
				internal	int	ColLB		{ get; set; }
				internal	int	ColUB		{ get; set; }

			#endregion
		}
}
