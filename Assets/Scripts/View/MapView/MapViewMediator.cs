using strange.extensions.mediation.impl;

public class MapViewMediator : Mediator
{
	[Inject]
	public MapView view { get;set; }

	[Inject]
	public NewMapCreatedSignal mapCreatedSignal { get; set; }

	[Inject]
	public PathFoundSignal pathFoundSignal { get; set; }

	[PostConstruct]
	public void Init()
	{
		mapCreatedSignal.AddListener(OnMapCreated);
		pathFoundSignal.AddListener(OnPathFound);
	}

	void OnMapCreated(NewMapCreatedSignalVO signalVO)
	{
		view.ChangeMapView(signalVO.mapData);
	}

	void OnPathFound(PathFoundSignalVO signalVO)
	{
		view.MarkPath(signalVO.path, signalVO.isCorrupt);
	}

	public override void OnRemove ()
	{
		base.OnRemove ();
		mapCreatedSignal.RemoveListener(OnMapCreated);
		pathFoundSignal.RemoveListener(OnPathFound);
	}
}