// Shader created with Shader Forge v1.38 
// Shader Forge (c) Neat Corporation / Joachim Holmer - http://www.acegikmo.com/shaderforge/
// Note: Manually altering this data may prevent you from opening it in Shader Forge
/*SF_DATA;ver:1.38;sub:START;pass:START;ps:flbk:,iptp:0,cusa:False,bamd:0,cgin:,lico:1,lgpr:1,limd:0,spmd:1,trmd:0,grmd:0,uamb:True,mssp:True,bkdf:False,hqlp:False,rprd:False,enco:False,rmgx:True,imps:True,rpth:0,vtps:0,hqsc:True,nrmq:1,nrsp:0,vomd:0,spxs:False,tesm:0,olmd:1,culm:0,bsrc:0,bdst:1,dpts:2,wrdp:True,dith:0,atcv:False,rfrpo:True,rfrpn:Refraction,coma:15,ufog:True,aust:True,igpj:False,qofs:0,qpre:1,rntp:1,fgom:False,fgoc:False,fgod:False,fgor:False,fgmd:0,fgcr:0.5,fgcg:0.5,fgcb:0.5,fgca:1,fgde:0.01,fgrn:0,fgrf:300,stcl:False,atwp:False,stva:128,stmr:255,stmw:255,stcp:6,stps:0,stfa:0,stfz:0,ofsf:0,ofsu:0,f2p0:False,fnsp:False,fnfb:False,fsmp:False;n:type:ShaderForge.SFN_Final,id:9361,x:32472,y:32656,varname:node_9361,prsc:2|custl-4763-OUT;n:type:ShaderForge.SFN_Tex2d,id:1263,x:31905,y:32710,ptovrint:False,ptlb:node_1263,ptin:_node_1263,varname:node_1263,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,tex:733c99519c0aa404ea2175683a552c30,ntxv:0,isnm:False;n:type:ShaderForge.SFN_Fresnel,id:5880,x:31863,y:32954,varname:node_5880,prsc:2|EXP-2464-OUT;n:type:ShaderForge.SFN_Add,id:4763,x:32244,y:32836,varname:node_4763,prsc:2|A-1263-RGB,B-3426-OUT;n:type:ShaderForge.SFN_Color,id:562,x:31842,y:33142,ptovrint:False,ptlb:GlossColor,ptin:_GlossColor,varname:node_562,prsc:2,glob:False,taghide:False,taghdr:True,tagprd:False,tagnsco:False,tagnrm:False,c1:0.9852941,c2:0.01448963,c3:0.9183422,c4:1;n:type:ShaderForge.SFN_Multiply,id:3426,x:32059,y:32988,varname:node_3426,prsc:2|A-5880-OUT,B-562-RGB;n:type:ShaderForge.SFN_Slider,id:2464,x:31493,y:32995,ptovrint:False,ptlb:GlossColorBright,ptin:_GlossColorBright,varname:node_2464,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,min:0,cur:1.760332,max:10;proporder:1263-562-2464;pass:END;sub:END;*/

Shader "Custom/OutGloss1" {
    Properties {
        _node_1263 ("node_1263", 2D) = "white" {}
        [HDR]_GlossColor ("GlossColor", Color) = (0.9852941,0.01448963,0.9183422,1)
        _GlossColorBright ("GlossColorBright", Range(0, 10)) = 1.760332
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
            #pragma multi_compile_fwdbase_fullshadows
            #pragma multi_compile_fog
            #pragma only_renderers d3d9 d3d11 glcore gles 
            #pragma target 3.0
            uniform sampler2D _node_1263; uniform float4 _node_1263_ST;
            uniform float4 _GlossColor;
            uniform float _GlossColorBright;
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
            float4 frag(VertexOutput i) : COLOR {
                i.normalDir = normalize(i.normalDir);
                float3 viewDirection = normalize(_WorldSpaceCameraPos.xyz - i.posWorld.xyz);
                float3 normalDirection = i.normalDir;
////// Lighting:
                float4 _node_1263_var = tex2D(_node_1263,TRANSFORM_TEX(i.uv0, _node_1263));
                float3 finalColor = (_node_1263_var.rgb+(pow(1.0-max(0,dot(normalDirection, viewDirection)),_GlossColorBright)*_GlossColor.rgb));
                fixed4 finalRGBA = fixed4(finalColor,1);
                UNITY_APPLY_FOG(i.fogCoord, finalRGBA);
                return finalRGBA;
            }
            ENDCG
        }
    }
    FallBack "Diffuse"
    CustomEditor "ShaderForgeMaterialInspector"
}
