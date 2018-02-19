//•••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••
namespace BxS_SAPBDC.Parser
{
	internal class DTO_BDCHeaderRowRef
		{
			#region "Constructors"

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				internal DTO_BDCHeaderRowRef()
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

			#endregion
		}
}
