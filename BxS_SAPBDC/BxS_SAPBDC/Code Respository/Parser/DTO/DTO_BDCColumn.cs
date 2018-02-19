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
						//.............................................
						this.IsFieldIndexColumn	= false;
						this.DoCursorIndex	= false;
						this.DoFieldIndex		= false;
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
				//.................................................
				internal	bool		IsFieldIndexColumn		{	get; set; }
				internal	bool		IsCursorIndexColumn		{	get; set; }
				internal	bool		DoOnlyIfValue					{	get; set; }
				internal	bool		DoCursorIndex					{	get; set; }
				internal	bool		DoFieldIndex					{	get; set; }
				//.................................................
				internal	Dictionary< string, string >	Commands			{	get; set; }

			#endregion
		}
}
