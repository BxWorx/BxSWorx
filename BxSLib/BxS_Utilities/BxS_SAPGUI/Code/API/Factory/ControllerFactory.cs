using System;
using System.Threading;
//.........................................................
using BxS_SAPGUI.XML;
using BxS_SAPGUI.INI;
using BxS_SAPGUI.USR;
using BxS_SAPGUI.COM.DL;
using BxS_SAPGUI.COM.CNTLR;
using BxS_Toolset;
//•••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••
namespace BxS_SAPGUI.API
{
	public class ControllerFactory
		{
			#region "Declarations"

				private readonly Lazy<ToolSet>	_TS		= new Lazy<ToolSet>(	() => new ToolSet()	,
																																			LazyThreadSafetyMode.ExecutionAndPublication );

			#endregion

			//===========================================================================================
			#region "Methods: Exposed: Public"

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				public IController CreateControllerForSAPXML(string fullPathName, bool onlySAPGUI = true)
					{
						IReposSAPGui			lo_Repos		= this.CreateRepository();
						IControllerSource lo_XMLCntlr	= new XMLController(lo_Repos);
						XMLParse2ReposDTO lo_Parser		=	this.CreateXMLParser();
						//.............................................
						lo_Parser.Load(lo_Repos, fullPathName, onlySAPGUI);
						//.............................................
						return	new Controller(lo_XMLCntlr);
					}

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				public IController CreateControllerForSAPINI(string fullPathNameINI, string fullPathNameLNK)
					{
						IReposSAPGui			lo_Repos	= this.CreateRepository();
						IControllerSource INICntlr	= new INIController(lo_Repos);
						INIParse2ReposDTO lo_Parser	=	this.CreateINIParser(	lo_Repos				,
																																fullPathNameINI	,
																																fullPathNameLNK		);
						//.............................................
						lo_Parser.Load();
						//.............................................
						return	new Controller(INICntlr);
					}

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				public IController CreateControllerForSAPUSR(string fullPathName, bool autoLoad = true)
					{
						IReposSAPGui			lo_Repos	= this.CreateRepository();
						IControllerSource	USRCntlr	= new USRController(	lo_Repos											,
																															fullPathName									,
																															this._TS.Value.GetIO()				,
																															this._TS.Value.GetSerlizser()	,
																															autoLoad												);
						//.............................................
						return	new Controller(USRCntlr, false);
					}

			#endregion

			//===========================================================================================
			#region "Methods: Exposed: Internal"

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				internal XMLParse2ReposDTO CreateXMLParser()
					{
						return	new XMLParse2ReposDTO();
					}

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				internal INIParse2ReposDTO CreateINIParser(	IReposSAPGui	repository			,
																										string				fullPathNameINI	,
																										string				fullPathNameLNK		)
					{
						return	new INIParse2ReposDTO(	this._TS.Value.GetIO()				,
																						this._TS.Value.GetSerlizser()	,
																						repository,
																						fullPathNameINI,
																						fullPathNameLNK		);
					}

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				internal IReposSAPGui CreateRepository()
					{
						return	new ReposSAPGui(this.CreateDC());
					}

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				internal DCSapGui CreateDC()
					{
						BxS_Toolset.DataContainer.DataTable<IDTOMsgServer	, Guid> m	= this._TS.Value.CreateDataTable<IDTOMsgServer	, Guid>	( (Guid ID) => new DTOMsgServer	()	{ UUID	= ID } );
						BxS_Toolset.DataContainer.DataTable<IDTOService		, Guid> s	= this._TS.Value.CreateDataTable<IDTOService		, Guid>	( (Guid ID) => new DTOService		()	{ UUID	= ID } );
						BxS_Toolset.DataContainer.DataTable<IDTOWorkspace	, Guid> w	= this._TS.Value.CreateDataTable<IDTOWorkspace	, Guid>	( (Guid ID) => new DTOWorkspace	()	{ UUID	= ID } );
						BxS_Toolset.DataContainer.DataTable<IDTONode			, Guid> n	= this._TS.Value.CreateDataTable<IDTONode				, Guid>	( (Guid ID) => new DTONode			()	{ UUID	= ID } );
						BxS_Toolset.DataContainer.DataTable<IDTOItem			, Guid> i	= this._TS.Value.CreateDataTable<IDTOItem				, Guid>	( (Guid ID) => new DTOItem			()	{ UUID	= ID } );
						//.............................................
						return	new	DCSapGui(m, s, w, n, i);
					}

			#endregion

		}
}
