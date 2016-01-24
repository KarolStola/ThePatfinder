using UnityEngine;
using strange.extensions.command.impl;

public class CreateMapCommand : Command
{
	[Inject]
	public MapModel mapModel { get; set; }

	[Inject]
	public NewMapCreatedSignal mapCreatedSignal { get; set; }

	private const int MIN_OBSTACLE_DELTA_SIZE = 0;
	private const int OBSTACLE_SIZE_CAP = 2;

	override public void Execute()
	{
		mapModel.MapData = new MapData(mapModel.MapSizeSetInView);

		FillWithObstacles(mapModel.MapData);
		PutObjectRandomly(mapModel.MapData, MapObject.PLAYER);
		PutObjectRandomly(mapModel.MapData, MapObject.FINISH);

		mapCreatedSignal.Dispatch(new NewMapCreatedSignalVO(mapModel.MapData));
	}

	void FillWithObstacles(MapData mapData)
	{
		var obstacleCount = //Mathf.Pow(2, mapData.Size - 6);
			mapData.Size - 1;
		for(int i = 0; i < obstacleCount; i++)
		{
			PutRandomObstacle(mapData);
		}
	}

	void PutRandomObstacle(MapData mapData)
	{
		ObstacleData obstacle;
		Position position;

		do
		{
			obstacle = GetRandomObstacle();
			position = GetRandomPosition(mapData);
		}
		while (!TryPutObstacle(mapData, obstacle, position));
	}
	
	ObstacleData GetRandomObstacle()
	{
		var obstacle = new ObstacleData();
		obstacle.deltaWidth = GetRandomObstacleDimension();
		obstacle.deltaHeight = GetRandomObstacleDimension();
		return obstacle;
	}

	Position GetRandomPosition(MapData mapData)
	{
		var position = new Position();
		position.column = GetRandomPositionDimension(mapData.Size);
		position.row = GetRandomPositionDimension(mapData.Size);
		return position;
	}

	int GetRandomPositionDimension(int size)
	{
		return Random.Range(0, size - 1);
	}

	public int GetRandomObstacleDimension()
	{
		return Random.Range(MIN_OBSTACLE_DELTA_SIZE, OBSTACLE_SIZE_CAP);
	}
	
	bool TryPutObstacle(MapData mapData, ObstacleData obstacle, Position position)
	{
		var putSuccessfully = false;

		if( mapData.ObstacleFits(obstacle, position) )
		{
			mapData.PutObstacle(obstacle, position);
			putSuccessfully = true;
		}

		return putSuccessfully;
	}

	void PutObjectRandomly(MapData mapData, MapObject mapObject)
	{
		Position position;

		do
		{
			position = GetRandomPosition(mapData);
		}
		while (!TryPutObject(mapData, position, mapObject));
	}

	bool TryPutObject(MapData mapData, Position position, MapObject mapObject)
	{
		var tileData = mapData.GetTileData(position.row, position.column);

		if(  tileData.objectOnTile == MapObject.EMPTY )
		{
			mapData.PutObject(mapObject, position);
			return true;
		}

		return false;
	}
}