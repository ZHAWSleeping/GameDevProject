using System;
using System.Collections.Generic;

namespace Gamedev.Main.UI.Menu
{
	public static class MenuStack
	{
		public static Stack<Action> History = new();
		public static void Clear()
		{
			History = new();
		}
	}
}