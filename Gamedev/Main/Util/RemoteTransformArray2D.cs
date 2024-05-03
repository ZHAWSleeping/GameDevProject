using Godot;
using System;
using System.Linq;

public partial class RemoteTransformArray2D : Node2D
{
	[Export]
	public Godot.Collections.Array<NodePath> NodePaths = new();

	public override void _Ready()
	{
		if (NodePaths.Any())
			PopulateTransformers(NodePaths.ToArray());
	}

	public void PopulateTransformers(NodePath[] paths)
	{
		GetChildren().ToList().ForEach(c => c.QueueFree());
		paths.ToList().ForEach(path =>
		{
			RemoteTransform2D transform = new() { RemotePath = path };
			AddChild(transform);
		});
	}
}
