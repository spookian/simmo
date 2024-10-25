using Godot;
using System;

namespace PlayerStates
{
	public partial class Idle : EntityState
	{
		// this state handles basic movements from walking to falling to jumping
		int phase = 0; // like a state machine basically
		const double SPEED = 4.0;
		const float GRAVITY = 16.0f;
		const float jumpReleaseMultiplier = 0.40f;
		bool jumping = false;

		public Idle(Entity _owner) : base(_owner)
		{

		}

		public override void onStart()
		{
			owner.allowForSkill = true;
			owner.animPlayer.Play("Stand");
		}

		Basis getCameraRotation()
		{
			Node GameManager = owner.GetNode("/root/GameManager");
			Camera3D cam = (Camera3D)( (GodotObject)GameManager.Get("clientManager") ).Get("camera");
			return Basis.FromEuler(new Vector3(0.0f, -Mathf.DegToRad( (float)cam.Get("disprot") ), 0.0f) );
		}

		void handleGroundPhase(double delta)
		{
			if (!owner.IsOnFloor())
			{
				phase = 1;
				owner.animPlayer.Play("Jump");
				handleAirPhase(delta);
				return;
			}

			if (Input.IsActionJustPressed("ui_select"))
			{
				phase = 1;
				owner.Velocity = new Vector3(owner.Velocity.X, 6.0f, owner.Velocity.Z);
				owner.animPlayer.Play("Jump");
				jumping = true;

				handleAirPhase(delta);
				return;
			}

			Vector3 ownerNewVelocity = new Vector3(0.0f, 0.0f, 0.0f);

			// place a check here for player control
			Vector2 direction = Input.GetVector("strafe_left", "strafe_right", "move_up", "move_down");
			if (direction != Vector2.Zero)
			{
				// add acceleration for better game feel
				Vector3 nDirection = new Vector3(direction.X, 0.0f, direction.Y) * getCameraRotation();
				owner.Rotation = new Vector3(0.0f, (float)Mathf.Atan2(nDirection.X, nDirection.Z) + Mathf.Pi, 0.0f);

				ownerNewVelocity = owner.Velocity + nDirection * (float)delta * 50.0f;
				Vector2 playerHorizontalVelocity = new Vector2(ownerNewVelocity.X, ownerNewVelocity.Z);		
				if (playerHorizontalVelocity.Length() > SPEED)
				{
					var playerHorizonalNorm = playerHorizontalVelocity.Normalized() * (float)SPEED;
					ownerNewVelocity = new Vector3(playerHorizonalNorm.X, owner.Velocity.Y, playerHorizonalNorm.Y);
				}
				
			}
			else
			{
				// add deceleration later for better game feel
				ownerNewVelocity = deceleration(delta, 25.0f);
			}

			if (ownerNewVelocity != Vector3.Zero) owner.animPlayer.Play("Walk");
			else owner.animPlayer.Play("Stand");

			owner.Velocity = ownerNewVelocity;
			return;
		}

		void handleAirPhase(double delta)
		{
			Vector2 direction = Input.GetVector("strafe_left", "strafe_right", "move_up", "move_down");
			if (direction != Vector2.Zero)
			{
				Vector3 nDirection = new Vector3(direction.X, 0.0f, direction.Y) * getCameraRotation();
				owner.Velocity = owner.Velocity + nDirection * (float)delta * 50.0f;

				Vector2 playerHorizontalVelocity = new Vector2(owner.Velocity.X, owner.Velocity.Z);		
				if (playerHorizontalVelocity.Length() > SPEED)
				{
					var playerHorizonalNorm = playerHorizontalVelocity.Normalized() * (float)SPEED;
					owner.Velocity = new Vector3(playerHorizonalNorm.X, owner.Velocity.Y, playerHorizonalNorm.Y);
				}
				float angle = (float)Mathf.Atan2(nDirection.X, nDirection.Z) + Mathf.Pi;
				owner.Rotation = new Vector3(0.0f, angle, 0.0f);
			}
			else
			{
				owner.Velocity = deceleration(delta, 25.0f);
			}

			if (owner.Velocity.Y <= 0.0f)
			{
				jumping = false;
			}
			else
			{
				if (!Input.IsActionPressed("ui_select") && jumping)
				{
					jumping = false;
					owner.Velocity = new Vector3(owner.Velocity.X, owner.Velocity.Y * jumpReleaseMultiplier, owner.Velocity.Z);
				}
			}
		}

		public override void onProcess(double delta)
		{
			// add code to check if being controlled by player
			switch (phase)
			{
				case 0: // Ground phase
				handleGroundPhase(delta);
				break;
				
				case 1: // Air phase
				handleAirPhase(delta);
				break;
			}

			//handle gravity and collision
			owner.Velocity = new Vector3(owner.Velocity.X, owner.Velocity.Y - GRAVITY * (float)delta, owner.Velocity.Z);
			owner.MoveAndSlide();

			if (owner.IsOnFloor())
			{
				phase = 0;
				jumping = false;
			}
		}
	}
}