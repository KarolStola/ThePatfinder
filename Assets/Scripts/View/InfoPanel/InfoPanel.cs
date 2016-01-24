using UnityEngine;
using strange.extensions.mediation.impl;

public class InfoPanel : View
{
	public void OnOutsideClicked()
	{
		gameObject.SetActive(false);
	}

	public void OnInfoButtonClicked()
	{
		gameObject.SetActive(true);
	}
}