using UnityEngine;
using System.Collections.Generic;

public class AStarFindPathStrategy : DijkstraFindPathStrategy
{
	override protected int GetCheckingPriority(KeyValuePair<TileData, int> tile)
	{
		var priority = tile.Value + GetDistanceBetween(tile.Key, finish);
		return priority < 0 ? int.MaxValue : priority;
	}

	int GetDistanceBetween(TileData tileA, TileData tileB)
	{
		var horizontal = Mathf.Max(tileA.column, tileB.column) - Mathf.Min(tileA.column, tileB.column);
		var vertical = Mathf.Max(tileA.row, tileB.row) - Mathf.Min(tileA.row, tileB.row) - 1;
		return horizontal + vertical;
	}
}
