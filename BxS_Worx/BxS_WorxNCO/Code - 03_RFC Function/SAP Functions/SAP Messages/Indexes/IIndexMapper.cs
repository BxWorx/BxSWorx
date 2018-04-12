using System;
using System.Collections.Generic;
//•••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••
namespace BxS_WorxNCO.RfcFunction.SAPMsg
{
	internal interface IIndexMapper
		{
			#region "Properties"

				Dictionary< string , int		> SAPIndex				{ get; }
				Dictionary<	string , string >	PropertyIndex		{ get; }

			#endregion

		}
}
