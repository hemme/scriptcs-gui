var gui = Require<Gui>();


bool done = false;
while(!done) {

Console.WriteLine(@"

1 - OpenFileDialog
2 - PopUp
3 - PopUp + Console
4 - Drawing
5 - Drawing + Console
6 - Clear
7 - Exit
");

var option = Console.ReadKey();
switch(option.KeyChar) {
	case '1':
		Console.WriteLine(" => emulating a modal window.");
		var folder = ScriptArgs.FirstOrDefault();
		var path = gui.OpenFileDialog(folder, join:true);
		Console.WriteLine("\r\nYou selected: '{0}'.", path ?? "nothing");
		break;
		
	case '2':
	case '3':
		var f = gui.PopUp();
		f.Sta.Start();
		
		Console.WriteLine(" => a non modal pop-up.");
		if (option.KeyChar=='3') {
			Console.WriteLine("You can use the console too.\r\nTo run again the script write #load \"gui-demo.csx\".");
			done = true;
		}
		
		f.Sta.Enqueue(delegate() {
			var label = new Label();
			label.Top = 20;
			label.Left = 20;
			label.Text = "Hello from a PopUp.";
			label.Dock = DockStyle.Fill;
			f.Controls.Add(label);
		
			var t = new Thread(new ThreadStart(
					delegate{
						while(true) {
							label.Text = DateTime.Now.ToLongTimeString();
							System.Threading.Thread.Sleep(1000);
						}}));
				t.IsBackground = true;
				t.Start();
		});
		
		
		
		break;
		
	case '4':
	case '5':
		Console.WriteLine(" => a drawing window.");
		if (option.KeyChar=='5') {
			Console.WriteLine("You can use the console too.\r\nTo run again the script write #load \"gui-demo.csx\".");
			done = true;
		}
		var d = gui.Drawing();
		
		var th = new Thread(new ThreadStart(
				delegate{
					while(true) {
						var now = DateTime.Now.Ticks;
						var c = Color.FromArgb((int)(now & 0xFFFFFF));
						c = Color.FromArgb(255,c);
						d.DrawRectangle(new Pen(c), now % 100, now % 180, now % 320, now % 270);
						System.Threading.Thread.Sleep(1000);
					}}));
		th.IsBackground = true;
		th.Start();
		break;
		
	case '6':
		Console.Clear();
		break;
	case '7':
		Console.WriteLine("\r\nBye.");
		done = true;
		break;
	default:
		Console.WriteLine("\r\nSorry?");
		break;
	}
}


