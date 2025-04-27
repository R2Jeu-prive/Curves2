using System;
using System.Threading.Tasks;
using Godot;
using Godot.Collections;

public partial class Screenshot : SubViewport
{
	public override void _Ready()
	{
		RenderingServer.FramePostDraw += SaveScreenshot;
	}

	public void SaveScreenshot(){
		GD.Print("Saving Screenshot !");
		Image img = GetTexture().GetImage();
		img.SavePng("res://icons/screenshot.png");
		RenderingServer.FramePostDraw -= SaveScreenshot;
	}
}
