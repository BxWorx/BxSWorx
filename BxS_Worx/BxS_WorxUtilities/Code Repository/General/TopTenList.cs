using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
//•••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••
namespace BxS_WorxUtil.General
{
	[DataContract()]

	public class TopTenList<T>
		{
			#region "Constructors"

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				public TopTenList( int	Size = 10 )
					{
						this.Size		= Size	;
						//.............................................
						this.List		= new	List<T>(Size);
						this.Types	= new	List<Type>() {	typeof( TopTenList<T> ) , typeof( T )	};
					}

			#endregion

			//===========================================================================================
			#region "Declarations"

				private	int	_Size;

			#endregion

			//===========================================================================================
			#region "Properties"

											public	int					Size		{ get { return	this._Size; }
																										set { this.Resize(value); } }

											public	int					Count		{ get { return	this.List.Count; } }
											public	List<Type>	Types		{ get; }
				//...
				[DataMember]	public	IList<T>	List	{ get; }

			#endregion

			//===========================================================================================
			#region "Methods: Exposed"

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				public void Add( T value)
					{
						if ( !this.List.Count.Equals(0) )
							{
								if ( this.List[0].Equals(value) )	return;
								//.............................................
								for ( int i = 1; i < this.Count ; i++ )
									{
										if ( this.List[i].Equals(value) )
											{
												this.List.RemoveAt(i);
												break;
											}
									}
								//.............................................
								if ( this.List.Count.Equals( this.Size ) )
									{
										this.List.RemoveAt( this.Size - 1 );
									}
							}
						//.............................................
						this.List.Insert(0,value);
					}

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				public void Remove( T value)
					{
						try
							{	this.List.RemoveAt( this.List.IndexOf( value ) );	}
						catch
							{	}
					}

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				public void Clear()	=>	this.List.Clear();

			#endregion

			//===========================================================================================
			#region "Methods: Private"

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				private void Resize( int size )
					{
						if ( size < this.Size )
							{
								int s = this.Count - 1;
								for ( int i = s; i >= size; i-- )
									{
										this.List.RemoveAt( i );
									}
							}
						//...
						this._Size	= size;
					}

			#endregion

		}
}