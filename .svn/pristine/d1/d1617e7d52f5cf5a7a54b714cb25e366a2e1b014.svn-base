using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using OpenTK;
using OpenGL_Game.Objects;
using OpenGL_Game.Components;
using OpenTK.Graphics.OpenGL;

public class ComponentLightDirection : IComponent
{
    static int NumberOfSpotlights;

    Vector3 direction;
    float cutOff;
    float outerCutOff;
    const float constant = 1.0f;
    const float linear = 0.09f;
    const float quadratic= 0.032f;

    public ComponentLightDirection(Vector3 pDirection, float pCutOff, float pOuterCutOff)
    {
        direction = pDirection;
        cutOff = pCutOff;
        outerCutOff = pOuterCutOff;
        if (NumberOfSpotlights > 0)
        {
            Console.WriteLine("ERROR.COMPONENT_LIGHT_DIRECTION ONLY ONE SPOTLIGHT IS POSSIBLE IN SHADER. DELETE EXTRA SPOTLIGHT ENTITY.");
        }
        NumberOfSpotlights++;
    }

    public float CutOff
    {
        get { return cutOff; }
        set { cutOff = value; }
    }
    public float OuterCutOff
    {
        get { return outerCutOff; }
        set { outerCutOff = value; }
    }
    public Vector3 Direction
    {
        get { return direction; }
        set { direction = value; }
    }
    public Vector3 Attenutation
    {
        get { return new Vector3(constant, linear, quadratic); }
    }

    ComponentTypes IComponent.ComponentType
    {
        get { return ComponentTypes.COMPONENT_LIGHT_DIRECTION; }
    }
}

