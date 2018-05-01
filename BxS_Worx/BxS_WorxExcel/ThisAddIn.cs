using System;
//.........................................................
using BxS_WorxExcel.Main;
using BxS_WorxIPX.Main;
using BxS_WorxNCO.API;

using static	BxS_WorxExcel.Main.EXL_Constants;
//•••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••
namespace BxS_WorxExcel
{
	public partial class ThisAddIn
		{
			#region "Declarations"

				//internal Lazy<Handler_Excel>		_ExlHndlr	;
				//internal Lazy< IIPX_Controller >		_IPXCntlr	;

			#endregion

			//===========================================================================================
			#region "Methods: Private"

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				private void ThisAddIn_Startup(object sender, System.EventArgs e)
					{
						//this._ExlHndlr	= new	Lazy<Handler_Excel>	(	()=>	new	Handler_Excel( Globals.ThisAddIn.Application ) , cz_LM );
						//this._IPXCntlr	= new	Lazy<IIPX_Controller>	(	()=>	IPX_Controller.Instance	, cz_LM );
					}

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				private void ThisAddIn_Shutdown(object sender, System.EventArgs e)
					{
					}

			#endregion

			//===========================================================================================
			#region "VSTO generated code"

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				/// <summary>
				/// Required method for Designer support - do not modify
				/// the contents of this method with the code editor.
				/// </summary>
				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				private void InternalStartup()
					{
						#pragma warning	disable RCS1114
							this.Startup		+= new	System.EventHandler( this.ThisAddIn_Startup	 );
							this.Shutdown		+= new	System.EventHandler( this.ThisAddIn_Shutdown );
						#pragma warning restore	RCS1114
					}

			#endregion

		}
}
