using Godot;
using System;

public interface ISkill
{
	void onInit(Entity caster);
	void onExit(Entity caster);
	void onProcess(Entity caster);
	void onHit(Entity caster, Entity target, Area3D hitbox);
	bool onHurt(Entity caster, Area3D attacker);
}