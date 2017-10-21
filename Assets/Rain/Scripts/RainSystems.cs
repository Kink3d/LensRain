using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RainSystems : MonoBehaviour 
{
	/*public ParticleSystems m_Systems;
	public Defaults m_Defaults;
	
	[Serializable]
	public class ParticleSystems
	{
		public ParticleSystem Dot;
		public ParticleSystem Dot2;
		public ParticleSystem Dot2Drip;
		public ParticleSystem Streaks;
		public ParticleSystem StreaksTrail;
	}

	[Serializable]
	public class Defaults
	{
		public Vector2 startSize = new Vector2(0.04f, 0.2f);
		public float speed = 1f;
		public float count = 1f;
		public Vector2 direction = new Vector2(1f, 2f);
	}

	public void SetRainParameters(Rain input)
	{
		SetParameters(m_Systems.Dot, input.dripCount * 4, input.dripSize * 1.5f, 0, 0, false, RotationAxis.X);
		SetParameters(m_Systems.Dot2, input.dripCount * 4, input.dripSize * 1.5f, 0, 0, false, RotationAxis.X);
		SetParameters(m_Systems.Dot2Drip, 0, input.dripSize * 1.5f, input.dripSpeed, input.direction, true, RotationAxis.X);
		SetParameters(m_Systems.Streaks, input.dripCount * 0.5f, input.dripSize, input.dripSpeed * 2, -input.direction * 10f, false, RotationAxis.Y);
		SetParameters(m_Systems.StreaksTrail, 8, input.dripSize, 0, 0, false, RotationAxis.X);
	}

	void SetParameters(ParticleSystem system, float count, float scale, float speed, float direction, bool inheritSize, RotationAxis directionAxis)
	{
		var main = system.main;
		Vector2 size = inheritSize ? Vector2.one : m_Defaults.startSize;
		main.startSize = new ParticleSystem.MinMaxCurve(size.x * scale, size.y * scale);
		main.startSpeed = new ParticleSystem.MinMaxCurve(m_Defaults.speed * speed);

		var emission = system.emission;
		emission.rateOverTime = new ParticleSystem.MinMaxCurve(count);

		var forceOverLifetime = system.forceOverLifetime;
		var forceCurve = new ParticleSystem.MinMaxCurve(m_Defaults.direction.x * direction, m_Defaults.direction.y * direction);
		switch(directionAxis)
		{
			case RotationAxis.X:
				forceOverLifetime.x = forceCurve;
				break;
			case RotationAxis.Y:
				forceOverLifetime.y = forceCurve;
				break;
			case RotationAxis.Z:
				forceOverLifetime.z = forceCurve;
				break;
		}
		
	}

	public enum RotationAxis {X, Y, Z}*/
}
