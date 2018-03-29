using System.Collections.Concurrent;
using System.Collections.Generic;
//•••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••
namespace BxS_WorxNCO.Helpers.General
{
	public class PriorityQueue<T> where T : class
		{
			#region "Constructors"

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				public PriorityQueue( int	noQueues = 3 )
					{
						this.MaxQueues	= noQueues	;
						//.............................................
						this.Setup();
					}

			#endregion

			//===========================================================================================
			#region "Declarations"

				private int _QMax;
				//.................................................
				private	Dictionary< int , ConcurrentQueue<T> >	_Queues	;

			#endregion

			//===========================================================================================
			#region "Properties"

				public int MaxQueues	{ get; private set; }
				public int QCount			{ get { return	this._Queues.Count; } }

			#endregion

			//===========================================================================================
			#region "Methods: Exposed"

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				// this result may not be 100% accurate as data is flowing in / out.
				// Must be regarded as a indication only.
				//
				public int Count( int level = -1 )
					{
						if ( level < 0 )
							{
								int ln_Cnt = 0;

								foreach ( KeyValuePair< int , ConcurrentQueue<T> > ls_kvp in this._Queues )
									{
										ln_Cnt += ls_kvp.Value.Count;
									}

								return	ln_Cnt;
							}
						//...............................................
						return	this._Queues[level].Count;
					}

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				public T Peek( int level = 1 )
					{
						T lo_Obj = null;
						//...............................................
						try
							{
								if ( ! this._Queues[level].TryPeek( out lo_Obj ) )
									{
										this._Queues[1].TryPeek( out lo_Obj );
									}
							}
						catch
							{
								this._Queues[0].TryPeek( out lo_Obj );
							}
						//...............................................
						return lo_Obj;
					}

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				public T Get( int level = 1 )
					{
						T lo_Obj = null;
						//...............................................
						try
							{
								if ( ! this._Queues[level].TryDequeue( out lo_Obj ) )
									{
										this._Queues[1].TryDequeue( out lo_Obj );
									}
							}
						catch
							{
								this._Queues[0].TryDequeue( out lo_Obj );
							}
						//...............................................
						return lo_Obj;
					}

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				public void Add( T item , int level = 1 )
					{
						try
							{
								this._Queues[level].Enqueue( item );
							}
						catch
							{
								this._Queues[0].Enqueue( item );
							}
					}

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				public T GetNext()
					{
						int		ln_Trgt	= this._QMax;
						//...............................................
						// Process algorithm for que: 3, 3/2, 3/2/1, 3/2/1/0
						// If entry found, that one is returned and loop
						// is stopped.
						do
							{
								for ( int i = this._QMax; i >= ln_Trgt; i-- )
									{
										if ( this._Queues[i].TryDequeue( out T lo_Obj ) )		return	lo_Obj;
									}

								ln_Trgt --;

							} while ( ln_Trgt >= 0 );
						//...............................................
						return	null;
					}

			#endregion

			//===========================================================================================
			#region "Methods: Private"

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				private void Setup()
					{
						if			( this.MaxQueues < 01 )		this.MaxQueues	= 01	;
						else if ( this.MaxQueues > 10 )		this.MaxQueues	= 10	;

						this._QMax	= this.MaxQueues - 1;
						//.............................................
						this._Queues	= new Dictionary< int , ConcurrentQueue<T> >();

						for ( int i = 0; i <= this.MaxQueues; i++ )
							{
								this._Queues.Add(	i ,	new ConcurrentQueue<T>() );
							}
					}

			#endregion

		}
}