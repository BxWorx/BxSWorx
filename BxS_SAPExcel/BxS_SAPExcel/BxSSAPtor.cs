using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Office.Tools.Ribbon;

using Microsoft.Office.Interop.Excel;


namespace BxS_SAPExcel
	{
	public partial class BxSSAPtor
		{
		private void BxSSAPtor_Load(object sender, RibbonUIEventArgs e)
			{

			}

		private void button1_Click(object sender, RibbonControlEventArgs e)
			{
				Worksheet x =	Globals.ThisAddIn.Application.ActiveSheet;
			//.Workbooks.Item["ZZZ"].Worksheets.Item[1];

				string c = x.UsedRange.Address;

				object[,] d = x.UsedRange.Value;

				//Return CType(Globals.ThisAddIn.Application.Workbooks().Item(Index:= i_WBName).Worksheets().Item(Index:= i_WSIndex),
				//						 Worksheet).UsedRange.Address

				//Globals.ThisAddIn.Application.Worksheets[1]
			}
		}
	}
