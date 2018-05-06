using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using BxS_WorxExcel.DTO;

namespace BxS_WorxExcel.Code_Repository.UI.User_Controls
	{
	public partial class Form1 : Form
		{

			private	DTO_WSConfig x = new DTO.DTO_WSConfig();


		public Form1()
			{
				InitializeComponent();

				this.xtbx.DataBindings.Add( new Binding( "Text" , this.x , "GUID" ,  true , DataSourceUpdateMode.OnValidation ) );

			}

		private void Button1_Click(object sender , EventArgs e)
			{
				this.x.GUID	= Guid.NewGuid();
				//this.xUC_WSConfig.LoadData( this.x );
			}
		}
	}
