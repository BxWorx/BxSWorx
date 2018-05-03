using System.Collections.Generic;
//•••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••
namespace BxS_WorxUtil.General
{
	public class TopTenList<T>
		{
			#region "Constructors"

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				public TopTenList( int	Size = 10 )
					{
						this.Size		= Size	;
						//.............................................
						this._List	= new	List<T>(Size);
					}

			#endregion

			//===========================================================================================
			#region "Declarations"

				private IList<T>	_List;
				private	int	_Size;

			#endregion

			//===========================================================================================
			#region "Properties"

				public	int Size	{ get { return	this._Size; }
														set { this.Resize(value); } }

				public	int	Count	{ get { return	this._List.Count; } }
				//...
				public	IList<T>	List	{ get {	return	this._List; } }

			#endregion

			//===========================================================================================
			#region "Methods: Exposed"

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				public void Add( T value)
					{
						if ( !this._List.Count.Equals(0) )
							{
								if ( this._List[0].Equals(value) )	return;
								//.............................................
								for ( int i = 1; i < this.Count ; i++ )
									{
										if ( this._List[i].Equals(value) )
											{
												this._List.RemoveAt(i);
												break;
											}
									}
								//.............................................
								if ( this._List.Count.Equals( this.Size ) )
									{
										this._List.RemoveAt( this.Size - 1 );
									}
							}
						//.............................................
						this._List.Insert(0,value);
					}

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				public void Remove( T value)
					{
						try
							{	this._List.RemoveAt( this._List.IndexOf( value ) );	}
						catch
							{	}
					}

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				public void Clear()
					{
						this._List.Clear();
					}

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
										this._List.RemoveAt( i );
									}
							}
						//...
						this._Size	= size;
					}

			#endregion

		}
}