using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using OpenGL_Game.Objects;
using OpenGL_Game.Components;
using OpenTK;
using OpenTK.Graphics.OpenGL;

namespace OpenGL_Game.Systems
{
    class SystemLighting : ISystem
    {
        const ComponentTypes MASK = (ComponentTypes.COMPONENT_POSITION | ComponentTypes.COMPONENT_LIGHT_EMITTER);

        public static int lightIndex; // Assigned and incremented by every light emitter component created.

        public SystemLighting() { }

        /// <summary>
        /// Sets the uniform values for all the light entities in the shader used in SystemRender.
        /// </summary>
        /// <param name="entity"></param>
        public void OnAction(Entity entity)
        {
            if ((entity.Mask & MASK) == MASK)
            {
                List<IComponent> components = entity.Components;

                // Retrieving components common to spotlights and point lights.
                IComponent positionComponent = components.Find(delegate (IComponent component)
                {
                    return component.ComponentType == ComponentTypes.COMPONENT_POSITION;
                });
                IComponent lightComponent = components.Find(delegate (IComponent component)
                {
                    return component.ComponentType == ComponentTypes.COMPONENT_LIGHT_EMITTER;
                });

                // If the entity contains a direction component it is a spotlight, otherwise it is a pointlight
                if ((entity.Mask & ComponentTypes.COMPONENT_LIGHT_DIRECTION) == ComponentTypes.COMPONENT_LIGHT_DIRECTION)
                {
                    GL.UseProgram(SystemRender.pgmID);

                    // Position is hard set to the camera position
                    int uniform_position = GL.GetUniformLocation(SystemRender.pgmID, ("flashLight.position"));
                    Vector3 mPosition = MyGame.gameInstance.playerCamera.Position;
                    GL.Uniform3(uniform_position, ref mPosition);

                    int uniform_Ambient = GL.GetUniformLocation(SystemRender.pgmID, ("flashLight.ambient"));
                    Vector3 mAmbient = ((ComponentLightEmitter)lightComponent).Ambient;
                    GL.Uniform3(uniform_Ambient, ref mAmbient);

                    int uniform_Diffuse = GL.GetUniformLocation(SystemRender.pgmID, ("flashLight.diffuse"));
                    Vector3 mDiffuse = ((ComponentLightEmitter)lightComponent).Diffuse;
                    GL.Uniform3(uniform_Diffuse, ref mDiffuse);

                    int uniform_Specular = GL.GetUniformLocation(SystemRender.pgmID, ("flashLight.specular"));
                    Vector3 mSpecular = ((ComponentLightEmitter)lightComponent).Specular;
                    GL.Uniform3(uniform_Specular, ref mSpecular);

                    // Retrieving unique component to spotlight
                    IComponent spotComponent = components.Find(delegate (IComponent component)
                    {
                        return component.ComponentType == ComponentTypes.COMPONENT_LIGHT_DIRECTION;
                    });
                    // Set the properties in the shader
                    // Direction is hard set to the camera direction.
                    int uniform_Direction = GL.GetUniformLocation(SystemRender.pgmID, ("flashLight.direction"));
                    Vector3 mDirection = MyGame.gameInstance.playerCamera.Front;
                    GL.Uniform3(uniform_Direction, ref mDirection);

                    int uniform_CutOff = GL.GetUniformLocation(SystemRender.pgmID, ("flashLight.cutOff"));
                    double mCutOff = Math.Cos(MathHelper.DegreesToRadians((((ComponentLightDirection)spotComponent).CutOff)));
                    GL.Uniform1(uniform_CutOff, mCutOff);

                    int uniform_OuterCutOff= GL.GetUniformLocation(SystemRender.pgmID, ("flashLight.outerCutOff"));
                    float mOuterCutOff = ((ComponentLightDirection)spotComponent).OuterCutOff;
                    GL.Uniform1(uniform_OuterCutOff, Math.Cos(MathHelper.DegreesToRadians(mOuterCutOff)));

                    int uniform_const = GL.GetUniformLocation(SystemRender.pgmID, ("flashLight.constant"));
                    float mconst = ((ComponentLightDirection)spotComponent).Attenutation.X;
                    GL.Uniform1(uniform_const, mconst);

                    int uniform_linear = GL.GetUniformLocation(SystemRender.pgmID, ("flashLight.linear"));
                    float mlinear = ((ComponentLightDirection)spotComponent).Attenutation.Y;
                    GL.Uniform1(uniform_linear, mlinear);

                    int uniform_quad = GL.GetUniformLocation(SystemRender.pgmID, ("flashLight.quadratic"));
                    float mquad = ((ComponentLightDirection)spotComponent).Attenutation.Z;
                    GL.Uniform1(uniform_quad, mquad);


                }
                else // setting uniform of point light
                {
                    int lightIndex = ((ComponentLightEmitter)lightComponent).Index;

                    GL.UseProgram(SystemRender.pgmID);

                    int uniform_position = GL.GetUniformLocation(SystemRender.pgmID, ("lights[" + lightIndex + "].position"));
                    Vector3 mPosition = ((ComponentPosition)positionComponent).Position;
                    GL.Uniform3(uniform_position, ref mPosition);

                    int uniform_Ambient = GL.GetUniformLocation(SystemRender.pgmID, ("lights[" + lightIndex + "].ambient"));
                    Vector3 mAmbient = ((ComponentLightEmitter)lightComponent).Ambient;
                    GL.Uniform3(uniform_Ambient, ref mAmbient);

                    int uniform_Diffuse = GL.GetUniformLocation(SystemRender.pgmID, ("lights[" + lightIndex + "].diffuse"));
                    Vector3 mDiffuse = ((ComponentLightEmitter)lightComponent).Diffuse;
                    GL.Uniform3(uniform_Diffuse, ref mDiffuse);

                    int uniform_Specular = GL.GetUniformLocation(SystemRender.pgmID, ("lights[" + lightIndex + "].specular"));
                    Vector3 mSpecular = ((ComponentLightEmitter)lightComponent).Specular;
                    GL.Uniform3(uniform_Specular, ref mSpecular);
                }
            }
        }

        public string Name
        {
            get { return "SystemLighting"; }
        }
    }
}
