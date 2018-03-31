using System;
//•••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••
namespace BxS_WorxNCO.RfcFunction.Main
{
	//===============================================================================================
	[	AttributeUsageAttribute(		AttributeTargets.Class
															|	AttributeTargets.Property
														,	AllowMultiple = false				)	]

	internal class SAPAttribute : Attribute
		{
			#region "Properties"

				public	string	Name	{	get; set; }

			#endregion

		}
}
