using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
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

		private void Init()
		{
			LoadSprite("player", Image.FromFile(@"res\Player.png"), new Point(0, 0));
			LoadSprite("evil", Image.FromFile(@"res\Evil.jpg"), new Point(433, 409));
		}

		private void FrameLoad()
		{

		}

		private void Form1_KeyDown(object sender, KeyEventArgs e)
		{
			if (e.KeyData == Keys.Down)
			{
				spriteList["player"].Y += 50;
			}
			else if (e.KeyData == Keys.Up)
			{
				spriteList["player"].Y -= 50;
			}
			else if (e.KeyData == Keys.Left)
			{
				spriteList["player"].X -= 50;
			}
			else if (e.KeyData == Keys.Right)
			{
				spriteList["player"].X += 50;
			}
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
