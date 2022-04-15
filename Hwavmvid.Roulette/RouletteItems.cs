using System.Collections.Generic;

namespace Hwavmvid.Roulette
{
    public class RouletteMap
    {
        public List<RouletteRow> Rows = new List<RouletteRow>();
        public List<RouletteColumn> Columns = new List<RouletteColumn>();
    }
    public class RouletteRow
    {
        public int RowId { get; set; }
    }
    public class RouletteColumn
    {
        // xy coordinates
        public int ColumnId { get; set; }
        public int RowId { get; set; }

        // column object items
        public List<RouletteItem> Items { get; set; } = new List<RouletteItem>();        
    }
    public class RouletteItem
    {
        public int RowId { get; set; }
        public int ColumnId { get; set; }
        public string Id { get; set; }
        public int ZIndex { get; set; }
        public double Opacity { get; set; }
        public string BackgroundColor { get; set; }
        public int Rotation { get; set; }
        public string ImageUrl { get; set; }
        public string ImageUrlExtension { get; set; }
        public double ImageWidth { get; set; }
        public double ImageHeight { get; set; }
        public int Value { get; set; }
    }

    public class RouletteBall : RouletteItem
    {

    }
    public class RouletteNumber : RouletteItem
    {

    }
    public class RouletteNumbers : RouletteItem
    {

    }
    public class RouletteBallRaceway : RouletteItem
    {

    }
    public class RouletteCarpet : RouletteItem
    {

    }

    public class RouletteEvent
    {

        public RouletteNumber WinItem { get; set; }

    }
    public enum RouletteDirection
    {
        left,
        right,
    }    

}
