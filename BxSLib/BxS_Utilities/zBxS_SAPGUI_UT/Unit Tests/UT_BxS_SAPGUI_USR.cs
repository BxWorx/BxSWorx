using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.IO;
//.........................................................
using BxS_SAPGUI.COM.DL;
using BxS_SAPGUI.USR;
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
			//...................................................
			private	static	readonly string	_Path					= Directory.GetParent( Directory.GetCurrentDirectory() ).Parent.Parent.FullName;
			private	static	readonly string	_FullPathName	= Path.Combine(_Path,	cz_TestDir, cz_UsrFileName);

			private readonly UT_BxS_DataContainerFiller	_DC	= new UT_BxS_DataContainerFiller();

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
					var	lo_DCSer		= new DCSerializer();
					var	lo_UsrCntlr	= new USRController(lo_Rep, _FullPathName, lo_IO, lo_DCSer, false);
					//...............................................
					ln_Cnt	= 1;
					lo_UsrCntlr.DeleteDCXMLFile();
					Assert.IsFalse	(lo_UsrCntlr.FileExists()	,	$"DLCntlr: {ln_Cnt}: Del:Dataset: Error");
					//...............................................
					ln_Cnt	= 2;
					lo_UsrCntlr.Save(true);
					Assert.IsTrue	(lo_UsrCntlr.FileExists(),	$"DLCntlr: {ln_Cnt}: Save: File DS: Error");
					//...............................................
					ln_Cnt	= 3;
					var	lo_UsrCntlrx	= new USRController(lo_RepX, _FullPathName, lo_IO, lo_DCSer);
					this.Validate_Rep(lo_UsrCntlrx.Repository.GetDataContainer(), ln_Cnt, "UsrCntlr");
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
						Assert.AreEqual	(1	,	rep.MsgServers.Count	,	$"{utName}: {cnt}: Rep:MsgSvr: Error"	);
						Assert.AreEqual	(1	,	rep.Services.Count		,	$"{utName}: {cnt}: Rep:Service: Error"	);
						Assert.AreEqual	(1	,	rep.WorkSpaces.Count	,	$"{utName}: {cnt}: Rep:Workspace: Error"	);
						//...............................................
						Assert.IsTrue(rep.MsgServers.ContainsKey	(this._DC.MsgID)	,	$"{utName}: {cnt}: Msg:Check Key: Error"	);
						Assert.IsTrue(rep.Services.ContainsKey		(this._DC.SrvID)	,	$"{utName}: {cnt}: Srv:Check Key: Error"	);
						Assert.IsTrue(rep.WorkSpaces.ContainsKey	(this._DC.WSpID)	,	$"{utName}: {cnt}: WSp:Check Key: Error"	);

						if (rep.WorkSpaces.TryGetValue(this._DC.WSpID, out IDTOWorkspace lo_WSx))
							{
								Assert.IsTrue(lo_WSx.Items.ContainsKey(this._DC.ItmID),	$"{utName}: {cnt}: WSpItem:Check Key: Error"	);
								Assert.IsTrue(lo_WSx.Nodes.ContainsKey(this._DC.NdeID),	$"{utName}: {cnt}: WSpNode:Check Key: Error"	);
							}
						else
							{	Assert.Fail($"{utName}: {cnt}: WS:Get: Error");	}
					}

			#endregion

		}
}
