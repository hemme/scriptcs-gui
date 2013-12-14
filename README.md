#ScriptCS-GUI

##What is it?
**ScriptCS-GUI** is a simple GUI toolkit for **ScriptCS**.
Its main aim is to help the user create fully interactive forms from the REPL without preventing her to use the REPL after the forms are run (on the contrary, tipically, creating and running a form from the console hangs the console).
Thanks to a bunch of Windows API's (declared in Native.dll assembly) **ScriptCS-GUI** hides you the complexity of instanciating independent windows forms from the REPL and lets you enjoy the rest.

##The toolkit
Script pack url: `https://www.nuget.org/packages/ScriptCs.Gui`.
Installation: 

```batchfile
c:\anyfolder>scriptcs -install ScriptCs.Gui
```

The following helpers are declared by the ~~static~~ class `Gui`.

* `Gui.OpenFileDialog()`: opens a classic Open File Dialog and return the path of the selected file.
* `Gui.SaveFileDialog()`: acts similar to OpenFileDialog()...
* `Gui.PopUp()`: opens a pop-up and returns a form object. You can use its Sta.Enqueue(action) method to inject code in the form's main thread (e.g. to add controls at runtime) while working inside the console!
* `Gui.Drawing()`: opens a drawing form and returns the Graphics object you can use to draw on the form (the console stays responsive in this case too!)

For a full demo see `.\demo\gui-demo.csx`.

###Example: creating Windows Form pop-up from the ScriptCS REPL
```batchfile
C:\git\scriptcs-gui\demo>scriptcs -install ScriptCs.Gui
INFO: Installing packages...
INFO: Installed: scriptcs.gui
INFO: Installation completed successfully.
Initiated saving packages into packages.config...
INFO: Added ScriptCs.Contracts, Version 0.8.1, .NET 4.5
INFO: Added ScriptCs.Gui, Version 0.1.0.1, .NET 4.5
INFO: Packages.config successfully created!

C:\git\scriptcs-gui\demo>scriptcs
scriptcs (ctrl-c to exit)

> using System.Windows.Forms;
> var gui = Require<Gui>();
> var f = gui.PopUp();
> f.TopMost = true;
true
> f.Sta += delegate {
var t = new TextBox();
f.Controls.Add(t);
};
{
  "$id": "1",
  "Length": 1
}
> f.Sta.Start();
> var t = (TextBox)f.Controls[0];
> t.Text = "Hello ScriptCS-GUI!";
"Hello ScriptCS-GUI!"
>
```

##What's next?

I will add more helpers. For instance:
* a live chart window (think about creating a chart with a couple of command and injecting a delegate to the chart so that it will be refreshed automagically in the background).
* a watch window (similar to Visual Studio)

Any more ideas are welcome!



