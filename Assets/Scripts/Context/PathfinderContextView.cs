using UnityEngine;
using strange.extensions.context.impl;
using System.Collections;

public class PathfinderContextView : ContextView
{
	void Awake()
	{
		this.context = new PathfinderContext(this);
	}
}
