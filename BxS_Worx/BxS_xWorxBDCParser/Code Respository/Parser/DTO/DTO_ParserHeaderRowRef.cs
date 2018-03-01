//•••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••
namespace BxS_SAPBDC.Parser
{
	internal class DTO_ParserHeaderRowRef
		{
			#region "Constructors"

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				internal DTO_ParserHeaderRowRef()
					{
						this.Ready	= false;
					}

			#endregion

			//===========================================================================================
			#region "Properties"

				public bool Ready { get; set; }

				public int Prog	{ get; set; }
				public int Scrn	{ get; set; }
				public int Strt	{ get; set; }
				public int OKCd	{ get; set; }
				public int Curs	{ get; set; }
				public int Subs	{ get; set; }
				public int FldN	{ get; set; }
				public int Desc	{ get; set; }
				public int Inst	{ get; set; }

			#endregion
		}
}
