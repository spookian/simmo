using Godot;
using System;

public partial class Player : Entity
{
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		base._Ready();
		var clientMgr = GetNode("/root/GameManager").GetChild<GodotObject>(0);
		clientMgr.Set("cur_player", this);
		
		state = new PlayerIdle(this);
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		base._Process(delta);
	}

	public override void switchStateIdle()
	{
		state = new PlayerIdle(this);
	}
}
