using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GraphicsEngine
{
	class Sentry : Character
	{
		public int Damage { get; set; }

		public Sentry(string name, int health, int damage)
		{
			Name = name;
			Health = health;
			Damage = damage;
		}
	}
}
