using System;
using System.Text.RegularExpressions;
//.........................................................
using static	BxS_SAPBDC.Parser.BDC_Constants;
//•••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••
namespace BxS_SAPBDC.Parser
{
	public class BDC_Processor_Columns
		{
			#region "Constructors"

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				internal BDC_Processor_Columns(	Func<DTO_BDCColumn>	createColumn )
					{
						this._CreateColumn	= createColumn;
						//.............................................
						this._Regex	= new	Regex(@"\((.*?)\)");
					}

			#endregion

			//===========================================================================================
			#region "Declaration"

				private	readonly	Regex									_Regex				;
				private readonly	Func<DTO_BDCColumn>		_CreateColumn	;

			#endregion

			//===========================================================================================
			#region "Methods: Exposed: Columns"

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				internal bool Process( DTO_BDCSession dto , string[,] data )
					{
						bool	lb_Ret	= false;
						//.............................................
						for ( int c = dto.ColDataStart; c <= dto.ColUB; c++ )
							{
								DTO_BDCColumn lo_Col	= CreateColumn(c);
								//.........................................
								lo_Col.ColNo		=	c	;
								lo_Col.ScreenNo	=	0	;
								//.........................................
								try
									{
										lo_Col.Program			=		data[ dto.BDCHeaderRowRef.Prog , c ];
										lo_Col.DynBegin			=		data[ dto.BDCHeaderRowRef.Strt , c ]?.Equals(string.Empty) == false;
										lo_Col.OKCode				=		data[ dto.BDCHeaderRowRef.OKCd , c ];
										lo_Col.Cursor				=		data[ dto.BDCHeaderRowRef.Curs , c ];
										lo_Col.Subscreen		=		data[ dto.BDCHeaderRowRef.Subs , c ];
										lo_Col.Field				=		data[ dto.BDCHeaderRowRef.FldN , c ];
										lo_Col.Description	=		data[ dto.BDCHeaderRowRef.Desc , c ];
										lo_Col.Instructions	=		data[ dto.BDCHeaderRowRef.Inst , c ];
										//.........................................
										if ( ushort.TryParse( data[dto.BDCHeaderRowRef.Scrn, c] , out ushort ln_SN ) )
											{
												lo_Col.ScreenNo	= ln_SN;
											}
										//.........................................
										if ( this.InterpretColumn( lo_Col ) )
											{
												dto.Columns.Add( lo_Col.ColNo , lo_Col );
												dto.ColDataEnd	= lo_Col.ColNo;
												lb_Ret	= true;
											}
									}
								catch (Exception)
									{
										throw;
									}
							}
						//.............................................
						return	lb_Ret;
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
						if ( !string.IsNullOrEmpty( column.Instructions ) )
							{
								column.DoFieldIndex					= Regex.IsMatch( column.Instructions , cz_Instr_SubFldIdx	, RegexOptions.IgnoreCase )	;
								column.DoCursorIndex				= Regex.IsMatch( column.Instructions , cz_Instr_SubCsrIdx	, RegexOptions.IgnoreCase )	;
								column.DoOnlyIfValue				= Regex.IsMatch( column.Instructions , cz_Instr_DoIf			, RegexOptions.IgnoreCase )	;
								column.IsFieldIndexColumn		= Regex.IsMatch( column.Instructions , cz_Instr_ValFldIdx	, RegexOptions.IgnoreCase )	;
								column.IsCursorIndexColumn	= Regex.IsMatch( column.Instructions , cz_Instr_ValCsrIdx	, RegexOptions.IgnoreCase )	;
							}
						//.............................................
						if ( column.DoFieldIndex	)
							{
								if ( !string.IsNullOrEmpty( column.Field ) )
									{
										column.Field	= this._Regex.Replace( column.Field	, cz_Sub_Token );
									}
							}
						//.............................................
						if ( column.DoCursorIndex	)
							{
								if ( !string.IsNullOrEmpty( column.Cursor ) )
									{
										column.Cursor	= this._Regex.Replace( column.Cursor , cz_Sub_Token );
									}
							}
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
