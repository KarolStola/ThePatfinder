using UnityEngine;
using System;
using System.IO;
using System.Collections.Generic;
using strange.extensions.command.impl;

public class SaveMapCommand : Command
{
	[Inject]
	public MapModel mapModel { get; set; }

	[Inject]
	public MapSavedSignal mapSavedSignal { get; set; }

	override public void Execute()
	{
		JSONObject[] jsonMap = new JSONObject[mapModel.MapData.Size];

		int i = 0;
		foreach(var row in mapModel.MapData)
		{
			JSONObject[] jsonRow = new JSONObject[row.Count];
			int j = 0;
			foreach(var tile in row)
			{
				jsonRow[j] = new JSONObject((int)tile.objectOnTile);
				j++;
			}
			jsonMap[i] = new JSONObject(jsonRow);
			i++;
		}

		File.WriteAllText(mapModel.SaveFilePath, new JSONObject(jsonMap).ToString());

		mapSavedSignal.Dispatch();
	}
}
