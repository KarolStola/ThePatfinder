using strange.extensions.mediation.impl;

public class GenerateMapButton : View
{
	[Inject]
	public GenerateMapButtonClickedSignal buttonClickedSignal { get; set; }

	public void OnClick()
	{
		buttonClickedSignal.Dispatch();
	}
}