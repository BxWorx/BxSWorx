using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
//•••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••
namespace BxS_WorxExcel.UI
{
	public abstract class NotifyPropertyChangedBase : INotifyPropertyChanged
		{
			#region "Events"

				public event PropertyChangedEventHandler PropertyChanged;

			#endregion

			//===========================================================================================
			#region "Methods: Internal"

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				protected bool SetProperty<T>(	ref	T	storage
																			,			T	value
																			,	[CallerMemberName]	string	propertyName = null)
					{
						if ( EqualityComparer<T>.Default.Equals( storage , value ) )	return false;
						//...
						storage = value;
						PropertyChanged?.Invoke( this , new PropertyChangedEventArgs( propertyName ) );
						return true;
					}

			#endregion

		}
}
