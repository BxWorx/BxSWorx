using System;
using System.Collections.Generic;
//.........................................................
using BxS_WorxIPX.BDC;

using BxS_WorxUtil.General;
using BxS_WorxUtil.Main;

using static	BxS_WorxIPX.Main.IPX_Constants;
//•••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••
namespace BxS_WorxIPX.Main
{
	public sealed class IPX_Controller : IIPX_Controller
		{
			#region "Constructors: Singleton"

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				private	static readonly	Lazy< IPX_Controller >	_Instance		= new		Lazy< IPX_Controller >( ()=>	new IPX_Controller() , cz_LM );
				public	static					IPX_Controller					Instance	{	get { return _Instance.Value; }	}

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				private IPX_Controller()
					{
						this._WSParser	= new	Lazy< ExcelBDC_Parser >( ()=>	new	ExcelBDC_Parser()		);
						this._UtlCntlr	= new	Lazy< IUTL_Controller >( ()=>	UTL_Controller.Instance );
					}

			#endregion

			//===========================================================================================
			#region "Properties"

				private	IO					IO					{ get	{ return	this._UtlCntlr.Value.IO					; } }
				private	Serializer	Serializer	{ get	{ return	this._UtlCntlr.Value.Serializer	; } }

			#endregion

			//===========================================================================================
			#region "Declarations"

				private	readonly	Lazy< ExcelBDC_Parser >	_WSParser;
				private	readonly	Lazy< IUTL_Controller >	_UtlCntlr;

			#endregion

			//===========================================================================================
			#region "Methods: Exposed"

				public	IExcelBDCSessionResult		CreateBDCSessionResult	()=> new ExcelBDCSessionResult	();

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				public	IExcelBDC_Config		Create_BDCConfig	()	=>	new	ExcelBDC_Config		();
				public	IExcelBDC_Logon			Create_Logon			()	=>	new	ExcelBDC_Logon		();

				public	IExcelBDC_WS				Create_ExcelBDCWS				()	=>	new ExcelBDC_WS				();
				public	IExcelBDC_Request		Create_ExcelBDCRequest	()	=>	new ExcelBDC_Request	();

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				public IExcelBDC_Request ParseWStoRequest( IExcelBDC_WS worksheet )
					{
						IExcelBDC_Request	lo_Req	= this.Create_ExcelBDCRequest();
						this._WSParser.Value.ParseWStoRequest( worksheet , lo_Req );
						return	lo_Req;
					}

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				public bool ExcelBDCWStoRequestXMLFile( IExcelBDC_WS worksheet , string pathName )
					{
						bool lb_Ret	= false;
						//.............................................
						try
							{
								IExcelBDC_Request lo_Req	= this.ParseWStoRequest( worksheet );
								//...
								var			lt			= new List<Type>	{	typeof(ExcelBDC_Request) };
								string	lc_XML	=	this.Serializer.Serialize( lo_Req , lt );
								//...
								this.IO.WriteFile( pathName , lc_XML );
								//...
								lb_Ret	= true;
							}
						catch (Exception ex)
							{
								throw	new	Exception( "Excel WS to XML file failure" , ex );
							}
						//.............................................
						return	lb_Ret;
					}

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				public IExcelBDC_Request XMLFiletoExcelBDCRequest( string pathName )
					{
						try
							{
								string	lc_XML	= this.IO.ReadFile( pathName );
								var			lt			= new List<Type>	{	typeof(ExcelBDC_Request) };
								return	this.Serializer.DeSerialize<IExcelBDC_Request>( lc_XML , lt );
							}
						catch (Exception ex)
							{
								throw	new	Exception("XML to Request Failure" , ex );
							}
					}

			#endregion

		}
}