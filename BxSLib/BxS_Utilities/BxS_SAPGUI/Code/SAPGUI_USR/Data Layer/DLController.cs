﻿using System;
using System.IO;
using System.Data;
//.........................................................
using SAPGUI.COM.DL;
using Toolset.Serialize;
using Toolset.IO;
//•••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••
namespace SAPGUI.USR.DL
{
		internal class DLController
		{
			#region "Constructors"

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				internal DLController(string dirPath, IO FileIO, DCSerializer dcSerializer)
					{
						this._DirPath	= dirPath;
						this._IO	= FileIO;
						this._DCSer	= dcSerializer;
						//.............................................
						this._DCFullName	= Path.Combine(	this._DirPath	,	_DCFileName	);
					}

			#endregion

			//===========================================================================================
			#region "Declarations"

				private const	string	_DCFileName	= "SAPGUI_USR_DataSet.xml"	;
				//.................................................
				private readonly string	_DirPath				;
				private readonly string	_DCFullName			;
				//.................................................
				private readonly IO							_IO;
				private	readonly DCSerializer		_DCSer;
				//.................................................
				private DataSet	_DS;

			#endregion

			//===========================================================================================
			#region "Properties"

				internal bool DCXMLExists	{ get { return	File.Exists(this._DCFullName); } }

			#endregion

			//===========================================================================================
			#region "Methods: Exposed"

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				internal void Save(DataContainer	repository)
					{
						//this.LoadSchema();
						//this._DCSer.ParseRep2DS(repository, this._DS);
						//this.SaveDataset();
					}

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				internal void LoadRepository(DataContainer repository)
					{
						//this.LoadSchema();
						//this.LoadData();
						//this._DCSer.ParseDS2Rep(this._DS, repository);
					}

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				internal void DeleteDatasetXMLFile()
					{
						if (this.DCXMLExists)
							File.Delete(this._DCFullName);
					}

			#endregion

			//===========================================================================================
			#region "Methods: Private"

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				private bool SaveDataset()
					{
						try
							{
								using (var SW = new StreamWriter(this._DCFullName))
									{
										this._DS.WriteXml(SW);
									}
							}
						catch (Exception)
							{
								int x = 1;
								x++;
							}
						return true;
					}

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				private void LoadData()
					{
						try
							{	this._DS.ReadXml(this._DCFullName, XmlReadMode.IgnoreSchema); }
						catch	(System.IO.FileNotFoundException)
							{	/* do nothing as this will be a new repository */ }
					}

			#endregion

		}
}
