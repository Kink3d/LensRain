using System;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

[Serializable]
[PostProcess(typeof(RainRenderer), PostProcessEvent.AfterStack, "Custom/Rain")]
public class Rain : PostProcessEffectSettings 
{
	public BoolParameter debug = new BoolParameter { value = false };
	[Range(0f, 1f)] public FloatParameter strength = new FloatParameter { value = 1f };
	//[Range(0f, 10f)] public FloatParameter dripCount = new FloatParameter { value = 1f };
	//[Range(0f, 1f)] public FloatParameter dripSize = new FloatParameter { value = 0.5f };
	//[Range(0f, 1f)] public FloatParameter dripSpeed = new FloatParameter { value = 0.5f };
	//[Range(-1f, 1f)] public FloatParameter direction = new FloatParameter { value = 0f };
}

public sealed class RainRenderer : PostProcessEffectRenderer<Rain>
{
	public override void Render(PostProcessRenderContext context)
    {
		var sheet = context.propertySheets.Get(Shader.Find("Hidden/PostProcessing/Extensions/Rain"));

		InitializeObjects(context.camera);

		sheet.properties.SetFloat("_Debug", settings.debug == true ? 1 : 0);
		sheet.properties.SetTexture("_RainTex", RainRelay.Instance.m_RainTexture);
		sheet.properties.SetFloat("_Strength", settings.strength);
        context.command.BlitFullscreenTriangle(context.source, context.destination, sheet, 0);
    }
	
	public override void Release()
	{
		if(RainRelay.Instance != null)
			RainRelay.Instance.Destroy();
	}

	void InitializeObjects(Camera camera)
	{
		if(RainRelay.Instance == null)
			camera.gameObject.AddComponent<RainRelay>();
	}
}
