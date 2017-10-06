using Microsoft.Win32;
//_________________________________________________________________________________________________
namespace BxS_WinUtilFRM
{
	public class WinRegistry
		{

			//-------------------------------------------------------------------------------------------
			public bool Exists(string registryPath)
				{
					bool	lb_Ret	= false;
					//...............................................
					using (RegistryKey	rootKey		= Registry.CurrentUser)
						{
							if (rootKey?.OpenSubKey(registryPath) != null)	lb_Ret	= true;
						}
					//...............................................
					return	lb_Ret;
				}

			//-------------------------------------------------------------------------------------------
			public void Remove(string registryPath,	string keyName)
				{
					using (RegistryKey rootKey	= Registry.CurrentUser)
						{
							rootKey?.DeleteSubKeyTree(registryPath);
						}
				}

			//-------------------------------------------------------------------------------------------
			public void Write(string registryPath,	string keyName, string value)
				{
					using (RegistryKey	rootKey		= Registry.CurrentUser)
						{
							RegistryKey rk = rootKey.CreateSubKey(registryPath);
							rk.SetValue(keyName, value, RegistryValueKind.String);
						}
				}

			//-------------------------------------------------------------------------------------------
			public string Read(string registryPath,	string keyName, string defaultValue)
				{
					RegistryKey	rootKey		= Registry.CurrentUser;

					using (RegistryKey rk = rootKey.OpenSubKey(registryPath, false))
						{
							if (rk == null)
								{
									return defaultValue;
								}

							var res = rk.GetValue(keyName, defaultValue);
							if (res == null)
								{
									return defaultValue;
								}

							return res.ToString();
						}
				}

		}
}
