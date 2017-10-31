using System;
using System.Data;
//•••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••
namespace SAPGUI.USR.DS
{
		internal class DSTable
		{
			#region "Constructors"

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				internal DSTable(References references, DataTable table)
					{
						this._Ref	= references;
						this._Tbl	= table;
					}

			#endregion

			//===========================================================================================
			#region "Declarations"

				private	readonly	References	_Ref;
				private readonly	DataTable		_Tbl;

			#endregion

			//===========================================================================================
			#region "Properties"

				internal	int	Count	{ get	{ return this._Tbl.Rows.Count; } }

			#endregion

			//===========================================================================================
			#region "Methods: Exposed"

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				internal DataRow NewRow()
					{
						return	this._Tbl.NewRow();
					}

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				internal void AddUpdate(DataRow data)
					{
						try
							{
								this._Tbl.Rows.Add(data);
							}
						catch (ConstraintException)
							{
								var			lc_ID		= (Guid)data[this._Ref.UUID];
								DataRow lo_Row	= this.GetRow(lc_ID);
								if (lo_Row == null)
									{ }
								else
									{ }
							}
						finally
							{
								this._Tbl.AcceptChanges();
							}
					}

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				internal DataRow GetRow(Guid keyVal)
					{
						return this._Tbl.Rows.Find(keyVal);
					}

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				internal bool Remove(Guid keyVal)
					{
						DataRow lo_Row	= this.GetRow(keyVal);
						//.............................................
						if (lo_Row == null)	return	false;
						//.............................................
						this._Tbl.Rows.Remove(lo_Row);
						this._Tbl.AcceptChanges();
						return	true;
					}

			#endregion

			//===========================================================================================
			#region "Methods: Private"
			#endregion

		}
}
