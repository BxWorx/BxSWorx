using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.Serialization;
using System.Threading;
//•••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••
namespace BxS_Toolset.DataContainer
{
	[DataContract]

	public class DCTable<TCls, TKey> where TCls : class
		{
			//===========================================================================================
			#region "Constructor"

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				public DCTable(Func<TKey, TCls> NewEntry)
					{
						this._CreateNew	= NewEntry;
						//.............................................
						this._DataTable	= new	Dictionary<TKey, TCls>();
					}

			#endregion

			//===========================================================================================
			#region "Declarations"

				private readonly	Lazy<Type>	_Type		= new Lazy<Type>(	() => typeof(TCls),
																																				LazyThreadSafetyMode.None );
				//.................................................
											private readonly	Func				<TKey, TCls>	_CreateNew;
				[DataMember]	private	readonly	Dictionary	<TKey, TCls>	_DataTable;

			#endregion

			//===========================================================================================
			#region "Properties"

				public bool IsDirty { get; private set; }
				public int	Count		{ get { return	this._DataTable.Count; } }

			#endregion

			//===========================================================================================
			#region "Methods: Exposed: Workspace: Item"

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				public void Reload(DCTable<TCls,TKey> DataTab)
					{
						foreach (KeyValuePair<TKey, TCls> ls_Kvp in this._DataTable)
							{
								DataTab.AddUpdate(ls_Kvp.Key, ls_Kvp.Value);
							}
					}

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				public bool Exists(TKey ID)
					{
						return	this._DataTable.ContainsKey(ID);
					}

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				public TCls Create(TKey ID	= default(TKey))
					{
						this.IsDirty	= true;
						return	this._CreateNew(ID);
					}

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				public void Clear()
					{
						if (!this._DataTable.Count.Equals(0))
							{
								this._DataTable.Clear();
								this.IsDirty	= true;
							}
					}

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				public TCls Get(TKey ID)
					{
						if (!this._DataTable.TryGetValue(ID, out TCls lo_DTO))
							{	lo_DTO	= this.Create(); }
						//.............................................
						return	lo_DTO;
					}

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				public bool Remove(TKey ID)
					{
						bool lb_Ret	=	this._DataTable.Remove(ID);
						if (lb_Ret)		this.IsDirty	= true;
						return	lb_Ret;
					}

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				public int Remove(IList<TKey> IDList)
					{
						return	this.Remove(IDList, false).Count;
					}

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				public IList<TKey> Remove(IList<TKey> IDList, bool ReturnErrors = true)
					{
						IList<TKey> lt_Ret	= new List<TKey>();
						//.............................................
						foreach (TKey ID in IDList)
							{
								if (this.Remove(ID))
									{	if (!ReturnErrors)	lt_Ret.Add(ID); }
								else
									if (ReturnErrors)
										{ lt_Ret.Add(ID); }
							}
						//.............................................
						return	lt_Ret;
					}

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				public bool AddUpdate(TKey Key, TCls DTO)
					{
						bool	lb_Ret	= false;
						//.............................................
						if (this._DataTable.ContainsKey(Key))
							{
								this._DataTable[Key]	= DTO;
								lb_Ret	= true;
							}
						else
							{
								lb_Ret	= this._DataTable.TryAdd(Key, DTO);
							}

						if (lb_Ret)		this.IsDirty	= true;
						//.............................................
						return	lb_Ret;
					}

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				public bool IsUsed(Guid ID, string InPropertyName)
					{
						return	this.List<Guid>(InPropertyName).Count(i => i.Equals(ID)).Equals(0);
					}

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				public IList<TKey> KeyListFor<P, P1>(	string PropertyName		= default(string),	P	 ID		= default(P)	,
																							string PropertyName1	= default(string),	P1 ID1	= default(P1)		)
					{
						PropertyInfo	lo_PI		= this.GetPropInfo(PropertyName	);
						PropertyInfo	lo_PI1	= this.GetPropInfo(PropertyName1);
						//.............................................
						if (lo_PI == null)
							{	return	this._DataTable.Keys.ToList();	}
						else
							{
								if (lo_PI1 == null)
									{
										return	this._DataTable
															.Where(	kvp => lo_PI.GetValue(kvp.Value).Equals(ID))
																.Select( kvp => kvp.Key)
																	.ToList();
									}
								else
									{
										return	this._DataTable
															.Where(	x =>			lo_PI	.GetValue(x.Value).Equals(ID)
																						&&	lo_PI1.GetValue(x.Value).Equals(ID1)	)
																.Select( kvp => kvp.Key)
																	.ToList();
									}
							}
					}

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				public IList<TCls> ValueListFor(	string PropertyName		= default(string),	TKey ID		= default(TKey) ,
																					string PropertyName1	= default(string),	TKey ID1	= default(TKey)		)
					{
						PropertyInfo	lo_PI		= this.GetPropInfo(PropertyName	);
						PropertyInfo	lo_PI1	= this.GetPropInfo(PropertyName1);
						//.............................................
						if (lo_PI == null)
							{	return	this._DataTable.Values.ToList();	}
						else
							{
								if (lo_PI1 == null)
									{
										return	this._DataTable.Values
															.Where(	x => lo_PI.GetValue(x).Equals(ID))
																.ToList();
									}
								else
									{
										return	this._DataTable.Values
															.Where(	x =>			lo_PI	.GetValue(x).Equals(ID)
																						&&	lo_PI1.GetValue(x).Equals(ID1)	)
																.ToList();
									}
							}
					}

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				public IList<P> List<P>(string PropertyName	= default(string))
					{
						PropertyInfo	lo_PI		= this.GetPropInfo(PropertyName	);
						//.............................................
						if (lo_PI == null)
							{	return	new List<P>();	}
						else
							{
								return	this._DataTable.Values.Select(
													x => (P)lo_PI.GetValue(x) )
														.Where(x => !x.Equals(default(P)))
															.Distinct()
																.ToList();
							}
					}

			#endregion

			//===========================================================================================
			#region "Methods: Private"

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				private PropertyInfo GetPropInfo(string Name)
					{
						return	Name == null	? null : this._Type.Value.GetProperty(	Name	,
																																						BindingFlags.Instance
																																					| BindingFlags.Public
																																					| BindingFlags.NonPublic	);
					}

			#endregion

		}
}