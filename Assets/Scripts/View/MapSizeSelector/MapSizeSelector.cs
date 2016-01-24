using UnityEngine;
using System;
using UnityEngine.UI;
using strange.extensions.mediation.impl;

public class MapSizeSelector : View
{
	[SerializeField]
	private int minSize;

	[SerializeField]
	private InputField sizeSelectionField;

	[Inject]
	public MapSizeSelectorUpdatedSignal selectorUpdatedSignal { get; set; }

	private int previousValue;

	[PostConstruct]
	public void Init()
	{
		CheckAndSetValue(minSize);
	}

	private void CheckAndSetValue(int value)
	{
		if(value < minSize)
		{
			value = minSize;
		}
			
		sizeSelectionField.text = value.ToString();
		previousValue = value;
		selectorUpdatedSignal.Dispatch(new MapSizeSelectorUpdatedSignalVO(value));
	}

	public void OnEndEdit(string stringValue)
	{
		int value;

		if(int.TryParse(stringValue, out value))
		{
			CheckAndSetValue(value);
		}
		else
		{
			CheckAndSetValue(previousValue);
		}
	}

	public void OnIncrement()
	{
		CheckAndSetValue(previousValue+1);
	}

	public void OnDecrement()
	{
		CheckAndSetValue(previousValue-1);
	}
}
