using Microsoft.Win32;

//•••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••

namespace BxS_WinUtilFRM
{
	public class WinRegistryCurrentUser
		{
			#region **[Definitions]**

			#endregion

			//___________________________________________________________________________________________

			#region **[Constructors]**

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				public WinRegistryCurrentUser(	string rootNode					= "Software"	,
																				string applicationName	= "BxSWorx"			)
					{
						this.RootNode					= rootNode;
						this.ApplicationName	= applicationName;
					}

			#endregion

			//___________________________________________________________________________________________

			#region **[Properties]**

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				public string RootNode
					{	get; set; }

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				public string ApplicationName
					{	get; set; }

			#endregion

			//___________________________________________________________________________________________

			#region **[Methods: Exposed]**

				//-------------------------------------------------------------------------------------------
				public bool ExistsPath()
					{
						return	this.ExistsPath(this.Path());
					}

				//-------------------------------------------------------------------------------------------
				public bool ExistsPath(string Path)
					{
						using (RegistryKey rootKey = Registry.CurrentUser)
							{
								return	rootKey?.OpenSubKey(Path, false) != null;
							}
					}

				//-------------------------------------------------------------------------------------------
				public bool Exists(string valueID)
					{
						return	this.Exists(this.Path(), valueID);
					}

				//-------------------------------------------------------------------------------------------
				public bool Exists(string Path,	string valueID)
					{
						using (RegistryKey rootKey = Registry.CurrentUser)
							{
								try
									{
										RegistryKey subKey = rootKey?.OpenSubKey(Path, false);
										return subKey == null ? false : subKey.GetValue(valueID) != null;
									}
								catch (System.Exception)
									{	return false; }
							}
					}

				//-------------------------------------------------------------------------------------------
				public bool RemovePath()
					{
						return	this.RemovePath(this.Path());
					}

				//-------------------------------------------------------------------------------------------
				public bool RemovePath(string Path)
					{
						using (RegistryKey rootKey = Registry.CurrentUser)
							{
								try
									{
										rootKey?.DeleteSubKeyTree(Path);
										return	true;
									}
								catch (System.Exception)
									{	return	false; }
							}
					}

				//-------------------------------------------------------------------------------------------
				public bool Remove(string valueID)
					{
						return	this.Remove(this.Path(), valueID);
					}

				//-------------------------------------------------------------------------------------------
				public bool Remove(string Path, string valueID)
					{
						using (RegistryKey rootKey = Registry.CurrentUser)
							{
								try
									{
										RegistryKey subKey = rootKey?.OpenSubKey(Path, true);
										subKey?.DeleteValue(valueID, true);
										return	true;
									}
								catch (System.Exception)
									{	return	false; }
							}
					}

				//-------------------------------------------------------------------------------------------
				public bool Write<T>(string valueID, T value)
					{
						return	this.Write(this.Path(), valueID, value);
					}

				//-------------------------------------------------------------------------------------------
				public bool Write<T>(string Path,	string valueID, T value)
					{
						using (RegistryKey rootKey = Registry.CurrentUser)
							{
								try
									{
										RegistryKey rk = rootKey.CreateSubKey(Path);
										rk.SetValue(valueID, value);
										return	true;
									}
								catch (System.Exception)
									{	return	false; }
							}
					}

				//-------------------------------------------------------------------------------------------
				public T Read<T>(string valueID, T defaultValue)
					{
						return	this.Read(this.Path(), valueID, defaultValue);
					}

				//-------------------------------------------------------------------------------------------
				public T Read<T>(string Path,	string valueID, T defaultValue)
					{
						using (RegistryKey rootKey = Registry.CurrentUser)
							{
								try
									{
										RegistryKey subKey = rootKey?.OpenSubKey(Path, false);
										return subKey == null ? defaultValue : (T)subKey.GetValue(valueID, defaultValue);
									}
								catch (System.Exception)
									{	return defaultValue; }
							}
					}

			#endregion

			//___________________________________________________________________________________________

			#region **[Methods: Private]**

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				private string Path()
					{
						return	$@"{this.RootNode}\{this.ApplicationName}";
					}

			#endregion

		}
}
