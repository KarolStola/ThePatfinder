using UnityEngine;
using strange.extensions.command.impl;

public class StoreMapSizeSelectionCommand : Command
{
	[Inject]
	public MapSizeSelectorUpdatedSignalVO selectorUpdatedSignalVO { get; set; }

	[Inject]
	public MapModel mapModel { get; set; }

	override public void Execute()
	{
		mapModel.MapSizeSetInView = selectorUpdatedSignalVO.size;
	}
}
