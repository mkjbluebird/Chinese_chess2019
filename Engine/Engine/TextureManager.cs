using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Tao.OpenGl;
using Tao.DevIl;

namespace Engine
{
    public class TextureManager : IDisposable
    {
        Dictionary<string, Texture> _textureDatabase = new Dictionary<string, Texture>();

        public Texture Get(string textureId)
        {
            return _textureDatabase[textureId];
        }

        public void LoadTexture(string textureId, string path, string path2 = null)
        {
            int devilId = 0;
            Il.ilGenImages(1, out devilId);
            Il.ilBindImage(devilId); // set as the active texture.

            if (!Il.ilLoadImage(path))
            {
                System.Diagnostics.Debug.Assert(false,
                    "Could not open file, [" + path + "].");
            }

            Ilu.iluFlipImage();

            int width = Il.ilGetInteger(Il.IL_IMAGE_WIDTH);
            int height = Il.ilGetInteger(Il.IL_IMAGE_HEIGHT);
            int openGLId = Ilut.ilutGLBindTexImage();

            System.Diagnostics.Debug.Assert(openGLId != 0);
            Il.ilDeleteImages(1, ref devilId);
            int? width2 = null;
            int? height2 = null;
            int? openGLId2 = null;

            //int devilId = 0;
            Il.ilGenImages(1, out devilId);
            Il.ilBindImage(devilId); // set as the active texture.

            if (path2 != null)
            {
                if (!Il.ilLoadImage(path2))
                {
                    System.Diagnostics.Debug.Assert(false,
                        "Could not open file, [" + path2 + "].");
                }

                Ilu.iluFlipImage();

                width2 = Il.ilGetInteger(Il.IL_IMAGE_WIDTH);
                height2 = Il.ilGetInteger(Il.IL_IMAGE_HEIGHT);
                openGLId2 = Ilut.ilutGLBindTexImage();

                System.Diagnostics.Debug.Assert(openGLId2 != 0);
                Il.ilDeleteImages(1, ref devilId);
            }

            _textureDatabase.Add(textureId, new Texture(openGLId, width, height, openGLId2, width2, height2));
        }


        #region IDisposable Members

        public void Dispose()
        {
            foreach (Texture t in _textureDatabase.Values)
            {
                if(t.Id2 == null)
                {
                    Gl.glDeleteTextures(1, new int[] { t.Id });
                }
                else
                {
                    Gl.glDeleteTextures(2, new int[] { t.Id, Convert.ToInt32(t.Id2) });
                }
                
            }
        }

        #endregion

    }
}
