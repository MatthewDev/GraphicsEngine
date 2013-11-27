using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GraphicsEngine
{
	class Character : IDisposable
	{
		public string Name { get; set; }
		public int Health { get; set; }

		public Character() { }

		public override string ToString()
		{
			return "Name: " + Name + ", Health: " + Health;
		}

		public void Dispose()
		{
			Name = "";
			Health = 0;
		}
	}
}
