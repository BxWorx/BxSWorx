using System;
using System.Collections.Generic;
//•••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••
namespace BxS_WorxNCO.BDCSession.Parser
{
	internal class DTO_ParserProfile
		{
			#region "Constructors"

				internal DTO_ParserProfile( DTO_ParserHeaderRowRef	bdcHeaderRowRef	)
					{
						this.BDCHeaderRowRef	= bdcHeaderRowRef		?? throw new Exception( "Parser: Profile: NULL Header row reference" );
						//.............................................
						this.Tokens		= new Dictionary< string	, DTO_ParserToken		>	();
						this.Columns	= new Dictionary< int			, DTO_ParserColumn	>	();
						this.TranRows	= new Dictionary< int			, List<int>					>	();
						//.............................................
						this.RowLB	= -1;
						this.RowUB	= -1;
						this.ColLB	= -1;
						this.ColUB	= -1;
					}

			#endregion

			//===========================================================================================
			#region "Properties"

				internal	Dictionary<	string	, DTO_ParserToken		>	Tokens		{ get; }
				internal	Dictionary<	int			, DTO_ParserColumn	>	Columns		{ get; }
				internal	Dictionary<	int			, List<int>					>	TranRows	{ get; }
				//.................................................
				internal	DTO_ParserHeaderRowRef	BDCHeaderRowRef		{ get; }
				internal	DTO_ParserXMLConfig			XMLConfig					{ get; set; }
				//.................................................
				internal	bool	IsTest	{ get; set; }

				internal	int		RowLB		{ get; set; }
				internal	int		RowUB		{ get; set; }
				internal	int		ColLB		{ get; set; }
				internal	int		ColUB		{ get; set; }

				internal	int		RowDataStart	{ get; set; }
				internal	int		ColDataStart	{ get; set; }
				internal	int		ColDataEnd		{ get; set; }
				internal	int		ColID					{ get; set; }
				internal	int		ColExec				{ get; set; }
				internal	int		ColPost				{ get; set; }
				internal	int		ColMsgs				{ get; set; }

				internal	int		OffsetRow		{ get; set; }
				internal	int		OffsetCol		{ get; set; }

			#endregion

		}
}
