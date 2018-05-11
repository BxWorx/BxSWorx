using System;
using System.Collections.Concurrent;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace BxS_WorxExcel.UI
	{
		public class UIManager
		{
				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
			public UIManager()
			{
				this._SlimLock	=	new	SemaphoreSlim(0,1);
				this.UIs				= new	ConcurrentDictionary<string, IMVVMController>();
			}

				private	readonly SemaphoreSlim _SlimLock;
				private readonly ConcurrentDictionary<string , IMVVMController>	UIs;
				private	int _IsShuttingDown;

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
			public void Shutdown()
				{
					if ( this._IsShuttingDown.Equals(1) )		return;
					//...............................................
					Interlocked.CompareExchange( ref this._IsShuttingDown , true , false );					

				}




				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
			public void	ShowUI( string ID )
				{
					if ( this._IsShuttingDown.Equals(1) )		return;
					//...............................................





					if ( this.UIs.TryGetValue( ID , out IMVVMController lo_MVVM ) )
						{
							//lo_MVVM.ShowDialogue( this , EventArgs.Empty );

						}


					switch ( ID )
						{
							case	"A":
								{
									IMVVMController sapbdc	= new MVVMController_SAPBDC();
									sapbdc.ShuttingDown +=	this.OnShuttingDown;
									this.UIs.Add(ID , sapbdc );

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
