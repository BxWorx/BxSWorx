using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Forms;
//•••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••
namespace BxS_WorxExcel.MVVM
{
	internal abstract class ViewModelBase : INotifyPropertyChanged
		{
			#region "Events"

				public	event	PropertyChangedEventHandler		PropertyChanged;

			#endregion

			//===========================================================================================
			#region "Properties"

				public	Form	View	{ get; set; }

			#endregion

			//===========================================================================================
			#region "Methods: Internal"

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				internal void ToggleView()
					{
						if ( this.View == null )		return;
						//...
						if (this.View.Visible)
							{
								if ( this.View.WindowState.Equals( FormWindowState.Minimized ) )
									{	this.View.WindowState = FormWindowState.Normal; }
								else
									{	this.View.Hide(); }
							}
						else
							{	this.View.Show(); }
					}

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
