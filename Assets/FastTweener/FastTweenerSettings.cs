using UnityEngine;

namespace Kovnir.FastTweener
{
    public class FastTweenerSettings
    {
        private int taskPoolSize = 16;

        public int TaskPoolSize
        {
            get { return taskPoolSize; }
            set
            {
                if (value <= 0)
                {
                    Debug.LogError(FastTweenerStringConstants.POOL_SIZE_ERROR);
                    return;
                }

                taskPoolSize = value;
            }
        }


        private Ease defaultEase = Ease.OutQuad;

        public Ease DefaultEase
        {
            get { return defaultEase; }
            set
            {
                if (value == Ease.Default)
                {
                    Debug.LogError(FastTweenerStringConstants.DEFAULT_EASE_ERROR);
                    return;
                }

                defaultEase = value;
            }
        }


        private bool saveGameObjectName = false;

        public bool SaveGameObjectName
        {
            get { return saveGameObjectName; }
            set { saveGameObjectName = value; }
        }

        private int rigidbodyExtensionsPoolSize = 16;

        public int RigidbodyExtensionsPoolSize
        {
            get { return rigidbodyExtensionsPoolSize; }
            set
            {
                if (value <= 0)
                {
                    Debug.LogError(FastTweenerStringConstants.POOL_SIZE_ERROR);
                    return;
                }

                rigidbodyExtensionsPoolSize = value;
            }
        }

        private int transformExtensionsPoolSize = 16;

        public int TransformExtensionsPoolSize
        {
            get { return transformExtensionsPoolSize; }
            set
            {
                if (value <= 0)
                {
                    Debug.LogError(FastTweenerStringConstants.POOL_SIZE_ERROR);
                    return;
                }

                transformExtensionsPoolSize = value;
            }
        }

        private int criticalFpsToLogWarning = 30;

        public int CriticalFpsToLogWarning
        {
            get { return criticalFpsToLogWarning; }
            set
            {
                if (value <= 0)
                {
                    Debug.LogError(FastTweenerStringConstants.FPS_VALUE_ERROR);
                    return;
                }

                criticalFpsToLogWarning = value;
            }
        }

    }
}