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
		private Dictionary<string, SpriteBox> spriteList = new Dictionary<string, SpriteBox>();

		private void Init() //This method executes at program startup on UI thread
		{
			throw new NotImplementedException();
		}

		private const int frameDelay = 1;
		private void FrameLoad() //This method executes constantly with an interval specified by frameDelay on seperate thread
		{
			throw new NotImplementedException();
		}
		
		private void MouseDownHandler(object sender, MouseEventArgs e) //Fires when mouse is clicked on UI thread - remove if not needed
		{
			throw new NotImplementedException();
		}

		private void KeyDownHandler(object sender, KeyEventArgs e) //Fires when key is clicked on UI thread - remove if not needed
		{
			throw new NotImplementedException();
		}

		#region Internals
		public Form1()
		{
			InitializeComponent();
			Init();

			MouseDown += MouseDownHandler;
			KeyDown += KeyDownHandler;

			Thread frameThread = new Thread(FrameThreadInit);
			frameThread.Start();
		}

		private void LoadSprite(string name, Image img, Point loc)
		{
			spriteList[name] = new SpriteBox();
			spriteList[name].Image = img;
			spriteList[name].Location = loc;
			spriteList[name].Size = img.Size;
			spriteList[name].BackColor = Color.Transparent;
			spriteList[name].MouseDown += MouseDownHandler;
			Controls.Add(spriteList[name]);
		}

		private void RemoveSprite(string name)
		{
			Invoke(new Action(() => { Controls.Remove(spriteList[name]); }));
			spriteList[name].MouseDown -= MouseDownHandler;
			spriteList[name].Dispose();
			spriteList.Remove(name);
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
