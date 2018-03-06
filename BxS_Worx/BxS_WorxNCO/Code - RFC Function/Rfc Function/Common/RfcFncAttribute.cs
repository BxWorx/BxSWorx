using System;
//•••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••
namespace BxS_WorxNCO.RfcFunction.Common
{
	[	AttributeUsageAttribute(	AttributeTargets.Property
														,	AllowMultiple = false			)	]
	internal class SAPFncParmNameAttribute : Attribute
		{
			#region "Properties"

				public	string	SAPName	{	get; set; }

			#endregion

		}
}
