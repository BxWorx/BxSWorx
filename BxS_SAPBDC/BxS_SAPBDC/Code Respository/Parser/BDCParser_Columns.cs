using System;
//•••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••
namespace BxS_SAPBDC.Parser
{
	internal class Parser_BDCColumns
		{
			#region "Constructors"

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				internal	Parser_BDCColumns(	BDCMain							BDCMain
																		, Func<DTO_BDCColumn>	createColumn )
					{
						this._BDCMain				= BDCMain;
						this._CreateColumn	= createColumn;
					}

			#endregion

			//===========================================================================================
			#region "Declaration"

				private readonly	BDCMain								_BDCMain			;
				private readonly	Func<DTO_BDCColumn>		_CreateColumn	;

			#endregion

			//===========================================================================================
			#region "Methods: Exposed: Columns"

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				internal void ParseForColumns()
					{
						for ( int c = this._BDCMain.ColLB; c < this._BDCMain.ColUB; c++ )
							{
								DTO_BDCColumn x = CreateColumn(c);

								x.Program = this._BDCMain.Data[this._BDCMain.BDCHeaderRowRef.Prog,c];

								this._BDCMain.Columns.Add(x.ColNo , x);
							}
					}

			#endregion

			//===========================================================================================
			#region "Methods: Private"

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				private DTO_BDCColumn CreateColumn( int ID )
					{
						DTO_BDCColumn lo_DTO	= this._CreateColumn();

						lo_DTO.ColNo	= ID;

						return	lo_DTO;
					}

			#endregion

		}
}
