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
		private const int frameDelay = 50;
		private Dictionary<string, SpriteBox> spriteList = new Dictionary<string, SpriteBox>();
		private List<Bullet> bulletList = new List<Bullet>();
		private Directions playerDirection = Directions.Down;
		private int bulletCount = 0;
		private List<Point> rockPoints = new List<Point>();

		private void Init()
		{
			LoadSprite("player", Image.FromFile(@"res\pd.png"), new Point(0, 0));
			LoadSprite("evil", Image.FromFile(@"res\eu.jpg"), new Point(450, 450));

			rockPoints.Add(new Point(50, 50));
			rockPoints.Add(new Point(50, 100));
			rockPoints.Add(new Point(200, 300));
			rockPoints.Add(new Point(350, 150));
			rockPoints.Add(new Point(350, 200));
			rockPoints.Add(new Point(350, 400));
			rockPoints.Add(new Point(300, 400));
			LoadRocks();

			BulletOverlap += Form1_SpriteOverlap;
		}

		private void LoadRocks()
		{
			Image rImage = Image.FromFile(@"res\r.png");
			int counter = 0;
			foreach (Point p in rockPoints)
			{
				LoadSprite("r" + ++counter, rImage, p);
			}
		}

		private void FrameLoad()
		{
			UpdateTitleWithSpriteloc();
			ManageBullets();
		}

		private void ManageBullets()
		{
			int count = bulletList.Count;
			for (int i = 0; i < count; i++)
			{
				try
				{
					MoveBullet(i);
				}
				catch { }
			}
		}

		private void MoveBullet(int i)
		{
			if (bulletList[i].Direction == Directions.Up)
			{
				spriteList[bulletList[i].BulletName].Y -= 50;
			}
			else if (bulletList[i].Direction == Directions.Down)
			{
				spriteList[bulletList[i].BulletName].Y += 50;
			}
			else if (bulletList[i].Direction == Directions.Left)
			{
				spriteList[bulletList[i].BulletName].X -= 50;
			}
			else if (bulletList[i].Direction == Directions.Right)
			{
				spriteList[bulletList[i].BulletName].X += 50;
			}

			if (!IsValidPoint(spriteList[bulletList[i].BulletName].Location))
			{
				RemoveBullet(i);
			}
		}

		private bool IsValidPoint(Point p)
		{
			if (p.Y < 0 || p.Y > 450 || p.X < 0 || p.X > 450)
			{
				return false;
			}

			foreach (Point ro in rockPoints)
			{
				if (p == ro)
				{
					return false;
				}
			}
			return true;
		}

		private void RemoveBullet(int i)
		{
			RemoveSprite(bulletList[i].BulletName);
			bulletList.RemoveAt(i);
		}

		private void Form1_SpriteOverlap(string sprite1, string sprite2)
		{
			try
			{
				RemoveSprite(sprite2);
			}
			catch { }
		}

		private void UpdateTitleWithSpriteloc()
		{
			try
			{
				Invoke(new Action(() => { Text = spriteList["player"].X + ", " + spriteList["player"].Y; }));
			}
			catch { }
		}

		private void Form1_KeyDown(object sender, KeyEventArgs e)
		{
			if (e.KeyData == Keys.Down)
			{
				playerDirection = Directions.Down;
				spriteList["player"].Image = Image.FromFile(@"res\pd.png");
				if (IsValidPoint(new Point(spriteList["player"].X, spriteList["player"].Y + 50)))
				{
					spriteList["player"].Y += 50;
				}
			}
			else if (e.KeyData == Keys.Up)
			{
				playerDirection = Directions.Up;
				spriteList["player"].Image = Image.FromFile(@"res\pu.png");
				if (IsValidPoint(new Point(spriteList["player"].X, spriteList["player"].Y - 50)))
				{
					spriteList["player"].Y -= 50;
				}
			}
			else if (e.KeyData == Keys.Left)
			{
				playerDirection = Directions.Left;
				spriteList["player"].Image = Image.FromFile(@"res\pl.png");
				if (IsValidPoint(new Point(spriteList["player"].X - 50, spriteList["player"].Y)))
				{
					spriteList["player"].X -= 50;
				}
			}
			else if (e.KeyData == Keys.Right)
			{
				playerDirection = Directions.Right;
				spriteList["player"].Image = Image.FromFile(@"res\pr.png");
				if (IsValidPoint(new Point(spriteList["player"].X + 50, spriteList["player"].Y)))
				{
					spriteList["player"].X += 50;
				}
			}
			else if (e.KeyData == Keys.Space)
			{
				Bullet thisBullet = new Bullet("b" + bulletCount++, playerDirection);
				bulletList.Add(thisBullet);

				Point bulletPoint = new Point(0, 0);
				if (playerDirection == Directions.Up)
					bulletPoint = new Point(spriteList["player"].X, spriteList["player"].Y - 50);
				else if (playerDirection == Directions.Down)
					bulletPoint = new Point(spriteList["player"].X, spriteList["player"].Y + 50);
				else if (playerDirection == Directions.Left)
					bulletPoint = new Point(spriteList["player"].X - 50, spriteList["player"].Y);
				else if (playerDirection == Directions.Right)
					bulletPoint = new Point(spriteList["player"].X + 50, spriteList["player"].Y);

				LoadSprite(thisBullet.BulletName, Image.FromFile(@"res\b.png"), bulletPoint);
			}
		}

		#region Internals
		public Form1()
		{
			InitializeComponent();
			Init();

			Thread frameThread = new Thread(FrameThreadInit);
			frameThread.Start();

			Thread overlapDetector = new Thread(OverlapDetectorInit);
			overlapDetector.Start();
		}

		private delegate void BulletOverlapHandler(string sprite1, string sprite2);
		private event BulletOverlapHandler BulletOverlap;

		private void OverlapDetectorInit()
		{
			while (true)
			{
				for (int i = 0; i < bulletList.Count; i++)
				{
					Bullet b = bulletList[i];
					for (int j = 0; j < spriteList.Count; j++)
					{
						try
						{
							KeyValuePair<string, SpriteBox> thisSprite = spriteList.ElementAt(j);
							if (b.BulletName == thisSprite.Key || thisSprite.Key[0] == 'b')
							{
								continue;
							}

							if (spriteList[b.BulletName].Location == thisSprite.Value.Location)
							{
								if (IsValidPoint(thisSprite.Value.Location))
								{
									BulletOverlap(b.BulletName, thisSprite.Key);
								}
							}
						}
						catch { }
					}
				}
			}
		}

		private bool IsOverlap(string sprite1, string sprite2)
		{
			if (spriteList[sprite1].Location == spriteList[sprite2].Location)
			{
				return true;
			}
			return false;
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

			Controls.Add(spriteList[name]);
		}

		private void RemoveSprite(string name)
		{
			Invoke(new Action(() =>
			{
				Controls.Remove(spriteList[name]);
			}));

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
