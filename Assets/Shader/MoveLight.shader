// Shader created with Shader Forge v1.38 
// Shader Forge (c) Neat Corporation / Joachim Holmer - http://www.acegikmo.com/shaderforge/
// Note: Manually altering this data may prevent you from opening it in Shader Forge
/*SF_DATA;ver:1.38;sub:START;pass:START;ps:flbk:,iptp:0,cusa:False,bamd:0,cgin:,lico:1,lgpr:1,limd:0,spmd:1,trmd:0,grmd:0,uamb:True,mssp:True,bkdf:False,hqlp:False,rprd:False,enco:False,rmgx:True,imps:True,rpth:0,vtps:0,hqsc:True,nrmq:1,nrsp:0,vomd:0,spxs:False,tesm:0,olmd:1,culm:0,bsrc:0,bdst:1,dpts:2,wrdp:True,dith:0,atcv:False,rfrpo:True,rfrpn:Refraction,coma:15,ufog:True,aust:True,igpj:False,qofs:0,qpre:1,rntp:1,fgom:False,fgoc:False,fgod:False,fgor:False,fgmd:0,fgcr:0.5,fgcg:0.5,fgcb:0.5,fgca:1,fgde:0.01,fgrn:0,fgrf:300,stcl:False,atwp:False,stva:128,stmr:255,stmw:255,stcp:6,stps:0,stfa:0,stfz:0,ofsf:0,ofsu:0,f2p0:False,fnsp:False,fnfb:False,fsmp:False;n:type:ShaderForge.SFN_Final,id:9361,x:33177,y:32492,varname:node_9361,prsc:2|emission-2085-OUT,custl-2917-OUT;n:type:ShaderForge.SFN_Tex2d,id:3073,x:32154,y:32799,ptovrint:False,ptlb:MainTex,ptin:_MainTex,varname:node_3073,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,tex:733c99519c0aa404ea2175683a552c30,ntxv:0,isnm:False;n:type:ShaderForge.SFN_Color,id:9432,x:32154,y:32618,ptovrint:False,ptlb:MainColor,ptin:_MainColor,varname:node_9432,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,c1:1,c2:1,c3:1,c4:1;n:type:ShaderForge.SFN_Multiply,id:2917,x:32539,y:32775,varname:node_2917,prsc:2|A-9432-RGB,B-3073-RGB,C-4793-OUT;n:type:ShaderForge.SFN_NormalVector,id:829,x:31082,y:33045,prsc:2,pt:False;n:type:ShaderForge.SFN_LightVector,id:5947,x:31071,y:32872,varname:node_5947,prsc:2;n:type:ShaderForge.SFN_Dot,id:3749,x:31374,y:32964,varname:node_3749,prsc:2,dt:1|A-5947-OUT,B-829-OUT;n:type:ShaderForge.SFN_Power,id:9892,x:31918,y:32978,varname:node_9892,prsc:2|VAL-6139-OUT,EXP-9779-OUT;n:type:ShaderForge.SFN_Slider,id:6972,x:31263,y:33202,ptovrint:False,ptlb:DiffuseArea,ptin:_DiffuseArea,varname:node_6972,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,min:0,cur:1.282051,max:5;n:type:ShaderForge.SFN_RemapRange,id:9779,x:31637,y:33143,varname:node_9779,prsc:2,frmn:0,frmx:5,tomn:5,tomx:0|IN-6972-OUT;n:type:ShaderForge.SFN_Multiply,id:4793,x:32165,y:32993,varname:node_4793,prsc:2|A-9892-OUT,B-250-OUT;n:type:ShaderForge.SFN_Slider,id:250,x:31839,y:33220,ptovrint:False,ptlb:DiffusePower,ptin:_DiffusePower,varname:node_250,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,min:0,cur:7.008553,max:10;n:type:ShaderForge.SFN_Add,id:7998,x:31558,y:32913,varname:node_7998,prsc:2|A-8714-OUT,B-3749-OUT;n:type:ShaderForge.SFN_Vector1,id:8714,x:31347,y:32780,varname:node_8714,prsc:2,v1:0.5;n:type:ShaderForge.SFN_Multiply,id:6139,x:31734,y:32855,varname:node_6139,prsc:2|A-8714-OUT,B-7998-OUT;n:type:ShaderForge.SFN_Tex2d,id:2164,x:32397,y:32096,ptovrint:False,ptlb:Texture,ptin:_Texture,varname:node_2164,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,tex:bd91fc259d43c0746b64b7dfea79d19d,ntxv:0,isnm:False|UVIN-5050-OUT;n:type:ShaderForge.SFN_TexCoord,id:6275,x:31735,y:32125,varname:node_6275,prsc:2,uv:0,uaff:False;n:type:ShaderForge.SFN_Panner,id:8743,x:31973,y:32125,varname:node_8743,prsc:2,spu:-0.1,spv:0.2|UVIN-6275-UVOUT;n:type:ShaderForge.SFN_Color,id:5356,x:32397,y:32292,ptovrint:False,ptlb:TexColor,ptin:_TexColor,varname:node_5356,prsc:2,glob:False,taghide:False,taghdr:True,tagprd:False,tagnsco:False,tagnrm:False,c1:0.9558824,c2:0.06325693,c3:0.7465771,c4:1;n:type:ShaderForge.SFN_Multiply,id:2085,x:32787,y:32289,varname:node_2085,prsc:2|A-2164-RGB,B-5356-RGB,C-8220-OUT;n:type:ShaderForge.SFN_ToggleProperty,id:8220,x:32573,y:32506,ptovrint:False,ptlb:bLight,ptin:_bLight,varname:node_8220,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,on:True;n:type:ShaderForge.SFN_Tex2d,id:7216,x:31679,y:32308,ptovrint:False,ptlb:node_7216,ptin:_node_7216,varname:node_7216,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,tex:bd175e87e546fbb40ba8f3ee471d7aac,ntxv:0,isnm:False|UVIN-8975-UVOUT;n:type:ShaderForge.SFN_Tex2d,id:7264,x:31679,y:32501,ptovrint:False,ptlb:node_7264,ptin:_node_7264,varname:node_7264,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,tex:521bd07370c7dbc42af888ac5d060a9b,ntxv:0,isnm:False|UVIN-5595-UVOUT;n:type:ShaderForge.SFN_TexCoord,id:4922,x:31258,y:32314,varname:node_4922,prsc:2,uv:0,uaff:False;n:type:ShaderForge.SFN_Panner,id:8975,x:31452,y:32314,varname:node_8975,prsc:2,spu:-0.3,spv:0|UVIN-4922-UVOUT;n:type:ShaderForge.SFN_TexCoord,id:7051,x:31258,y:32481,varname:node_7051,prsc:2,uv:0,uaff:False;n:type:ShaderForge.SFN_Panner,id:5595,x:31452,y:32481,varname:node_5595,prsc:2,spu:0,spv:0.3|UVIN-7051-UVOUT;n:type:ShaderForge.SFN_Add,id:8029,x:31925,y:32412,varname:node_8029,prsc:2|A-7216-RGB,B-7264-RGB;n:type:ShaderForge.SFN_ComponentMask,id:5948,x:32106,y:32412,varname:node_5948,prsc:2,cc1:0,cc2:1,cc3:-1,cc4:-1|IN-8029-OUT;n:type:ShaderForge.SFN_Add,id:5050,x:32168,y:32125,varname:node_5050,prsc:2|A-8743-UVOUT,B-5948-OUT;proporder:3073-9432-6972-250-2164-5356-8220-7216-7264;pass:END;sub:END;*/

Shader "Custom/MoveLight" {
    Properties {
        _MainTex ("MainTex", 2D) = "white" {}
        _MainColor ("MainColor", Color) = (1,1,1,1)
        _DiffuseArea ("DiffuseArea", Range(0, 5)) = 1.282051
        _DiffusePower ("DiffusePower", Range(0, 10)) = 7.008553
        _Texture ("Texture", 2D) = "white" {}
        [HDR]_TexColor ("TexColor", Color) = (0.9558824,0.06325693,0.7465771,1)
        [MaterialToggle] _bLight ("bLight", Float ) = 1
        _node_7216 ("node_7216", 2D) = "white" {}
        _node_7264 ("node_7264", 2D) = "white" {}
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
            
            
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #define UNITY_PASS_FORWARDBASE
            #include "UnityCG.cginc"
            #include "AutoLight.cginc"
            #pragma multi_compile_fwdbase_fullshadows
            #pragma multi_compile_fog
            #pragma only_renderers d3d9 d3d11 glcore gles 
            #pragma target 3.0
            uniform sampler2D _MainTex; uniform float4 _MainTex_ST;
            uniform float4 _MainColor;
            uniform float _DiffuseArea;
            uniform float _DiffusePower;
            uniform sampler2D _Texture; uniform float4 _Texture_ST;
            uniform float4 _TexColor;
            uniform fixed _bLight;
            uniform sampler2D _node_7216; uniform float4 _node_7216_ST;
            uniform sampler2D _node_7264; uniform float4 _node_7264_ST;
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
                LIGHTING_COORDS(3,4)
                UNITY_FOG_COORDS(5)
            };
            VertexOutput vert (VertexInput v) {
                VertexOutput o = (VertexOutput)0;
                o.uv0 = v.texcoord0;
                o.normalDir = UnityObjectToWorldNormal(v.normal);
                o.posWorld = mul(unity_ObjectToWorld, v.vertex);
                o.pos = UnityObjectToClipPos( v.vertex );
                UNITY_TRANSFER_FOG(o,o.pos);
                TRANSFER_VERTEX_TO_FRAGMENT(o)
                return o;
            }
            float4 frag(VertexOutput i) : COLOR {
                i.normalDir = normalize(i.normalDir);
                float3 normalDirection = i.normalDir;
                float3 lightDirection = normalize(_WorldSpaceLightPos0.xyz);
////// Lighting:
////// Emissive:
                float4 node_1767 = _Time;
                float2 node_8975 = (i.uv0+node_1767.g*float2(-0.3,0));
                float4 _node_7216_var = tex2D(_node_7216,TRANSFORM_TEX(node_8975, _node_7216));
                float2 node_5595 = (i.uv0+node_1767.g*float2(0,0.3));
                float4 _node_7264_var = tex2D(_node_7264,TRANSFORM_TEX(node_5595, _node_7264));
                float2 node_5050 = ((i.uv0+node_1767.g*float2(-0.1,0.2))+(_node_7216_var.rgb+_node_7264_var.rgb).rg);
                float4 _Texture_var = tex2D(_Texture,TRANSFORM_TEX(node_5050, _Texture));
                float3 emissive = (_Texture_var.rgb*_TexColor.rgb*_bLight);
                float4 _MainTex_var = tex2D(_MainTex,TRANSFORM_TEX(i.uv0, _MainTex));
                float node_8714 = 0.5;
                float3 finalColor = emissive + (_MainColor.rgb*_MainTex_var.rgb*(pow((node_8714*(node_8714+max(0,dot(lightDirection,i.normalDir)))),(_DiffuseArea*-1.0+5.0))*_DiffusePower));
                fixed4 finalRGBA = fixed4(finalColor,1);
                UNITY_APPLY_FOG(i.fogCoord, finalRGBA);
                return finalRGBA;
            }
            ENDCG
        }
        Pass {
            Name "FORWARD_DELTA"
            Tags {
                "LightMode"="ForwardAdd"
            }
            Blend One One
            
            
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #define UNITY_PASS_FORWARDADD
            #include "UnityCG.cginc"
            #include "AutoLight.cginc"
            #pragma multi_compile_fwdadd_fullshadows
            #pragma multi_compile_fog
            #pragma only_renderers d3d9 d3d11 glcore gles 
            #pragma target 3.0
            uniform sampler2D _MainTex; uniform float4 _MainTex_ST;
            uniform float4 _MainColor;
            uniform float _DiffuseArea;
            uniform float _DiffusePower;
            uniform sampler2D _Texture; uniform float4 _Texture_ST;
            uniform float4 _TexColor;
            uniform fixed _bLight;
            uniform sampler2D _node_7216; uniform float4 _node_7216_ST;
            uniform sampler2D _node_7264; uniform float4 _node_7264_ST;
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
                LIGHTING_COORDS(3,4)
                UNITY_FOG_COORDS(5)
            };
            VertexOutput vert (VertexInput v) {
                VertexOutput o = (VertexOutput)0;
                o.uv0 = v.texcoord0;
                o.normalDir = UnityObjectToWorldNormal(v.normal);
                o.posWorld = mul(unity_ObjectToWorld, v.vertex);
                o.pos = UnityObjectToClipPos( v.vertex );
                UNITY_TRANSFER_FOG(o,o.pos);
                TRANSFER_VERTEX_TO_FRAGMENT(o)
                return o;
            }
            float4 frag(VertexOutput i) : COLOR {
                i.normalDir = normalize(i.normalDir);
                float3 normalDirection = i.normalDir;
                float3 lightDirection = normalize(lerp(_WorldSpaceLightPos0.xyz, _WorldSpaceLightPos0.xyz - i.posWorld.xyz,_WorldSpaceLightPos0.w));
////// Lighting:
                float4 _MainTex_var = tex2D(_MainTex,TRANSFORM_TEX(i.uv0, _MainTex));
                float node_8714 = 0.5;
                float3 finalColor = (_MainColor.rgb*_MainTex_var.rgb*(pow((node_8714*(node_8714+max(0,dot(lightDirection,i.normalDir)))),(_DiffuseArea*-1.0+5.0))*_DiffusePower));
                fixed4 finalRGBA = fixed4(finalColor * 1,0);
                UNITY_APPLY_FOG(i.fogCoord, finalRGBA);
                return finalRGBA;
            }
            ENDCG
        }
    }
    FallBack "Diffuse"
    CustomEditor "ShaderForgeMaterialInspector"
}
