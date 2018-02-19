using System.Collections.Generic;
//•••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••
namespace BxS_SAPBDC.Parser
{
	internal class DTO_BDCColumn
		{
			#region "Constructors"

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				internal DTO_BDCColumn()
					{
						this.Commands		= new Dictionary<string, string>();
					}

			#endregion

			//===========================================================================================
			#region "Properties"

				internal	int														ColNo					{	get; set; }
				internal	string												Program				{	get; set; }
				internal	ushort												ScreenNo			{	get; set; }
				internal	bool													DynBegin			{	get; set; }
				internal	string												OKCode				{	get; set; }
				internal	string												Cursor				{	get; set; }
				internal	string												Subscreen			{	get; set; }
				internal	string												Field					{	get; set; }
				internal	string												Description		{	get; set; }
				internal	string												Instructions	{	get; set; }
				internal	Dictionary< string, string >	Commands			{	get; set; }

			#endregion
		}
}
