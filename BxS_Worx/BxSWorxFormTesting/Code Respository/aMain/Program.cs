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
					IDBAssembly		lo_DBAss	= DBAssemblyExcel.Create() ;
					lo_DBAss.LoadFromSource()	;
					//...
					DB_Presenter	lo_DBP	=	DB_Factory.CreateDBPresenter() ;
					lo_DBP.AssembleDashboard( lo_DBAss ) ;
					//...
					lo_DBP.Startup() ;
					//...
					if ( lo_DBP.View.ViewForm != null )
						{
							Application.Run( lo_DBP.View.ViewForm )	;
						}
				}
		}
	}
