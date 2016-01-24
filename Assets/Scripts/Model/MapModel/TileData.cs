using System.Collections.Generic;

public class TileData
{
	public MapObject objectOnTile = MapObject.EMPTY;
	public HashSet<TileData> neighbors = new HashSet<TileData>();
	public int row;
	public int column;

	public void DisconnectFromNeighbors()
	{
		foreach(var neighbor in neighbors)
		{
			neighbor.neighbors.Remove(this);
		}
		neighbors.Clear();
	}
}