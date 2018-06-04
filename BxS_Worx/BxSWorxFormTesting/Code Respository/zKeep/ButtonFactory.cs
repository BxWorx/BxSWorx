using System;
using System.Collections.Generic;
using System.Reflection;
//.........................................................
using BxS_Worx.Dashboard.Utilities;
//•••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••
namespace BxS_Worx.Dashboard.UI.Buttons
{
	public static class ButtonFactory
		{
			#region "Declarations"

				private	static	readonly	Lazy<Dictionary<string , Type>>
					_BtnTypes		= new	Lazy<Dictionary<string, Type>>( ()=> GetManifestOf<IUC_Button>() );

			#endregion

			//===========================================================================================
			#region "Methods: Exposed"

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				public static	IUC_Button CreateButton( string	buttonType )
					{
						if ( ! _BtnTypes.Value.TryGetValue( buttonType , out Type lo_BtnType ) )
							{	return	null; }
						//...
						return	Activator.CreateInstance( lo_BtnType )	as IUC_Button;
					}

			#endregion

			//===========================================================================================
			#region "Methods: Private"

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				private	static Dictionary<string , Type>	GetManifestOf<T>() where T:class
					{
						var lt	=	new Dictionary<string , Type>();
						//...
						foreach ( Type lo_Type in Toolset.TypesImplementingIFaceOf<T>() )
							{
								ButtonTypeAttribute lc_Attr		=	lo_Type.GetCustomAttribute<ButtonTypeAttribute>();
								lt.Add( lc_Attr.BtnType , lo_Type );
							}
						return	lt;
					}

			#endregion

		}
}
