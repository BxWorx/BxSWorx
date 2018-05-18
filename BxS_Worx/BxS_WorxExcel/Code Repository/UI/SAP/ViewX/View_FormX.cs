using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Forms;
using BxS_WorxIPX.NCO;

namespace BxS_WorxExcel.UI
	{
	public partial class FormX : Form , INotifyPropertyChanged
		{

				private	List<IDTO_Session>					List		{ get;  set; }
				private	BindingList<IDTO_Session>		BDCList		{ get; }
				private readonly BindingSource			BDC_BS;

		public FormX()
			{
				InitializeComponent();

				this.BDC_BS		= new BindingSource();
				this.BDCList	= new	BindingList<IDTO_Session>();
				this.BDC_BS.DataSource	= this.BDCList;
				this.xdgv_Main.DataSource	= this.BDC_BS;

				//this.xtbx_Test.DataBindings.Add("Text", this.BDCList, "UserID");
				this.xtbx_Test.DataBindings.Add("Text", this , "MyText" , false , DataSourceUpdateMode.OnPropertyChanged );
			}

				public	event	PropertyChangedEventHandler		PropertyChanged;


		//....
		private void LoadList()
			{
				//this.List	= new	List<IDTO_Session>();

					for ( int i = 0; i < 10; i++ )
						{
							var d		= new DTO_Session	{
																					UserID       = $"User-{i.ToString()}" ,
																					SessionName  = $"Session-{i.ToString()}" ,
																					CreationDate = DateTime.Today ,
																					CreationTime = new TimeSpan(DateTime.Now.Hour , DateTime.Now.Minute , DateTime.Now.Second) ,
																					SAPTCode     = $"SAPTCde-{i.ToString()}"
																				};

							this.BDCList.Add( d );
						}
			}



		private String _MyText;

		public string	MyText	{ get	=>	this._MyText;
														set	=>	this.SetProperty(ref this._MyText , value); }

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				private bool SetProperty<T>(	ref	T	storage
																			,			T	value
																			,	[CallerMemberName]	string	propertyName = ""	)
					{
						if ( EqualityComparer<T>.Default.Equals( storage , value ) )		{	return false; }
						//...
						storage	= value;
						PropertyChanged?.Invoke( this , new PropertyChangedEventArgs( propertyName ) );
						return	true;
					}


		private void button1_Click(object sender , EventArgs e)
			{
				if ( this._MyText == null )	this._MyText	= "Z";
				this.MyText	=	this._MyText.Equals("A") ? "B" : "A";
				this.xtbx_Lab.Text	= this._MyText;
			}

		private void button2_Click(object sender , EventArgs e)
			{
				this.LoadList();
			}
		//this.BDCList	= new	BindingList<IDTO_Session>( this.List );
		}
}
