using Godot;
using System;

public partial class EntityState : Node
{
	protected Entity owner;
	// Called when the node enters the scene tree for the first time.
	public EntityState(Entity _owner)
	{
		owner = _owner;
		onStart();
	}

	public virtual void onStart()
	{

	}

	public virtual void onProcess(double delta)
	{

	}

	protected Vector3 deceleration(double delta, float decelAmount)
	{
		Vector3 n = owner.Velocity;
		float y = n.Y;
		n.Y = 0.0f;
		float l = n.Length();
		l = l - ((float)delta * decelAmount);
		if (l < 0.15) l = 0.0f;

		Vector3 nn = n.Normalized() * l;
		nn.Y = y;
		return nn;
	}

	public virtual byte[] createPacket()
	{
		return new byte[1];
	}
}
