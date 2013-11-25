using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GraphicsEngine
{
	class Bullet
	{
		public string BulletName { get; set; }
		public Directions Direction { get; set; }

		public Bullet(string bulletName, Directions direction)
		{
			BulletName = bulletName;
			Direction = direction;
		}
	}
}
