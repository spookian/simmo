using Godot;
using System;

public partial class EntityHurt : EntityState
{
	public EntityHurt(Entity _owner, Vector3 _knockback) : base(_owner)
	{
		owner.Velocity = _knockback;
	}

	// Called when the node enters the scene tree for the first time.
	public override void onStart()
	{
		if (owner.animPlayer != null)
		{
			owner.animPlayer.Play("Stand");
		}
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void onProcess(double delta)
	{
		owner.Velocity = deceleration(delta, 15.0f);
		Vector3 horizontalVelo = new Vector3( owner.Velocity.X, 0.0f, owner.Velocity.Z);
		if (horizontalVelo == Vector3.Zero) owner.switchStateIdle();
		owner.MoveAndSlide();
	}
}
