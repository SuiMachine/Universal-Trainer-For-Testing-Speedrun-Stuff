﻿using System.Windows.Forms;

namespace Flying47.Structs
{
	public class KeySet
	{
		public Keys StorePosition = Keys.None;
		public Keys LoadPosition = Keys.None;
		public Keys Up = Keys.None;
		public Keys Down = Keys.None;
		public Keys Forward = Keys.None;

		public bool IsTopMost = false;
		public bool CheckActiveWindow = false;

		public KeySet()
		{
			this.StorePosition = Keys.NumPad7;
			this.LoadPosition = Keys.NumPad9;
			this.Up = Keys.Add;
			this.Down = Keys.Subtract;
			this.Forward = Keys.NumPad8;
			this.IsTopMost = false;
			this.CheckActiveWindow = false;
		}
	}
}
