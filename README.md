# wpfgui

Super-simple gui generation for wpf applications.
Full demo available here: [https://github.com/smncrl/wpfguiDemo](https://github.com/smncrl/wpfguiDemo)

## Usage

1. Download the `dll` library and add it as reference in your solution
2. Declare which public methods and members must be exposed:

```cs
	[wpfgui.Gui]
	public float value = 0;

	[wpfgui.Gui]
	public string name = "nome";

	[wpfgui.Gui]
	public void MethodName() {}
```

3. Register the gui controls somehwere in class initialization

```cs
	#if DEBUG
		wpfgui.GUIWindow.register(this)
	#endif
```

Now run the application in **DEBUG** mode, and a secondary window will appear with generated controls:

![screenshot](https://i.imgur.com/Jw5cH12.png)
