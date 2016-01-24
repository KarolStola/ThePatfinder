using strange.extensions.signal.impl;

public class NewMapCreatedSignal : Signal<NewMapCreatedSignalVO>
{
}

public class NewMapCreatedSignalVO
{
	public readonly MapData mapData;

	public NewMapCreatedSignalVO(MapData mapData)
	{
		this.mapData = mapData;
	}
}
