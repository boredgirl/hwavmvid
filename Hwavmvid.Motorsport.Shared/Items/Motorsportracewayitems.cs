using System.Collections.Generic;
using System.Linq;

namespace Hwavmvid.Motorsport.Shared.Items
{

    public class Racewaymap
    {

        public List<Racewayrow> Rows { get; set; } = new List<Racewayrow>();
        public List<Racewaycolumn> Columns { get; set; } = new List<Racewaycolumn>();

    }
    public class Racewayrow
    {

        public int RowId { get; set; }

    }
    public class Racewaycolumn
    {

        public int ColumnId { get; set; }
        public int RowId { get; set; }
        
        public List<Racewaymapitem<Racewayitemtype>> Racecars { get; set; } = new List<Racewaymapitem<Racewayitemtype>>();
        public List<Racewaymapitem<Racewayitemtype>> Streetways { get; set; } = new List<Racewaymapitem<Racewayitemtype>>();
        public List<Racewaymapitem<Racewayitemtype>> Buildings { get; set; } = new List<Racewaymapitem<Racewayitemtype>>();
        public List<Racewaymapitem<Racewayitemtype>> Landscapes { get; set; } = new List<Racewaymapitem<Racewayitemtype>>();

        public List<Racewaymapitem<Racewayitemtype>> GetColumnItemsGenericlistBytype(Racewayitemtype itemtype) {

            return itemtype == Racewayitemtype.Racecar ? this.Racecars :
                   itemtype == Racewayitemtype.Streetway ? this.Streetways :
                   itemtype == Racewayitemtype.Building ? this.Buildings :
                   itemtype == Racewayitemtype.Platform ? this.Landscapes : 
                   null;
        }
    
    }
    public class Racewaymapitem<ItemType>
    {

        public Racewaymapitem(string id, Racewayitemtype itemtype)
        {
            this.Id = id;
            this.Racewayitemtype = itemtype;
        }

        public string Id { get; set; }
        public Racewayitemtype Racewayitemtype { get; set; }

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
    public enum Racewayitemtype
    {

        Racecar,
        Streetway,
        Building,
        Platform,

    }
    public class MotorsportracewayEvent
    {

        public Racewaymapitem<Racewayitemtype> Item { get; set; }

    }

}
