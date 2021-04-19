using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.GameScripts
{
    public static class MathFormulas
    {
        public static float GetSpawnTime(float time)
        {
            return 0.1301276F + (1.042602F - 0.1301276F) / (1F + Mathf.Pow(time / 55.89115F, 2.445163F));
        }
        //public static float GetSpeed(float time)
        //{
        //    return (48.52444F + (12.00285F - 48.52444F) / (1F + Mathf.Pow(time / 43.60367F, 1.90414F))) * 80F;
        //}

        public static float GetSpeed(float time)
        {
            return (3260.034F + (1186.738F - 3260.034F) / (1 + Mathf.Pow(time / 59.21717F, 2.443079F)));
        }
    }
}
