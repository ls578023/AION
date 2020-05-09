// Shader created with Shader Forge v1.38 
// Shader Forge (c) Neat Corporation / Joachim Holmer - http://www.acegikmo.com/shaderforge/
// Note: Manually altering this data may prevent you from opening it in Shader Forge
/*SF_DATA;ver:1.38;sub:START;pass:START;ps:flbk:,iptp:0,cusa:False,bamd:0,cgin:,lico:1,lgpr:1,limd:1,spmd:1,trmd:0,grmd:0,uamb:True,mssp:True,bkdf:False,hqlp:False,rprd:False,enco:False,rmgx:True,imps:True,rpth:0,vtps:0,hqsc:True,nrmq:1,nrsp:0,vomd:0,spxs:False,tesm:0,olmd:1,culm:2,bsrc:0,bdst:1,dpts:2,wrdp:True,dith:0,atcv:False,rfrpo:True,rfrpn:Refraction,coma:15,ufog:True,aust:True,igpj:False,qofs:0,qpre:1,rntp:1,fgom:False,fgoc:False,fgod:False,fgor:False,fgmd:0,fgcr:0.5,fgcg:0.5,fgcb:0.5,fgca:1,fgde:0.01,fgrn:0,fgrf:300,stcl:False,atwp:False,stva:128,stmr:255,stmw:255,stcp:6,stps:0,stfa:0,stfz:0,ofsf:0,ofsu:0,f2p0:False,fnsp:False,fnfb:False,fsmp:False;n:type:ShaderForge.SFN_Final,id:4013,x:33344,y:32543,varname:node_4013,prsc:2|emission-5756-OUT;n:type:ShaderForge.SFN_Tex2d,id:9660,x:32314,y:32860,ptovrint:False,ptlb:MainTex,ptin:_MainTex,varname:node_9660,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,tex:733c99519c0aa404ea2175683a552c30,ntxv:0,isnm:False;n:type:ShaderForge.SFN_Color,id:3,x:32475,y:33012,ptovrint:False,ptlb:MainColor,ptin:_MainColor,varname:node_3,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,c1:1,c2:1,c3:1,c4:1;n:type:ShaderForge.SFN_Multiply,id:5917,x:32722,y:32791,varname:node_5917,prsc:2|A-9660-RGB,B-3-RGB;n:type:ShaderForge.SFN_Fresnel,id:3919,x:32468,y:32295,varname:node_3919,prsc:2|NRM-3394-OUT,EXP-1984-OUT;n:type:ShaderForge.SFN_Color,id:5330,x:32431,y:32455,ptovrint:False,ptlb:FresnelColor,ptin:_FresnelColor,varname:node_5330,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,c1:0.4117647,c2:0.3137931,c3:0.1029412,c4:1;n:type:ShaderForge.SFN_Multiply,id:7312,x:32722,y:32563,varname:node_7312,prsc:2|A-3919-OUT,B-5330-RGB,C-9225-OUT,D-2119-OUT;n:type:ShaderForge.SFN_Add,id:5756,x:32987,y:32606,varname:node_5756,prsc:2|A-7312-OUT,B-5917-OUT;n:type:ShaderForge.SFN_Slider,id:2318,x:31953,y:32459,ptovrint:False,ptlb:FresneRange,ptin:_FresneRange,varname:node_2318,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,min:0,cur:8.290599,max:10;n:type:ShaderForge.SFN_ToggleProperty,id:2119,x:32157,y:32743,ptovrint:False,ptlb:FreToggle,ptin:_FreToggle,varname:node_2119,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,on:True;n:type:ShaderForge.SFN_Slider,id:9225,x:32078,y:32634,ptovrint:False,ptlb:FresnelBright,ptin:_FresnelBright,varname:node_9225,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,min:0,cur:3.247864,max:5;n:type:ShaderForge.SFN_RemapRange,id:1984,x:32266,y:32414,varname:node_1984,prsc:2,frmn:0,frmx:10,tomn:10,tomx:0|IN-2318-OUT;n:type:ShaderForge.SFN_NormalVector,id:3394,x:32160,y:32235,prsc:2,pt:False;proporder:9660-3-5330-2119-2318-9225;pass:END;sub:END;*/

Shader "Custom//InnerGloss" {
    Properties {
        _MainTex ("MainTex", 2D) = "white" {}
        _MainColor ("MainColor", Color) = (1,1,1,1)
        _FresnelColor ("FresnelColor", Color) = (0.4117647,0.3137931,0.1029412,1)
        [MaterialToggle] _FreToggle ("FreToggle", Float ) = 1
        _FresneRange ("FresneRange", Range(0, 10)) = 8.290599
        _FresnelBright ("FresnelBright", Range(0, 5)) = 3.247864
    }
    SubShader {
        Tags {
            "RenderType"="Opaque"
        }
        Pass {
            Name "FORWARD"
            Tags {
                "LightMode"="ForwardBase"
            }
            Cull Off
            
            
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #define UNITY_PASS_FORWARDBASE
            #include "UnityCG.cginc"
            #pragma multi_compile_fwdbase_fullshadows
            #pragma multi_compile_fog
            #pragma only_renderers d3d9 d3d11 glcore gles 
            #pragma target 3.0
            uniform sampler2D _MainTex; uniform float4 _MainTex_ST;
            uniform float4 _MainColor;
            uniform float4 _FresnelColor;
            uniform float _FresneRange;
            uniform fixed _FreToggle;
            uniform float _FresnelBright;
            struct VertexInput {
                float4 vertex : POSITION;
                float3 normal : NORMAL;
                float2 texcoord0 : TEXCOORD0;
            };
            struct VertexOutput {
                float4 pos : SV_POSITION;
                float2 uv0 : TEXCOORD0;
                float4 posWorld : TEXCOORD1;
                float3 normalDir : TEXCOORD2;
                UNITY_FOG_COORDS(3)
            };
            VertexOutput vert (VertexInput v) {
                VertexOutput o = (VertexOutput)0;
                o.uv0 = v.texcoord0;
                o.normalDir = UnityObjectToWorldNormal(v.normal);
                o.posWorld = mul(unity_ObjectToWorld, v.vertex);
                o.pos = UnityObjectToClipPos( v.vertex );
                UNITY_TRANSFER_FOG(o,o.pos);
                return o;
            }
            float4 frag(VertexOutput i, float facing : VFACE) : COLOR {
                float isFrontFace = ( facing >= 0 ? 1 : 0 );
                float faceSign = ( facing >= 0 ? 1 : -1 );
                i.normalDir = normalize(i.normalDir);
                i.normalDir *= faceSign;
                float3 viewDirection = normalize(_WorldSpaceCameraPos.xyz - i.posWorld.xyz);
                float3 normalDirection = i.normalDir;
////// Lighting:
////// Emissive:
                float4 _MainTex_var = tex2D(_MainTex,TRANSFORM_TEX(i.uv0, _MainTex));
                float3 emissive = ((pow(1.0-max(0,dot(i.normalDir, viewDirection)),(_FresneRange*-1.0+10.0))*_FresnelColor.rgb*_FresnelBright*_FreToggle)+(_MainTex_var.rgb*_MainColor.rgb));
                float3 finalColor = emissive;
                fixed4 finalRGBA = fixed4(finalColor,1);
                UNITY_APPLY_FOG(i.fogCoord, finalRGBA);
                return finalRGBA;
            }
            ENDCG
        }
        Pass {
            Name "ShadowCaster"
            Tags {
                "LightMode"="ShadowCaster"
            }
            Offset 1, 1
            Cull Off
            
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #define UNITY_PASS_SHADOWCASTER
            #include "UnityCG.cginc"
            #include "Lighting.cginc"
            #pragma fragmentoption ARB_precision_hint_fastest
            #pragma multi_compile_shadowcaster
            #pragma multi_compile_fog
            #pragma only_renderers d3d9 d3d11 glcore gles 
            #pragma target 3.0
            struct VertexInput {
                float4 vertex : POSITION;
            };
            struct VertexOutput {
                V2F_SHADOW_CASTER;
            };
            VertexOutput vert (VertexInput v) {
                VertexOutput o = (VertexOutput)0;
                o.pos = UnityObjectToClipPos( v.vertex );
                TRANSFER_SHADOW_CASTER(o)
                return o;
            }
            float4 frag(VertexOutput i, float facing : VFACE) : COLOR {
                float isFrontFace = ( facing >= 0 ? 1 : 0 );
                float faceSign = ( facing >= 0 ? 1 : -1 );
                SHADOW_CASTER_FRAGMENT(i)
            }
            ENDCG
        }
    }
    FallBack "Diffuse"
    CustomEditor "ShaderForgeMaterialInspector"
}
