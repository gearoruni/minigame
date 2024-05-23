// Upgrade NOTE: replaced 'mul(UNITY_MATRIX_MVP,*)' with 'UnityObjectToClipPos(*)'


Shader"Sprites/kirakira"
{
	Properties
	{
		[PerRendererData] _MainTex ("Sprite Texture", 2D) = "white" {}
		_Color ("Tint", Color) = (1,1,1,1)
		[MaterialToggle] PixelSnap ("Pixel snap", Float) = 0
		_kirakira ("kirakiraColor", Color) = (1,0,0,1)
		[MaterialToggle]_shanshuo ("kirakira", Float) = 0
	}
 
	SubShader
	{
		Tags
		{ 
			"Queue"="Transparent" 
			"IgnoreProjector"="True" 
			"RenderType"="Transparent" 
			"PreviewType"="Plane"
			"CanUseSpriteAtlas"="True"
		}
 
Cull Off //�رձ����޳�

Lighting Off //�رյƹ�

ZWrite Off //�ر�Z����

Blend One OneMinusSrcAlpha     //���Դϵ��one(1)  Ŀ��ϵ��OneMinusSrcAlpha(1-one=0)
 
		Pass
		{
		CGPROGRAM
			#pragma vertex vert
			#pragma fragment frag
			#pragma multi_compile _ PIXELSNAP_ON       //����Unity���벻ͬ�汾��Shader,����ͺ���vert�е�PIXELSNAP_ON��Ӧ
			#pragma shader_feature ETC1_EXTERNAL_ALPHA
#include "UnityCG.cginc"
			
struct appdata_t                           //vert����
{
    float4 vertex : POSITION;
    float4 color : COLOR;
    float2 texcoord : TEXCOORD0;
};
 
struct v2f                                 //vert������ݽṹ
{
    float4 vertex : SV_POSITION;
    fixed4 color : COLOR;
    float2 texcoord : TEXCOORD0;
};
			
fixed4 _Color;
fixed4 _kirakira;
float _shanshuo;

v2f vert(appdata_t IN)
{
    v2f OUT;
    OUT.vertex = UnityObjectToClipPos(IN.vertex);
    OUT.texcoord = IN.texcoord;
    OUT.color = IN.color * _Color;
#ifdef PIXELSNAP_ON
				OUT.vertex = UnityPixelSnap (OUT.vertex);
#endif
    if (_shanshuo == 1)
    {
		
        OUT.color *= _kirakira;
    }
        return OUT;
}
 
sampler2D _MainTex;
sampler2D _AlphaTex;
 
fixed4 SampleSpriteTexture(float2 uv)
{
    fixed4 color = tex2D(_MainTex, uv);
 
#if ETC1_EXTERNAL_ALPHA         //etc1û��͸��ͨ��������һͼ��ȡaֵ
				// get the color from an external texture (usecase: Alpha support for ETC1 on android)
				color.a = tex2D (_AlphaTex, uv).r;
#endif //ETC1_EXTERNAL_ALPHA
 
    return color;
}
 
fixed4 frag(v2f IN) : SV_Target
{
    fixed4 c = SampleSpriteTexture(IN.texcoord) * IN.color;
    c.rgb *= c.a;
    return c;
}
		ENDCG
		}
	}
}