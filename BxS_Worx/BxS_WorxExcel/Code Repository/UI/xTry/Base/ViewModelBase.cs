using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
//•••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••
namespace BxS_WorxExcel.MVVM
{
	internal abstract class ViewModelBase : INotifyPropertyChanged
		{
			#region "Constructors"

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				internal ViewModelBase( IViewHandler	viewHandler )
					{
						this.ViewHandler	= viewHandler;
					}

			#endregion

			//===========================================================================================
			#region "Events"

				public	event	PropertyChangedEventHandler		PropertyChanged;

			#endregion

			//===========================================================================================
			#region "Properties"

				internal	IViewHandler	ViewHandler	{ get; }

			#endregion

			//===========================================================================================
			#region "Methods: Internal"

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				protected bool SetProperty<T>(	ref	T	storage
																			,			T	value
																			,	[CallerMemberName]	string	propertyName = null)
					{
						if ( EqualityComparer<T>.Default.Equals( storage , value ) )
							{	return false; }
						//...
						storage	= value;
						PropertyChanged?.Invoke( this , new PropertyChangedEventArgs( propertyName ) );
						return	true;
					}

			#endregion

		}
}
