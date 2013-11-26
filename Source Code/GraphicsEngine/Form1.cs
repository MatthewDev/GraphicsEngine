using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GraphicsEngine
{
	public partial class Form1 : Form
	{
		private const int frameDelay = 1;
		private Dictionary<string, SpriteBox> spriteList = new Dictionary<string, SpriteBox>();
		private List<string> duckList = new List<string>();
		private Random rdm = new Random(DateTime.Now.Millisecond * DateTime.Now.Second);

		private int duckCount = 0;
		private int duckKillCount = 0;
		private int duckSpeed = 5;
		Image duck = Image.FromFile(@"res\duck.png");

		private void Init()
		{
			Text = "Duck Hunt";
			BackgroundImage = Image.FromFile(@"res\back.png");

			duckList.Add(GetNextDuckName());
			duckList.Add(GetNextDuckName());
			duckList.Add(GetNextDuckName());
			LoadDucks();

			//LoadSprite("dot", Image.FromFile(@"res\dot.png"), new Point(0, 0));
		}

		private void LoadDucks()
		{
			foreach (string s in duckList)
			{
				LoadSprite(s, duck, new Point(rdm.Next(-300, -100), rdm.Next(0, Size.Height - 100)));
				spriteList[s].MouseDown += Form1_MouseDown;
			}
		}

		private void FrameLoad()
		{
			MoveDucks();
			WrapDucks();
		}

		private void WrapDucks()
		{
			for (int i=0;i<duckList.Count;i++)
			{
				string s = duckList[i];
				try
				{
					if (spriteList[s].X > Size.Width)
					{
						spriteList[s].X = 0;
					}
				}
				catch { }
			}
		}

		private void Form1_MouseDown(object sender, MouseEventArgs e)
		{
			((SpriteBox)sender).Location = new Point(-150, rdm.Next(0, Size.Height - 200));

			Text = "Duck Hunt - Killed " + ++duckKillCount + " ducks!";

			if (duckKillCount % 10 == 0)
			{
				duckSpeed++;

				string name = GetNextDuckName();
				duckList.Add(name);
				LoadSprite(name, duck, new Point(-150, rdm.Next(0, Size.Height - 100)));
				spriteList[name].MouseDown += Form1_MouseDown;
			}
		}

		private bool IsOverlapping(string s1, string s2)
		{
			Rectangle sp1 = new Rectangle(spriteList[s1].X, spriteList[s1].Y, spriteList[s1].Width, spriteList[s1].Height);
			Rectangle sp2 = new Rectangle(spriteList[s2].X, spriteList[s2].Y, spriteList[s2].Width, spriteList[s2].Height);
			Rectangle overlapArea = Rectangle.Intersect(sp1, sp2);

			if (!overlapArea.IsEmpty)
			{
				return true;
			}
			else
			{
				return false;
			}
		}

		private void MoveDucks()
		{
			for (int i = 0; i < duckList.Count;i++)
			{
				try
				{
					spriteList[duckList[i]].X += duckSpeed;
				}
				catch { }
			}
		}

		private string GetNextDuckName()
		{
			return "d" + duckCount++;
		}

		#region Internals
		public Form1()
		{
			InitializeComponent();
			Init();

			Thread frameThread = new Thread(FrameThreadInit);
			frameThread.Start();
		}

		private void FrameThreadInit()
		{
			while (true)
			{
				FrameLoad();
				Thread.Sleep(frameDelay);
			}
		}

		private void LoadSprite(string name, Image img, Point loc)
		{
			spriteList[name] = new SpriteBox();
			spriteList[name].Image = img;
			spriteList[name].Location = loc;
			spriteList[name].Size = img.Size;
			spriteList[name].BackColor = Color.Transparent;

			Controls.Add(spriteList[name]);
		}

		private void RemoveSprite(string name)
		{
			Invoke(new Action(() => { Controls.Remove(spriteList[name]); }));
			spriteList.Remove(name);
		}

		private void Form1_FormClosing(object sender, FormClosingEventArgs e)
		{
			e.Cancel = true;
			Environment.Exit(0);
		}
		#endregion
	}
}
