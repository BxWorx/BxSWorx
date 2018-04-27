using System;
using System.Collections.Generic;
//.........................................................
using BxS_WorxNCO.BDCSession.DTO;

using static	BxS_WorxNCO.Main								.NCO_Constants;
using static	BxS_WorxNCO.BDCSession.Parser		.BDC_Parser_Constants;
//•••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••
namespace BxS_WorxNCO.BDCSession.Parser
{
	internal class BDC_Parser_Transaction : BDC_Parser_Base
		{
			#region "Constructors"

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				internal	BDC_Parser_Transaction(	Lazy< BDC_Parser_Factory > factory ) : base( factory )
					{
						this._FldIndex	= -1;
						this._CsrIndex	= -1;
					}

			#endregion

			//===========================================================================================
			#region "Declaration"

				private	int		_FldIndex	;
				private	int		_CsrIndex	;

			#endregion

			//===========================================================================================
			#region "Methods: Exposed: Columns"

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				internal void	Process(	DTO_ParserSession	dtoRequest
															,	DTO_ParserProfile	dtoProfile
															, DTO_BDC_Session		Session			)
					{
						if (dtoRequest.WSData == null)	return;
						//.............................................
						foreach ( KeyValuePair< int , List< int > > ls_KvpRow in dtoProfile.TranRows )
							{
								DTO_BDC_Transaction lo_BDCTran	= Session.CreateTransDTO();
								//.........................................
								for ( int r = 0; r < ls_KvpRow.Value.Count; r++ )
									{
										foreach ( KeyValuePair< int , DTO_ParserColumn > ls_KvpCol in dtoProfile.Columns )
											{
												this.CompileBDCEntries(		lo_BDCTran
																								,	ls_KvpCol.Value
																								,	dtoRequest.WSData[ ls_KvpRow.Value[r] , ls_KvpCol.Key ] );
											}
									}
								//.........................................
								Session.Trans.Enqueue( lo_BDCTran );
							}
					}

			#endregion

			//===========================================================================================
			#region "Methods: Private"

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				private void CompileBDCEntries(		DTO_BDC_Transaction bdcTran
																				, DTO_ParserColumn		column
																				, string					value		)
					{
						if ( column.DoOnlyIfValue && value.Equals(string.Empty) )
								{
									return;
								}
						//.............................................
						if (		column.IsFieldIndexColumn
								||	column.IsCursorIndexColumn )
							{
								if ( int.TryParse( value, out int ln_Idx ) )
									{
										if ( column.IsCursorIndexColumn )
											{
												this._CsrIndex	= ln_Idx;
											}

										if ( column.IsFieldIndexColumn )
											{
												this._FldIndex	= ln_Idx;
											}
									}
								//.........................................
								return;
							}

						//.............................................
						// Process after all criteria met
						//.............................................
						if (column.DynBegin)
							{
								bdcTran.AddBDCData( programName: column.Program	, dynpro: column.ScreenNo , begin: true );
							}
						//.............................................
						if ( !column.OKCode.Equals( string.Empty ) )
							{
								bdcTran.AddBDCData( field: cz_SAP_OKCode , value: column.OKCode );
							}
						//.............................................
						if ( !column.Cursor.Equals( string.Empty ) )
							{
								if ( ! column.DoCursorIndex || ! this._CsrIndex.Equals(-1) )
									{
										string lc_CsrFld;

										if ( column.DoCursorIndex )
											{
												lc_CsrFld	=	column.Cursor.Replace( cz_Sub_Token	,	this._FldIndex.ToString()	);
											}
										else
											{
												lc_CsrFld	= column.Cursor;
											}

										bdcTran.AddBDCData( field: cz_SAP_Cursor , value: lc_CsrFld );
									}
							}
						//.............................................
						if ( !column.Subscreen.Equals(string.Empty) )
							{
								bdcTran.AddBDCData( field: cz_SAP_SubScr , value: column.Subscreen );
							}
						//.............................................
						if ( !value.Equals( cz_Sym_ActionCol ) )
							{
								if ( ! column.DoFieldIndex || ! this._FldIndex.Equals(-1) )
									{
										string	lc_Fld;
										string	lc_Val	= value;

										if ( column.DoFieldIndex )
											{
												lc_Fld	=	column.Field.Replace(	cz_Sub_Token , this._FldIndex.ToString() );
											}
										else
											{
												lc_Fld	= column.Field;
											}

										if ( value.Equals( cz_Sym_ClearFld ) )
											{
												lc_Val	= cz_Null;
											}

										bdcTran.AddBDCData( field: lc_Fld , value: lc_Val );
									}
							}
					}

			#endregion

		}
}
