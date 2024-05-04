using Godot;
using System;

public partial class PlayerParticleManager : Node2D
{
	[Export]
	private GpuParticles2D JumpParticles;

	[Export]
	private GpuParticles2D StompParticles;

	[Export]
	private GpuParticles2D DashTrail;

	[Export]
	public GpuParticles2D WallJumpParticles;

	public bool JumpParticlesEmitting
	{
		get
		{
			return GetEmitting(JumpParticles);
		}
		set
		{
			SetEmitting(JumpParticles, value);
		}
	}

	public bool StompParticlesEmitting
	{
		get
		{
			return GetEmitting(StompParticles);
		}
		set
		{
			SetEmitting(StompParticles, value);
		}
	}

	public bool DashTrailEmitting
	{
		get
		{
			return GetEmitting(DashTrail);
		}
		set
		{
			SetEmitting(DashTrail, value);
		}
	}

	public bool WallJumpParticlesEmitting
	{
		get
		{
			return GetEmitting(WallJumpParticles);
		}
		set
		{
			SetEmitting(WallJumpParticles, value);
		}
	}

	private bool GetEmitting(GpuParticles2D particles)
	{
		return particles.Emitting;
	}

	private void SetEmitting(GpuParticles2D particles, bool value)
	{
		if (particles.Emitting && value)
		{
			particles.Restart();
			//Action func = null;
			//func = () =>
			//{
			//	particles.Finished -= func;
			//	particles.Emitting = true;
			//};
			//particles.Finished += func;
		}
		else
		{
			particles.Emitting = value;
		}
	}
}
