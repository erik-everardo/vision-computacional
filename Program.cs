using System.Drawing;
using OpenCvSharp;

var mat = new Mat("autobus-escolar.png");

Console.WriteLine(mat.Width);
Console.WriteLine(mat.Height);

var mat3 = new Mat<Vec3b>(mat);
var indexer = mat3.GetIndexer();

var output = File.CreateText("output.txt");
var output2 = File.CreateText("output2.txt");

for (var y = 0; y < mat.Height; y++)
{
    for (var x = 0; x < mat.Width; x++)
    {
        // BGR
        var color = indexer[x,y];
        var blue = color.Item0;
        var green = color.Item1;
        var red = color.Item2;
        
        output.Write($"[{red},{green}, {blue}],");
        var gValue = Math.Truncate((red+blue+green) / 3.0 );
        output2.Write($"{gValue},");
        color.Item0 = Negative(Convert.ToByte(gValue));
        color.Item1 = Negative(Convert.ToByte(gValue));
        color.Item2 = Negative(Convert.ToByte(gValue));
        mat.Set<Vec3b>(x, y, color);
    }
    output.WriteLine();
    output2.WriteLine();
}

output.Close();
output2.Close();

var bitmap = OpenCvSharp.Extensions.BitmapConverter.ToBitmap(mat);


bitmap.Save("out.bmp");
byte Negative(byte value)
{
    return Convert.ToByte(Convert.ToByte(255) - value);
}