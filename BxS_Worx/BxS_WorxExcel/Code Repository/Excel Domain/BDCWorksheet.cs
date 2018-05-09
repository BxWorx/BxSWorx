using System;
using System.Text;
//.........................................................
using Microsoft.Office.Interop.Excel;
//.........................................................
using BxS_WorxIPX.BDC;
using BxS_WorxExcel.DTO;
using System.Collections.Generic;
//•••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••
namespace BxS_WorxExcel.Main
{
	internal class BDCWorksheet
		{
			#region "Constructors"

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				internal BDCWorksheet()
					{
					}

			#endregion

			//===========================================================================================
			#region "Declarations"
			#endregion

			//===========================================================================================
			#region "Methods: Exposed"

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				internal void Format( Worksheet WS )
					{
						this.FormatHeader( WS );
					}

			#endregion

			//===========================================================================================
			#region "Methods: Private"

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				private void FormatHeader( Worksheet WS )
					{
						const string lz_RngS	= "B2:B11";
						const string lz_RngT	= "D2:N11";
						//...
						int									ln_Idx	= 0;
						IList<BDCInterior> lt_Int
							= new List<BDCInterior>
											{
													new BDCInterior(23 , .9	, "Program Name:"		)
												,	new BDCInterior(23 , .8	, "Screen Number:"	)
												,	new BDCInterior(23 , .7	, "Screen Start:"		)
												,	new BDCInterior(46 , .9	, "BDC: OK Code:"		)
												,	new BDCInterior(46 , .8	, "BDC: Cursor:"		)
												,	new BDCInterior(46 , .7	, "BDC: Subscreen:"	)
												,	new BDCInterior(46 , .6	, "Field Name:"			)
												,	new BDCInterior(50 , .9	, "Description:"		)
												,	new BDCInterior(50 , .8	, "Instructions:"		)
												,	new BDCInterior(03 , .9	, ""								)
											};

						foreach ( Range lo_Cell in WS.Range[lz_RngS] )
							{
								lo_Cell.Interior.ColorIndex		= lt_Int[ln_Idx].Color	;
								lo_Cell.Interior.TintAndShade	= lt_Int[ln_Idx].Tint		;
								ln_Idx	++;
							}

						WS.Range[lz_RngS].Copy(WS.Range[lz_RngT]);

						ln_Idx	= 0;
						foreach ( Range lo_Cell in WS.Range[lz_RngS] )
							{
								lo_Cell.Offset[0,2].Value	=	lt_Int[ln_Idx].Text		;
								ln_Idx	++;
							}
						//...
						WS.Range["A:A"]	.ColumnWidth	= 0.5			;
						WS.Range["C:C"]	.ColumnWidth	= 0.2			;
						WS.Range["1:1"]	.RowHeight		=	4				;
						WS.Range["D:D"]	.EntireColumn.AutoFit()	;

						WS.Range["B10:N10"]	.Borders[XlBordersIndex.xlEdgeTop].LineStyle	= XlLineStyle.xlContinuous	;
						WS.Range["B10:N10"]	.Borders[XlBordersIndex.xlEdgeTop].Weight			= XlBorderWeight.xlThin			;

						WS.Range["B10:N10"]	.Borders[XlBordersIndex.xlEdgeBottom].LineStyle	= XlLineStyle.xlDouble		;
						WS.Range["B10:N10"]	.Borders[XlBordersIndex.xlEdgeBottom].Weight		= XlBorderWeight.xlThick	;

						WS.Range["D2:D11"]	.Borders[XlBordersIndex.xlEdgeRight].LineStyle	= XlLineStyle.xlDouble		;
						WS.Range["D2:D11"]	.Borders[XlBordersIndex.xlEdgeRight].Weight			= XlBorderWeight.xlThick	;

						WS.Range["E11"].Select();
						Globals.ThisAddIn.Application.ActiveWindow.FreezePanes	= true;


				//var detailedName = new Obj();
				//detailedName.With(o => {
				//    o.Prop1 = 1;
				//    o.Prop2 = 2;
				//    o.Prop3 = 3;
				//    o.Prop4 = 4;
				//});



						//WS.Range["C2:N10"].PasteSpecial(XlPasteType.xlPasteFormats);

						
//50
//46
//42
//48
//3



						//ln_Idx	=0;
						//foreach ( Style lo in Globals.ThisAddIn.Application.ActiveWorkbook.Styles )
						//	{
						//		WS.Range["D20"].Offset[ln_Idx,0].Style = lo;
						//		WS.Range["D20"].Offset[ln_Idx,0].Value	= lo.Interior.ColorIndex		;
						//		WS.Range["D20"].Offset[ln_Idx,1].Value	= lo.Interior.TintAndShade	;
						//		ln_Idx	++;
						//	}



						//Style x = Globals.ThisAddIn.Application.ActiveWorkbook.Styles.Add("BxS");
						//x.Interior.ColorIndex	= 24;
						////...
						////.Interior.ColorIndex	= x.Interior.ColorIndex;	//			.Style = "BxS";
						//double ln_Tnt	= 0;
						//WS.Range["B2:B10"].Style	= x;
						//foreach ( Range lo_Cell in WS.Range["B2:B10"] )
						//	{
						//		lo_Cell.Interior.TintAndShade	= ln_Tnt;
						//		ln_Tnt += .1;
						//	}

						//x.Interior.ColorIndex	= 19;

						//Interior I;

						////Range x = WS.Range["$B2:B10"];
						//WS.Range["B2"].Offset[0,0].Interior.ColorIndex	= x.Interior.ColorIndex;	//			.Style = "BxS";
					}

			#endregion

		//•••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••
		private	struct BDCInterior
			{
				#region "Constructors"

					//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
					internal BDCInterior( double color , double tint , string text	= "" )
						{
							this.Color	= color	;
							this.Tint		= tint	;
							this.Text		= text	;
						}

				#endregion

			//===========================================================================================
				#region "Properties"

					public	double	Color	{ get; set; }
					public	double	Tint	{ get; set; }
					public	string	Text	{ get; set; }

				#endregion

			}
		}

		internal	static class WithExt
			{
				public static void With<T>(this T obj, Action<T> a)
					{
						a(obj);
					}
			}

}
