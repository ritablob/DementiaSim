using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

namespace Visual
{
    /// <summary>
    /// NOTE: this is actually cut from the game for now, as I am unable to figure out a solution for it and have way too little experience with texture
    /// and material manipulation. Instead I reduce the resolution of the camera view. 
    /// </summary>
    public class ResizeTexture : MonoBehaviour
    {
        private Material objMat;
        private Texture2D texture;
        /* - Texture2d.Resize for textures that are far away 
         * 
         */
        // Start is called before the first frame update
        void Start()
        {
            objMat = GetComponent<MeshRenderer>().material;
            texture = objMat.mainTexture as Texture2D;
        }

        public void MakePixelated()
        {
            texture.Reinitialize(128, 128);
            Graphics.Blit(texture,objMat);
        }

        public void MakeNormal()
        {
            texture.Reinitialize(2048, 2048);
            Graphics.Blit(texture,objMat);
        }
        // possible solution? 
        // https://discussions.unity.com/t/built-in-camera-filter-mode-customization/891966
        // " you can avoid using two blits. You only need 1. Just set the targetTexture on your camera to a RenderTexture (that is sized at your low resolution),
        // make sure that RenderTexture has filterMode = FilterMode.Point, then do 1 final blit to blit to the framebuffer (like the last blit you are already doing)."
        void OnRenderImage(RenderTexture source, RenderTexture destination)
        {
            RenderTexture r = RenderTexture.GetTemporary(
                source.width, source.height, 0, source.format);

            r.filterMode = FilterMode.Point;

            Graphics.Blit(source, r);
            Graphics.Blit(r, destination);

            RenderTexture.ReleaseTemporary(r);
        }


        private IEnumerator UpdateGraphicsWithDelay(float delay)
        {
            yield return new WaitForSeconds(delay);
            Graphics.Blit(texture,objMat);
        }
    }
}
