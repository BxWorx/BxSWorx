using System;
using System.Collections.Generic;
//.........................................................
using BxS_SAPGUI.COM.DL;
using BxS_Toolset.DataContainer;
using BxS_Toolset.IODisk;
using BxS_Toolset.Serialize;
//•••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••
namespace BxS_SAPGUI.INI
{
	internal class INIParse2ReposDTO
		{
			#region "Constructors"

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				public INIParse2ReposDTO(	IO						io							,
																	ObjSerializer serializer			,
																	IReposSAPGui	repository			,
																	string				fullPathNameINI	,
																	string				fullPathNameLNK		)
					{
						this._IO							= io;
						this._Serializer			= serializer;
						this._Repos						= repository;
						this._FullPathNameINI	= fullPathNameINI;
						this._FullPathNameLNK	= fullPathNameLNK;
						//.............................................
						this._ActiveSection		= string.Empty;
						this._WSID						= Guid.NewGuid();
						this._Items						= new Dictionary<int, Dictionary<string, string>>();
						this._LinkDesc2Srv		= new DataTable<INILinkDTO, string>	( (string ID) => new INILinkDTO()	{ INIItemDesc	= ID } );
						this._Used						= new List<string>();
					}

			#endregion

			//===========================================================================================
			#region "Declarations"

				private const	string cz_BROpen	= "["						;
				private const	string cz_BRClse	= "]"						;
				private const	string cz_Equals	= "="						;
				private const	string cz_TagItm	= "Item"				;
				private const	string cz_TagDes	= "Description"	;
				//.................................................
				private readonly	IO						_IO								;
				private readonly	ObjSerializer	_Serializer				;
				private readonly	IReposSAPGui	_Repos						;
				private readonly	string				_FullPathNameINI	;
				private readonly	string				_FullPathNameLNK	;
				private	readonly	Guid					_WSID							;
				private						string				_ActiveSection		;
				//.................................................
				private	readonly	Dictionary<int, Dictionary<string, string>>		_Items;
				private readonly	DataTable<INILinkDTO, string>										_LinkDesc2Srv;
				private readonly	IList<string>																	_Used;
			#endregion

			//===========================================================================================
			#region "Methods: Exposed"

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				internal void Load()
					{
						this.LoadLink();
						this.LoadSAPLogoINI();
						this.AddWorkspaceNode();
						this.TranslateToRepository();
						this.LinkHousekeeping();
						this.SaveLink();
					}

			#endregion

			//===========================================================================================
			#region "Methods: Private"

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				public void SaveLink()
					{
						if (this._LinkDesc2Srv.IsDirty)
							{
								this._IO.WriteFile(this._FullPathNameLNK, this._Serializer.Serialize(this._LinkDesc2Srv));
							}
					}

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				private void LoadLink()
					{
						if (this._IO.FileExists(this._FullPathNameLNK))
							{
								DataTable<INILinkDTO, string> lo_DTO	= this._Serializer
																												.DeSerialize<DataTable<INILinkDTO, string>>
																													(this._IO.ReadFile(this._FullPathNameLNK));
								lo_DTO.TransferTo(this._LinkDesc2Srv);
							}
					}

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				public void LinkHousekeeping()
					{
						IList<string> lt_Rem	= new List<string>();
						//.............................................
						foreach (string lc_Key in this._LinkDesc2Srv.KeyListFor<bool, bool>())
							{
								if (!this._Used.Contains(lc_Key))		lt_Rem.Add(lc_Key);
							}
						//.............................................
						this._LinkDesc2Srv.Remove(lt_Rem);
					}

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				// INI file does not have concept of workspace so a root one is created and all services
				// are added at item level on workspace, no NODE.
				//
				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
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
								// create/update link
								//
								string			lc_Key	=	ls_Item.Value[cz_TagDes];
								INILinkDTO	lo_Link	= this._LinkDesc2Srv.Get(lc_Key);

								if (lo_Link.ServiceID.Equals(Guid.Empty))
									{
										lo_Link.INIItemDesc	= lc_Key;
										lo_Link.ServiceID		= Guid.NewGuid();
										this._LinkDesc2Srv.AddUpdate(lo_Link.INIItemDesc, lo_Link);
									}
								this._Used.Add(lo_Link.INIItemDesc);

								//.........................................
								// create service
								//
								IDTOService lo_Srv = this.GetAddService(lo_Link.ServiceID);

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
						foreach (string lc_Line in this._IO.ReadTextFile(this._FullPathNameINI, false))
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
						bool		lb_IsItem	= line.StartsWith(cz_TagItm) && line.Contains(cz_Equals);
						string	lc_Val		= string.Empty;
						int			ln_ID			= 0;
						//.............................................
						if (lb_IsItem)
							{
								string[] lt_Const		= line.Split(cz_Equals);
								string	 ln_ItemNo	=	lt_Const[0].Replace(cz_TagItm,string.Empty);

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
						if (!line.StartsWith(cz_BROpen) || !line.EndsWith(cz_BRClse))
							{	return	false; }
						//.............................................
						string lc_Sect	= line.Replace(cz_BROpen,null).Replace(cz_BRClse,null);

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

								case cz_TagDes		:	this._ActiveSection	=	cz_TagDes	;	break;

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
