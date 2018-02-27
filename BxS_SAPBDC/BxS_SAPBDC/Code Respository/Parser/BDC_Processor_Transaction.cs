using System;
using System.Collections.Generic;
using System.Threading.Tasks;
//.........................................................
using					BxS_SAPBDC.BDC;
using static	BxS_SAPBDC.BDC.BDC_Constants;
//•••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••
namespace BxS_SAPBDC.Parser
{
	internal class BDC_Processor_Transaction
		{
			#region "Constructors"

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				internal	BDC_Processor_Transaction(	Lazy< BDC_Processor_Factory > factory )
					{
						this._Factory	= factory;
						//.............................................
						this._FldIndex	= -1;
						this._CsrIndex	= -1;
					}

			#endregion

			//===========================================================================================
			#region "Declaration"

				private	readonly Lazy< BDC_Processor_Factory > 	_Factory;
				//.................................................
				private	int		_FldIndex	;
				private	int		_CsrIndex	;

			#endregion

			//===========================================================================================
			#region "Methods: Exposed: Columns"

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				internal async Task< BDC_Session > Process( DTO_BDCSession dto , string[,] data )
					{
						BDC_Session	lo_BDCSession	= this._Factory.Value.CreateBDCSession();
						int X = await Task.Run( () => 3 ).ConfigureAwait(false);
						return	lo_BDCSession;
					}

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
						return	null;
						//BDC_Transaction x = this._BDCMain.CreateTransaction();

						//for ( int i = 0; i < rows.Count; i++ )
						//	{
						//		foreach ( KeyValuePair< int , DTO_BDCColumn > ls_kvp in this._BDCMain.Columns )
						//			{
						//				this.CompileBDCEntries( x , ls_kvp.Value , this._BDCMain.Data[i,ls_kvp.Key] );
						//			}
						//	}
						////.............................................
						//return	x;
					}

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				private void CompileBDCEntries(		BDC_Transaction bdcTran
																				, DTO_BDCColumn		column
																				, string					value		)
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
								bdcTran.AddBDCData( this.CompileBDCScreen( column.Program	, column.ScreenNo ) );
							}
						//.............................................
						if ( !column.OKCode.Equals( string.Empty ) )
							{
								bdcTran.AddBDCData(	this.CompileBDCField(	cz_SAP_OKCode	, column.OKCode	) );
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
												lc_CsrFld	=	column.Cursor.Replace( cz_Sub_Token	,	this._FldIndex.ToString()	);
											}
										else
											{
												lc_CsrFld	= column.Cursor;
											}

										bdcTran.AddBDCData( this.CompileBDCField(	cz_SAP_Cursor	, lc_CsrFld	) );
									}
							}
						//.............................................
						if ( !column.Subscreen.Equals(string.Empty) )
							{
								bdcTran.AddBDCData( this.CompileBDCField(	cz_SAP_SubScr	, column.Subscreen ) );
							}
						//.............................................
						if ( !value.Equals( cz_Sym_ActionCol ) )
							{
								if ( column.DoFieldIndex && this._FldIndex.Equals(-1) )
									{ }
								else
									{
										string lc_Fld;

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
												bdcTran.AddBDCData( this.CompileBDCField(	lc_Fld , string.Empty	) );
											}
										else
											{
												bdcTran.AddBDCData( this.CompileBDCField( lc_Fld , value ) );
											}
									}
							}
					}

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				private DTO_SessionTranData CompileBDCScreen( string program , int screenNo )
					{
						DTO_SessionTranData lo_BDCData	= this._Factory.Value.CreateDTOTranData();
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
						DTO_SessionTranData lo_BDCData	= this._Factory.Value.CreateDTOTranData();
						//.............................................
						lo_BDCData.FieldName	= field;
						lo_BDCData.FieldValue	=	value;
						//.............................................
						return	lo_BDCData;
					}

			#endregion

		}
}
