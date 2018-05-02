using System;
using System.Collections.Generic;
//.........................................................
using BxS_WorxExcel.DTO;

using BxS_WorxNCO.API;

using BxS_WorxIPX.Main;
using BxS_WorxIPX.BDC;

using static	BxS_WorxExcel.Main.EXL_Constants;
//•••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••
namespace BxS_WorxExcel.Main
{
	internal class BxSExcel
		{
			#region "Constructors"

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				internal BxSExcel()
					{
						this._NCOCntlr	= new	Lazy<INCO_Controller>	(	()=>	NCO_Controller.Instance	, cz_LM )	;
						this._IPXCntlr	= new	Lazy<IIPX_Controller>	(	()=>	IPX_Controller.Instance	, cz_LM )	;
						//...
						this._BDCCntlr	= new	Lazy<IBDC_Controller>	( ()=>	this._IPXCntlr.Value.Create_BDCController() , cz_LM )	;
						//...
						this._XLHndlr		= new Lazy<Excel_Handler>	( ()=>	new	Excel_Handler	()									, cz_LM )	;
						this._BDCHndlr	= new	Lazy<BDC_Handler>		( ()=>	new	BDC_Handler		( this._BDCCntlr )	, cz_LM )	;
					}

			#endregion

			//===========================================================================================
			#region "Declarations"

				private readonly Lazy< INCO_Controller >	_NCOCntlr	;
				private readonly Lazy< IIPX_Controller >	_IPXCntlr	;
				//...
				private readonly Lazy< IBDC_Controller >	_BDCCntlr	;
				//...
				private	readonly	Lazy<Excel_Handler>	_XLHndlr	;
				private	readonly	Lazy<BDC_Handler>		_BDCHndlr	;

			#endregion

			//===========================================================================================
			#region "Properties"

				private INCO_Controller	NCOCntlr	{ get { return	this._NCOCntlr.Value	; } }
				//...
				private IBDC_Controller BDCCntlr	{ get { return	this._BDCCntlr.Value	; } }
				private	Excel_Handler		XLHndlr		{ get { return	this._XLHndlr	.Value	; } }

			#endregion

			//===========================================================================================
			#region "Methods: Exposed"

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				internal	IList<string>			GetSAPiniList()		=>	this.NCOCntlr	.GetSAPINIList();
				internal	IList<DTO_WSNode>	GetManifest()			=>	this.XLHndlr	.GetManifest();
				internal	IXMLConfig				CreateXMLConfig()	=>	this.BDCCntlr	.Create_XMLConfig();
				//...
				internal	DTO_WSNode	GetActiveWSNode()	=> this.XLHndlr.GetActiveWSNode();
				//...
				internal	void	WriteRequestToFile( IRequest request , string fullPath )	=> this.BDCCntlr.DispatchRequest_ToFile( request , fullPath );

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				internal IRequest CreateRequest()
					{
						IList<DTO_WSNode> lt = new List<DTO_WSNode>	{	this.GetActiveWSNode() };
						//.............................................
						return	this.CreateRequest( lt );
					}

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				internal IRequest CreateRequest( IList<DTO_WSNode> wsList )
					{
						IRequest lo_Reqst		= this.BDCCntlr.Create_Request();
						//.............................................
						foreach ( DTO_WSNode lo_Node in wsList )
							{
								ISession		lo_Ssn	= this.BDCCntlr.Create_Session();
								DTO_WSData	lo_WSD	= this.XLHndlr.GetWSData( lo_Node );
								this._BDCHndlr.Value.TransaferWStoSession( lo_WSD , lo_Ssn );
								lo_Reqst.Add_Session( lo_Ssn );
							}
						//.............................................
						return	lo_Reqst;
					}

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				internal void WriteConfig( IXMLConfig config , string address = "$A$1" )
					{
						this.XLHndlr.WriteConfig( this.BDCCntlr.SerializeXMLConfig( config ) , address );
					}

			#endregion

		}
}
