var input = await File.ReadAllLinesAsync("input-05.txt");

var lines = input.Select(x => x.Split(" -> ")).Select(x =>
    new Line(int.Parse(x[0].Split(",")[0]), int.Parse(x[0].Split(",")[1]), int.Parse(x[1].Split(",")[0]), int.Parse(x[1].Split(",")[1])))
    .ToArray();

IEnumerable<Coord> HorizontalCoordinates(IEnumerable<Line> horizontalLines)
{
    foreach (var line in horizontalLines)
    {
        var length = Math.Abs(line.X0 - line.X1);
        var start = Math.Min(line.X0, line.X1);

        for (var x = 0; x <= length; x++)
        {
            yield return new Coord(start + x, line.Y0);
        }
    }
}

IEnumerable<Coord> VerticalCoordinates(IEnumerable<Line> verticalLines)
{
    foreach (var line in verticalLines)
    {
        var length = Math.Abs(line.Y0 - line.Y1);
        var start = Math.Min(line.Y0, line.Y1);

        for (var y = 0; y <= length; y++)
        {
            yield return new Coord(line.X0, start + y);
        }
    }
}

IEnumerable<Coord> DiagonalCoordinates(IEnumerable<Line> diagonalLines)
{
    foreach (var line in diagonalLines)
    {
        var length = Math.Abs(line.Y0 - line.Y1);

        var normalized = line;

        if (line.X0 > line.X1)
            normalized = new Line(line.X1, line.Y1, line.X0, line.Y0);

        for (var i = 0; i <= length; i++)
        {
            if (normalized.Y0 < normalized.Y1)
                yield return new Coord(normalized.X0 + i, normalized.Y0 + i);
            else
                yield return new Coord(normalized.X0 + i, normalized.Y0 - i);
        }
    }
}

void Part1()
{
    var coordinates = new List<Coord>();
    
    var horizontalLines = lines.Where(x => x.Y0 == x.Y1);
    
    var verticalLines = lines.Where(x => x.X0 == x.X1);
    
    coordinates.AddRange(HorizontalCoordinates(horizontalLines));
    
    coordinates.AddRange(VerticalCoordinates(verticalLines));
    
    var overlaps = coordinates.GroupBy(x => x).Select(x => new { x.Key, Count = x.Count() }).Count(x => x.Count > 1);
    
    Console.WriteLine(overlaps); // 4826
}

void Part2()
{
    var coordinates = new List<Coord>();
    
    var horizontalLines = lines.Where(x => x.Y0 == x.Y1);
    
    var verticalLines = lines.Where(x => x.X0 == x.X1);
    
    var diagonalLines = lines.Where(x => Math.Abs(x.Y1 - x.Y0) == Math.Abs(x.X1 - x.X0));

    coordinates.AddRange(HorizontalCoordinates(horizontalLines));

    coordinates.AddRange(VerticalCoordinates(verticalLines));
    
    coordinates.AddRange(DiagonalCoordinates(diagonalLines));
    
    var overlaps = coordinates.GroupBy(x => x).Select(x => new { x.Key, Count = x.Count() }).Count(x => x.Count > 1);

    Console.WriteLine(overlaps); // 16793

}

Part1();
Part2();

internal record Line(int X0, int Y0, int X1, int Y1);

internal record Coord(int X, int Y);