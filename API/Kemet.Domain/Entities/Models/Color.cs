namespace Entities.Models;

public class Color
{
    public int ColorId { get; set; }
    public string Name { get; set; }
    public string HexaCode { get; set; }

    public override bool Equals(object? obj)
    {
        var color = obj as Color;
        if (color is null) return false;

        return this.ColorId == color.ColorId && this.HexaCode == color.HexaCode && this.Name == color.Name;
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(HexaCode, Name, ColorId);
    }
}
