using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class RainRelay : MonoBehaviour 
{
	private static RainRelay m_Instance;
	public static RainRelay Instance
	{
		get 
		{
			if(m_Instance == null) 
				m_Instance = GameObject.FindObjectOfType<RainRelay>();
			return m_Instance; 
		}
	}

	[HideInInspector] public RenderTexture m_RainTexture;
	RainSystems m_RainSystems;

	// Called when component is added
	void Reset()
	{
		m_RainTexture = GetRainTexture(); // Get rain texture
		m_RainSystems = GetRainSystems(); // Get rain system
		m_RainSystems.GetComponent<Camera>().targetTexture = m_RainTexture; // Set camera target
	}

	// Get rain texture
	RenderTexture GetRainTexture()
	{
		if(m_RainTexture) // If texture exists
			return m_RainTexture; // Return
		else // If texture doesnt exist
		{
			var tex = new RenderTexture(Screen.width, Screen.height, 16, RenderTextureFormat.ARGB32); // Create new RT
			tex.name = "RainTexture"; // Set name
			return tex; // Return
		}
	}

	// Get rain systems
	RainSystems GetRainSystems()
	{
		Transform systemTransform = transform.Find("RainSystems"); // Find current systems
		if(systemTransform) // If systems exist
			return systemTransform.GetComponent<RainSystems>(); // Return
		else // If systems dont exist
		{
			var prefab = (GameObject)Resources.Load("RainSystems"); // Load prefab
			var instance = Instantiate(prefab, transform.position, transform.rotation, transform); // Instantiate
			instance.name = "RainSystems"; // Set name
			return instance.GetComponent<RainSystems>(); // Return
		}
	}

	// Called from Rain.cs when effect is disabled
	public void Destroy()
	{
		if(m_RainSystems) // If rain systems are active
			DestroyImmediate(m_RainSystems.gameObject); // Destroy them
		DestroyImmediate(this); // Destroy the relay // TODO - Error on stop play mode
	}
}
