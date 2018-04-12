using System.Collections.Generic;
//•••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••
namespace BxS_WorxNCO.RfcFunction.Main
{
	internal interface IRfcFncIndexMapper
		{
			#region "Properties"

				string	ReferenceParameterName	{ get; }
				//.................................................
				Dictionary< string , int		> SAPIndex				{ get; }
				Dictionary<	string , string >	PropertyIndex		{ get; }

			#endregion

			//===========================================================================================
			#region "Methods: Exposed"

				void AddMap( string SAPName , string propertyName );

			#endregion

		}
}
