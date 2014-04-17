float4x4 World;
float4x4 View;
float4x4 Projection;
texture BasicTexture;
float3 DiffuseColor = float3(1, 1, 1);
float3 AmbientColor = float3(.1, .1, .1);

sampler BasicSampler = sampler_state { 
	texture = <BasicTexture>;
	MinFilter = Anisotropic;
	MaxFilter = Anisotropic;
	MipFilter = Linear;
	AddressU = Wrap;
	AddressV = Wrap;
	};

bool TextureEnabled;

// TODO: add effect parameters here.

struct VertexShaderInput
{
    float4 Position : SV_POSITION;
	float2 UV : TEXCOORD0;

    // TODO: add input channels such as texture
    // coordinates and vertex colors here.
};

struct VertexShaderOutput
{
    float4 Position : SV_POSITION;
	float2 UV : TEXCOORD0;

    // TODO: add vertex shader outputs such as colors and texture
    // coordinates here. These values will automatically be interpolated
    // over the triangle, and provided as input to your pixel shader.
};

VertexShaderOutput VertexShaderFunction(VertexShaderInput input)
{
    VertexShaderOutput output;

    float4 worldPosition = mul(input.Position, World);
    float4 viewPosition = mul(worldPosition, View);
    output.Position = mul(viewPosition, Projection);
	output.UV = input.UV;
    // TODO: add your vertex shader code here.

    return output;
}

float4 PixelShaderFunction(VertexShaderOutput input) : COLOR0
{
    // TODO: add your pixel shader code here.
	float3 output = DiffuseColor;
	output *= tex2D(BasicSampler, input.UV);

	float3 light = AmbientColor;
    return float4(output, 1);
}

technique Technique1
{
    pass Pass1
    {
        // TODO: set renderstates here.

        VertexShader = compile vs_2_0 VertexShaderFunction();
        PixelShader = compile ps_2_0 PixelShaderFunction();
    }
}
