Shader "Custom/GrassShader"
{
    //Reference:https://roystan.net/articles/grass-shader.html
    Properties
    {
        _TopColor("Top Color", Color) = (1,1,1,1)
        _BottomColor("Bottom Color", Color) = (1,1,1,1)
        _TranslucentGain("Translucent Gain", Range(0,1)) = 0.5
        _BendRotationRandom("Bend Rotation Random", Range(0, 1)) = 0.2
        _BladeWidth("Blade Width", Float) = 0.05
        _BladeWidthRandom("Blade Width Random", Float) = 0.02
        _BladeHeight("Blade Height", Float) = 0.5
        _BladeHeightRandom("Blade Height Random", Float) = 0.3
        _BladeForward("Blade Forward Amount", Float) = 0.38
        _BladeCurve("Blade Curveature Amount", Range(1,4)) = 2
        _TessellationUniform("Tessellation Uniform", Range(1, 64)) = 1
    }

    CGINCLUDE
    // Upgrade NOTE: excluded shader from OpenGL ES 2.0 because it uses non-square matrices
    #pragma exclude_renderers gles
    #include "UnityCG.cginc"
    #include "Autolight.cginc"

    // Add inside the CGINCLUDE block, below the other #include statements.
    #include "Utility/TesselationUtility.cginc"

    //Variables we need access to in this portion are added here
    float _BendRotationRandom;
    float _BladeHeight;
    float _BladeHeightRandom;
    float _BladeWidth;
    float _BladeWidthRandom;
    float _BladeForward;
    float _BladeCurve;

    //Define a constant. Means how many segments are in one blade of grass
    #define BLADE_SEGMENTS 3

    // Simple noise function, sourced from http://answers.unity.com/answers/624136/view.html
    // Extended discussion on this function can be found at the following link:
    // https://forum.unity.com/threads/am-i-over-complicating-this-random-function.454887/#post-2949326
    // Returns a number in the 0...1 range.
    float rand(float3 co)
    {
        return frac(sin(dot(co.xyz, float3(12.9898, 78.233, 53.539))) * 43758.5453);
    }

    // Construct a rotation matrix that rotates around the provided axis, sourced from:
    // https://gist.github.com/keijiro/ee439d5e7388f3aafc5296005c8c3f33
    float3x3 AngleAxis3x3(float angle, float3 axis)
    {
        float c, s;
        sincos(angle, s, c);

        float t = 1 - c;
        float x = axis.x;
        float y = axis.y;
        float z = axis.z;

        return float3x3(
            t * x * x + c, t * x * y - s * z, t * x * z + s * y,
            t * x * y + s * z, t * y * y + c, t * y * z - s * x,
            t * x * z - s * y, t * y * z + s * x, t * z * z + c
            );
    }

    //Structs for different function inputs or outputs below
    struct geometryOutput 
    {
        float4 pos : SV_POSITION;
        float2 uv : TEXCOORD0;
        unityShadowCoord4 _ShadowCoord : TEXCOORD1;
        float3 normal : NORMAL;
    }; 

    //Helper function since we have to call it three times
    geometryOutput VertexOutput(float3 pos, float2 uv, float3 normal) 
    {
        geometryOutput o;
        o.pos = UnityObjectToClipPos(pos);
        o.uv = uv;
        o._ShadowCoord = ComputeScreenPos(o.pos);
        #if UNITY_PASS_SHADOWCASTER
        // Applying the bias prevents artifacts from appearing on the surface.
        o.pos = UnityApplyLinearShadowBias(o.pos);
        #endif
        o.normal = UnityObjectToWorldNormal(normal);
        return o;
    }

    //Helper function that generates grass vertexes given inputs
    geometryOutput GenerateGrassVertex(float3 vertexPosition, float width, float height, float forward, float2 uv, float3x3 transformMatrix) 
    {
        float3 tangentPoint = float3(width, forward, height);

        //Assuming no curvature calculation
        float3 tangentNormal = normalize(float3(0, -1, forward));
        float3 localNormal = mul(transformMatrix, tangentNormal);

        float3 localPosition = vertexPosition + mul(transformMatrix, tangentPoint);
        return VertexOutput(localPosition, uv, localNormal);
    }

    //Custom geometry shader. maxvertexcount tells us how many vertexes should be in this function.
    [maxvertexcount(BLADE_SEGMENTS * 2 + 1)]
    void geo(triangle vertexOutput IN[3], inout TriangleStream<geometryOutput> triStream)
    {
        //Getting a point to emit blades from. In this case we are taking only one vertice of a face
        float3 pos = IN[0].vertex;

        //Calculate the cross product between two vectors, which gives us a perpendicular vector to the two vectors
        float3 vNormal = IN[0].normal;
        float4 vTangent = IN[0].tangent;
        float3 vBinormal = cross(vNormal, vTangent) * vTangent.w;

        // Add below the lines declaring the three vectors.
        float3x3 tangentToLocal = float3x3(
            vTangent.x, vBinormal.x, vNormal.x,
            vTangent.y, vBinormal.y, vNormal.y,
            vTangent.z, vBinormal.z, vNormal.z
            );

        //Input for our rand functions so that it will relate to world position not local position
        //The calculation is the world position of that vertex
        float3 worldPos = mul(unity_ObjectToWorld, pos);

        //Create a random rotation matrix around the up axis. This can then be used to rotate very blade randomly.
        //We use "pos" as the input so that the rotation of the blade is consistent every frame.
        float3x3 facingRotationMatrix = AngleAxis3x3(rand(worldPos) * UNITY_TWO_PI, float3(0, 0, 1));

        //pos.zzx is SWIZZLING. Composes a Vector3 composed like new Vector3(z, z, x)
        //Creates a random rotation matrix based off of position (using a swizzle).
        //Multiply by PI and 0.5 to get a range of rotation of 0 -> 90.
        float3x3 bendRotationMatrix = AngleAxis3x3(rand(worldPos.zzx) * _BendRotationRandom * UNITY_PI * 0.5, float3(-1, 0, 0));

        //The transformation matrix that needs to be applied to each vertex of our blade's triangle.
        float3x3 transformationMatrix = mul(mul(tangentToLocal, facingRotationMatrix), bendRotationMatrix);

        //Calculates a random height and width based off our inspector variables
        float height = (rand(worldPos.zyx) * 2 - 1) * _BladeHeightRandom + _BladeHeight;
        float width = (rand(worldPos.xzy) * 2 - 1) * _BladeWidthRandom + _BladeWidth;

        //Create a random forward based on a swizzle and the inputted blade forward amount
        float forward = rand(worldPos.yyz) * _BladeForward;

        for (int i = 0; i < BLADE_SEGMENTS; i++) 
        {
            float t = i / (float)BLADE_SEGMENTS;
            //As t goes up, height increases
            float segmentHeight = height * t;

            //As t goes up, width decreases, so we invert t.
            float segmentWidth = width * (1 - t);

            //Create how forward the segment will be. We use a pow so that it results in a curve
            float segmentForward = pow(t, _BladeCurve) * forward;

            //float3x3 transformMatrix = i == 0 ? transformationMatrixFacing : transformationMatrix;
        
            //We add the "horizontal vertices" of the blade going up for each blade segment.
            //The diagonal is automatically connected since we are adding vertices.
            triStream.Append(GenerateGrassVertex(pos, segmentWidth, segmentHeight, segmentForward, float2(0, t), transformationMatrix));
            triStream.Append(GenerateGrassVertex(pos, -segmentWidth, segmentHeight, segmentForward, float2(1, t), transformationMatrix));
        }

        //Add the last vertex that makes the tip of the grass blade
        triStream.Append(GenerateGrassVertex(pos, 0, height, forward, float2(0.5, 1), transformationMatrix));
    }
    ENDCG

    SubShader
    {
        Cull Off

        Pass
        {
            Tags
            {
                "RenderType" = "Opaque"
                "LightMode" = "ForwardBase"
            }

            CGPROGRAM
            //Using vertex and frag shader
            #pragma vertex vert
            #pragma fragment frag

            //Using the custom geometry shader above
            #pragma geometry geo

            #pragma target 4.6
            #pragma multi_compile_fwdbase
            
            //Hull and domain are two different stages of the rendering pipeline (similar to vert and frag being other parts of the pipeline)
            #pragma hull hull
            #pragma domain domain

            #include "Lighting.cginc"

            float4 _TopColor;
            float4 _BottomColor;
            float _TranslucentGain;

            float4 frag(geometryOutput i, fixed facing : VFACE) : SV_Target
            {
                //Get a normal value based on which side we're facing
               float3 normal = facing > 0 ? i.normal : -i.normal;

               //Get calculated shadow values on our color
               float shadow = SHADOW_ATTENUATION(i);

               //Calculate our color based on translucence, shadow, lighting, etc.
               //This is from the formula I = N * L, I is intensity, N is normal of surface, L is normal dir of the main directional light
               float NdotL = saturate(saturate(dot(normal, _WorldSpaceLightPos0)) + _TranslucentGain) * shadow;

               float3 ambient = ShadeSH9(float4(normal, 1));
               float4 lightIntensity = NdotL * _LightColor0 + float4(ambient, 1);
               float4 col = lerp(_BottomColor, _TopColor * lightIntensity, i.uv.y);

               return col;
            }
            ENDCG
        }

        // Second pass that enables casting shadows
        Pass
        {
            Tags
            {
                "LightMode" = "ShadowCaster"
            }

            CGPROGRAM
            #pragma vertex vert
            #pragma geometry geo
            #pragma fragment frag
            #pragma hull hull
            #pragma domain domain
            #pragma target 4.6
            #pragma multi_compile_shadowcaster

            float4 frag(geometryOutput i, fixed facing : VFACE) : SV_Target
            {
               SHADOW_CASTER_FRAGMENT(i)
            }
            ENDCG
        }
    }
}
