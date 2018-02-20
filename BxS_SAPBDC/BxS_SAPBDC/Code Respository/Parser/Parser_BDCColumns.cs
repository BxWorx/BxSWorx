using System;
using System.Text.RegularExpressions;
//.........................................................
using static	BxS_SAPBDC.Parser.BDC_Constants;
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
						//.............................................
						this._Regex		= new	Regex(@"\((.*?)\)");
					}

			#endregion

			//===========================================================================================
			#region "Declaration"

				private	readonly	Regex		_Regex;

				private readonly	BDCMain								_BDCMain			;
				private readonly	Func<DTO_BDCColumn>		_CreateColumn	;

			#endregion

			//===========================================================================================
			#region "Methods: Exposed: Columns"

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				internal void ParseForColumns()
					{
						for ( int c = this._BDCMain.ColDataStart; c <= this._BDCMain.ColDataEnd; c++ )
							{
								DTO_BDCColumn lo_Col	= CreateColumn(c);
								//.........................................
								lo_Col.ColNo		=	c	;
								lo_Col.ScreenNo	=	0	;
								//.........................................
								lo_Col.Program			=		this._BDCMain.Data[ this._BDCMain.BDCHeaderRowRef.Prog , c ];
								lo_Col.DynBegin			= !	this._BDCMain.Data[ this._BDCMain.BDCHeaderRowRef.Strt , c ].Equals(string.Empty);
								lo_Col.OKCode				=		this._BDCMain.Data[ this._BDCMain.BDCHeaderRowRef.OKCd , c ];
								lo_Col.Cursor				=		this._BDCMain.Data[ this._BDCMain.BDCHeaderRowRef.Curs , c ];
								lo_Col.Subscreen		=		this._BDCMain.Data[ this._BDCMain.BDCHeaderRowRef.Subs , c ];
								lo_Col.Field				=		this._BDCMain.Data[ this._BDCMain.BDCHeaderRowRef.FldN , c ];
								lo_Col.Description	=		this._BDCMain.Data[ this._BDCMain.BDCHeaderRowRef.Desc , c ];
								lo_Col.Instructions	=		this._BDCMain.Data[ this._BDCMain.BDCHeaderRowRef.Inst , c ];
								//.........................................
								if ( ushort.TryParse( this._BDCMain.Data[this._BDCMain.BDCHeaderRowRef.Scrn, c] , out ushort ln_SN ) )
									{
										lo_Col.ScreenNo	= ln_SN;
									}
								//.........................................
								if ( this.InterpretColumn( lo_Col ) )
									{
										this._BDCMain.Columns.Add(lo_Col.ColNo , lo_Col);
									}
							}
					}

			#endregion

			//===========================================================================================
			#region "Methods: Private"

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				private bool InterpretColumn( DTO_BDCColumn column )
					{
						if (		string.IsNullOrEmpty( column.Program			)
								&&	string.IsNullOrEmpty( column.OKCode				)
								&&	string.IsNullOrEmpty( column.Cursor				)
								&&	string.IsNullOrEmpty( column.Subscreen		)
								&&	string.IsNullOrEmpty( column.Field				)
								&&	string.IsNullOrEmpty( column.Description	)
								&&	string.IsNullOrEmpty( column.Instructions	)
								&&												column.ScreenNo.Equals(0)
								&&												!column.DynBegin					)
							{
								return	false;
							}
						//.............................................
						column.DoFieldIndex					= Regex.IsMatch( column.Instructions , cz_Instr_SubFldIdx	, RegexOptions.IgnoreCase )	;
						column.DoCursorIndex				= Regex.IsMatch( column.Instructions , cz_Instr_SubCsrIdx	, RegexOptions.IgnoreCase )	;
						column.DoOnlyIfValue				= Regex.IsMatch( column.Instructions , cz_Instr_DoIf			, RegexOptions.IgnoreCase )	;
						column.IsFieldIndexColumn		= Regex.IsMatch( column.Instructions , cz_Instr_ValFldIdx	, RegexOptions.IgnoreCase )	;
						column.IsCursorIndexColumn	= Regex.IsMatch( column.Instructions , cz_Instr_ValCsrIdx	, RegexOptions.IgnoreCase )	;
						//.............................................
						if ( column.DoFieldIndex	)		column.Field	= this._Regex.Replace( column.Field		, cz_Sub_Token );
						if ( column.DoCursorIndex	)		column.Cursor	= this._Regex.Replace( column.Cursor	, cz_Sub_Token );
						//.............................................
						return	true;
					}

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
