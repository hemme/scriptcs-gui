#load "../script/dialogz.csx"
// Run with:
// scriptcs -script "dialogz-demo.csx" -- c:\some-path

var folder = ScriptArgs.FirstOrDefault();
var path = OpenFileDialog(folder);
Console.WriteLine("You selected: '{0}'.", path ?? "nothing");