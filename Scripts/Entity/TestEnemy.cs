using Godot;
using System;

public partial class TestEnemy : Entity
{
	// Called when the node enters the scene tree for the first time.
	MeshInstance3D hitRep;
	public override void _Ready()
	{
		friendly = false;

		hitRep = GetNode<MeshInstance3D>("HitboxRep");
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		
	}
}
