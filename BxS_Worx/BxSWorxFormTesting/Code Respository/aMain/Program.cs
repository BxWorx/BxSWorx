using System								;
using System.Windows.Forms	;
//.........................................................
using BxS_Worx.Dashboard.UI					;
using BxS_Worx.Dashboard.UI.Window	;
//•••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••
namespace BxSWorxFormTesting
{
	internal static class Program
		{
			//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
			/// <summary>
			/// The main entry point for the application.
			/// </summary>
			[STAThread]
			private static void Main()
				{
					Application.EnableVisualStyles();
					Application.SetCompatibleTextRenderingDefault(false);
					//...
					IDB_ViewConfig		lo_DBVCfg		= DB_Factory.CreateDBViewConfig()												;
					IDB_View					lo_DBView		=	DB_Factory.CreateDBView()															;
					DB_ViewPresenter	lo_DBP			=	DB_Factory.CreateDBPresenter( lo_DBVCfg , lo_DBView )	;
					IDBAssembly				lo_DBAss		= DBAssemblyExcel.Create()															;
					//...
					lo_DBAss.LoadFromSource()		;
					//...
					lo_DBP.Assembly	=	lo_DBAss	;
					lo_DBP.AssembleDashboard()	;
					lo_DBP.Startup()						;
					//...
					if ( lo_DBP.View.ViewForm != null )
						{	Application.Run( lo_DBP.View.ViewForm ); }
				}
		}
	}
