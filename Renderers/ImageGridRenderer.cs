using System.Diagnostics.CodeAnalysis;
using System.Drawing;
using System.Drawing.Drawing2D;
using WaveFunctionCollapse.CellContents;
using WaveFunctionCollapse.Grids;

namespace WaveFunctionCollapse.Renderers;

[SuppressMessage("Interoperability", "CA1416:Validate platform compatibility")]
public class ImageGridRenderer : IGridRenderer
{
    private readonly string _path;

    public string Path => _path;

    public ImageGridRenderer(string path)
    {
        _path = path;
    }
    
    public void DrawGrid(Grid grid)
    {
        var imageLookup = GenerateImageLookup();

        var size = GetSizeFromImageList(imageLookup.Values);

        var width = grid.Width * size.Width;
        var height = grid.Height * size.Height;
        var bitmap = new Bitmap(width, height);
        using (var g = Graphics.FromImage(bitmap))
        {
            var outlinePen = new Pen(Color.Red, 5);

            grid.IterateThroughCells(cell =>
            {
                if (cell.CellContent is not IEmbeddedResourceContent embeddedResourceContent)
                    throw new Exception($"Cell {cell} is not of type {nameof(IEmbeddedResourceContent)}, but is of type {cell.CellContent.GetType().Name}");

                var resourceName = embeddedResourceContent.GetEmbeddedResourceName();
                var image = imageLookup[resourceName.ToLower()];
                
                var x = cell.XPosition * size.Width;
                var y = cell.YPosition * size.Height;
                var imageWidth = image.Width;
                var imageHeight = image.Height;
                
                g.DrawImage(image, x, y, imageWidth, imageHeight);
                
                //g.DrawRectangle(outlinePen, x,y, imageWidth, imageHeight);
                // var pen2 = new Pen(Color.Blue, 5);
                // g.DrawRectangle(pen2, x,y, image.Width, image.Height);


            });
        }

        bitmap.Save(_path);
    }

    private static Size GetSizeFromImageList(IEnumerable<Image> images)
    {
        var heights = images.Select(x => x.Size.Height).Distinct().ToList();
        if (heights.Count > 1)
            throw new Exception($"Images have different heights: {string.Join(", ", heights)}");
        var height = heights.Single();
        var widths = images.Select(x => x.Size.Width).Distinct().ToList();
        if (widths.Count > 1)
            throw new Exception($"Images have different widths: {string.Join(", ", widths)}");
        var width = widths.Single();
        var size = new Size(width, height);
        return size;
    }

    private static Dictionary<string, Image> GenerateImageLookup()
    {
        // Load images into lookup by resource name
        var rootType = typeof(ImageGridRenderer);
        var resourceNames = rootType.Assembly.GetManifestResourceNames();

        var imageLookup = new Dictionary<string, Image>();
     
        for (int i = 0; i < resourceNames.Length; i++)
        {
            var resourceName = resourceNames[i];
            var stream = rootType.Assembly.GetManifestResourceStream(resourceName)!;
            var shorterName = resourceName.Split(".")[^2];
            imageLookup[shorterName.ToLower()] = Image.FromStream(stream);
        }

        return imageLookup;
    }
}