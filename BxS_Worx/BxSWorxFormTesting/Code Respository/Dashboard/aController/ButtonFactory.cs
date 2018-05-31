using System;
using System.Collections.Generic;
using System.Linq;
//•••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••
namespace BxS_Worx.UI.Dashboard
{
	public static class ButtonFactory
		{
				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				public static List<string> GetAllEntities()
					{
						return	AppDomain.CurrentDomain.GetAssemblies()
											.SelectMany(x => x.GetTypes())
												.Where(x => typeof(IUC_BtnBase).IsAssignableFrom(x) && !x.IsInterface && !x.IsAbstract)
													.Select(x => x.Name).ToList();
					}

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				public static IUC_BtnBase Get(string buttonType)
				{


						var types		= typeof(IUC_BtnBase).Assembly.GetTypes()
														.Where( t	=>		! t.IsAbstract
																					&&	t.IsSubclassOf( typeof(IUC_BtnBase) ) )
															.ToList();
						//...
						IUC_BtnBase position = null;
						//...
						//foreach( Type lo_BtnType in types)
						//{
						//	 lo_BtnType.GetCustomAttributes<ButtonTypeAttribute>();

						//	 if(lo_BtnType .ty  .PositionId == id)
						//	 {
						//			 position = Activator.CreateInstance(lo_BtnType) as Position;
						//			 break;
						//	 }
						//}

						//if(position == null)
						//{
						//		var message = $"Could not find a Position to create for id {id}.";
						//		throw new NotSupportedException(message);
						//}

						return	position;
				}

		}
}
