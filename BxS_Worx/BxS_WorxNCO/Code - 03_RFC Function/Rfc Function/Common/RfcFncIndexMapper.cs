using System.Collections.Generic;
//•••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••
namespace BxS_WorxNCO.RfcFunction.Main
{
	internal sealed class RfcFncIndexMapper : IRfcFncIndexMapper
		{
			#region "Constructors"

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				internal RfcFncIndexMapper()
					{
						this.SAPIndex				= new	Dictionary< string , int		>();
						this.PropertyIndex	= new Dictionary< string , string	>();
					}

			#endregion

			//===========================================================================================
			#region "Properties"

				public	string	ReferenceParameterName		{ get; }
				//.................................................
				public	Dictionary< string , int		> SAPIndex				{ get; }
				public	Dictionary<	string , string >	PropertyIndex		{ get; }

			#endregion

			//===========================================================================================
			#region "Methods: Exposed"

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				public void AddMap( string SAPName , string propertyName )
					{
						this.SAPIndex				.Add( SAPName , -1 );
						this.PropertyIndex	.Add( propertyName , SAPName );
					}

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				public int GetPropertyIndex( string propertyName )
					{
						if	(			this.PropertyIndex.TryGetValue( propertyName, out string lc_SAPName )
									&&	this.SAPIndex			.TryGetValue( lc_SAPName	, out int		 ln_Idx			) )
							{
								return	ln_Idx;
							}
						else
							{
								return	-1;
							}
					}

			#endregion

		}
}
