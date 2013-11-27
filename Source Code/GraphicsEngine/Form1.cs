using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Media;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GraphicsEngine
{
	public partial class Form1 : Form
	{
		#region config

		private const int frameDelay = 50;
		private const int aiDelay = 200;
		private const int aiShootDelay = 100;

		#endregion

		#region vars
		private Dictionary<string, SpriteBox> spriteList = new Dictionary<string, SpriteBox>();
		private List<Bullet> bulletList = new List<Bullet>();
		private Directions playerDirection = Directions.Down;
		private int bulletCount = 0;
		private List<Point> rockPoints = new List<Point>();
		private Random rdm = new Random(DateTime.Now.Millisecond * DateTime.Now.Second);
		private SoundPlayer impactSound = new SoundPlayer(@"res\boom.wav");
		#endregion

		#region Main Code
		private void Init()
		{
			Text = "Shooter...";

			LoadSprite("player", Image.FromFile(@"res\pd.png"), new Point(450, 0));
			LoadSprite("evil", Image.FromFile(@"res\eu.jpg"), new Point(0, 450));

			rockPoints.Add(new Point(50, 50));
			rockPoints.Add(new Point(50, 100));
			rockPoints.Add(new Point(200, 300));
			rockPoints.Add(new Point(350, 150));
			rockPoints.Add(new Point(350, 200));
			rockPoints.Add(new Point(350, 400));
			rockPoints.Add(new Point(300, 400));
			rockPoints.Add(new Point(200, 50));
			rockPoints.Add(new Point(200, 100));
			rockPoints.Add(new Point(50, 300));
			rockPoints.Add(new Point(50, 350));
			rockPoints.Add(new Point(100, 300));
			rockPoints.Add(new Point(100, 350));
			rockPoints.Add(new Point(400, 50));
			rockPoints.Add(new Point(100, 100));
			rockPoints.Add(new Point(400, 300));
			rockPoints.Add(new Point(200, 200));
			LoadRocks();
		}

		private void FrameLoad()
		{
			ManageBullets();
		}
		#endregion

		#region AI
		private void MoveAI()
		{
			#region Moving
			int rdmChoice = rdm.Next(0, 2);

			if (rdmChoice == 0)
			{
				//Up and down, y axis
				int orig = spriteList["evil"].Y;
				int player = spriteList["player"].Y;
				int up = orig - 50;
				int down = orig + 50;

				if (Math.Abs(player - up) < Math.Abs(player - down))
				{
					if (IsValidPoint(new Point(spriteList["evil"].X, up)))
					{
						spriteList["evil"].Image = Image.FromFile(@"res\eu.jpg");
						spriteList["evil"].Y = up;
					}
				}
				else
				{
					if (IsValidPoint(new Point(spriteList["evil"].X, down)))
					{
						spriteList["evil"].Image = Image.FromFile(@"res\ed.jpg");
						spriteList["evil"].Y = down;
					}
				}
			}
			else
			{
				//Left and right
				int orig = spriteList["evil"].X;
				int player = spriteList["player"].X;
				int left = orig - 50;
				int right = orig + 50;

				if (Math.Abs(player - left) < Math.Abs(player - right))
				{
					if (IsValidPoint(new Point(left, spriteList["evil"].Y)))
					{
						spriteList["evil"].Image = Image.FromFile(@"res\el.jpg");
						spriteList["evil"].X = left;
					}
				}
				else
				{
					if (IsValidPoint(new Point(right, spriteList["evil"].Y)))
					{
						spriteList["evil"].Image = Image.FromFile(@"res\er.jpg");
						spriteList["evil"].X = right;
					}
				}
			}
			#endregion

			AIAttemptToShoot();
		}

		private void AIAttemptToShoot()
		{
			if (spriteList["evil"].X == spriteList["player"].X)
			{
				Debug.WriteLine("aha!");
				//Up and down, y axis
				int orig = spriteList["evil"].Y;
				int player = spriteList["player"].Y;
				int up = orig - 50;
				int down = orig + 50;

				if (Math.Abs(player - up) < Math.Abs(player - down))
				{
					spriteList["evil"].Image = Image.FromFile(@"res\eu.jpg");
					AIShoot(Directions.Up);
				}
				else
				{
					spriteList["evil"].Image = Image.FromFile(@"res\ed.jpg");
					AIShoot(Directions.Down);
				}
			}
			else if (spriteList["evil"].Y == spriteList["player"].Y)
			{
				//Left and right
				int orig = spriteList["evil"].X;
				int player = spriteList["player"].X;
				int left = orig - 50;
				int right = orig + 50;

				if (Math.Abs(player - left) < Math.Abs(player - right))
				{
					spriteList["evil"].Image = Image.FromFile(@"res\el.jpg");
					AIShoot(Directions.Left);
				}
				else
				{
					spriteList["evil"].Image = Image.FromFile(@"res\er.jpg");
					AIShoot(Directions.Right);
				}
			}
			Debug.WriteLine("Cool");
		}

		private void AIShoot(Directions d)
		{
			Thread.Sleep(aiShootDelay);
			Bullet thisBullet = new Bullet("b" + bulletCount++, d);
			bulletList.Add(thisBullet);

			Point bulletPoint = new Point(0, 0);
			if (d == Directions.Up)
				bulletPoint = new Point(spriteList["evil"].X, spriteList["evil"].Y - 50);
			else if (d == Directions.Down)
				bulletPoint = new Point(spriteList["evil"].X, spriteList["evil"].Y + 50);
			else if (d == Directions.Left)
				bulletPoint = new Point(spriteList["evil"].X - 50, spriteList["evil"].Y);
			else if (d == Directions.Right)
				bulletPoint = new Point(spriteList["evil"].X + 50, spriteList["evil"].Y);

			impactSound.Play();
			Invoke(new Action(() => LoadSprite(thisBullet.BulletName, Image.FromFile(@"res\b.png"), bulletPoint)));
		}

		private void InitAI()
		{
			while (true)
			{
				try
				{
					MoveAI();
				}
				catch (Exception ex)
				{
					Debug.WriteLine("AI is confused... " + ex);
				}
				Thread.Sleep(aiDelay);
			}
		}
		#endregion

		#region Physics

		private void LoadRocks()
		{
			Image rImage = Image.FromFile(@"res\r.png");
			int counter = 0;
			foreach (Point p in rockPoints)
			{
				LoadSprite("r" + ++counter, rImage, p);
			}
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
			if (!IsValidPoint(spriteList[bulletList[i].BulletName].Location))
			{
				RemoveBullet(i);
			}

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
			//try
			//{
				RemoveSprite(sprite2);

				if (sprite2 == "evil")
				{
					Invoke(new Action(() => { Text += " you win!"; }));
				}
				else if (sprite2 == "player")
				{
					Invoke(new Action(() => { Text += " you lose..."; }));
				}
			//}
			//catch { }
		}

		private void Form1_KeyDown(object sender, KeyEventArgs e)
		{
			try
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
					if (thisBullet.Direction == Directions.Up)
						bulletPoint = new Point(spriteList["player"].X, spriteList["player"].Y - 50);
					else if (thisBullet.Direction == Directions.Down)
						bulletPoint = new Point(spriteList["player"].X, spriteList["player"].Y + 50);
					else if (thisBullet.Direction == Directions.Left)
						bulletPoint = new Point(spriteList["player"].X - 50, spriteList["player"].Y);
					else if (thisBullet.Direction == Directions.Right)
						bulletPoint = new Point(spriteList["player"].X + 50, spriteList["player"].Y);

					impactSound.Play();
					LoadSprite(thisBullet.BulletName, Image.FromFile(@"res\b.png"), bulletPoint);
				}
			}
			catch { }
		}

		#endregion

		#region Internals
		public Form1()
		{
			InitializeComponent();
			Init();

			BulletOverlap += Form1_SpriteOverlap;

			Thread frameThread = new Thread(FrameThreadInit);
			frameThread.Start();

			Thread overlapDetector = new Thread(OverlapDetectorInit);
			overlapDetector.Start();

			Thread aiThread = new Thread(InitAI);
			aiThread.Start();
		}

		private delegate void BulletOverlapHandler(string sprite1, string sprite2);
		private event BulletOverlapHandler BulletOverlap;

		private void OverlapDetectorInit()
		{
			while (true)
			{
				for (int i = 0; i < bulletList.Count; i++)
				{
					try
					{
						Bullet b = bulletList[i];
						for (int j = 0; j < spriteList.Count; j++)
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
					}
					catch { }
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

		private void LoadSprite(string name, Image img, Point loc)
		{
			spriteList[name] = new SpriteBox();
			spriteList[name].Image = img;
			spriteList[name].Location = loc;
			spriteList[name].Size = img.Size;
			//Disabled by default - Winforms ain't very good with transparency...
			//spriteList[name].BackColor = Color.Transparent;
			Controls.Add(spriteList[name]);
		}

		private void RemoveSprite(string name)
		{
			Invoke(new Action(() => { Controls.Remove(spriteList[name]); }));
			//spriteList[name].Dispose();
			spriteList.Remove(name);
		}

		private bool AreOverlappingSprites(string spName1, string spName2)
		{
			Rectangle sp1 = new Rectangle(spriteList[spName1].X, spriteList[spName1].Y, spriteList[spName1].Width, spriteList[spName1].Height);
			Rectangle sp2 = new Rectangle(spriteList[spName2].X, spriteList[spName2].Y, spriteList[spName2].Width, spriteList[spName2].Height);
			Rectangle overlapArea = Rectangle.Intersect(sp1, sp2);

			if (overlapArea.IsEmpty)
			{
				return false;
			}
			return true;
		}

		private void FrameThreadInit()
		{
			while (true)
			{
				FrameLoad();
				Thread.Sleep(frameDelay);
			}
		}

		private Point GetCursorPos()
		{
			return PointToClient(Cursor.Position);
		}

		private void Form1_FormClosing(object sender, FormClosingEventArgs e)
		{
			e.Cancel = true;
			Environment.Exit(0);
		}
		#endregion
	}
}
