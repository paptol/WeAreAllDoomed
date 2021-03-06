﻿using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using OpenTK;
using OpenTK.Audio.OpenAL;
using OpenGL_Game.Managers;
using OpenGL_Game.Objects;
using OpenTK.Audio;
using OpenGL_Game.Components;

namespace OpenGL_Game.Systems
{

    public class SystemAudio : ISystem
    {
     
      
        

        const ComponentTypes MASK = (ComponentTypes.COMPONENT_AUDIOEMITTER);

        public string Name
        {
            get { return "SystemAudio"; }
        }

        /// <summary>
        /// Constructor method for sounds
        /// </summary>
        /// <param name="activebuffer"></param>
        /// <param name="filename"></param>
        /// 
      /* public SystemAudio(string filename)
        {
            AC = new AudioContext();
        }*/

        public void OnAction(Entity entity)
        {
            if ((entity.Mask & MASK) == MASK)
            {
                List<IComponent> components = entity.Components;

                IComponent emitterComponent = components.Find(delegate (IComponent component)
                {
                    return component.ComponentType == ComponentTypes.COMPONENT_AUDIOEMITTER;
                });

                ASound emit = ((ComponentAudioEmitter)emitterComponent).GetSound();
                
                AL.Source(emit.source, ALSourcei.Buffer, emit.buffer); // attach the buffer to a source
                AL.Source(emit.source, ALSourceb.Looping, false);
                AL.Source(emit.source, ALSource3f.Position, ref emit.emitPos);
            }
            
        }
        public int mySource= AL.GenSource();
        public bool IsPlaying()
        {
            ALSourceState state = AL.GetSourceState(mySource);
           return state == ALSourceState.Playing;
        }

        public bool IsPaused()
        {
           ALSourceState state = AL.GetSourceState(mySource);
            return state == ALSourceState.Paused;
        }

        public bool IsStopped()
        {
            ALSourceState state = AL.GetSourceState(mySource);
            return state == ALSourceState.Stopped;
        }

        public void Play()
        {
            Play(false);
        }

        public void Play(bool loop)
        {
            AL.Source(mySource, ALSourceb.Looping, loop);
            AL.SourcePlay(mySource);
        }

        public void Pause()
        {
            AL.SourcePause(mySource);
        }

        public void Stop()
        {
            AL.SourceStop(mySource);
        }
    }
}