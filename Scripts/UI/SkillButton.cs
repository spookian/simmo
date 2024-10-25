using Godot;
using System;

public partial class SkillButton : Button
{
	bool cooldown = false;
	TextureProgressBar cooldownSprite;
	float maxCooldownTime = 1.0f;
	float cooldownTime = 0.0f;

	SkillDef skill;
	
	// Called when the node enters the scene tree for the first time.

	public void onPressed()
	{
		cooldown = true;
		cooldownTime = 0.0f;
	}

	public override void _Ready()
	{
		cooldownSprite = GetNode<TextureProgressBar>("CanvasGroup/TextureProgressBar");
		cooldownTime = 100.0f;
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		if (cooldown)
		{
			this.MouseFilter = MouseFilterEnum.Ignore;
			if (cooldownTime < 100.0)
			{
				cooldownTime += (float)(delta * 100 / maxCooldownTime);
				if (cooldownTime >= 100.0f) cooldown = false;
			}
		}
		else
		{
			this.MouseFilter = MouseFilterEnum.Stop;
		}
		cooldownSprite.Value = cooldownTime;
	}
}
