using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LensRainSystems : MonoBehaviour 
{
	public ParticleSystem[] m_Systems; // Particle system array

	// Set parameters from the effect on the particle systems
	public void SetParameters(LensRain input)
	{
		for(int i = 0; i < m_Systems.Length; i++) // Iterate particle systems
		{
			var main = m_Systems[i].main; // Get main module
			main.startSize = new ParticleSystem.MinMaxCurve(input.size, input.size * 0.5f); // Set size
			var emission = m_Systems[i].emission; // Get emission module
			emission.rateOverTime = new ParticleSystem.MinMaxCurve(input.amount * 2f); // Set amount
			var subEmitters = m_Systems[i].subEmitters; // Get sub emitters
			bool hasSubmitters = subEmitters.subEmittersCount > 0 ? true : false; // Does the system have subemitters?
			if(hasSubmitters) // If the system has subemitters
			{
				ParticleSystem childSystem = subEmitters.GetSubEmitterSystem(0); // Get sub emitter
				if(childSystem)
				{
					var forceOverLifeTime = childSystem.forceOverLifetime; // Get force over lifetime
					forceOverLifeTime.x = new ParticleSystem.MinMaxCurve(input.direction, 0); // Set direction
				}
			}
		}
	}
}
