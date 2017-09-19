using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class RainRelay : MonoBehaviour 
{
	private GameObject prefab;
	[SerializeField][HideInInspector]
	private GameObject m_Instance;
	[SerializeField][HideInInspector]
	private RenderTexture m_Rt;
	[SerializeField][HideInInspector]	
	private RainSystems m_RainSystems;

	public RenderTexture TrySetup()
	{
		if(RequiresInit())
		{
			Cleanup();
			Init();
		}	
		return m_Rt;
	}

	bool RequiresInit()
	{
		if(!m_Rt || !m_Instance)
			return true;
		else 
			return false;
	}

	void Cleanup()
	{
		if(prefab)
			prefab = null;
		if(m_Rt)
			DestroyImmediate(m_Rt);
		if(m_Instance)
			Destroy(m_Instance);
	}

	public void RelayParameters(Rain input)
	{
		m_RainSystems.SetRainParameters(input);
	}

	// Use this for initialization
	public void Init () 
	{
		m_Rt = new RenderTexture(Screen.width, Screen.height, 16, RenderTextureFormat.ARGB32);
		prefab = (GameObject)Resources.Load("RainSystems");
		Camera c = GetComponent<Camera>();
		m_Instance = Instantiate(prefab, c.transform.position, c.transform.rotation, c.transform);
		m_Instance.name = "RainSystems";
		m_Instance.GetComponent<Camera>().targetTexture = m_Rt;
		m_RainSystems = m_Instance.GetComponent<RainSystems>();
	}
}
