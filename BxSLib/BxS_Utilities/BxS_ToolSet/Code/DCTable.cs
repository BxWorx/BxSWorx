using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.Serialization;
//•••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••
namespace BxS_Toolset.DataContainer
{
	[DataContract]

	public class DCTable<T,K> where T : class
		{
			//===========================================================================================
			#region "Constructor"

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				public DCTable(Func<K, T> NewEntry)
					{
						this.DataTable	= new	Dictionary<K, T>();
						this.CreateNew	= NewEntry;
					}

			#endregion

			//===========================================================================================
			#region "Declarations"

											private readonly Func<K, T>				CreateNew;
				[DataMember]	private	readonly Dictionary<K, T>	DataTable;

			#endregion

			//===========================================================================================
			#region "Properties"

				public bool IsDirty { get; private set; }
				public int	Count		{ get { return	this.DataTable.Count; } }

			#endregion

			//===========================================================================================
			#region "Methods: Exposed: Workspace: Item"

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				public bool Exists(K ID)
					{
						return	this.DataTable.ContainsKey(ID);
					}

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				public T Create(Guid ID	= default(Guid))
					{
						return	this.CreateNew(ID);
					}

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				public void Clear()
					{
						if (!this.DataTable.Count.Equals(0))
							{
								this.DataTable.Clear();
								this.IsDirty	= true;
							}
					}

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				public T Get(Guid ID)
					{
						if (!this.DataTable.TryGetValue(ID, out T lo_DTO))
							{	lo_DTO	= this.Create(); }
						//.............................................
						return	lo_DTO;
					}

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				public bool Remove(Guid ID)
					{
						bool lb_Ret	=	this.DataTable.Remove(ID);
						if (lb_Ret)		this.IsDirty	= true;
						return	lb_Ret;
					}

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				public int Remove(IList<Guid> IDList)
					{
						return	this.Remove(IDList, false).Count;
					}

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				public IList<Guid> Remove(IList<Guid> IDList, bool ReturnErrors = true)
					{
						IList<Guid> lt_Ret	= new List<Guid>();
						//.............................................
						foreach (Guid ID in IDList)
							{
								if (this.DataTable.Remove(ID))
									this.IsDirty	= true;
									if (!ReturnErrors)	lt_Ret.Add(ID);
								else
									if (ReturnErrors)		lt_Ret.Add(ID);
							}
						//.............................................
						return	lt_Ret;
					}

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				public bool AddUpdate(Guid Key, T DTO)
					{
						bool	lb_Ret	= false;
						//.............................................
						if (this.DataTable.ContainsKey(Key))
							{
								this.DataTable[Key]	= DTO;
								lb_Ret	= true;
							}
						else
							{
								lb_Ret	= this.DataTable.TryAdd(Key, DTO);
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
				public IList<Guid> KeyListFor(	string PropertyName		= default(string),	Guid ID		= default(Guid) ,
																				string PropertyName1	= default(string),	Guid ID1	= default(Guid)		)
					{
						PropertyInfo	lo_PI		= PropertyName	== null ? null : typeof(T).GetProperty(PropertyName)	;
						PropertyInfo	lo_PI1	= PropertyName1 == null ? null : typeof(T).GetProperty(PropertyName1)	;
						//.............................................
						if (lo_PI == null)
							{	return	this.DataTable.Keys.ToList();	}
						else
							{
								if (lo_PI1 == null)
									{
										return	this.DataTable
															.Where(	kvp => lo_PI.GetValue(kvp.Value).Equals(ID))
																.Select( kvp => kvp.Key)
																	.ToList();
									}
								else
									{
										return	this.DataTable
															.Where(	x =>			lo_PI	.GetValue(x.Value).Equals(ID)
																						&&	lo_PI1.GetValue(x.Value).Equals(ID1)	)
																.Select( kvp => kvp.Key)
																	.ToList();
									}
							}
					}

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				public IList<T> ValueListFor(	string PropertyName		= default(string),	Guid ID		= default(Guid) ,
																			string PropertyName1	= default(string),	Guid ID1	= default(Guid)		)
					{
						PropertyInfo	lo_PI		= PropertyName	== null ? null : typeof(T).GetProperty(PropertyName)	;
						PropertyInfo	lo_PI1	= PropertyName1 == null ? null : typeof(T).GetProperty(PropertyName1)	;
						//.............................................
						if (lo_PI == null)
							{	return	this.DataTable.Values.ToList();	}
						else
							{
								if (lo_PI1 == null)
									{
										return	this.DataTable.Values
															.Where(	x => lo_PI.GetValue(x).Equals(ID))
																.ToList();
									}
								else
									{
										return	this.DataTable.Values
															.Where(	x =>			lo_PI	.GetValue(x).Equals(ID)
																						&&	lo_PI1.GetValue(x).Equals(ID1)	)
																.ToList();
									}
							}
					}

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				public IList<P> List<P>(string PropertyName	= default(string))
					{
						PropertyInfo	lo_PI	= PropertyName	== null ? null : typeof(T).GetProperty(PropertyName)	;
						//.............................................
						if (lo_PI == null)
							{	return	new List<P>();	}
						else
							{
								return	this.DataTable.Values.Select(
																x => (P)lo_PI.GetValue(x) )
																	.Where(x => !x.Equals(default(P)))
																		.Distinct()
																			.ToList();
							}
					}

			#endregion
		}
}