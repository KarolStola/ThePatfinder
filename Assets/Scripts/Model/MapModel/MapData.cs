using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class MapData : IEnumerable<RowData>
{
	private List<RowData> rows;
	private TileData playerTile;
	private TileData finishTile;

	public TileData PlayerTile
	{
		get { return playerTile; }
	}

	public TileData FinishTile
	{
		get { return finishTile; }
	}

	public IEnumerator<RowData> GetEnumerator()
	{
		return rows.GetEnumerator();
	}

	public int Size
	{
		get { return rows.Count; }
	}

	public MapData(int size)
	{
		CreateEmptyMap (size);
		InitNeighboringData();
	}

	void CreateEmptyMap (int size)
	{
		rows = new List<RowData> (size);
		for (int i = 0; i < size; i++)
		{
			CreateNewRow (size);
		}
	}

	public MapData(List<List<int>> saveData)
	{
		CreateEmptyMap(saveData.Count);
		for( int i = 0; i < saveData.Count; i++)
		{
			var saveDataRow = saveData[i];
			for(int j = 0; j < saveDataRow.Count; j++)
			{
				var position = new Position();
				position.column = j;
				position.row = i;
				PutObject((MapObject)saveDataRow[j], position);
			}
		}
		InitNeighboringData();
	}

	void CreateNewRow (int size)
	{
		var row = new RowData (size); 

		for(int i = 0; i < size; i++)
		{
			row.Add(new TileData());
		}

		rows.Add (row);
	}

	void InitNeighboringData()
	{
		for(int row = 0; row < Size; row++)
		{
			for(int column = 0; column < Size; column++)
			{
				InitTileNeighboringData (row, column);
			}
		}
	}

	void InitTileNeighboringData (int row, int column)
	{
		var tileData = GetTileData (row, column);
		tileData.row = row;
		tileData.column = column;

		if (row > 0)
		{
			tileData.neighbors.Add (GetTileData (row - 1, column));
		}
		if (row < Size - 1)
		{
			tileData.neighbors.Add (GetTileData (row + 1, column));
		}
		if (column > 0)
		{
			tileData.neighbors.Add (GetTileData (row, column - 1));
		}
		if (column < Size - 1)
		{
			tileData.neighbors.Add (GetTileData (row, column + 1));
		}
	}

	public RowData GetRowData(int row)
	{
		return rows[row];
	}

	public TileData GetTileData(int row, int column)
	{
		return GetRowData(row)[column];
	}

	public bool ObstacleFits(ObstacleData obstacle, Position position)
	{
		var obstacleMaxRow = GetObstacleMaxPosition( position.row, obstacle.deltaHeight );
		var obstacleMaxColumn = GetObstacleMaxPosition( position.column, obstacle.deltaWidth);

		if(obstacleMaxRow >= Size || obstacleMaxColumn >= Size)
		{
			return false;
		}

		for(var row = position.row; row <= obstacleMaxRow; row++)
		{
			if( !ObstacleFitsInRow(row,position.column, obstacleMaxColumn) )
			{
				return false;
			}
		}

		return true;
	}

	bool ObstacleFitsInRow(int row, int minColum, int maxColumn)
	{
		var rowData = GetRowData(row);

		for(var column = minColum; column <= maxColumn; column++)
		{
			if(rowData[column].objectOnTile != MapObject.EMPTY)
			{
				return false;
			}
		}

		return true;
	}

	public void PutObstacle(ObstacleData obstacle, Position position)
	{
		var obstacleMaxRow = GetObstacleMaxPosition( position.row, obstacle.deltaHeight );
		var obstacleMaxColumn = GetObstacleMaxPosition( position.column, obstacle.deltaWidth);

		for(var row = position.row; row <= obstacleMaxRow; row++)
		{
			for(var column = position.column; column <= obstacleMaxColumn; column++)
			{
				var tileData = GetTileData(row, column);
				tileData.DisconnectFromNeighbors();
				tileData.objectOnTile = MapObject.OBSTACLE;
			}
		}
	}

	int GetObstacleMaxPosition(int position, int deltaPosition)
	{
		return position + deltaPosition;
	}

	public void PutObject(MapObject mapObject, Position position)
	{
		var tileData = GetTileData(position.row, position.column);
		tileData.objectOnTile = mapObject;

		switch (mapObject)
		{
		case MapObject.PLAYER:
			playerTile = tileData;
			break;
		case MapObject.FINISH:
			finishTile = tileData;
			break;
		default:
			break;
		}
	}

	/*
	 *  Functions required by IEnumerable interface.
	 */
	private IEnumerator GetEnumerator1()
	{
		return GetEnumerator();
	}

	IEnumerator IEnumerable.GetEnumerator()
	{
		return GetEnumerator1();
	}
}
