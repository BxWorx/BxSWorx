using System;
//•••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••
namespace BxS_WorxNCO.RfcFunction.Common
{
	[	AttributeUsageAttribute(	AttributeTargets.Property
														,	AllowMultiple = false			)	]
	internal class SAPFncAttribute : Attribute
		{
			#region "Properties"

				public	string	Stru	{	get; set; }
				public	string	Name	{	get; set; }

			#endregion

		}
}
