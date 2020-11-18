using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace KeyboardHook
{
	/// <summary>
	/// This class is based of LiveSplit's KeyboardHook by Christopher "CryZe" Serr. This code was reused and modified under MIT licence. See https://github.com/LiveSplit/LiveSplit.
	/// </summary>
	public class KeyboardHook
	{
		protected List<Keys> RegisteredKeys { get; set; }
		public event KeyEventHandler KeyPressed;
		public bool KeysEnabled;

		[DllImport("user32.dll", CharSet = CharSet.Auto, ExactSpelling = true)]
		public static extern short GetAsyncKeyState(Keys vkey);

		public KeyboardHook()
		{
			RegisteredKeys = new List<Keys>();
		}

		public void RegisterHotKey(Keys key)
		{
			if (!RegisteredKeys.Contains(key) && key != Keys.None)
				RegisteredKeys.Add(key);
		}

		public void UnregisterAllHotkeys()
		{
			RegisteredKeys.Clear();
		}

		public void Poll()
		{
			if (KeysEnabled)
			{
				foreach (var key in RegisteredKeys)
				{
					var modifiersDown = true;
					var modifiers = Keys.None;
					if (key.HasFlag(Keys.Shift))
					{
						modifiersDown &= IsKeyDown(Keys.ShiftKey);
						modifiers |= Keys.Shift;
					}
					if (key.HasFlag(Keys.Control))
					{
						modifiersDown &= IsKeyDown(Keys.ControlKey);
						modifiers |= Keys.Control;
					}
					if (key.HasFlag(Keys.Alt))
					{
						modifiersDown &= IsKeyDown(Keys.Menu);
						modifiers |= Keys.Alt;
					}

					var keyWithoutModifiers = key & ~modifiers;
					var result = GetAsyncKeyState(keyWithoutModifiers);
					var isPressed = ((result >> 15) & 1) == 1;

					if (modifiersDown && isPressed)
						KeyPressed?.Invoke(this, new KeyEventArgs(key));
				}
			}

		}

		protected bool IsKeyDown(Keys key)
		{
			var result = GetAsyncKeyState(key);
			return ((result >> 15) & 1) == 1;
		}
	}
}