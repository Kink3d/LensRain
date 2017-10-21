using System;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

public enum RainDebugMode
{
	None,
	Height,
	Distortion
}

[Serializable]
public sealed class RainDebugModeParameter : ParameterOverride<RainDebugMode> {}

[Serializable]
[PostProcess(typeof(RainRenderer), PostProcessEvent.AfterStack, "Custom/Rain")]
public class Rain : PostProcessEffectSettings 
{
	[Tooltip("Debug visualizations for parts of the effect")]
    public RainDebugModeParameter debugMode = new RainDebugModeParameter { value = RainDebugMode.None };
	[Range(0f, 1f)] public FloatParameter dripRefraction = new FloatParameter { value = 0.5f };
	[Range(0f, 10f)] public FloatParameter dripCount = new FloatParameter { value = 1f };
	[Range(0f, 1f)] public FloatParameter dripSize = new FloatParameter { value = 0.5f };
	[Range(0f, 1f)] public FloatParameter dripSpeed = new FloatParameter { value = 0.5f };
	[Range(-1f, 1f)] public FloatParameter direction = new FloatParameter { value = 0f };
}

public sealed class RainRenderer : PostProcessEffectRenderer<Rain>
{
	void InitializeObjects(Camera camera)
	{
		if(!RainRelay._Instance)
			camera.gameObject.AddComponent<RainRelay>();
	}

	void PrepareRender(PropertySheet sheet)
	{
		RenderTexture lensRainRt = RainRelay._Instance.TrySetup();
		if(lensRainRt)
			sheet.properties.SetTexture("_DropletNormal", lensRainRt);
	}

	void SendParameters()
	{
		RainRelay._Instance.RelayParameters(settings);
	}

	public override void Release()
	{
		base.Release();
		if(RainRelay._Instance)
			RainRelay._Instance.Destroy();
	}

    public override void Render(PostProcessRenderContext context)
    {
		var sheet = context.propertySheets.Get(Shader.Find("Hidden/PostProcessing/Extensions/LensRain"));
		InitializeObjects(context.camera);
		PrepareRender(sheet);
		SendParameters();
		//SetDebugDefine(sheet); // Enable this
		sheet.properties.SetFloat("_DebugMode", (int)settings.debugMode.value); // Remove this
		sheet.properties.SetFloat("_Strength", settings.dripRefraction);
        context.command.BlitFullscreenTriangle(context.source, context.destination, sheet, 0);
    }

	public void SetDebugDefine(PropertySheet sheet)
	{
		Debug.Log(settings.debugMode.value);
		switch(settings.debugMode.value)
		{
			case RainDebugMode.None:
				sheet.DisableKeyword("DEBUG_HEIGHT");
				sheet.DisableKeyword("DEBUG_DISTORTION");
				break;
			case RainDebugMode.Height:
				sheet.EnableKeyword("DEBUG_HEIGHT");
				sheet.DisableKeyword("DEBUG_DISTORTION");
				break;
			case RainDebugMode.Distortion:
				sheet.DisableKeyword("DEBUG_HEIGHT");
				sheet.EnableKeyword("DEBUG_DISTORTION");
				break;
		}
	}
}
