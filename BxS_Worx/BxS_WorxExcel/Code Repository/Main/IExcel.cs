﻿using Microsoft.Office.Interop.Excel;
//•••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••
namespace BxS_WorxExcel.Main
{
	public interface IExcel
		{
			#region "Properties"

				string	CurrentAddress { get; }

				Worksheet	GetActiveWorksheet();
				Worksheet	AddWorksheet();

				void	ScreenUpdating( bool AsOn = true );

			#endregion

		}
}
