using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using Hwavmvid.Motorsport.Shared.Items;

namespace Hwavmvid.Motorsport.Racewaymaps
{
    public class Motorsportracewaymapcreatorbase : ComponentBase
    {

        [Inject] public Motorsportracewayservice Motorsportracewayservice { get; set; }

        public bool loading { get; set; }

        public const double containerwidth = rows * griditemwidth;
        public const double containerheight = cols * griditemheight;

        public const double griditemwidth = 10;
        public const double griditemheight = 10;

        private const int rows = 80;
        private const int cols = 80;

        public string Platformcolor { get; set; } = ConsoleColor.DarkGray.ToString(); // #216d46

        protected override Task OnInitializedAsync()
        {
            this.Motorsportracewayservice.Map = this.GetMap();
            this.InitlandscapeItems();

            return base.OnInitializedAsync();
        }

        public Racewaymap GetMap()
        {

            Racewaymap map = new Racewaymap();
            for (var r = 1; r <= rows; r++)
            {

                Racewayrow row = new Racewayrow();
                row.RowId = r;
                map.Rows.Add(row);

                for (var c = 1; c <= cols; c++)
                {

                    Racewaycolumn column = new Racewaycolumn();
                    column.ColumnId = c;
                    column.RowId = r;
                    map.Columns.Add(column);
                }
            }

            return map;
        }

        public Racewaymapitem<Racewayitemtype> landscapeitem { get; set; }
        public void InitlandscapeItems()
        {

            foreach (var row in Motorsportracewayservice.Map.Rows)
            {

                foreach (var container in Motorsportracewayservice.Map.Columns.Where(item => item.RowId == row.RowId).Select((item, index) => new { item = item, index = index }))
                {

                    this.landscapeitem = new Racewaymapitem<Racewayitemtype>(Guid.NewGuid().ToString(), Racewayitemtype.Platform);
                    this.landscapeitem.RowId = row.RowId;
                    this.landscapeitem.ColumnId = container.index + 1;
                    this.landscapeitem.ZIndex = 1;
                    this.landscapeitem.Opacity = 1;
                    this.landscapeitem.BackgroundColor = this.Platformcolor;
                    this.landscapeitem.Rotation = 0;
                    this.landscapeitem.ImageWidth = 0;
                    this.landscapeitem.ImageHeight = 0;
                    this.landscapeitem.ImageUrl = string.Empty;
                    this.landscapeitem.ImageUrlExtension = string.Empty;
                    this.landscapeitem.Value = 0;

                    this.Motorsportracewayservice.AddMapColumnItem(landscapeitem.RowId, landscapeitem.ColumnId, this.landscapeitem);
                }
            }
        }

    }
}
