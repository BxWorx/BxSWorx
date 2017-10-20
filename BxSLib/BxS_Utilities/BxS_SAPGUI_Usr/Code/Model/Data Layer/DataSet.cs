using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace BxS_SAPGUI_Usr.Code.Model.Data_Layer
{
		internal class DataSetCreate
		{
			#region "Declarations"

				private readonly DataSet	_sapgui;

			#endregion

			//===========================================================================================
			#region "Constructors"

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				internal DataSetCreate()
					{
						this._sapgui	= new DataSet();
						//.............................................
						this.AddMsgServer();
					}

			#endregion

			//===========================================================================================
			#region "Methods: Private"

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				private void AddMsgServer()
					{
						var MsgServer	= new DataTable("MsgServer");
						//.............................................
						var UUID			= new DataColumn("UUID", typeof(string))
															{	Unique = true	};
						MsgServer.Columns.Add(UUID);
						//.............................................
						MsgServer.Columns.Add("Name"				, typeof(string));
						MsgServer.Columns.Add("Port"				, typeof(string));
						MsgServer.Columns.Add("Description"	, typeof(string));
					}

			#endregion

		}
}
