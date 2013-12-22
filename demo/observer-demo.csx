using ScriptCs.Gui.Watch;
class Foo { public string Bar {get; set;} }
var f = new Foo();
f.Bar = "bar-1";
ConsoleObserver.Attach(f, "Bar");
f.Bar = "bar-2";
