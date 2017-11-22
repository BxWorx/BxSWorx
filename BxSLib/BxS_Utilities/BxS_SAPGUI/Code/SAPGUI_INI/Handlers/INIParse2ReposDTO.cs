using System;
using System.Collections.Generic;
using BxS_SAPGUI.API;
using BxS_Toolset.IO;
//•••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••
namespace BxS_SAPGUI.INI
{
	internal partial class INIParse2ReposDTO
		{
			#region "Constructors"

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				public INIParse2ReposDTO(IO io)
					{
						this._IO			= io;
						this._ItemID	= new	Dictionary<string, Guid>();
					}

			#endregion

			//===========================================================================================
			#region "Declarations"

				private enum IniSect
					{
						NotUsed		= 0,
						EntryKey	= 1,
						Router		= 2
					}

				private readonly	IO												_IO;
				private						Dictionary<string, Guid>	_ItemID;

			#endregion

			//===========================================================================================
			#region "Methods: Exposed"

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				internal void Load(IRepository repository, string fullName)
					{
						var			lt_Items	= new	Dictionary<string, Guid>();
						IniSect	lc_Sect		= IniSect.NotUsed;

						foreach (string lc_Line in this._IO.ReadTextFile(fullName))
							{
								if (lc_Line.Equals(string.Empty))	continue;

								if (lc_Line.StartsWith("[") && lc_Line.EndsWith("]"))
									{	lc_Sect	= this.GetSection(lc_Line);	}

								if (lc_Sect == IniSect.NotUsed)	continue;


								if (lc_Line.StartsWith("Item") && lc_Line.Contains("="))
									{
										(int ID, string Value) data = this.GetItemInfo(lc_Line);
									}

							}


						IRepository	z = repository;
						string			xx = fullName;
					}

			#endregion

			//===========================================================================================
			#region "Methods: Exposed"

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				private (int ID, string Value) GetItemInfo(string line)
					{
						int			ln_ID		= 0;
						string	lc_Val	= string.Empty;
						//.............................................
						string[] x = line.Split('=');
						x[0].Replace("Item",string.Empty);
						lc_Val	= x[1];
						//.............................................
						return	(ID: ln_ID, Value: lc_Val);
					}

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				private IniSect GetSection(string line)
					{
						switch (line.Substring(1,line.Length-2))
							{
								case "EntryKey"	:	return	IniSect.EntryKey	;
								default					:	return	IniSect.NotUsed		;
							}
					}

			#endregion

		}
}
