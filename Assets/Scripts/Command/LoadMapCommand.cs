using UnityEngine;
using strange.extensions.command.impl;
using System.IO;
using System.Collections.Generic;

public class LoadMapCommand : Command
{
	[Inject]
	public MapModel mapModel { get; set; }

	[Inject]
	public NewMapCreatedSignal mapCreatedSignal {get; set; }
	
	override public void Execute()
	{
		var save = File.ReadAllText(mapModel.SaveFilePath);
		JSONObject saveJson = new JSONObject(save);

		var list = saveJson.list;

		List<List<int>> parsedSaveData = new List<List<int>>();

		foreach(var row in list)
		{
			var rowList = row.list;
			var parsedRowData = new List<int>();
			foreach(var tile in rowList)
			{
				parsedRowData.Add((int)tile.i);
			}
			parsedSaveData.Add(parsedRowData);
		}
			
		mapModel.MapData = new MapData(parsedSaveData);

		mapCreatedSignal.Dispatch(new NewMapCreatedSignalVO(mapModel.MapData));
	}
}
