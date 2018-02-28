//•••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••
namespace BxS_SAPBDC.Parser
{
	internal class DTO_ParserToken
		{
			#region "Constructors"

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				internal DTO_ParserToken( string ID = ""	)
					{
						this.ID	= ID;
					}

			#endregion

			//===========================================================================================
			#region "Properties"

				public	string	ID			{	get; set; }
				public	int			Row			{	get; set; }
				public	int			Col			{	get; set; }
				public	string	Value		{	get; set; }

			#endregion
		}
}
