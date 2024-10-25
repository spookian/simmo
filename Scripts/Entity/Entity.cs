using Godot;
using System;
using System.Collections.Generic;
public struct EntityStats
{
	public double maxHealth;

	public int attack;
	public int defense;
	public int intelligence;
	public double movespeed;
	public double attackSpeed;
}

public partial class Entity : CharacterBody3D
{
	public bool allowForSkill = true; // allows for skill to be activated
	public bool isHighlightable = true;
	public bool friendly = true;

	[Export]
	public Node2D hpBar;

	[Export]
	public Godot.Collections.Array<MeshInstance3D> meshList;

	[Export]
	public AnimationPlayer animPlayer;

	public EntityState state = null;
	public Vector3 meshRotation; // might go deprecated depending on how i feel

	// Collision variables
	float invulnerabilityFrames = 0;
	[Export]
	public Area3D hurtBox;

	// delegate definitions, might contain these in a subclass
	// no enter proc functions because those already happened silly
	// these are mostly for passives but actives can use them too. Actives should mostly rely on states though
	List<SkillDelegates.onSkillExit> removeProcFunctions = new List<SkillDelegates.onSkillExit>();
	List<SkillDelegates.onSkillProcess> normalProcFunctions = new List<SkillDelegates.onSkillProcess>();
	List<SkillDelegates.onSkillHit> hitProcFunctions = new List<SkillDelegates.onSkillHit>();
	List<SkillDelegates.onSkillHurt> hurtProcFunctions = new List<SkillDelegates.onSkillHurt>();

	// In-game stats
	double health = 100.0;
	double mp = 0.0; // gets recharged with certain items & hitting the enemy with a basic attack
	public EntityStats stats;
	public EntityStats statOffsets; // used by passive skills and active skills mostly.
	// i considered having a stat list that gets looped through... but for every calculation? no way!
	// just be careful with your skills and clean up your messes, okay?

	public void highlightObject(bool enable)
	{
		var GameManager = GetNode("/root/GameManager");
		if ((bool)GameManager.Get("is_client"))
		{
			foreach (MeshInstance3D mesh in meshList)
			{
				mesh.SetInstanceShaderParameter("highlighted", enable);
			}
		}
		return;
	}

	void updateHealthbar()
	{
		if (health < 0.0) health = 0.0; // clamp

		Node GameManager = GetNode("/root/GameManager");
		bool isClient = (bool)GameManager.Get("is_client");
		if (isClient && (hpBar != null))
		{
			ProgressBar prog = (ProgressBar)hpBar.GetChild(1).GetChild(1);
			prog.Value = health / (stats.maxHealth + statOffsets.maxHealth) * 100;

			Camera3D cam = (Camera3D)( ((GodotObject)GameManager.Get("clientManager")).Get("camera") );
			Vector2 hpBarPosition = cam.UnprojectPosition(Position);
			hpBarPosition.Y -= 120.0f;

			hpBar.GlobalPosition = hpBarPosition;
		}
	}

	public override void _Ready()
	{
		highlightObject(true);
		if (hurtBox != null)
		{
			//hurtBox.Connect( "area_entered", Callable.From(hurtBoxHandler) );
			hurtBox.AreaEntered += hurtBoxHandler;
		}

		stats.maxHealth = 100.0;
	}

	void hurtBoxHandler(Area3D area)
	{
		Hitbox hitbox = (Hitbox)area; 
		// shhh... downcasting is a code smell but gdot doesn't give me much to work with
		if (invulnerabilityFrames == 0.0f)
		{
			GD.Print("Ouch!");
			bool applyDamage = true;
			invulnerabilityFrames = 0.25f;
			foreach (SkillDelegates.onSkillHurt hurtProc in hurtProcFunctions)
			{
				applyDamage = applyDamage && hurtProc(this, hitbox);
			}

			if (applyDamage) 
			{
				Vector3 knockback = hitbox.knockbackDirection * hitbox.knockbackStrength;
				GD.Print(knockback);
				state = new EntityHurt(this, knockback);
				health -= hitbox.damage;
			}
		}
		else
		{
			// idk store into a list of hitboxes to check after invincibility frames run out?
		}
	}

	// seperate public function for checking hitscan attacks? 

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		if (state != null)
		{
			state.onProcess(delta);
		}

		if (invulnerabilityFrames > 0.0f)
		{
			invulnerabilityFrames -= (float)delta;
			if (invulnerabilityFrames < 0.0f) invulnerabilityFrames = 0.0f;
		}
		updateHealthbar();
	}

	public virtual void switchStateIdle()
	{
		state = new EntityState(this);
	}
}
