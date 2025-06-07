using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonoGame.Jolpango.Graphics.Sprites
{
    public class JAnimation
    {
        public int[] KeyFrames { get; set; }
        private float timePerFrame;
        public float FramesPerSecond
        {
            get
            {
                return 1.0f / timePerFrame;
            }
            set
            {
                timePerFrame = 1.0f / value;
            }
        }
        public float TotalTime { get => KeyFrames.Length * timePerFrame; }
        public float CurrentTime { get; set; } = 0.0f;
        public int CurrentFrame { get => KeyFrames[CurrentFrameIndex]; }
        public int CurrentFrameIndex { get => (int)(CurrentTime / timePerFrame) % KeyFrames.Length; }
        public bool IsActive { get; set; } = false;
        public bool IsLooping { get; set; } = false;
        public bool IsCompleted { get; protected set; } = false;
        private Action onCompleteAction;
        public JAnimation(JAnimationCycleSettings cycleSettings)
        {
            KeyFrames = cycleSettings.Frames;
            timePerFrame = cycleSettings.FrameDuration;
        }
        public void StartAnimation(Action onComplete = null, bool loop = false)
        {
            onCompleteAction = onComplete;
            IsActive = true;
            IsLooping = loop;
            CurrentTime = 0;
            IsCompleted = false;
        }

        public void StopAnimation()
        {
            CurrentTime = 0;
            IsActive = false;
        }
        public bool Update(GameTime gameTime)
        {
            if (!IsActive)
                return false;

            CurrentTime += (float)gameTime.ElapsedGameTime.TotalSeconds;

            if (CurrentTime >= TotalTime)
            {
                onCompleteAction?.Invoke();
                IsCompleted = true;
                if (IsLooping)
                {
                    CurrentTime %= TotalTime;
                }
                else
                {
                    IsActive = false;
                    onCompleteAction = null;
                }
            }

            return IsCompleted;
        }
    }
}
