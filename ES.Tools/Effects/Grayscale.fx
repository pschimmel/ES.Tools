/////////////////////////////////////////////////////////////////////////////////
//                                                                             //
//   An effect that turns the input into shades of gray.                       //
//                                                                             //
//   This file is needed to create the Grayscale.ps pixel shader file used     //
//   in GrayscaleEffect.cs. It's just stored here for completeness. The        //
//   actual effect uses the compiled .ps file.                                 //
//                                                                             //
//   Use the following command to generate the pixel shader file:              //
//                                                                             //
//   fxc /T ps_2_0 /E main / Fo"C:\temp\Grayscale.ps" "c:\temp\Grayscale.fx"   //
//   fxc.exe is located under                                                  //
//                                                                             //
//   C:\Program Files (x86)\Windows Kits\10\bin\10.0.15063.0\x86               //
//                                                                             //
/////////////////////////////////////////////////////////////////////////////////

//-----------------------------------------------------------------------------------------
// Shader constant register mappings (scalars - float, double, Point, Color, Point3D, etc.)
//-----------------------------------------------------------------------------------------

//--------------------------------------------------------------------------------------
// Saturation Factor (default: 0)
//--------------------------------------------------------------------------------------
float factor : register(c0);

//--------------------------------------------------------------------------------------
// Sampler Inputs (Brushes, including ImplicitInput)
//--------------------------------------------------------------------------------------
sampler2D implicitInput : register(s0);

//--------------------------------------------------------------------------------------
// Pixel Shader
//--------------------------------------------------------------------------------------
float4 main(float2 uv : TEXCOORD) : COLOR
{
    float4 color = tex2D(implicitInput, uv);
    float gray = color.r * 0.3 + color.g * 0.59 + color.b *0.11;

    float4 result;
    result.r = (color.r - gray) * factor + gray;
    result.g = (color.g - gray) * factor + gray;
    result.b = (color.b - gray) * factor + gray;
    result.a = color.a;

    return result;
}