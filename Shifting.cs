using OpenCvSharp;

namespace VisionComputacional;

public enum ShiftingType
{
    Horizontal, Vertical, Diagonal
}


public class Shifting
{
    public static Mat Shift(Mat array, ShiftingType shiftingType)
    {
        var result = new Mat();
        array.CopyTo(result);

        var indexer = array.GetGenericIndexer<byte>();
        switch (shiftingType)
        {
            case ShiftingType.Horizontal:
                for (var y = 0; y < array.Height; y++)
                {
                    var pivot = indexer[y, 0];
                    result.Set(y, 0, pivot);
                    for (var x = 1; x < array.Width; x++)
                    {
                        var current = indexer[y, x];
                        double proportion;
                        if (pivot > current)
                        {
                            proportion = Convert.ToDouble(current) / Convert.ToDouble(pivot);
                        }
                        else if(pivot < current)
                        {
                            proportion = Convert.ToDouble(pivot) / Convert.ToDouble(current);
                        }
                        else
                        {
                            proportion = 1;
                        }
                        

                        var value = Convert.ToByte(Math.Floor(pivot * proportion));
                        result.Set(y, x, value);
                    }
                }
                break;
            case ShiftingType.Vertical: 
                for (var x = 0; x < array.Width; x++)
                {
                    
                    var pivot = indexer[0, x];
                    result.Set(0, x, pivot);
                    for (var y = 1; y < array.Height; y++)
                    {
                        var current = indexer[y, x];
                        var proportion = Convert.ToDecimal(pivot) / Convert.ToDecimal(current);

                        try
                        {
                            result.Set(y, x, Convert.ToByte(Math.Floor(pivot * proportion)));
                        }
                        catch (OverflowException)
                        {
                            result.Set(y, x, 0);
                        }
                    }
                }
                break;
            case ShiftingType.Diagonal:
                // Diagonales superiores
                for (var y = 0; y < array.Height; y++)
                {
                    var pivot = indexer[y, 0];
                    
                    var x = 0;
                    // Aqui recorro cada diagonal
                    while (x <= array.Width)
                    {
                        var current = indexer[y, x];
                        double proportion;
                        if (pivot > current)
                        {
                            proportion = Convert.ToDouble(current) / Convert.ToDouble(pivot);
                        }
                        else if(pivot < current)
                        {
                            proportion = Convert.ToDouble(pivot) / Convert.ToDouble(current);
                        }
                        else
                        {
                            proportion = 1;
                        }
                        var value = Convert.ToByte(Math.Floor(pivot * proportion));
                        result.Set(y, x, value);
                        x++;
                    }
                }
                // Diagonales inferiores
                for(var x = 0; x < array.Width; x++)
                {
                    var pivot = indexer[0, x];
                    
                    var y = 0;
                    // Aqui recorro cada diagonal
                    while (y <= array.Height)
                    {
                        var current = indexer[y, x];
                        double proportion;
                        if (pivot > current)
                        {
                            proportion = Convert.ToDouble(current) / Convert.ToDouble(pivot);
                        }
                        else if(pivot < current)
                        {
                            proportion = Convert.ToDouble(pivot) / Convert.ToDouble(current);
                        }
                        else
                        {
                            proportion = 1;
                        }
                        var value = Convert.ToByte(Math.Floor(pivot * proportion));
                        result.Set(y, x, value);
                        y++;
                    }
                }
                break;
            default: break;
        }
        return result;
    }
}