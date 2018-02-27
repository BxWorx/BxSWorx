using System;
using System.Collections.Generic;
//•••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••
namespace BxS_SAPBDC.Parser
{
	internal class DTO_BDCSession
		{
			#region "Constructors"

				internal DTO_BDCSession(	DTO_BDCHeaderRowRef	bdcHeaderRowRef	)
					{
						this.BDCHeaderRowRef	= bdcHeaderRowRef		?? throw new Exception();
						//.............................................
						this.Tokens		= new Dictionary< string	, DTO_TokenReference >	();
						this.Columns	= new Dictionary< int			, DTO_BDCColumn >				();
						this.TranRows	= new Dictionary< int			, List< int > >					();
					}

			#endregion

			//===========================================================================================
			#region "Properties"

				internal	Dictionary<	string	, DTO_TokenReference >	Tokens		{ get; }
				internal	Dictionary<	int			, DTO_BDCColumn >				Columns		{ get; }
				internal	Dictionary<	int			, List< int > >					TranRows	{ get; }
				//.................................................
				internal	DTO_BDCHeaderRowRef		BDCHeaderRowRef		{ get; }
				internal	DTO_BDCXMLConfig			XMLConfig					{ get; set; }
				//.................................................
				internal	bool	IsTest	{ get; set; }

				internal	int		RowLB		{ get; set; }
				internal	int		RowUB		{ get; set; }
				internal	int		ColLB		{ get; set; }
				internal	int		ColUB		{ get; set; }

				internal	int		RowDataStart	{ get; set; }
				internal	int		ColDataStart	{ get; set; }
				internal	int		ColDataEnd		{ get; set; }
				internal	int		ColDataExec		{ get; set; }
				internal	int		ColDataPost		{ get; set; }
				internal	int		ColDataMsgs		{ get; set; }

			#endregion

		}
}
