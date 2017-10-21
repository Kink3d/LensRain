using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RainSystems : MonoBehaviour 
{
	public ParticleSystem[] m_Systems; // Particle system array

	// Set parameters from the effect on the particle systems
	public void SetParameters(Rain input)
	{
		for(int i = 0; i < m_Systems.Length; i++) // Iterate particle systems
		{
			var main = m_Systems[i].main; // Get main module
			main.startSize = new ParticleSystem.MinMaxCurve(input.size, input.size * 0.5f); // Set size
			var emission = m_Systems[i].emission; // Get emission module
			emission.rateOverTime = new ParticleSystem.MinMaxCurve(input.amount * 2f); // Set amount
		}
	}
}
