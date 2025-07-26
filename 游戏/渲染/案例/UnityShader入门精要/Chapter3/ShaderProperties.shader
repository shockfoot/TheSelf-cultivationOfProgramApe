Shader "MyCustom/Chapter3/ChapShaderProperties"
{
    Properties
    {
        _Int1 ("Int", Integer) = 1
        _Int2 ("Int(Unsupported)", Int) = 1
        _Float1 ("Float", Float) = 0.5
        _Float2 ("FloatRange", Range(0.0, 1.0)) = 0.5
        _Texture2D ("Texture2D", 2D) = "white" {}
        _Texture3D ("Texture3D", 3D) = "white" {}
        _Cubmap ("Cubmap", Cube) = "white" {}
        _Color ("Color", Color) = (1, 1, 1, 1)
        _Vector ("Vector", Vector) = (1, 1, 1, 1)
    }

    SubShader
    {
        Pass
        {
        }
    }
}
