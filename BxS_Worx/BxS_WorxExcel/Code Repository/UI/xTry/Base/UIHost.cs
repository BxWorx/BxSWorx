using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using BxS_WorxExcel.MVVM;

namespace BxS_WorxExcel.UI
{
	public class UIHost
		{
				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				public UIHost()
					{
						this.CurrentUIs				= new	ConcurrentDictionary<string, IMvC>();
						this._Locks						= new Dictionary<string, object>();
						this._SlimLock				=	new	SemaphoreSlim(1,1);
						this._IsShuttingDown	= 0;
					}

				//public event  EventHandler Shuttingdown;
				private	readonly SemaphoreSlim _SlimLock;
				private readonly ConcurrentDictionary<string , IMvC>	CurrentUIs;
				private	int _IsShuttingDown;

				private readonly Dictionary<string , object>	_Locks;

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				public void Shutdown()
					{
						if ( this._IsShuttingDown.Equals(1) )		return;
						//await	this._SlimLock.WaitAsync().ConfigureAwait(false);
						this._SlimLock.Wait();
						if ( this._IsShuttingDown.Equals(1) )		return;
						Interlocked.CompareExchange( ref this._IsShuttingDown , 1 , 0 );
						//...............................................
						foreach ( IMvC lo_MVVMC in this.CurrentUIs.Values )
							{
								lo_MVVMC.Shutdown();
							}
						this.CurrentUIs.Clear();
						//...............................................
						this._SlimLock.Release();
					}

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				public void	ShowUI( string ID )
					{
						if ( this._IsShuttingDown.Equals(1) )		return;
						//...............................................
						if ( this.CurrentUIs.TryGetValue( ID , out IMvC lo_MVVMC ) )
							{
								lo_MVVMC.ToggleView();
							}
						else
							{
													switch ( ID )
														{
															case	"A":
																{
																	lo_MVVMC	= new MvC_SAPBDC( ID );
																	if ( this.CurrentUIs.TryAdd(lo_MVVMC.ID , lo_MVVMC) )
																		{
																			lo_MVVMC.ToggleView();
																		}
																	break;
																}

															default:	break;
														}
							}
					}

				////¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				//protected virtual void OnShuttingDown( object sender , EventArgs e )
				//	{
				//	}

		}
}
