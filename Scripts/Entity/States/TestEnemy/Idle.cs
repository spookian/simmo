using Godot;
using System;

namespace TestEnemyStates
{
	public partial class Idle : EntityState
	{
		int phase = 0;
		public Idle(Entity _owner) : base(_owner)
		{

		}

		// Called when the node enters the scene tree for the first time.
		public override void onStart()
		{

		}

		// Called every frame. 'delta' is the elapsed time since the previous frame.
		public override void onProcess(double delta)
		{
			Entity player = ((TestEnemy)owner).findNearestPlayer();
			if (player != null)
			{
				// chase player around then stop
				Vector3 displacement = (player.Position - owner.Position);
				Vector3 chaseDirection = displacement.Normalized();

				float rotation = Mathf.Atan2( chaseDirection.X, chaseDirection.Z );
				owner.Rotation = new Vector3(owner.Rotation.X, rotation, owner.Rotation.Z);

				if (displacement.Length() <= 0.8f)
				{
					owner.Velocity = Vector3.Zero;
				}
				else
				{
					owner.Velocity = chaseDirection;
				}
			} 
			else
			{
				owner.Velocity = Vector3.Zero;
			}
			owner.MoveAndSlide();
		}
	}
}
