using UnityEngine;
using strange.extensions.command.impl;

public class FindPathCommand : Command
{
	[Inject]
	public MapModel mapModel { get; set; }

	[Inject]
	public PathFoundSignal pathFoundSignal { get; set; }

	[Inject]
	public PathfindingAlgorithm pathfindingAlgorithm { get; set; }

	override public void Execute()
	{
		var findPathStartegy = GetPathfindingStrategy(pathfindingAlgorithm);
		var path = findPathStartegy.FindPath(mapModel.MapData);
		pathFoundSignal.Dispatch(new PathFoundSignalVO(path, findPathStartegy.IsCorrupt) );
	}

	IFindPathStrategy GetPathfindingStrategy(PathfindingAlgorithm algorithm)
	{
		switch (algorithm)
		{
		case PathfindingAlgorithm.DIJKSTRA:
			return new DijkstraFindPathStrategy();
		case PathfindingAlgorithm.A_STAR:
			return new AStarFindPathStrategy();
		default:
			return null;
		}
	}
}
