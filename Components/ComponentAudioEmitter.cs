using OpenGL_Game.Managers;
using OpenTK;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OpenGL_Game.Components
{
    public struct ASound
    {
        public int buffer;
        public int source;
        public byte[] soundData;
        public Vector3 emitPos;

        public ASound(int whichBuffer, int whichSource, byte[] whichData, Vector3 emitter)
        {
            buffer = whichBuffer;
            source = whichSource;
            soundData = whichData;
            emitPos = emitter;
        }
    }

    class ComponentAudioEmitter : IComponent
    {
        //Vector3 emitterPosition;
        
        private ASound currentSound;

        public ComponentTypes ComponentType
        {
            get { return ComponentTypes.COMPONENT_AUDIOEMITTER; }
        }
      
        public ComponentAudioEmitter(string soundName, Vector3 emitPos)
        {
            currentSound = ResourceManager.LoadAudio(emitPos, soundName);
        }

        public ASound GetSound()
        {
            return currentSound;
        }
    }
}
