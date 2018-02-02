using System.Collections.Generic;
//•••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••
namespace BxS_SAPNCO.API.SAPFunctions
{
	internal class BDCData
		{
			#region "Constructors"

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				internal BDCData()
					{
						this.DataTable	= new	Dictionary<int, BDCEntry>();
						this._Index			= 0;
					}

			#endregion

			//===========================================================================================
			#region "Declarations"

				private int		_Index;

			#endregion

			//===========================================================================================
			#region "Properties"

				public	Dictionary<int, BDCEntry>		DataTable		{ get; }
				public	int													Count				{ get { return	this.DataTable.Count; } }

			#endregion

			//===========================================================================================
			#region "Methods: Exposed"

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				public bool Add(	string	ProgramName	= BDCConstants.lz_E	,
													string	Dynpro			= BDCConstants.lz_D	,
													string	Begin				= BDCConstants.lz_F	,
													string	Field				= BDCConstants.lz_E	,
													string	Value				= BDCConstants.lz_E		)
					{
						return	this.Add(	new BDCEntry(	ProgramName, Dynpro, Begin, Field, Value ) );
					}

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				public bool Add(	string	ProgramName	= BDCConstants.lz_E	,
													int			Dynpro			= 0			,
													bool		Begin				= false	,
													string	Field				= BDCConstants.lz_E	,
													string	Value				= BDCConstants.lz_E		)
					{
						return	this.Add(	new BDCEntry(	ProgramName, Dynpro, Begin, Field, Value ) );
					}

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				public bool	Add(BDCEntry entry)
					{
						this._Index ++;
						this.DataTable.Add(this._Index,	entry);
						return	true;
					}

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				public void	Reset()
					{
						this.DataTable.Clear();
						this._Index	= 0;
					}

			#endregion

		}
}
