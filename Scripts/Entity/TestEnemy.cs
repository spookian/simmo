using Godot;
using System;

public partial class TestEnemy : Entity
{
	// Called when the node enters the scene tree for the first time.
	MeshInstance3D hitRep;
	public Area3D detectionField;
	const float playerStoppingDistance = 2.0f;

	public override void _Ready()
	{
		friendly = false;
		
		hitRep = GetNode<MeshInstance3D>("HitboxRep");
		createDetectionField();
		state = new TestEnemyStates.Idle(this);
		base._Ready();
	}

	void createDetectionField()
	{
		detectionField = new Area3D();
		CollisionShape3D fieldShape = new CollisionShape3D();
		SphereShape3D sphere = new SphereShape3D();
		sphere.Radius = 5.0f;
		
		fieldShape.Shape = sphere;
		detectionField.AddChild(fieldShape);
		detectionField.CollisionMask = 64;
		
		AddChild(detectionField);

		return;
	}

	public Entity findNearestPlayer()
	{
		var detectedPlayers = detectionField.GetOverlappingBodies();
		float shortestDist = 9999999.9f;
		Node3D closestPlayer = null;

		foreach (Node3D player in detectedPlayers)
		{
			Vector3 displacement = Position - player.Position;
			float newDist = displacement.Length();
			if (shortestDist > newDist)
			{
				closestPlayer = player;
				shortestDist = newDist;
			}
		} 

		return (Entity)closestPlayer;
	}

	//public override void _Process(double delta)
	//{

	//}
}
