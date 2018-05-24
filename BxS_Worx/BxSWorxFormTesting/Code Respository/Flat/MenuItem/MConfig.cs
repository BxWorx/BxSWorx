using System.Drawing;
//•••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••
namespace BxS_WorxExcel.UI.Menu
{
	public sealed class MConfig : IMConfig
		{
			#region "Constructors"

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				private MConfig( bool	withDefaults )
					{
						if ( withDefaults )
							{
								this.ColourBack		= Color.FromArgb( 255	, 031 , 031 , 031 )	;
								this.ColourMove		= Color.FromArgb( 255	, 045 , 045 , 045 )	;
								this.ColourFocus	= Color.FromArgb( 150	, 000 , 100 , 000 )	;
								this.ColourSlide	= Color.FromArgb( 150	, 024 , 024 , 024 )	;
							}
					}

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				public	static	IMConfig	CreateBlank()					=>	new	MConfig( false )	;
				public	static	IMConfig	CreateWithDefaults()	=>	new	MConfig( true	)		;

			#endregion

			//===========================================================================================
			#region "Properties"

				public	Color		ColourBack		{ get;  set; }
				public	Color		ColourMove		{ get;  set; }
				public	Color		ColourFocus		{ get;  set; }
				public	Color		ColourSlide		{ get;  set; }

			#endregion


		}
}
