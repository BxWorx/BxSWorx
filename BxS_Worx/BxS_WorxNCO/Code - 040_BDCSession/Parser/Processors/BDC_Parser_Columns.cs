using System;
using System.Text.RegularExpressions;
//.........................................................
using static	BxS_WorxNCO.Main							.NCO_Constants;
using static	BxS_WorxNCO.BDCSession.Parser	.BDC_Parser_Constants;
//•••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••
namespace BxS_WorxNCO.BDCSession.Parser
{
	internal class BDC_Parser_Columns : BDC_Parser_Base
		{
			#region "Constructors"

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				internal BDC_Parser_Columns(	Lazy< BDC_Parser_Factory > factory ) : base( factory )
					{
						this._Regex	= new	Regex(@"\((.*?)\)");
					}

			#endregion

			//===========================================================================================
			#region "Declaration"

				private	readonly	Regex	_Regex;

			#endregion

			//===========================================================================================
			#region "Methods: Exposed: Columns"

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				internal void	Process(	DTO_ParserRequest	dtoRequest
															,	DTO_ParserProfile	dtoProfile )
					{
						if (dtoRequest.WSData == null)	return;
						//.............................................
						for ( int c = dtoProfile.ColDataStart; c < dtoProfile.ColUB; c++ )
							{
								DTO_ParserColumn lo_Col	= CreateColumn(c);
								//.........................................
								lo_Col.ColNo		=	c	;
								lo_Col.ScreenNo	=	0	;
								//.........................................
								try
									{
										lo_Col.Program				=	dtoRequest.WSData[ dtoProfile.BDCHeaderRowRef.Prog , c ] ?? cz_Null;
										lo_Col.OKCode					=	dtoRequest.WSData[ dtoProfile.BDCHeaderRowRef.OKCd , c ] ?? cz_Null;
										lo_Col.Cursor					=	dtoRequest.WSData[ dtoProfile.BDCHeaderRowRef.Curs , c ] ?? cz_Null;
										lo_Col.Subscreen			=	dtoRequest.WSData[ dtoProfile.BDCHeaderRowRef.Subs , c ] ?? cz_Null;
										lo_Col.Field					=	dtoRequest.WSData[ dtoProfile.BDCHeaderRowRef.FldN , c ] ?? cz_Null;
										lo_Col.Description		=	dtoRequest.WSData[ dtoProfile.BDCHeaderRowRef.Desc , c ] ?? cz_Null;
										lo_Col.Instructions		=	dtoRequest.WSData[ dtoProfile.BDCHeaderRowRef.Inst , c ] ?? cz_Null;
										//.........................................
										lo_Col.DynBegin				=	dtoRequest.WSData[ dtoProfile.BDCHeaderRowRef.Strt , c ]?.Equals(string.Empty) == false;
										//.........................................
										if ( ushort.TryParse( dtoRequest.WSData[dtoProfile.BDCHeaderRowRef.Scrn, c] , out ushort ln_SN ) )
											{
												lo_Col.ScreenNo	= ln_SN;
											}
										//.........................................
										if ( this.InterpretColumn( lo_Col ) )
											{
												dtoProfile.Columns.Add( lo_Col.ColNo , lo_Col );
												dtoProfile.ColDataEnd	= lo_Col.ColNo;
											}
									}
								catch (Exception)
									{
										throw;
									}
							}
					}

			#endregion

			//===========================================================================================
			#region "Methods: Private"

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				private bool InterpretColumn( DTO_ParserColumn column )
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
				private DTO_ParserColumn CreateColumn( int ID )
					{
						DTO_ParserColumn lo_DTO	= this._PFactory.Value.CreateDTOColumn();

						lo_DTO.ColNo	= ID;

						return	lo_DTO;
					}

			#endregion

		}
}
