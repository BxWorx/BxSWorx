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
						this._BDCMain	= BDCMain;
					}

				private readonly	BDCMain _BDCMain;

			#endregion

			//===========================================================================================
			#region "Methods: Exposed: Columns"

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				internal void ParseTransaction( BDCMain BDCMain )
					{
						BDCTransaction x = BDCMain.CreateTransaction();


						for (int c = BDCMain.ColLB; c < BDCMain.ColUB; c++)
							{
								DTO_BDCColumn x = CreateColumn(c);

								x.Program = BDCMain.Data[BDCMain.BDCHeaderRowRef.Prog,c];

								BDCMain.Columns.Add(x.ColNo , x);
							}
					}

			#endregion

			//===========================================================================================
			#region "Methods: Private"

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				private BDC_Transaction ProcessTransactionData( IList<int> rows )
					{
						BDC_Transaction x = this._BDCMain.CreateTransaction();

						for (int i = 0; i < rows.Count; i++)
							{
								foreach (KeyValuePair<int, DTO_BDCColumn> ls_kvp in this._BDCMain.Columns)
									{
										DTO_SessionTranData lo_BDCData = this.CompileBDCEntry( x , ls_kvp.Value , this._BDCMain.Data[i,ls_kvp.Key] );
										if (lo_BDCData != null)
											x.AddBDCData(lo_BDCData);
									}
							}
						//.............................................
						return	x;
					}







				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				private DTO_SessionTranData CompileBDCEntry( BDC_Transaction bdcTran , DTO_BDCColumn column , string value )
					{
						if (column.DynBegin)
							bdcTran.AddBDCData( this.CompileBDCScreen( column.Program , column.ScreenNo ) );

						if ( !column.OKCode.Equals(string.Empty) )
							bdcTran.AddBDCData( this.CompileBDCField( "OK_CODE" , column.OKCode ) );

						if ( !column.Cursor.Equals(string.Empty) )
							bdcTran.AddBDCData( this.CompileBDCField( "OK_CODE" , column.OKCode ) );

						if ( !column.Subscreen.Equals(string.Empty) )
							bdcTran.AddBDCData( this.CompileBDCField( "OK_CODE" , column.OKCode ) );


						//	Add BDC_OKCODE, BDC_CURSOR, BDC_SUBSCR {OK code, Cursor, Sub Screen}
						//
			//			If Not IsNothing(lo_HCol.OKCode) AndAlso lo_HCol.OKCode.Length <> 0
			//				ln_Key	+= 1
			//				lo_BDCTran.BDC_Data.TryAdd(ln_Key,  Me.BDC_Field(	i_FldName:= xBDC_TypePool.cz_Cmd_OKCode,
			//																													i_FldVal := lo_HCol.OKCode) )
			//			End If
			//			If Not IsNothing(lo_HCol.Cursor_Before) AndAlso lo_HCol.Cursor_Before.Length <> 0
			//				ln_Key	+= 1
			//				lo_BDCTran.BDC_Data.TryAdd(ln_Key,  Me.BDC_Field(	i_FldName:= xBDC_TypePool.cz_Cmd_Cursor,
			//																													i_FldVal := lo_HCol.Cursor_Before) )
			//			End If
			//			If Not IsNothing(lo_HCol.Subscreen) AndAlso lo_HCol.Subscreen.Length <> 0
			//				ln_Key	+= 1
			//				lo_BDCTran.BDC_Data.TryAdd(ln_Key,  Me.BDC_Field(	i_FldName:= xBDC_TypePool.cz_Cmd_SubScr,
			//																													i_FldVal := lo_HCol.Subscreen) )
			//			End If




						lo_BDCData.FieldName		= column.Field;
						lo_BDCData.FieldValue		= value;

						lb_IsOK	= false;

						//.............................................
						return	lb_IsOK	? lo_BDCData : null;
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

			//'¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
			//Private	Function	CreateBDCTransaction(ByVal	rowList	As List(Of Integer)) As iBxS_BDCTran_Tran

			//	Dim lo_ExcelRow As iExcelRow    = Nothing
			//	Dim lo_HCol     As iExcelColumn = Nothing
			//	Dim lc_FldName  As String       = Nothing
			//	Dim lc_FldVal   As String       = Nothing
			//	Dim lb_RowUsed  As Boolean
			//	Dim ln_FldIdx   As UShort
			//	Dim ln_CsrIdx   As UShort
			//	Dim ln_ExcelRow	As Integer
			//	Dim ln_Key			As Integer

			//	Dim lo_BDCTran	As iBxS_BDCTran_Tran = Me.co_BDCTran.ShallowCopy()

			//	ln_ExcelRow							= 0
			//	lo_BDCTran.SAPTCode			= Me.SAPTrnCode
			//	lo_BDCTran.SAPSessionID	= Me.SAPSessionID

			//	For Each ln_RowIndex In rowList

			//		If Not Me.co_WSData.Rows.TryGetValue(key:= ln_RowIndex, value:= lo_ExcelRow)
			//			Continue For
			//		End If

			//		lb_RowUsed = False

			//		For Each ln_ColIndex In Me.co_WSHeader.ColumnIndex

			//			If Not Me.co_WSHeader.Columns.TryGetValue(	key   := ln_ColIndex,
			//																									value	:= lo_HCol)
			//				Continue For
			//			End If

			//			If Not lo_ExcelRow.Values.TryGetValue(key   := ln_ColIndex,
			//																						value := lc_FldVal)
			//				Continue For
			//			End If

			//			If lo_HCol.IsFieldIndexColumn OrElse lo_HCol.IsCursorIndexColumn
			//				If lo_HCol.IsFieldIndexColumn
			//					ln_FldIdx = CUShort(lc_FldVal)
			//				Else
			//					ln_CsrIdx = CUShort(lc_FldVal)
			//				End If
			//				Continue For ' Column Loop
			//			End If

			//			If lo_HCol.DoIFHasValue  AndAlso
			//				 lc_FldVal.Equals(0)
			//				Continue For ' Column Loop
			//			End If

			//			If lo_HCol.DoFieldIndex OrElse lo_HCol.DoCursorIndex

			//				If    lo_HCol.DoFieldIndex
			//					lc_FldName  = lo_HCol.Field_Name.Replace(xBDC_TypePool.cz_Sub_Token, 
			//																									 String.Concat( "(", ln_FldIdx.ToString, ")")
			//																									 )
			//				Else
			//					lc_FldName  = lo_HCol.Field_Name.Replace(xBDC_TypePool.cz_Sub_Token, 
			//																									 String.Concat( "(", ln_CsrIdx.ToString, ")")
			//																									 )
			//				End If

			//			Else
			//				lc_FldName  = lo_HCol.Field_Name
			//			End If

			//			' Start of DYNPRO screen
			//			'
			//			If lo_HCol.DynPro_Begin.Length <> 0
			//				ln_Key	+= 1
			//				lo_BDCTran.BDC_Data.TryAdd(ln_Key,  Me.BDC_DynPro(	i_PrgName:= lo_HCol.Program_Name,
			//																														i_DynPro := lo_HCol.DynPro_Number) )
			//			End If

			//			' Add BDC_OKCODE, BDC_CURSOR, BDC_SUBSCR {OK code, Cursor, Sub Screen}
			//			'
			//			If Not IsNothing(lo_HCol.OKCode) AndAlso lo_HCol.OKCode.Length <> 0
			//				ln_Key	+= 1
			//				lo_BDCTran.BDC_Data.TryAdd(ln_Key,  Me.BDC_Field(	i_FldName:= xBDC_TypePool.cz_Cmd_OKCode,
			//																													i_FldVal := lo_HCol.OKCode) )
			//			End If
			//			If Not IsNothing(lo_HCol.Cursor_Before) AndAlso lo_HCol.Cursor_Before.Length <> 0
			//				ln_Key	+= 1
			//				lo_BDCTran.BDC_Data.TryAdd(ln_Key,  Me.BDC_Field(	i_FldName:= xBDC_TypePool.cz_Cmd_Cursor,
			//																													i_FldVal := lo_HCol.Cursor_Before) )
			//			End If
			//			If Not IsNothing(lo_HCol.Subscreen) AndAlso lo_HCol.Subscreen.Length <> 0
			//				ln_Key	+= 1
			//				lo_BDCTran.BDC_Data.TryAdd(ln_Key,  Me.BDC_Field(	i_FldName:= xBDC_TypePool.cz_Cmd_SubScr,
			//																													i_FldVal := lo_HCol.Subscreen) )
			//			End If

			//			' Add actual field value, If not @@ which is a Psuedo action
			//			' and value not @@[] which is don't skip but make SAP field blank
			//			'
			//			If lc_FldVal <> xBDC_TypePool.cz_Sym_PsuedoAction

			//				If lc_FldVal = xBDC_TypePool.cz_Sym_ClearFld
			//					lc_FldVal = ""
			//				End If

			//				ln_Key	+= 1
			//				lo_BDCTran.BDC_Data.TryAdd(ln_Key,  Me.BDC_Field(	i_FldName:= lc_FldName,
			//																													i_FldVal := lc_FldVal) )

			//			End If

			//			lb_RowUsed = True

			//		Next

			//		If lb_RowUsed
			//			ln_ExcelRow  = lo_ExcelRow.RowNo()
			//		End If

			//	Next
				
			//	lo_BDCTran.ExcelRow	= CUInt(ln_ExcelRow)

			//	Return lo_BDCTran

			//End Function
