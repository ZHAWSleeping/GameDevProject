using Godot;
using System;
using System.Linq;

public partial class RemoteTransformArray2D : Node2D
{
	[Export]
	public Godot.Collections.Array<NodePath> NodePaths = new();

	public override void _Ready()	
	{
		NodePaths.ToList().ForEach(path => {
			RemoteTransform2D transform = new();
			transform.RemotePath = path;
			AddChild(transform);
		});
	}
}
