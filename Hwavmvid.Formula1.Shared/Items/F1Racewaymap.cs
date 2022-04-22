using System.Collections.Generic;

namespace Hwavmvid.Formula1.Shared.Items
{

    public class F1Racewaymap
    {

        public List<F1Racewayrow> Rows { get; set; }
        public List<F1Racewaycolumn> Columns { get; set; }

    }

    public class F1Racewayrow
    {

        public int RowId { get; set; }

    }

    public class F1Racewaycolumn
    {

        public int ColumnId { get; set; }
        public int RowId { get; set; }
        
        public List<F1Racewaymapitem<F1Racewayitemtype>> Racecars { get; set; } = new List<F1Racewaymapitem<F1Racewayitemtype>>();
        public List<F1Racewaymapitem<F1Racewayitemtype>> Streetways { get; set; } = new List<F1Racewaymapitem<F1Racewayitemtype>>();
        public List<F1Racewaymapitem<F1Racewayitemtype>> Buildings { get; set; } = new List<F1Racewaymapitem<F1Racewayitemtype>>();
        public List<F1Racewaymapitem<F1Racewayitemtype>> Landscapes { get; set; } = new List<F1Racewaymapitem<F1Racewayitemtype>>();

    }

    public class F1Racewaymapitem<F1Racewayitemtype>
    {

        public F1Racewaymapitem(string id, F1Racewayitemtype itemtype)
        {
            this.Id = id;
            this.Mapitemtype = itemtype;
        }

        public string Id { get; set; }
        public F1Racewayitemtype Mapitemtype { get; set; }

        public int RowId { get; set; }
        public int ColumnId { get; set; }
        public int ZIndex { get; set; }        
        public string BackgroundColor { get; set; }
        public int Rotation { get; set; }
        public int Value { get; set; }
        public double Opacity { get; set; }
        public string ImageUrl { get; set; }
        public string ImageUrlExtension { get; set; }
        public double ImageWidth { get; set; }
        public double ImageHeight { get; set; }

    }

    public enum F1Racewayitemtype
    {

        Racecar,
        Streetway,
        Building,
        Landscape,

    }

}
