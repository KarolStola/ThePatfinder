using UnityEngine;
using UnityEngine.UI;
using strange.extensions.mediation.impl;

public class LoadMapButton : View
{
	[Inject]
	public LoadMapButtonClickedSignal buttonClickedSignal { get; set; }

	[Inject]
	public MapModel mapModel { get; set; }

	[Inject]
	public MapSavedSignal mapSavedSignal { get; set; }

	private Button button;

	[PostConstruct]
	public void Init()
	{
		button = GetComponent<Button>();
		button.interactable = mapModel.HasSave;
		mapSavedSignal.AddListener(OnGameSaved);
	}

	void OnGameSaved()
	{
		button.interactable = true;
	}

	public void OnClick()
	{
		buttonClickedSignal.Dispatch();
	}

	override protected void OnDestroy()
	{
		mapSavedSignal.RemoveListener(OnGameSaved);
	}
}