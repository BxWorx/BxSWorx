using System;
using System.Collections.Generic;
//.........................................................
using BxS_WorxExcel.DTO;

using BxS_WorxIPX.Main;
using BxS_WorxIPX.BDC;
using BxS_WorxIPX.NCO;

using BxS_WorxUtil.Main;

using static	BxS_WorxExcel.Main.EXL_Constants;
//•••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••
namespace BxS_WorxExcel.Main
{
	internal class BxS_XLController
		{
			#region "Constructors"

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				internal BxS_XLController()
					{
						this._IPXCntlr	= new	Lazy<IIPX_Controller>	(	()=>	IPX_Controller.Instance	, cz_LM )	;
						this._UTLCntlr	= new	Lazy<IUTL_Controller>	(	()=>	UTL_Controller.Instance	, cz_LM )	;
						//...
						this._IPXBDCCntlr		= new	Lazy<IBDC_Controller>	( ()=>	this._IPXCntlr.Value.Create_BDCController() , cz_LM )	;
						//...
						this._BDCHndlr	= new	Lazy<BDC_Handler>		( ()=>	new	BDC_Handler		( this._IPXBDCCntlr )	, cz_LM )	;
						//...
						this._FavHndlr	= new	Lazy<BxS_Favourites<ISAP_Logon>>	( ()=>	new	BxS_Favourites<ISAP_Logon>(		this._UTLCntlr.Value.CreateTopTenList<ISAP_Logon>()
																																																						,	this._UTLCntlr.Value.Serializer
																																																						,	this._IPXBDCCntlr.Value.Create_SAPLogon								)	, cz_LM )	;
					}

			#endregion

			//===========================================================================================
			#region "Declarations"

				private readonly Lazy< IIPX_Controller >	_IPXCntlr	;
				private readonly Lazy< IUTL_Controller >	_UTLCntlr	;
				//...
				private readonly Lazy< IBDC_Controller >	_IPXBDCCntlr	;
				//...
				private	readonly	Lazy<BDC_Handler>			_BDCHndlr	;
				//...
				private	readonly	Lazy<BxS_Favourites<ISAP_Logon>>	_FavHndlr	;

			#endregion

			//===========================================================================================
			#region "Properties"

				private		IBDC_Controller IPXBDCCntlr	{ get { return	this._IPXBDCCntlr.Value	; } }
				//...
				private		IExcel	XLHndlr							{ get { return	Globals.ThisAddIn.XLHndlr; } }
				//...
				internal	BxS_Favourites<ISAP_Logon>	FavHndlr	{ get { return	this._FavHndlr.Value;	} }

			#endregion

			//===========================================================================================
			#region "Methods: Exposed"

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				internal	void Startup()
					{
						this._FavHndlr.Value.Load();
					}

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				internal	void Shutdown()
					{
						this._FavHndlr.Value.Save();
						//...
						Properties.Settings.Default.Save();
					}

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				//internal	IList<string>			GetSAPiniList()		=>	this.NCOCntlr	.GetSAPINIList();
				internal	IList<DTO_WSNode>	GetManifest()			=>	this.XLHndlr	.GetManifest();
				internal	IXMLConfig				CreateXMLConfig()	=>	this.IPXBDCCntlr	.Create_XMLConfig();
				//...
				internal	DTO_WSNode	GetActiveWSNode()	=> this.XLHndlr.GetActiveWSNode();
				//...
				internal	void	WriteRequestToFile( IRequest request , string fullPath )	=> this.IPXBDCCntlr.DispatchRequest_ToFile( request , fullPath );

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				internal IRequest CreateRequest( string ID )
					{
						IList<DTO_WSNode> lt = new List<DTO_WSNode>	{	this.GetActiveWSNode() };
						//.............................................
						return	this.CreateRequest( lt , ID );
					}

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				internal IRequest CreateRequest( IList<DTO_WSNode> wsList , string ID )
					{
						IRequest lo_Reqst		= this.IPXBDCCntlr.Create_Request();
						lo_Reqst.ID	= ID;
						//.............................................
						foreach ( DTO_WSNode lo_Node in wsList )
							{
								ISession		lo_Ssn	= this.IPXBDCCntlr.Create_Session();
								DTO_WSData	lo_WSD	= this.XLHndlr.GetWSData( lo_Node );
								this._BDCHndlr.Value.TransferWSDatatoSession( lo_WSD , lo_Ssn );
								lo_Reqst.Add_Session( lo_Ssn );
							}
						//.............................................
						return	lo_Reqst;
					}

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				internal void WriteConfig( IXMLConfig config , string address = "$A$1" )
					{
						this.XLHndlr.WriteConfig( this.IPXBDCCntlr.SerializeXMLConfig( config ) , address );
					}

			#endregion

			//===========================================================================================
			#region "Methods: Exposed: Request"

			#endregion

		}
}
