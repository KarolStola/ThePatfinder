using System.Collections.Generic;

public interface IFindPathStrategy
{
	List<TileData> FindPath(MapData mapData);
	bool IsCorrupt {get; set;}
}
