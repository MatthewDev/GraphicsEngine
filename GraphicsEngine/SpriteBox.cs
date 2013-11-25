using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GraphicsEngine
{
	class SpriteBox : PictureBox
	{
		public int X
		{
			get 
			{ 
				if (InvokeRequired) 
				{
					int toReturn = -1;
					Invoke(new Action(() => { toReturn = Location.X; }));
					return toReturn;
				} 
				else 
				{ 
					return Location.X;
				} 
			}
			set 
			{
				if (InvokeRequired)
				{
					Invoke(new Action(() => Location = new Point(value, Y)));
				}
				else
				{
					Location = new Point(value, Y);
				}
			}
		}

		public int Y
		{
			get 
			{
				if (InvokeRequired)
				{
					int toReturn = -1;
					Invoke(new Action(() => { toReturn = Location.Y; }));
					return toReturn;
				}
				else
				{
					return Location.Y;
				} 
			}
			set 
			{
				if (InvokeRequired)
				{
					Invoke(new Action(() => Location = new Point(X, value)));
				}
				else
				{
					Location = new Point(X, value);
				}
			}
		}
	}
}
