using System.Drawing;
//•••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••
namespace BxS_WorxExcel.UI.Menu
{
	public class MConfig : IMConfig
		{
			#region "Constructors"

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				internal MConfig()
					{
						this.ColourBack		= Color.FromArgb( 255	, 31 , 31 , 31 )	;
						this.ColourMove		= Color.FromArgb( 255	, 45 , 45 , 45 )	;
						this.ColourSlide	= Color.FromArgb( 150	, 24 , 24 , 24 )	;
					}

			#endregion

			//===========================================================================================
			#region "Properties"

				public	Color		ColourBack		{ get;  set; }
				public	Color		ColourMove		{ get;  set; }
				public	Color		ColourSlide		{ get;  set; }

			#endregion

		}
}
