using System;
using System.Collections.Generic;
using System.Linq;
//.........................................................
using BxS_Toolset.DataContainer;
//•••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••
namespace BxS_SAPLogon.API
{
	public class Favourites : IFavourites
		{
			#region "Constructors"

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				internal Favourites(	DTController<IDTOFavourite, Guid>	cntlr	,
															int   max				= 3											,
															bool	autoLoad	= true									,
															bool	autoSave	= true										)
					{
						this._DCCntlr		= cntlr;
						this._AutoSave	= autoSave;
						this._Max				= this.CheckMax(max);
						//.............................................
						if (autoLoad)	this.Load();
					}

			#endregion

			//===========================================================================================
			#region "Declarations"

				private readonly	DTController<IDTOFavourite, Guid>	_DCCntlr;
				private readonly	bool		_AutoSave;
				private int _Max;

			#endregion

			//===========================================================================================
			#region "Properties"

				public int Count	{	get { return	this._DCCntlr.DataTable.Count;	} }

				public int Max		{	get { return	this._Max		;	}
														set { this.SetMax(value)	;	} }

			#endregion

			//===========================================================================================
			#region "Methods: Exposed"

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				public	IDTOFavourite Create(Guid ID = default(Guid))
					{
						return	this._DCCntlr.DataTable.Create(ID);
					}

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				public	void Add(IDTOFavourite DTO)
					{
						DTO.SeqNo	= 0;
						this._DCCntlr.DataTable.AddUpdate(DTO.UUID, DTO);
						this.ReSequence();
						if (this._AutoSave)	this.Save();
					}

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				public IList<IDTOFavourite> List()
					{
						return	this._DCCntlr.DataTable
											.ValueListFor()
												.OrderBy(o=>o.SeqNo).ToList();
					}

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				public bool	Save(bool Force = false)
					{
						return	this._DCCntlr.Save(Force);
					}

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				private void Load()
					{
						this._DCCntlr.Load();
						this.ReSequence();
					}

			#endregion

			//===========================================================================================
			#region "Methods: Private"

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				private int CheckMax(int max)
					{
						if (max > 10)	return	10;
						if (max < 1)	return	1;
						return	max;
					}

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				private void SetMax(int value)
					{
						this._Max	= this.CheckMax(value);
						//.............................................
						if (this._DCCntlr.DataTable.Count > value)
							{
								this.ReSequence();
							}
					}

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				private void ReSequence()
					{
						int ln_Cnt	= 0;
						//.............................................
						IList<IDTOFavourite> lt_Lst	= this.List();
						this._DCCntlr.DataTable.Clear();

						foreach (IDTOFavourite lo_Entry in lt_Lst)
							{
								ln_Cnt ++;
								lo_Entry.SeqNo	= ln_Cnt;
								this._DCCntlr.DataTable.AddUpdate(lo_Entry.UUID, lo_Entry);
								if (ln_Cnt >= this.Max)	return;
							}
					}

			#endregion

		}
}