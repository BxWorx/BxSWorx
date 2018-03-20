//•••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••
namespace BxS_WorxNCO.BDCSession.Parser
{
	internal class DTO_ParserToken
		{
			#region "Constructors"

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				internal DTO_ParserToken( string ID = ""	)
					{
						this.ID	= ID;
						//.............................................
						this.Row			= -1;
						this.Col			= -1;
						this.Value		= string.Empty;
						this.AltID		= string.Empty;
						this.Found		= false;
						this.FoundAlt	= false;
					}

			#endregion

			//===========================================================================================
			#region "Properties"

				public	string	ID				{	get; set; }
				public	int			Row				{	get; set; }
				public	int			Col				{	get; set; }
				public	string	Value			{	get; set; }
				public	bool		Found			{	get; set; }
				public	string	AltID			{	get; set; }
				public	bool		FoundAlt	{	get; set; }

			#endregion
		}
}
