using System;
using System.ComponentModel;
using System.Windows.Forms;

namespace BxS_WorxExcel.UI.UC
{
	public class UC_AbstractDescriptionProvider<TAbstract, TBase> : TypeDescriptionProvider
		{
				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				public UC_AbstractDescriptionProvider()
						: base(TypeDescriptor.GetProvider(typeof(TAbstract)))
					{
					}

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				public override Type GetReflectionType(Type objectType, object instance)
					{
						if (objectType == typeof(TAbstract))
								return typeof(TBase);

						return base.GetReflectionType(objectType, instance);
					}

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				public override object CreateInstance(IServiceProvider provider, Type objectType, Type[] argTypes, object[] args)
					{
						if (objectType == typeof(TAbstract))
								objectType = typeof(TBase);

						return base.CreateInstance(provider, objectType, argTypes, args);
					}
		}

		public class AbstractCommunicatorProvider : TypeDescriptionProvider
			 {
					 public AbstractCommunicatorProvider()
							 : base(TypeDescriptor.GetProvider(typeof(UserControl)))
					 {
					 }
					 public override Type GetReflectionType(Type objectType, object instance)
					 {
							 return typeof(UserControl);
					 }
					 public override object CreateInstance(IServiceProvider provider, Type objectType, Type[] argTypes, object[] args)
					 {
							 objectType = typeof(UserControl);
							 return base.CreateInstance(provider, objectType, argTypes, args);
					 }
			 }

}
