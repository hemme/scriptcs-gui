using ScriptCs.Gui.Watch;
class Foo { public string Bar {get; set;} }
var f = new Foo();
f.Bar = "bar-1";
ConsoleObserver.Instance.Attach(f, "Bar");
f.Bar = "bar-2";
ConsoleObserver.Instance.Detach(f);
System.Threading.Thread.Sleep(1000);
f.Bar = "bar-3";
