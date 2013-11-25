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

		/*	
		//Sample Code
		
		private void Init()
		{
			LoadSprite("img", Image.FromFile(@"C:\Users\Andrew\Desktop\img.png"), new Point(50, 50));
		}

		private void FrameLoad()
		{
			spriteList["img"].X += 5;
		}
		*/

		private void Init()
		{
			
		}

		private void FrameLoad()
		{

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
