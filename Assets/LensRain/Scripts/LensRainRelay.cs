using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class LensRainRelay : MonoBehaviour 
{
	//Singleton
	private static LensRainRelay m_Instance;
	public static LensRainRelay Instance
	{
		get 
		{
			if(m_Instance == null) 
				m_Instance = GameObject.FindObjectOfType<LensRainRelay>();
			return m_Instance; 
		}
	}

	[HideInInspector] public RenderTexture m_LensRainTexture; // Target texture for rain camera
	[HideInInspector] public LensRainSystems m_LensRainSystems; // Reference to systems

	// Called when component is added
	void Reset()
	{
		m_LensRainTexture = GetRainTexture(); // Get rain texture
		m_LensRainSystems = GetRainSystems(); // Get rain system
		m_LensRainSystems.GetComponent<Camera>().targetTexture = m_LensRainTexture; // Set camera target
	}

	// Get rain texture
	RenderTexture GetRainTexture()
	{
		if(m_LensRainTexture) // If texture exists
			return m_LensRainTexture; // Return
		else // If texture doesnt exist
		{
			var tex = new RenderTexture(Screen.width, Screen.height, 16, RenderTextureFormat.ARGB32); // Create new RT
			tex.name = "RainTexture"; // Set name
			return tex; // Return
		}
	}

	// Get rain systems
	LensRainSystems GetRainSystems()
	{
		Transform systemTransform = transform.Find("RainSystems"); // Find current systems
		if(systemTransform) // If systems exist
			return systemTransform.GetComponent<LensRainSystems>(); // Return
		else // If systems dont exist
		{
			var prefab = (GameObject)Resources.Load("RainSystems"); // Load prefab
			var instance = Instantiate(prefab, transform.position, transform.rotation, transform); // Instantiate
			instance.name = "RainSystems"; // Set name
			return instance.GetComponent<LensRainSystems>(); // Return
		}
	}

	// Called from Rain.cs when effect is disabled
	public void Destroy()
	{
		if(m_LensRainSystems) // If rain systems are active
			DestroyImmediate(m_LensRainSystems.gameObject); // Destroy them
		DestroyImmediate(this); // Destroy the relay // TODO - Error on stop play mode
	}

	// Relay parameters from Rain.cs to RainSystems
	public void RelayParameters(LensRain input)
	{
		if(m_LensRainSystems) // If rain systems are active
			m_LensRainSystems.SetParameters(input); // Relay
	}
}
