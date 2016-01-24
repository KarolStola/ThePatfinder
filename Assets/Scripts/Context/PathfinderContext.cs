using UnityEngine;
using strange.extensions.context.impl;
using strange.extensions.command.api;
using strange.extensions.command.impl;

public class PathfinderContext : MVCSContext
{

	public PathfinderContext(MonoBehaviour view)
		:base(view)
	{
	}

	override protected void addCoreComponents()
	{
		base.addCoreComponents();
		injectionBinder.Unbind<ICommandBinder>();
		injectionBinder.Bind<ICommandBinder>().To<SignalCommandBinder>().ToSingleton();
	}

	override protected void mapBindings()
	{
		BindSignals();
		BindModels();
		BindCommands();
		BindMediators();
	}

	void BindSignals()
	{
		injectionBinder.Bind<MapSizeSelectorUpdatedSignal> ().ToSingleton ();
		injectionBinder.Bind<NewMapCreatedSignal>().ToSingleton();
		injectionBinder.Bind<GenerateMapButtonClickedSignal>().ToSingleton();
		injectionBinder.Bind<FindPathButtonClickedSignal>().ToSingleton();
		injectionBinder.Bind<PathFoundSignal>().ToSingleton();
		injectionBinder.Bind<SaveMapButtonClickedSignal>().ToSingleton();
		injectionBinder.Bind<LoadMapButtonClickedSignal>().ToSingleton();
		injectionBinder.Bind<MapSavedSignal>().ToSingleton();
	}

	void BindModels()
	{
		injectionBinder.Bind<MapModel>().ToSingleton();
	}

	void BindCommands()
	{
		commandBinder.Bind<MapSizeSelectorUpdatedSignal>().To<StoreMapSizeSelectionCommand>();
		commandBinder.Bind<GenerateMapButtonClickedSignal>().To<CreateMapCommand>();
		commandBinder.Bind<FindPathButtonClickedSignal>().To<FindPathCommand>();
		commandBinder.Bind<SaveMapButtonClickedSignal>().To<SaveMapCommand>();
		commandBinder.Bind<LoadMapButtonClickedSignal>().To<LoadMapCommand>();
	}

	void BindMediators()
	{
		mediationBinder.Bind<MapView>().To<MapViewMediator>();
	}
}
