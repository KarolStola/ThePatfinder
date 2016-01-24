using UnityEngine;
using System.Collections.Generic;

public class DijkstraFindPathStrategy : IFindPathStrategy
{
	Dictionary<TileData, int> distancesToCheck = new Dictionary<TileData, int>();
	Dictionary<TileData, int> foundDistances = new Dictionary<TileData, int>();
	Dictionary<TileData, TileData> predecessors = new Dictionary<TileData, TileData>();

	protected TileData source;
	protected TileData finish;
	public bool IsCorrupt { get; set; }

	public List<TileData> FindPath(MapData mapData)
	{
		InitAlgorithm(mapData);
		FindPaths();
		return GetPathToFinish();
	}

	void InitAlgorithm (MapData mapData)
	{
		source = mapData.PlayerTile;
		finish = mapData.FinishTile;

		foreach(var row in mapData)
		{
			foreach(var tile in row)
			{
				distancesToCheck.Add(tile, tile == mapData.PlayerTile ? 0 : int.MaxValue);
				predecessors.Add(tile, null);
			}
		}
	}

	void FindPaths()
	{
		var closestUnchecked = PopClosestUnchecked();

		while(closestUnchecked.Key != null && closestUnchecked.Value != int.MaxValue)
		{
			foundDistances.Add(closestUnchecked.Key, closestUnchecked.Value);

			CheckNeighbors (closestUnchecked);

			if(closestUnchecked.Key == finish)
			{
				break;
			}
			closestUnchecked = PopClosestUnchecked();
		}
	}


	KeyValuePair<TileData, int> PopClosestUnchecked()
	{
		KeyValuePair<TileData, int> closest = new KeyValuePair<TileData, int>();

		foreach(var distanceToCheck in distancesToCheck)
		{
			if(closest.Key == null || distanceToCheck.Value < closest.Value)
			{
				closest = distanceToCheck;
			}
		}
		if(closest.Key != null)
		{
			distancesToCheck.Remove(closest.Key);
		}
		return closest;
	}

	void CheckNeighbors (KeyValuePair<TileData, int> closestUnchecked)
	{
		foreach (var neighbor in closestUnchecked.Key.neighbors)
		{
			var checkingPriority = GetCheckingPriority(closestUnchecked);
			if (distancesToCheck.ContainsKey (neighbor) && distancesToCheck [neighbor] > checkingPriority + 1)
			{
				distancesToCheck [neighbor] = checkingPriority + 1;
				predecessors [neighbor] = closestUnchecked.Key;
			}
		}
	}

	List<TileData> GetPathToFinish()
	{
		IsCorrupt = false;
		var path = new List<TileData>();
		var tile = predecessors[finish];

		while(tile != source)
		{
			if(tile == null)
			{
				IsCorrupt = true;
				break;
			}
			path.Add(tile);
			tile = predecessors[tile];
		}

		if(IsCorrupt)
		{
			path.Clear();
			foreach(var distance in foundDistances)
			{
				if(distance.Value != int.MaxValue && distance.Key != source)
				{
					path.Add(distance.Key);
				}
			}
		}

		return path;
	}

	protected virtual int GetCheckingPriority(KeyValuePair<TileData, int> tile)
	{
		return tile.Value;
	}
}
