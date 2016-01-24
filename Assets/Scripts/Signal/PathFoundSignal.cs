using System.Collections.Generic;
using strange.extensions.signal.impl;

public class PathFoundSignal : Signal<PathFoundSignalVO>
{
}

public class PathFoundSignalVO
{
	public readonly List<TileData> path;
	public readonly bool isCorrupt;

	public PathFoundSignalVO(List<TileData> path, bool isCorrupt)
	{
		this.path = path;
		this.isCorrupt = isCorrupt;
	}
}
