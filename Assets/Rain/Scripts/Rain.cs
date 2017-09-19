using System;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

[Serializable]
[PostProcess(typeof(RainRenderer), PostProcessEvent.AfterStack, "Custom/Rain")]
public class Rain : PostProcessEffectSettings 
{
	[Range(0f, 1f)] public FloatParameter dripRefraction = new FloatParameter { value = 0.5f };
	[Range(0f, 10f)] public FloatParameter dripCount = new FloatParameter { value = 1f };
	[Range(0f, 1f)] public FloatParameter dripSize = new FloatParameter { value = 0.5f };
	[Range(0f, 1f)] public FloatParameter dripSpeed = new FloatParameter { value = 0.5f };
	[Range(-1f, 1f)] public FloatParameter direction = new FloatParameter { value = 0f };
}

public sealed class RainRenderer : PostProcessEffectRenderer<Rain>
{
	RainRelay InitializeObjects(Camera camera)
	{
		RainRelay relay = camera.gameObject.GetComponent<RainRelay>();
		if(!relay)
			relay = camera.gameObject.AddComponent<RainRelay>();
		return relay;
	}

	void PrepareRender(RainRelay relay, PropertySheet sheet)
	{
		RenderTexture lensRainRt = relay.TrySetup();
		if(lensRainRt)
			sheet.properties.SetTexture("_DropletNormal", lensRainRt);
	}

	void SendParameters(RainRelay relay)
	{
		relay.RelayParameters(settings);
	}

    public override void Render(PostProcessRenderContext context)
    {
		var sheet = context.propertySheets.Get(Shader.Find("Hidden/PostProcessing/Extensions/LensRain"));
		RainRelay relay = InitializeObjects(context.camera);
		PrepareRender(relay, sheet);
		SendParameters(relay);
		sheet.properties.SetFloat("_Strength", settings.dripRefraction);
        context.command.BlitFullscreenTriangle(context.source, context.destination, sheet, 0);
    }
}
