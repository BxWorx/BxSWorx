using System;
using System.ComponentModel;
//•••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••
namespace BxS_WorxExcel.DTO
{
	internal struct DTO_WSConfig : INotifyPropertyChanged
		{
			#region "Properties"

				public	Guid	GUID		{ get; set; }

			public event PropertyChangedEventHandler PropertyChanged;

			private	void NotifyPropertyChanged( string name )
				{
					this.PropertyChanged?.Invoke( this , new PropertyChangedEventArgs(name) );
				}

			#endregion

		}
	}
