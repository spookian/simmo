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
		
		state = new PlayerStates.Idle(this);
	}

	public override void switchStateIdle()
	{
		state = new PlayerStates.Idle(this);
	}
}
