using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using strange.extensions.mediation.impl;

public class MapView : View
{
	[SerializeField]
	private GameObject rowPrefab;

	[SerializeField]
	private GameObject tilePrefab;

	private RectTransform rectTransform;
	private VerticalLayoutGroup verticalLayoutGroup;

	override protected void Awake()
	{
		base.Awake();
		rectTransform = GetComponent<RectTransform>();
		verticalLayoutGroup = GetComponent<VerticalLayoutGroup>();
	}

	public void ChangeMapView(MapData mapData)
	{
		Clear();
		CreateMap(mapData);
	}

	void Clear()
	{
		for( int i = 0; i < transform.childCount; i++)
		{
			GameObject.Destroy( transform.GetChild(i).gameObject );
		}
	}

	void CreateMap (MapData mapData)
	{
		var width = rectTransform.rect.width - verticalLayoutGroup.padding.left - verticalLayoutGroup.padding.right;
		var verticalPaddings = verticalLayoutGroup.padding.top + verticalLayoutGroup.padding.bottom;
		var height = (rectTransform.rect.height - verticalPaddings) / mapData.Size;

		foreach (var rowData in mapData)
		{
			CreateRow(rowData, width, height);
		}
	}

	void CreateRow(RowData rowData, float width, float height)
	{
		var row = GetNewRow(rowData, width, height);
		row.transform.SetParent (this.transform, false);
	}

	GameObject GetNewRow(RowData rowData, float width, float height)
	{
		var row = GetEmptyRow(width, height);

		var tileWidth = width / rowData.Count;
		var tileHeight = height;

		foreach (var tileData in rowData)
		{
			var tile = GetNewTile(tileData, tileWidth, tileHeight);
			tile.transform.SetParent(row.transform, false);
		}

		return row;
	}

	GameObject GetEmptyRow(float width, float height)
	{
		var row = GameObject.Instantiate (rowPrefab);
		var rowLayoutElement = row.GetComponent<LayoutElement>();
		rowLayoutElement.preferredWidth = width;
		rowLayoutElement.preferredHeight = height;
		return row;
	}

	GameObject GetNewTile(TileData tileData, float width, float height)
	{
		var tile = GameObject.Instantiate(tilePrefab);
		var tileLayoutElement = tile.GetComponent<LayoutElement>();
		tileLayoutElement.preferredWidth = width;
		tileLayoutElement.preferredHeight = height;

		tile.GetComponent<MapTileView>().SetObject(tileData.objectOnTile);
		return tile;
	}

	public void MarkPath(List<TileData> path, bool isCorrupt)
	{
		ClearPath();
		var mapObject = isCorrupt ? MapObject.CORRUPT_PATH : MapObject.PATH;
		foreach(var tile in path)
		{
			transform.GetChild(tile.row).GetChild(tile.column).GetComponent<MapTileView>().SetObject(mapObject);
		}
	}

	void ClearPath()
	{
		for(int i = 0; i <transform.childCount; i++)
		{
			var row = transform.GetChild(i);
			for(int j = 0; j < row.childCount; j++)
			{
				var tileView = row.GetChild(j).GetComponent<MapTileView>();
				if
				(
					tileView.GetMapObject() == MapObject.PATH
					|| tileView .GetMapObject() == MapObject.CORRUPT_PATH
				)
				{
					tileView.SetObject(MapObject.EMPTY);
				}
			}
		}
	}
}
