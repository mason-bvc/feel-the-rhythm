inline half3 GammaToLinearSpace (half3 sRGB)
{
  // Approximate version from http://chilliant.blogspot.com.au/2012/08/srgb-approximations-for-hlsl.html?m=1
  return sRGB * (sRGB * (sRGB * 0.305306011h + 0.682171111h) + 0.012522878h);
}

inline half3 LinearToGammaSpace (half3 linRGB)
{
  linRGB = max(linRGB, half3(0.h, 0.h, 0.h));
  // An almost-perfect approximation from http://chilliant.blogspot.com.au/2012/08/srgb-approximations-for-hlsl.html?m=1
  return max(1.055h * pow(linRGB, 0.416666667h) - 0.055h, 0.h);
}

float4 Posterize(float4 value, float steps, float bayerValue)
{
  value.rgb = LinearToGammaSpace(value.rgb);
  value = floor(value * steps + bayerValue) / steps;
  value.rgb = GammaToLinearSpace(value.rgb);
  return value;
}

float GetBayer2x2(float2 pixelPosition)
{
  const float bayer_matrix_2x2[2][2] = {
    {  0.00,  1.00 },
    {  0.25,  0.75 },
  };
  return bayer_matrix_2x2[pixelPosition.x % 2][pixelPosition.y % 2];
}

float GetBayer4x4(float2 pixelPosition)
{
  const float bayer_matrix_4x4[4][4] = {
    { 0.0,    0.5,    0.125,  0.625 },
    { 0.75,   0.25,   0.875,  0.375 },
    { 0.1875, 0.6875, 0.0625, 0.5625 },
    { 0.9375, 0.4375, 0.8125, 0.3125 },
  };
  return bayer_matrix_4x4[pixelPosition.x % 4][pixelPosition.y % 4];
}

float GetBayer8x8(float2 pixelPosition)
{
  const float bayer_matrix_8x8[8][8] = {
    { 0.000, 0.500, 0.125, 0.625, 0.03125, 0.53125, 0.15625, 0.65625 },
    { 0.750, 0.250, 0.875, 0.375, 0.78125, 0.28125, 0.90625, 0.40625 },
    { 0.1875, 0.6875, 0.0625, 0.5625, 0.21875, 0.71875, 0.09375, 0.59375 },
    { 0.9375, 0.4375, 0.8125, 0.3125, 0.96875, 0.46875, 0.84375, 0.34375 },
    { 0.015625, 0.515625, 0.140625, 0.640625, 0.046875, 0.546875, 0.171875, 0.671875 },
    { 0.765625, 0.265625, 0.890625, 0.390625, 0.796875, 0.296875, 0.921875, 0.421875 },
    { 0.203125, 0.703125, 0.078125, 0.578125, 0.234375, 0.734375, 0.109375, 0.609375 },
    { 0.953125, 0.453125, 0.828125, 0.328125, 0.984375, 0.484375, 0.859375, 0.359375 },
  };
  return bayer_matrix_8x8[pixelPosition.x % 8][pixelPosition.y % 8];
}

void BayerDithering_float(float4 PixelPosition, float4 Color, float Steps, float RenderScale, out float4 Result)
{
  PixelPosition.xy *= _ScreenParams.xy * RenderScale;

  float bayerValue = GetBayer4x4(PixelPosition.xy);
  float4 outputBayer = step(bayerValue, Color);

  Color = Posterize(Color, Steps, bayerValue);
  Result = Color;
}