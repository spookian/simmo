using Godot;
using System;
namespace SkillDelegates
{
	public delegate void onSkillExit(Entity caster);
	public delegate void onSkillProcess(Entity caster);
	public delegate void onSkillHit(Entity caster, Entity target, Hitbox hitbox); // retool area3d into hitbox
	// happens right before the entity deals damage
	public delegate bool onSkillHurt(Entity caster, Hitbox attackBox);
	// happens right before the entity gets hit
}

public partial class SkillDef : Node
{
	public bool activeSkill = false;
	public bool enableOnIdleOnly = false; // might change it where the entity has a boolean that checks if a skill can be used
	// also only works when the skill is active

	public enum SelectionType
	{
		None,
		SelectEnemy,
		SelectPlace
	}
	public SelectionType selectType; 
	public Texture2D icon;
	public string className; // use it for easier referencing and dictionary calling
	public string displayName;

	public SkillDelegates.onSkillExit skillExit;
	public SkillDelegates.onSkillProcess skillProcess;
	public SkillDelegates.onSkillHit skillHit;
	public SkillDelegates.onSkillHurt skillHurt;
}
