using System;
using System.Collections.Generic;
using BxS_SAPGUI.COM.DL;
using BxS_Toolset.IO;
//•••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••
namespace BxS_SAPGUI.INI
{
	internal class INIParse2ReposDTO
		{
			#region "Constructors"

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				public INIParse2ReposDTO(IO io, IReposSAPGui repository, string fullPathName)
					{
						this._IO						= io;
						this._Repos					= repository;
						this._FullPathName	= fullPathName;
						//.............................................
						this._ActiveSection	= string.Empty;
						this._WSID					= Guid.NewGuid();
						this._Items					= new Dictionary<int, Dictionary<string, string>>();
						this._LinkDesc2Srv					= new Dictionary<string, Guid>();
					}

			#endregion

			//===========================================================================================
			#region "Declarations"

				private readonly	IO						_IO;
				private readonly	IReposSAPGui	_Repos;
				private readonly	string				_FullPathName;
				private	readonly	Guid					_WSID;
				private						string				_ActiveSection;
				//.................................................
				private	readonly	Dictionary<int, Dictionary<string, string>>	_Items;
				private readonly	Dictionary<string, Guid>										_LinkDesc2Srv	;

			#endregion

			//===========================================================================================
			#region "Methods: Exposed"

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				internal void Load()
					{
						this.LoadSAPLogoINI();
						this.AddWorkspaceNode();
						this.TranslateToRepository();
					}

			#endregion

			//===========================================================================================
			#region "Methods: Private"

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				// INI file does not have concept of workspace so a root one is created and all services
				// are added at item level on workspace, no NODE.
				//
				private void AddWorkspaceNode()
					{
						IDTOWorkspace lo_WSDTO	= this._Repos.CreateWorkspaceDTO(this._WSID);
						lo_WSDTO.Description		= "Connections";
						this._Repos.AddUpdateWorkspace(lo_WSDTO);
					}

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				private void TranslateToRepository()
					{
						foreach (KeyValuePair<int, Dictionary<string, string>> ls_Item in this._Items)
							{
								//.........................................
								// create service
								//
								string x =	ls_Item.Value["Description"];

								if (!this._LinkDesc2Srv.TryGetValue(x, out Guid lg_ID))
									{
										lg_ID	= Guid.NewGuid();
										this._LinkDesc2Srv.Add(x, lg_ID);
									}


								IDTOService lo_Srv = this.GetAddService(lg_ID);

								foreach (KeyValuePair<string, string> ls_Prop in ls_Item.Value)
									{
										lo_Srv.GetType().GetProperty(ls_Prop.Key).SetValue(lo_Srv, ls_Prop.Value);
									}
								//.........................................
								// add service as item to workspace node
								//
								IDTOItem lo_Item	= this._Repos.CreateItemDTO(Guid.NewGuid());

								lo_Item.ServiceID	= lo_Srv.UUID;
								lo_Item.WSID			= this._WSID;

								this._Repos.AddUpdateItem(lo_Item);
							}
					}

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				private IDTOService GetAddService(Guid id)
					{
						if (!this._Repos.ServiceExists(id))
							{
								this._Repos.AddUpdateService(this._Repos.CreateServiceDTO(id));
							}
						//.............................................
						return	this._Repos.GetService(id);
					}

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				private void LoadSAPLogoINI()
					{
						this._Items.Clear();
						//.............................................
						foreach (string lc_Line in this._IO.ReadTextFile(this._FullPathName, false))
							{
								if (	this.GetSection(lc_Line)							)	continue;
								if (	this._ActiveSection.Length.Equals(0)	)	continue;
								//.........................................
								(bool IsValidItem, int Index, string Value) ls_Info = this.GetItemInfo(lc_Line);
								if (!ls_Info.IsValidItem)	continue;
								//.........................................
								if (!this._Items.ContainsKey(ls_Info.Index))
									{
										this._Items.Add(ls_Info.Index, new Dictionary<string, string>());
									}

								if (this._Items.TryGetValue(ls_Info.Index, out Dictionary<string, string> lt_List))
									{
										lt_List[this._ActiveSection]	= ls_Info.Value;
									}
							}
					}

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				private (bool IsItem, int ID, string Value) GetItemInfo(string line)
					{
						bool		lb_IsItem	= line.StartsWith("Item") && line.Contains("=");
						string	lc_Val		= string.Empty;
						int			ln_ID			= 0;
						//.............................................
						if (lb_IsItem)
							{
								string[] lt_Const		= line.Split('=');
								string	 ln_ItemNo	=	lt_Const[0].Replace("Item",string.Empty);

								if (int.TryParse(ln_ItemNo, out ln_ID))
									lc_Val	= lt_Const[1];

								if (ln_ID.Equals(0) || lc_Val.Length.Equals(0))
									lb_IsItem	= false;
							}
						//.............................................
						return	(IsItem: lb_IsItem, ID: ln_ID, Value: lc_Val);
					}

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				private bool GetSection(string line)
					{
						if (!line.StartsWith("[") || !line.EndsWith("]"))
							{	return	false; }
						//.............................................
						string lc_Sect	= line.Replace("[",null).Replace("]",null);

						switch (lc_Sect)
							{
								case "EntryKey"						:	this._ActiveSection	=	"UUID"				;	break;
								case "Database"						:	this._ActiveSection	=	"SystemNo"		;	break;
								case "MSSysName"					:	this._ActiveSection	=	"SystemID"		;	break;
								case "SncChoice"					:	this._ActiveSection	=	"SNCOp"				;	break;
								case "Codepage"						:	this._ActiveSection	=	"SAPCPG"			;	break;
								case "MSSrvName"					:	this._ActiveSection	=	"MSID"				;	break;
								case "SncNoSSO"						:	this._ActiveSection	=	"Mode"				;	break;
								case "SncName"						:	this._ActiveSection	=	"SNCName"			;	break;
								case "Server"							:	this._ActiveSection	=	"Server"			;	break;
								case "Description"				:	this._ActiveSection	=	"Description"	;	break;

								//.........................................
								// TO-DO: FIX THIS ISSUE
								//case "Router"							:	this._ActiveSection	=	"Router"		;	break;
								//case "System"							:	this._ActiveSection	=	"";	break;
								//case "Address"						:	this._ActiveSection	=	"";	break;
								//case "MSSrvPort"					:	this._ActiveSection	=	"";	break;
								//case "SessManKey"					:	this._ActiveSection	=	"";	break;
								//case "CodepageIndex"			:	this._ActiveSection	=	"";	break;
								//case "LowSpeedConnection"	:	this._ActiveSection	=	"";	break;
								//case "Utf8Off"						:	this._ActiveSection	=	"";	break;
								//case "EncodingID"					:	this._ActiveSection	=	"";	break;

								default										:	this._ActiveSection	=	"";	break;
							}
						//.............................................
						return	true;
					}

			#endregion

		}
}
