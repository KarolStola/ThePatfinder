using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using strange.extensions.mediation.impl;

public class MapTileView : View
{
	[SerializeField]
	private Image objectImage;

	[SerializeField]
	private List<Sprite> obstacles;

	[SerializeField]
	private Sprite playerSprite;

	[SerializeField]
	private Sprite finishSprite;

	[SerializeField]
	private Sprite pathSprite;

	[SerializeField]
	private Sprite corruptPathSprite;

	private MapObject mapObject = MapObject.EMPTY;

	public MapObject GetMapObject()
	{
		return mapObject;
	}

	public void SetObject(MapObject mapObject)
	{
		this.mapObject = mapObject;

		if(mapObject == MapObject.EMPTY)
		{
			objectImage.enabled = false;
		}
		else
		{
			objectImage.enabled = true;
			objectImage.sprite = GetSprite(mapObject);
		}
	}

	Sprite GetSprite(MapObject mapObject)
	{
		switch (mapObject)
		{
		case MapObject.PLAYER:
			return playerSprite;
		case MapObject.OBSTACLE:
			return GetRandomObstacle();
		case MapObject.FINISH:
			return finishSprite;
		case MapObject.PATH:
			return pathSprite;
		case MapObject.CORRUPT_PATH:
			return corruptPathSprite;
		default:
			return null;
		}
	}

	Sprite GetRandomObstacle()
	{
		return obstacles[Random.Range(0, obstacles.Count)];
	}
}

