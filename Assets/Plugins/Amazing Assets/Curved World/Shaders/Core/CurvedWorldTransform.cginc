#ifndef CURVEDWORLD_TRANSFORM_CGINC
#define CURVEDWORLD_TRANSFORM_CGINC


#ifndef CURVEDWORLD_IS_INSTALLED
#define CURVEDWORLD_IS_INSTALLED
#endif


#if defined (CURVEDWORLD_BEND_TYPE_CLASSICRUNNER_X_POSITIVE)
    #if defined (CURVEDWORLD_BEND_ID_1)
        #include "../CGINC/Classic Runner/CurvedWorld_ClassicRunner_X_Positive_ID1.cginc"
        #define CURVEDWORLD_TRANSFORM_VERTEX_AND_NORMAL(v, n, t) CurvedWorld_ClassicRunner_X_Positive_ID1(v, n, t);
        #define CURVEDWORLD_TRANSFORM_VERTEX(v)                  CurvedWorld_ClassicRunner_X_Positive_ID1(v);	
    #else
        #define CURVEDWORLD_TRANSFORM_VERTEX_AND_NORMAL(v, n, t) 
        #define CURVEDWORLD_TRANSFORM_VERTEX(v)    
    #endif
#elif defined (CURVEDWORLD_BEND_TYPE_LITTLEPLANET_Y)
    #if defined (CURVEDWORLD_BEND_ID_1)
        #include "../CGINC/Little Planet/CurvedWorld_LittlePlanet_Y_ID1.cginc"
        #define CURVEDWORLD_TRANSFORM_VERTEX_AND_NORMAL(v, n, t) CurvedWorld_LittlePlanet_Y_ID1(v, n, t);
        #define CURVEDWORLD_TRANSFORM_VERTEX(v)                  CurvedWorld_LittlePlanet_Y_ID1(v);		
    #else
        #define CURVEDWORLD_TRANSFORM_VERTEX_AND_NORMAL(v, n, t) 
        #define CURVEDWORLD_TRANSFORM_VERTEX(v)    
    #endif
#elif defined (CURVEDWORLD_BEND_TYPE_CYLINDRICALTOWER_X)
    #if defined (CURVEDWORLD_BEND_ID_1)
        #include "../CGINC/Cylindrical Tower/CurvedWorld_CylindricalTower_X_ID1.cginc"
        #define CURVEDWORLD_TRANSFORM_VERTEX_AND_NORMAL(v, n, t) CurvedWorld_CylindricalTower_X_ID1(v, n, t);
        #define CURVEDWORLD_TRANSFORM_VERTEX(v)                  CurvedWorld_CylindricalTower_X_ID1(v);	
    #else
        #define CURVEDWORLD_TRANSFORM_VERTEX_AND_NORMAL(v, n, t) 
        #define CURVEDWORLD_TRANSFORM_VERTEX(v)    
    #endif
#elif defined (CURVEDWORLD_BEND_TYPE_CYLINDRICALROLLOFF_Z)
    #if defined (CURVEDWORLD_BEND_ID_1)
        #include "../CGINC/Cylindrical Rolloff/CurvedWorld_CylindricalRolloff_Z_ID1.cginc"
        #define CURVEDWORLD_TRANSFORM_VERTEX_AND_NORMAL(v, n, t) CurvedWorld_CylindricalRolloff_Z_ID1(v, n, t);
        #define CURVEDWORLD_TRANSFORM_VERTEX(v)                  CurvedWorld_CylindricalRolloff_Z_ID1(v);	
    #else
        #define CURVEDWORLD_TRANSFORM_VERTEX_AND_NORMAL(v, n, t) 
        #define CURVEDWORLD_TRANSFORM_VERTEX(v)    
    #endif
#elif defined (CURVEDWORLD_BEND_TYPE_SPIRALHORIZONTAL_X_POSITIVE)
    #if defined (CURVEDWORLD_BEND_ID_1)
        #include "../CGINC/Spiral Horizontal/CurvedWorld_SpiralHorizontal_X_Positive_ID1.cginc"
        #define CURVEDWORLD_TRANSFORM_VERTEX_AND_NORMAL(v, n, t) CurvedWorld_SpiralHorizontal_X_Positive_ID1(v, n, t);
        #define CURVEDWORLD_TRANSFORM_VERTEX(v)                  CurvedWorld_SpiralHorizontal_X_Positive_ID1(v);	
    #else
        #define CURVEDWORLD_TRANSFORM_VERTEX_AND_NORMAL(v, n, t) 
        #define CURVEDWORLD_TRANSFORM_VERTEX(v)    
    #endif
#elif defined (CURVEDWORLD_BEND_TYPE_SPIRALHORIZONTALDOUBLE_X)
    #if defined (CURVEDWORLD_BEND_ID_1)
        #include "../CGINC/Spiral Horizontal Double/CurvedWorld_SpiralHorizontalDouble_X_ID1.cginc"
        #define CURVEDWORLD_TRANSFORM_VERTEX_AND_NORMAL(v, n, t) CurvedWorld_SpiralHorizontalDouble_X_ID1(v, n, t);
        #define CURVEDWORLD_TRANSFORM_VERTEX(v)                  CurvedWorld_SpiralHorizontalDouble_X_ID1(v);	
    #else
        #define CURVEDWORLD_TRANSFORM_VERTEX_AND_NORMAL(v, n, t) 
        #define CURVEDWORLD_TRANSFORM_VERTEX(v)    
    #endif
#elif defined (CURVEDWORLD_BEND_TYPE_SPIRALVERTICAL_X_POSITIVE)
    #if defined (CURVEDWORLD_BEND_ID_1)
        #include "../CGINC/Spiral Vertical/CurvedWorld_SpiralVertical_X_Positive_ID1.cginc"
        #define CURVEDWORLD_TRANSFORM_VERTEX_AND_NORMAL(v, n, t) CurvedWorld_SpiralVertical_X_Positive_ID1(v, n, t);
        #define CURVEDWORLD_TRANSFORM_VERTEX(v)                  CurvedWorld_SpiralVertical_X_Positive_ID1(v);	
    #else
        #define CURVEDWORLD_TRANSFORM_VERTEX_AND_NORMAL(v, n, t) 
        #define CURVEDWORLD_TRANSFORM_VERTEX(v)    
    #endif
#elif defined (CURVEDWORLD_BEND_TYPE_SPIRALVERTICAL_X_NEGATIVE)
    #if defined (CURVEDWORLD_BEND_ID_1)
        #include "../CGINC/Spiral Vertical/CurvedWorld_SpiralVertical_X_Negative_ID1.cginc"
        #define CURVEDWORLD_TRANSFORM_VERTEX_AND_NORMAL(v, n, t) CurvedWorld_SpiralVertical_X_Negative_ID1(v, n, t);
        #define CURVEDWORLD_TRANSFORM_VERTEX(v)                  CurvedWorld_SpiralVertical_X_Negative_ID1(v);	
    #else
        #define CURVEDWORLD_TRANSFORM_VERTEX_AND_NORMAL(v, n, t) 
        #define CURVEDWORLD_TRANSFORM_VERTEX(v)    
    #endif
#elif defined (CURVEDWORLD_BEND_TYPE_TWISTEDSPIRAL_X_POSITIVE)
    #if defined (CURVEDWORLD_BEND_ID_1)
        #include "../CGINC/Twisted Spiral/CurvedWorld_TwistedSpiral_X_Positive_ID1.cginc"
        #define CURVEDWORLD_TRANSFORM_VERTEX_AND_NORMAL(v, n, t) CurvedWorld_TwistedSpiral_X_Positive_ID1(v, n, t);
        #define CURVEDWORLD_TRANSFORM_VERTEX(v)                  CurvedWorld_TwistedSpiral_X_Positive_ID1(v);	
    #else
        #define CURVEDWORLD_TRANSFORM_VERTEX_AND_NORMAL(v, n, t) 
        #define CURVEDWORLD_TRANSFORM_VERTEX(v)    
    #endif
#elif defined (CURVEDWORLD_BEND_TYPE_TWISTEDSPIRAL_Z_POSITIVE)
    #if defined (CURVEDWORLD_BEND_ID_1)
        #include "../CGINC/Twisted Spiral/CurvedWorld_TwistedSpiral_Z_Positive_ID1.cginc"
        #define CURVEDWORLD_TRANSFORM_VERTEX_AND_NORMAL(v, n, t) CurvedWorld_TwistedSpiral_Z_Positive_ID1(v, n, t);
        #define CURVEDWORLD_TRANSFORM_VERTEX(v)                  CurvedWorld_TwistedSpiral_Z_Positive_ID1(v);		
    #else
        #define CURVEDWORLD_TRANSFORM_VERTEX_AND_NORMAL(v, n, t) 
        #define CURVEDWORLD_TRANSFORM_VERTEX(v)    
    #endif
#else
    #define CURVEDWORLD_TRANSFORM_VERTEX_AND_NORMAL(v, n, t) 
    #define CURVEDWORLD_TRANSFORM_VERTEX(v)    
#endif


#endif
