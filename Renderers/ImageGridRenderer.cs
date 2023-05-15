using System.Drawing;
using System.Resources;
using System.Text;
using ConsoleTables;
using Microsoft.VisualStudio.TestPlatform.CommunicationUtilities.Resources;

namespace WaveFunctionCollapse.Renderers;

public class ImageGridRenderer : IGridRenderer
{
    private readonly string _path;
    private int _size = 600;
    
    public string Path => _path;

    public ImageGridRenderer(string path)
    {
        _path = path;
    }

    private Bitmap MergeImages(IEnumerable<Bitmap> images)
    {
        var enumerable = images as IList<Bitmap> ?? images.ToList();

        var width = 0;
        var height = 0;

        foreach (var image in enumerable)
        {
            width += image.Width;
            height = image.Height > height
                ? image.Height
                : height;
        }

        var bitmap = new Bitmap(width, height);
        using (var g = Graphics.FromImage(bitmap))
        {
            var localWidth = 0;
            foreach (var image in enumerable)
            {
                g.DrawImage(image, localWidth, 0);
                localWidth += image.Width;
            }
        }

        return bitmap;
    }

    private (string Name, string FileName, int ResourceIndex)[] _mappings = new[]
    {
        ("Empty", "blank", 0),
        ("A", "down", 1),
        ("B", "left", 2),
        ("C", "right", 3),
        ("D", "up", 4),
    };

    public void DrawGrid(Grid grid)
    {
        var rootType = typeof(ImageGridRenderer);
        var resourceNames = rootType.Assembly.GetManifestResourceNames();

        var images = new Image[5];
        for (int i = 0; i < 5; i++)
        {
            var resourceName = resourceNames[i];
            var stream = rootType.Assembly.GetManifestResourceStream(resourceName);
            images[i] = Image.FromStream(stream);
        }
        
        var bitmap = new Bitmap(grid.Width * _size, grid.Height * _size);
        using (var g = Graphics.FromImage(bitmap))
        {
            for (int y = 0; y < grid.Height; y++)
            {
                for (int x = 0; x < grid.Width; x++)
                {
                    var cellIndex = y * grid.Width + x;
                    var cell = grid.GetCell(x,y);
                    // var mapping = _mappings.SingleOrDefault(m => m.Name == cell.Text);
                    // // if (mapping is null)
                    // //     throw new Exception($"No mapping found for {cell.Text}");
                    // var image = images[mapping.ResourceIndex];
                    // g.DrawImage(image, x * _size, y * _size);
                }
            }
        }
        bitmap.Save(_path);

       
    }
}