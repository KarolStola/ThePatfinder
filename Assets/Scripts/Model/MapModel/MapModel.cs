using UnityEngine;
using System.IO;

public class MapModel
{
	private int mapSizeSetInView;
	private MapData mapData;

	public string SaveFilePath
	{
		get { return Application.persistentDataPath + "/save.txt"; }
	}

	public bool HasSave
	{
		get { return File.Exists(SaveFilePath); }
	}

	public int MapSizeSetInView
	{
		get { return mapSizeSetInView; }
		set { mapSizeSetInView = value; }
	}

	public MapData MapData
	{
		get { return mapData; }
		set { mapData = value; }
	}
}
