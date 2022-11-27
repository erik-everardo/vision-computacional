namespace VisionComputacional;

using OpenCvSharp;
public class Program
{
    public static void Main()
    {
        // byte[,] matrix =
        // {
        //     {156, 170, 189},
        //     {152, 168, 186},
        //     {150, 164, 180}
        // };
        //
        // var result = Shifting.Shift(matrix, ShiftingType.Vertical);
        //
        // for (var i = 0; i < result.GetLength(0); i++)
        // {
        //     for (var j = 0; j < result.GetLength(1); j++)
        //     {
        //         Console.Write(result[i,j]);
        //         Console.Write(" ");
        //     }
        //     Console.WriteLine();
        // }

        Console.WriteLine("Enter image path");
        var path = Console.ReadLine();

        if (path == null)
        {
            Console.WriteLine("Invalid file...");
            return;
        }
        
        var image = new Mat(path, ImreadModes.Grayscale);

        using var result = Shifting.Shift(image, ShiftingType.Horizontal);

        using(new Window("Original Image", image))
        using (new Window("Result", result))
        {
            Cv2.WaitKey();
        }

    }
}