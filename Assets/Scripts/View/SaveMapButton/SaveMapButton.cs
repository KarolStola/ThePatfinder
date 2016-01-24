using UnityEngine;
using UnityEngine.UI;
using strange.extensions.mediation.impl;

public class SaveMapButton : View
{
	[Inject]
	public NewMapCreatedSignal mapCreatedSignal { get; set; }

	[Inject]
	public SaveMapButtonClickedSignal buttonClickedSignal { get; set; }

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
		buttonClickedSignal.Dispatch();
	}

	override protected void OnDestroy()
	{
		mapCreatedSignal.RemoveListener(OnMapCreated);
	}
}