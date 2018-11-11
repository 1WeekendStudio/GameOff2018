// Upgrade NOTE: replaced 'mul(UNITY_MATRIX_MVP,*)' with 'UnityObjectToClipPos(*)'

Shader "Custom/Ground"
{
    Properties
    {
        _GridAlpha ("Grid Alpha", Range(0,1)) = 1
        _GridColor ("Grid Color", Color) = (0,0,0,1)
        _GridWidth("Grid Width", float) = 10
        _GridHeight("Grid Height", float) = 10
        _LineWidth ("Line Width", float) = 0.1
        _LineHardness ("Line Hardness", Range(0,1)) = 0.8
        _TileStatesTexture ("Tile States Texture", 2D) = "" {}
    }
    SubShader
    {
        Tags {"Queue" = "Transparent" "IgnoreProjector" = "True" "RenderType" = "Transparent"}
        ZWrite Off
        Blend SrcAlpha OneMinusSrcAlpha
        Pass
        {
            // 1.) This will be the base forward rendering pass in which ambient, vertex, and
            // main directional light will be applied. Additional lights will need additional passes
            // using the "ForwardAdd" lightmode.
            // see: http://docs.unity3d.com/Manual/SL-PassTags.html
            Tags { "LightMode" = "ForwardBase" }

            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #include "UnityCG.cginc"

            // 2.) This matches the "forward base" of the LightMode tag to ensure the shader compiles
            // properly for the forward bass pass. As with the LightMode tag, for any additional lights
            // this would be changed from _fwdbase to _fwdadd.
            #pragma multi_compile_fwdbase

            // 3.) Reference the Unity library that includes all the lighting shadow macros
            #include "AutoLight.cginc"
 
            uniform float _GridAlpha;
            uniform float4 _GridColor;
            uniform float _LineWidth;
            uniform float _GridWidth;
            uniform float _GridHeight;
            uniform float _LineHardness;
            sampler2D _TileStatesTexture;
  
            struct v2f
            {
                float4 pos : SV_POSITION;
                float2 texcoord : TEXCOORD0;
                
                // 4.) The LIGHTING_COORDS macro (defined in AutoLight.cginc) defines the parameters needed to sample 
                // the shadow map. The (0,1) specifies which unused TEXCOORD semantics to hold the sampled values - 
                // As I'm not using any texcoords in this shader, I can use TEXCOORD0 and TEXCOORD1 for the shadow 
                // sampling. If I was already using TEXCOORD for UV coordinates, say, I could specify
                // LIGHTING_COORDS(1,2) instead to use TEXCOORD1 and TEXCOORD2.
                LIGHTING_COORDS(1,2)
            };
 
            v2f vert (appdata_full v)
            {
                v2f o;
                o.pos = UnityObjectToClipPos( v.vertex);
                o.texcoord = v.texcoord1;

                // 5.) The TRANSFER_VERTEX_TO_FRAGMENT macro populates the chosen LIGHTING_COORDS in the v2f structure
                // with appropriate values to sample from the shadow/lighting map
                TRANSFER_VERTEX_TO_FRAGMENT(o);

                return o;
            }
 
            fixed4 frag(v2f i) : COLOR
            {
                float shadowStrength = 0.5;
                fixed4 gridColor;

                float gridWidthRatio = 1 / _GridWidth;
                float gridHeightRatio = 1 / _GridHeight;

                /* GRID */
                // Line width in cell size percent.
                float lineWidth = 1 - _LineWidth;

                float xModulo = fmod(i.texcoord.x, gridWidthRatio);
                float yModulo = fmod(i.texcoord.y, gridHeightRatio);

                // Linear function that equal 1 when the pixel is on a line and equal 0 when exactly between lines.
                float xColorFactor = 1 - (min(xModulo, gridWidthRatio - xModulo)) * 2 / gridWidthRatio;
                float yColorFactor = 1 - (min(yModulo, gridHeightRatio - yModulo)) * 2 / gridHeightRatio;

                float colorFactor = max(xColorFactor, yColorFactor);

                // Linear function that equal 1 when the pixel is on a line and equal 0 when pixel is at a distance 'lineWidth' of the line.
                colorFactor = (max(colorFactor, lineWidth) - lineWidth) * 1 / (1 - lineWidth);

                // Smooth.
                colorFactor = smoothstep(0, 1 - _LineHardness, colorFactor);
                
                /* FILL */
                float halfWidthRatio = gridWidthRatio / 2;
                float halfHeightRatio = gridHeightRatio / 2;
                float x = round((i.texcoord.x - halfWidthRatio) / gridWidthRatio) * gridWidthRatio + halfWidthRatio;
                float y = round((i.texcoord.y - halfHeightRatio) / gridHeightRatio) * gridHeightRatio + halfHeightRatio;
                float4 tileColor = tex2D(_TileStatesTexture, float2(x, y));
                
                // Final color.
                gridColor = lerp(tileColor, _GridColor, colorFactor * _GridAlpha);

                // 6.) The LIGHT_ATTENUATION samples the shadowmap (using the coordinates calculated by TRANSFER_VERTEX_TO_FRAGMENT
                // and stored in the structure defined by LIGHTING_COORDS), and returns the value as a float.
                float attenuation = LIGHT_ATTENUATION(i);

                return lerp(gridColor, tileColor, tileColor.a) * (1 - shadowStrength + attenuation * shadowStrength) * UNITY_LIGHTMODEL_AMBIENT;
            }
            ENDCG
        }
    } 
    Fallback "Diffuse"
}