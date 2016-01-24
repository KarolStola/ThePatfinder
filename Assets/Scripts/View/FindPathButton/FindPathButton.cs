using UnityEngine;
using UnityEngine.UI;
using strange.extensions.mediation.impl;

public class FindPathButton : View
{
	[Inject]
	public FindPathButtonClickedSignal buttonClickedSignal { get; set; }

	[Inject]
	public NewMapCreatedSignal mapCreatedSignal { get; set; }

	[SerializeField]
	private PathfindingAlgorithm algorithm;

	[PostConstruct]
	public void Init()
	{
		mapCreatedSignal.AddListener(OnMapCreated);
	}

	void OnMapCreated(NewMapCreatedSignalVO signalVO)
	{
		GetComponent<Button>().interactable = true;
	}

	public void OnClick()
	{
		buttonClickedSignal.Dispatch(algorithm);
	}

	override protected void OnDestroy()
	{
		mapCreatedSignal.RemoveListener(OnMapCreated);
	}
}