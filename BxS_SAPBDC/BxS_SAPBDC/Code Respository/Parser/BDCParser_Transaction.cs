using System.Collections.Generic;
//.........................................................
using BxS_SAPIPC.BDCData;
//•••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••
namespace BxS_SAPBDC.Parser
{
	internal class Parser_BDCTransaction
		{
			#region "Constructors"

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				internal	Parser_BDCTransaction( BDCMain BDCMain )
					{
						this._BDCMain		= BDCMain;
						this._FldIndex	= -1;
						this._CsrIndex	= -1;
					}

			#endregion

			//===========================================================================================
			#region "Declaration"

				private readonly	BDCMain	_BDCMain	;

				private	int		_FldIndex	;
				private	int		_CsrIndex	;

			#endregion

			//===========================================================================================
			#region "Methods: Exposed: Columns"

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				internal void ParseTransaction()
					{
						//BDCTransaction x = BDCMain.CreateTransaction();

						//for (int c = BDCMain.ColLB; c < BDCMain.ColUB; c++)
						//	{
						//		DTO_BDCColumn x = CreateColumn(c);

						//		x.Program = BDCMain.Data[BDCMain.BDCHeaderRowRef.Prog,c];

						//		BDCMain.Columns.Add(x.ColNo , x);
						//	}
					}

			#endregion

			//===========================================================================================
			#region "Methods: Private"

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				private BDC_Transaction ProcessTransactionData( IList<int> rows )
					{
						BDC_Transaction x = this._BDCMain.CreateTransaction();

						for ( int i = 0; i < rows.Count; i++ )
							{
								foreach ( KeyValuePair< int , DTO_BDCColumn > ls_kvp in this._BDCMain.Columns )
									{
										this.CompileBDCEntries( x , ls_kvp.Value , this._BDCMain.Data[i,ls_kvp.Key] );
									}
							}
						//.............................................
						return	x;
					}

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				private void CompileBDCEntries( BDC_Transaction bdcTran , DTO_BDCColumn column , string value )
					{
						if ( column.DoOnlyIfValue && value.Equals(string.Empty) )
								{
									return;
								}
						//.............................................
						if ( column.IsFieldIndexColumn || column.IsCursorIndexColumn )
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
								bdcTran.AddBDCData( this.CompileBDCScreen(	column.Program
																													, column.ScreenNo ) );
							}
						//.............................................
						if ( !column.OKCode.Equals( string.Empty ) )
							{
								bdcTran.AddBDCData(	this.CompileBDCField(		this._BDCMain.Constants.FldName_OKCode
																													, column.OKCode														) );
							}
						//.............................................
						if ( !column.Cursor.Equals( string.Empty ) )
							{
								if ( column.DoCursorIndex && this._CsrIndex.Equals(-1) )
									{ }
								else
									{
										string lc_CsrFld;

										if ( column.DoCursorIndex )
											{
												lc_CsrFld	=	column.Cursor.Replace(	this._BDCMain.Constants.IndexSubstitute
																													,	this._FldIndex.ToString()						);
											}
										else
											{
												lc_CsrFld	= column.Cursor;
											}

										bdcTran.AddBDCData( this.CompileBDCField(		this._BDCMain.Constants.FldName_Cursor
																															, lc_CsrFld																) );
									}
							}
						//.............................................
						if ( !column.Subscreen.Equals(string.Empty) )
							{
								bdcTran.AddBDCData( this.CompileBDCField(		this._BDCMain.Constants.FldName_Subscreen
																													, column.Subscreen													) );
							}
						//.............................................
						if ( !value.Equals( this._BDCMain.Constants.FldValue_Psuedo ) )
							{
								if ( column.DoFieldIndex && this._FldIndex.Equals(-1) )
									{ }
								else
									{
										string lc_Fld;

										if ( column.DoFieldIndex )
											{
												lc_Fld	=	column.Field.Replace(		this._BDCMain.Constants.IndexSubstitute
																												,	this._FldIndex.ToString()						);
											}
										else
											{
												lc_Fld	= column.Field;
											}

										if ( value.Equals( this._BDCMain.Constants.FldValue_Clear ) )
											{
												bdcTran.AddBDCData( this.CompileBDCField(		lc_Fld
																																	, string.Empty	) );
											}
										else
											{
												bdcTran.AddBDCData( this.CompileBDCField(		lc_Fld
																																	, value					) );
											}
									}
							}
					}

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				private DTO_SessionTranData CompileBDCScreen( string program , int screenNo )
					{
						DTO_SessionTranData lo_BDCData	= this._BDCMain.CreateBDCData();
						//.............................................
						lo_BDCData.ProgramName	= program;
						lo_BDCData.Dynpro				= screenNo.ToString("0000");
						lo_BDCData.Begin				= "X";
						//.............................................
						return	lo_BDCData;
					}

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				private DTO_SessionTranData CompileBDCField( string field , string value )
					{
						DTO_SessionTranData lo_BDCData	= this._BDCMain.CreateBDCData();
						//.............................................
						lo_BDCData.FieldName	= field;
						lo_BDCData.FieldValue	=	value;
						//.............................................
						return	lo_BDCData;
					}

			#endregion

		}
}
