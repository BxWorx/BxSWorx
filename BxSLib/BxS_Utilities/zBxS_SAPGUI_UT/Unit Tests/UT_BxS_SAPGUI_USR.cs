using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.IO;
//.........................................................
using BxS_SAPGUI.COM.DL;
using BxS_SAPGUI.USR;
using BxS_SAPGUI.INI;
using BxS_Toolset.IO;
using BxS_Toolset.Serialize;
//•••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••
namespace zBxS_SAPGUI_UT
{
	[TestClass]
	public class UT_BxS_SAPGUI_USR
		{
			private const string	cz_TestDir			= "Test Resources";
			private const	string	cz_UsrFileName	= "SAPGUI_USR_DC.xml"	;
			private const string	cz_INIFileName	= "saplogon_Test.ini";
			private const string	cz_LNKFileName	= "saplogon_Link.xml";
			//...................................................
			private	static	readonly string	_Path					= Directory.GetParent( Directory.GetCurrentDirectory() ).Parent.Parent.FullName;
			private	static	readonly string	_FullPathName	= Path.Combine(_Path,	cz_TestDir, cz_UsrFileName);

			private readonly UT_BxS_DataContainerFiller	_DC	= new UT_BxS_DataContainerFiller();

			//-------------------------------------------------------------------------------------------
			[TestMethod]
			public void UT_SapGuiUsr_Parser()
				{
					string				lc_IniFullName	= Path.Combine(_Path,	cz_TestDir, cz_INIFileName);
					string				lc_LnkFullName	= Path.Combine(_Path,	cz_TestDir, cz_LNKFileName);
					IReposSAPGui	lo_Repos				= new ReposSAPGui(new DCSapGui());
					var						lo_IO						= new IO();
					var						lo_Ser					= new ObjSerializer();
					//.............................................
					var	lo_Parser	=	new INIParse2ReposDTO(lo_IO, lo_Ser, lo_Repos, lc_IniFullName, lc_LnkFullName);
					lo_Parser.Load();
				}

			//-------------------------------------------------------------------------------------------
			[TestMethod]
			public void UT_SapGuiUsr_UsrController()
				{
					int	ln_Cnt;
					//...............................................
					IReposSAPGui		lo_Rep	= this._DC.CreateRepository();
					IReposSAPGui		lo_RepX	= this._DC.CreateRepository(true);
					IReposSAPGui		lo_RepY	= this._DC.CreateRepository(true);

					var	lo_IO				= new IO();
					var	lo_DCSer		= new ObjSerializer();
					var	lo_UsrCntlr	= new USRController(lo_Rep, _FullPathName, lo_IO, lo_DCSer, false);
					//...............................................
					ln_Cnt	= 1;
					lo_UsrCntlr.DeleteDCXMLFile();
					Assert.IsFalse	(lo_UsrCntlr.FileExists()	,	$"DLCntlr: {ln_Cnt}: Del:Dataset: Error");
					//...............................................
					ln_Cnt	= 2;
					lo_UsrCntlr.Save(true);
					Assert.IsTrue	(lo_UsrCntlr.FileExists(),	$"DLCntlr: {ln_Cnt}: Save: File DS: Error");
					this.Validate_Rep(lo_UsrCntlr.Repository.DataContainer, ln_Cnt, "UsrCntlr");
					//...............................................
					ln_Cnt	= 3;
					var	lo_UsrCntlrx	= new USRController(lo_RepX, _FullPathName, lo_IO, lo_DCSer);
					this.Validate_Rep(lo_UsrCntlrx.Repository.DataContainer, ln_Cnt, "UsrCntlr");
					//...............................................
					ln_Cnt	= 4;
					lo_UsrCntlr.Repository.AddUpdateMsgServer(this._DC.Create_MsgSvrDTO());
					lo_UsrCntlr.Save(true);
					var	lo_UsrCntlry	= new USRController(lo_RepY, _FullPathName, lo_IO, lo_DCSer);
					Assert.AreEqual	(2,	lo_UsrCntlry.Repository.MsgServerCount,	$"DLCntlr: {ln_Cnt}: Del:Dataset: Error");
				}

			//===========================================================================================
			#region "Methods: Private"

				//-----------------------------------------------------------------------------------------
				private void Validate_Rep(DCSapGui rep, int cnt, string utName)
					{
						Assert.AreEqual	(1	,	rep.MsgServers	.Count	,	$"{utName}: {cnt}: Rep:MsgSvr"		);
						Assert.AreEqual	(1	,	rep.Services		.Count	,	$"{utName}: {cnt}: Rep:Service"		);
						Assert.AreEqual	(1	,	rep.Workspaces	.Count	,	$"{utName}: {cnt}: Rep:Workspace"	);
						Assert.AreEqual	(1	,	rep.Nodes			.Count	,	$"{utName}: {cnt}: Rep:Node"			);
						Assert.AreEqual	(2	,	rep.Items			.Count	,	$"{utName}: {cnt}: Rep:Item"			);
						//...............................................
						Assert.IsTrue(rep.MsgServers	.Exists(this._DC.MsgID)	,	$"{utName}: {cnt}: Msg:Check Key"			);
						Assert.IsTrue(rep.Services		.Exists(this._DC.SrvID)	,	$"{utName}: {cnt}: Srv:Check Key"			);
						Assert.IsTrue(rep.Workspaces	.Exists(this._DC.WSpID)	,	$"{utName}: {cnt}: WSp:Check Key"			);
						Assert.IsTrue(rep.Nodes			.Exists(this._DC.NdeID),	$"{utName}: {cnt}: WSpNode:Check Key"	);
						Assert.IsTrue(rep.Items			.Exists(this._DC.ItNID),	$"{utName}: {cnt}: NdeItem:Check Key"	);
						Assert.IsTrue(rep.Items			.Exists(this._DC.ItWID),	$"{utName}: {cnt}: WSpItem:Check Key"	);
					}

			#endregion

		}
}
