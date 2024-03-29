using Godot;
using static Godot.Node;

namespace Gamedev.Main.Extensions
{
	public static class NodeExtensions
	{
		public static void SetProcessModeDeferred(this Node node, ProcessModeEnum mode)
		{
			node.CallDeferred(Node.MethodName.SetProcessMode, Variant.From(ProcessModeEnum.Disabled));
		}
	}
}