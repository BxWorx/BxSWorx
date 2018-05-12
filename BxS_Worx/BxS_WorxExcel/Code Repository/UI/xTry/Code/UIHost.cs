using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace BxS_WorxExcel.UI
{
	public class UIHost
		{
				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				public UIHost()
					{
						this.CurrentUIs				= new	ConcurrentDictionary<string, IMVVMController>();
						this._Locks						= new Dictionary<string, object>();
						this._SlimLock				=	new	SemaphoreSlim(0,1);
						this._IsShuttingDown	= 0;
					}

				//public event  EventHandler Shuttingdown;



				private	readonly SemaphoreSlim _SlimLock;
				private readonly ConcurrentDictionary<string , IMVVMController>	CurrentUIs;
				private	int _IsShuttingDown;

				private readonly Dictionary<string , object>	_Locks;


				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				public async Task Shutdown()
					{
						if ( this._IsShuttingDown.Equals(1) )		return;
						await	this._SlimLock.WaitAsync().ConfigureAwait(false);
						if ( this._IsShuttingDown.Equals(1) )		return;
						Interlocked.CompareExchange( ref this._IsShuttingDown , 1 , 0 );
						//...............................................



						//...............................................
						this._SlimLock.Release();
					}




				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				public void	ShowUI( string ID )
					{
						if ( this._IsShuttingDown.Equals(1) )		return;
						//...............................................





						if ( this.CurrentUIs.TryGetValue( ID , out IMVVMController lo_MVVM ) )
							{
								//lo_MVVM.ShowDialogue( this , EventArgs.Empty );
							}


						switch ( ID )
							{
								case	"A":
									{
										IMVVMController sapbdc	= new MVVMController_SAPBDC();
										sapbdc.ShuttingDown +=	this.OnShuttingDown;
										//this.CurrentUIs.Add(ID , sapbdc );

										break;
									}

								default:	break;
							}

					}

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				protected virtual void OnShuttingDown( object sender , EventArgs e )
					{
					}

		}
}
