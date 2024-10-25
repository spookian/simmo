using Godot;
using System;

public partial class Hitbox : Area3D
{
	public bool friendly = false; 
	public float damage = 10.0f;

	public Vector3 knockbackDirection = new Vector3(0.0f, 0.0f, -1.0f);
	public float knockbackStrength = 4.0f;

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		this.Monitoring = true;
		this.Monitorable = true;
		//GD.Print(knockbackStrength);
	}

	public void updateAlignment()
	{
		if (friendly) 
		{
			CollisionLayer = 8;
			CollisionMask = 16;
		}
		else 
		{
			CollisionLayer = 32;
			CollisionMask = 4;
		}
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		updateAlignment();
		// update direction
		knockbackDirection = knockbackDirection * Basis.FromEuler( new Vector3(0.0f, GlobalRotation.Y, 0.0f) );

	}
}
