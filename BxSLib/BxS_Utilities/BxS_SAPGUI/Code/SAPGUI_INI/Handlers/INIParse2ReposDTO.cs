using System;
using System.Collections.Generic;
using BxS_SAPGUI.API;
using BxS_SAPGUI.API.DL;
using BxS_Toolset.IO;
//•••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••
namespace BxS_SAPGUI.INI
{
	internal partial class INIParse2ReposDTO
		{
			#region "Constructors"

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				public INIParse2ReposDTO(IO io, IRepository repository, string fullPathName)
					{
						this._IO						= io;
						this._Repos					= repository;
						this._FullPathName	= fullPathName;
						//.............................................
						this._ItemID	= new	Dictionary<int, Guid>();
						this._Items		= new Dictionary<int, Dictionary<string, string>>();
					}

			#endregion

			//===========================================================================================
			#region "Declarations"

				private enum IniSect
					{
						NotUsed		= 0,
						EntryKey	= 1,
						Router		= 2,
						Server		= 3
					}

				private readonly IO						_IO;
				private readonly IRepository	_Repos;
				private readonly string				_FullPathName;

				private	Dictionary<int, Guid>	_ItemID;
				
				private Dictionary<int, Dictionary<string, string>>	_Items;
				

			#endregion

			//===========================================================================================
			#region "Methods: Exposed"

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				internal void Load()
					{
						this.LoadSAPLogoINI();
					}

			#endregion

			//===========================================================================================
			#region "Methods: Private"

				private void LoadSAPLogoINI()
					{
						string  lc_Sectn	= string.Empty;
						//.............................................
						this._Items.Clear();

						foreach (string lc_Line in this._IO.ReadTextFile(this._FullPathName, false))
							{
								if (lc_Line.StartsWith("[") && lc_Line.EndsWith("]"))
									{
										lc_Sectn	=	this.GetSection(lc_Line);	
										continue;
									}
								//.........................................
								if (lc_Sectn.Length.Equals(0))	continue;
								if (!lc_Line.StartsWith("Item") || !lc_Line.Contains("="))	continue;
								//.........................................
								(int Index, string Value) ls_Info = this.GetItemInfo(lc_Line);

								if (ls_Info.Index.Equals(0) || ls_Info.Value.Length.Equals(0))	continue;
								//.........................................
								if (!this._Items.ContainsKey(ls_Info.Index))
									{
										this._Items.Add(ls_Info.Index, new Dictionary<string, string>());
									}

								if (this._Items.TryGetValue(ls_Info.Index, out Dictionary<string, string> lt_List))
									{
										lt_List[lc_Sectn]	= ls_Info.Value;
									}
							}
					}















				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				private IDTOService GetService(Guid id)
					{
						if (!this._Repos.ServiceExists(id))
							{
								this._Repos.AddUpdateService(this._Repos.CreateServiceDTO(id));
							}
						//.............................................
						return	this._Repos.GetService(id);
					}

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				private Guid GetIndexID(int index)
					{
						Guid lg_ID	= Guid.Empty;
						if (!this._ItemID.TryGetValue(index, out lg_ID))
							{
								lg_ID	= Guid.NewGuid();
								this._ItemID.Add(index, lg_ID);
							}
						//.............................................
						return	lg_ID;
					}

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				private (int ID, string Value) GetItemInfo(string line)
					{
						string lc_Val = string.Empty;
						//.............................................
						string[] x	= line.Split('=');
						var y				=	x[0].Replace("Item",string.Empty);

						if (!int.TryParse(y, out int ln_ID)) ln_ID = 0;
						lc_Val	= x[1];
						//.............................................
						return	(ID: ln_ID, Value: lc_Val);
					}

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				private string GetSection(string line)
					{
						string lc_Sectn	=	line.Replace("[",null).Replace("]",null);

						switch (lc_Sectn)
							{
								case "EntryKey"						:	return	"UUID"			;
								case "Database"						:	return	"SystemNo"	;
								case "MSSysName"					:	return	"SystemID"	;
								case "SncChoice"					:	return	"SNCOp"			;
								case "Codepage"						:	return	"SAPCPG"		;
								case "MSSrvName"					:	return	"MSID"			;
								case "SncNoSSO"						:	return	"Mode"			;

								case "Server"							:	return	lc_Sectn		;
								case "Description"				:	return	lc_Sectn		;
								case "SncName"						:	return	lc_Sectn		;

								case "Router"							:	return	"Router"					;
								case "System"							:	return	"";
								case "Address"						:	return	"";
								case "MSSrvPort"					:	return	"";
								case "SessManKey"					:	return	"";
								case "CodepageIndex"			:	return	"";
								case "LowSpeedConnection"	:	return	"";
								case "Utf8Off"						:	return	"";
								case "EncodingID"					:	return	"";

								default										:	return	"";
							}
					}

			#endregion

		}
}
