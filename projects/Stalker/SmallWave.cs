using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Stalker {
	public partial class SmallWave : Form {

		

		Bitmap b;
		public SmallWave() {
			InitializeComponent();

			b = new Bitmap(pictureBox1.Size.Width, pictureBox1.Size.Height);
			using (Graphics g = Graphics.FromImage(b)) {
				g.FillRectangle(Brushes.Black, pictureBox1.DisplayRectangle);
			}

			pictureBox1.Image = b;

			map = new Thing[64, 48];

			t = new Timer();
			t.Tick += new EventHandler((o, e) => {
				DoMove();
			});
		}


		private void DoMove() {

			for (int i = 0; i < body.Count; i++) {

				map[body[i].x, body[i].y] = Thing.Empty;

				int newx = body[i].x;
				int newy = body[i].y;
				if (body[i].d == Direction.Up) {
					newy--;
				}
				if (body[i].d == Direction.Down) {
					newy++;
				}
				if (body[i].d == Direction.Left) {
					newx--;
				}
				if (body[i].d == Direction.Right) {
					newx++;
				}

				if (newy == 0) {
					newy = 47;
				}
				if (newy == 48) {
					newy = 0;
				}
				if (newx == 0) {
					newx = 63;
				}
				if (newx == 64) {
					newx = 0;
				}

				if (i == 0) {
					// head
					if (map[newx, newy] == Thing.Snake) {
						// die
						Stope();
						Start();
						return;

					} else if (map[newx, newy] == Thing.Apple) {
						// score
						score++;
						body.Add(body.Last());
					}

				} else {
					// tail
					if (map[newx, newy] == Thing.Snake) {
						newx = body[i].x;
						newy = body[i].y;
					}
				}

				body[i] = new Body() { x = newx, y = newy, d = body[i].d };



				map[body[i].x, body[i].y] = Thing.Snake;

			}

			for (int i = body.Count-1; i > 0; i--) {
				body[i] = new Body() { x = body[i].x, y = body[i].y, d = body[i - 1].d };
			}

			using (Graphics g = Graphics.FromImage(b)) {
				g.FillRectangle(Brushes.Black, pictureBox1.DisplayRectangle);

				for (int yy = 0; yy < 48; yy++) {
					for (int xx = 0; xx < 64; xx++) {
						if (map[xx, yy] == Thing.Apple) {
							g.FillRectangle(Brushes.Red, new Rectangle(xx * 10, yy * 10, 10, 10));
						} else if (map[xx, yy] == Thing.Snake) {
							g.FillRectangle(Brushes.White, new Rectangle(xx * 10, yy * 10, 10, 10));
						}
					}
				}
			}

			pictureBox1.Refresh();
		}


		enum Thing {
			Empty,
			Snake,
			Apple,
			Wall,
		}

		Thing[,] map;
		List<Body> body = new List<Body>();
		int score = 0;


		Timer t;
		private void Start() {

			score = 0;
			body = new List<Body>();

			for (int yy = 0; yy < 48; yy++)
			for (int xx = 0; xx < 64; xx++) {
				map[xx, yy] = Thing.Empty;
			}

			body.Add(new Body() { x = 32, y = 24, d = Direction.Right });
			body.Add(new Body() { x = 32, y = 24, d = Direction.Right });
			body.Add(new Body() { x = 32, y = 24, d = Direction.Right });
			body.Add(new Body() { x = 32, y = 24, d = Direction.Right });
			body.Add(new Body() { x = 32, y = 24, d = Direction.Right });

			t.Stop();
			t.Interval = 200;
			t.Start();
		}

		private void SmallWave_Load(object sender, EventArgs e) {
			Start();
		}

		private void SmallWave_FormClosing(object sender, FormClosingEventArgs e) {
			Stope();
		}

		private void Stope() {
			t.Stop();
		}

		private void SmallWave_KeyDown(object sender, KeyEventArgs e) {


			switch (e.KeyCode) {
				case Keys.Up:
					if (body[0].d != Direction.Down) {
						body[0] = new Body() { x = body[0].x, y = body[0].y, d = Direction.Up };
					}
					break;
				case Keys.Down:
					if (body[0].d != Direction.Up) {
						body[0] = new Body() { x = body[0].x, y = body[0].y, d = Direction.Down };
					}
					break;
				case Keys.Right:
					if (body[0].d != Direction.Left) {
						body[0] = new Body() { x = body[0].x, y = body[0].y, d = Direction.Right };
					}
					break;
				case Keys.Left:
					if (body[0].d != Direction.Right) {
						body[0] = new Body() { x = body[0].x, y = body[0].y, d = Direction.Left };
					}
					break;
			}

		}
	}

	enum Direction {
		Up, Down, Left, Right
	}


	struct Body {
		public int x;
		public int y;
		public Direction d;
	}
}
