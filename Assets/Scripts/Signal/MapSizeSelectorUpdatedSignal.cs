using strange.extensions.signal.impl;

public class MapSizeSelectorUpdatedSignal : Signal<MapSizeSelectorUpdatedSignalVO>
{
}

public class MapSizeSelectorUpdatedSignalVO
{
	public readonly int size;

	public MapSizeSelectorUpdatedSignalVO(int size)
	{
		this.size = size;
	}
}