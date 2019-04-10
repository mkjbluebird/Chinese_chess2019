
namespace Engine
{
    public struct Texture
    {
        public int Id { get; set; }
        public int Width { get; set; }
        public int Height { get; set; }
        public int? Id2 { get; set; }
        public int? Width2 { get; set; }
        public int? Height2 { get; set; }

        public Texture(int id, int width, int height, int? id2 = null, int? width2 = null, int? height2 = null)
            : this()
        {
            Id = id;
            Width = width;
            Height = height;

            Id2 = id2;
            Width2 = width2;
            Height2 = height2;
        }
    }
}
