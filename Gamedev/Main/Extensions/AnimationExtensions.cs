using System.Drawing;
using Godot;

namespace Gamedev.Main.Extensions
{
	public static class AnimationExtensions
	{
		public static AnimationNodeStateMachinePlayback GetStateMachinePlayback(this AnimationTree tree)
		{
			return tree.Get("parameters/playback").As<AnimationNodeStateMachinePlayback>();
		}
	}
}