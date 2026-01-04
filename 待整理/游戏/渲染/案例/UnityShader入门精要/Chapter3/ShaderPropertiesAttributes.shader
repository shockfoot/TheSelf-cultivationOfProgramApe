Shader "MyCustom/Chapter3/ShaderPropertiesAttributes"
{
    Properties
    {
        [Header(NoScaleOffset)]_Texture ("Texture", 2D) = "white" {}
        [NoScaleOffset]_NoScaleOffsetTexture ("NoScaleOffsetTexture", 2D) = "white" {}

        [Header(Space)] _Int1 ("Int1", Integer) = 0
        [Space(10)] _Int2 ("Int2", Integer) = 1
        
        [Header(Toggle)]
        [Toggle] _Int3 ("Toggle", Integer) = 0
        [Toggle(KEYWORD)] _Int4 ("KeywordToggle", Integer) = 0
        
        [Header(Enum)]
        [Enum(On, 1, Off, 0, Middle, 2)] _Int5 ("Enum", Integer) = 0
        [KeywordEnum(A, B, C)] _Int6 ("KeywordEnum", Integer) = 0
        
        [Header(Slider)]
        [IntRange] _Int7 ("IntRange", Range(0, 10)) = 0
        [PowerSlider] _Int8 ("PowerSlider", Range(0, 1)) = 0
    }

    SubShader
    {
        Pass
        {
        }
    }
}
