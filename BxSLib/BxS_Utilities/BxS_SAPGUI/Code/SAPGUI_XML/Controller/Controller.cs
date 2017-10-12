using System;
using System.Collections.Generic;
using System.Text;
//•••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••
namespace SAPGUI.XML
{
	internal class Controller
		{
			private string	cc_FullPath;

			private Lazy<Loader> _loader	= new Lazy<Loader>();




			internal Controller(string fullPath)
				{
					this.cc_FullPath	= fullPath;
				}
		}
}
