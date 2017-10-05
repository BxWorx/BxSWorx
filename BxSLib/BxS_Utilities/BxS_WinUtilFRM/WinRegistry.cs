using Microsoft.Win32;
//_________________________________________________________________________________________________
namespace BxS_WinUtilFRM
{
	public class WinRegistry
		{
			//-------------------------------------------------------------------------------------------
			public void Remove(string registryPath,	string keyName)
				{
					//RegistryKey	rootKey		= this.GetCurrentUser();

					using (RegistryKey rootKey	= this.GetCurrentUser())
						{
							rootKey?.DeleteSubKeyTree(registryPath);
						}
				}

			//-------------------------------------------------------------------------------------------
			public void Write(string registryPath,	string keyName, string value)
				{
					RegistryKey	rootKey		= this.GetCurrentUser();

					using (RegistryKey rk = rootKey.CreateSubKey(registryPath))
						{
							rk.SetValue(keyName, value, RegistryValueKind.String);
						}
				}

			//-------------------------------------------------------------------------------------------
			public string Read(string registryPath,	string keyName, string defaultValue)
				{
					RegistryKey	rootKey		= this.GetCurrentUser();

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

			//-------------------------------------------------------------------------------------------
			private RegistryKey GetCurrentUser()
				{
					return	Registry.CurrentUser;
				}

		}
}
