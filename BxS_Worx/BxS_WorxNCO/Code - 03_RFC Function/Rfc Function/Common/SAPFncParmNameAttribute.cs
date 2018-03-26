using System;
//•••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••
namespace BxS_WorxNCO.RfcFunction.Main
{
	//===============================================================================================
	[	AttributeUsageAttribute(	AttributeTargets.Property
														,	AllowMultiple = false			)	]

	internal class SAPFncAttribute : Attribute
		{
			#region "Properties"

				public	string	Stru	{	get; set; }
				public	string	Name	{	get; set; }

			#endregion

		}

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

	////===============================================================================================
	//[	AttributeUsageAttribute(	AttributeTargets.Property
	//													,	AllowMultiple = false			)	]

	//internal class SAPFncPropAttribute : Attribute
	//	{
	//		#region "Properties"

	//			public	string	Name	{	get; set; }

	//		#endregion

	//	}
}
