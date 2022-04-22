using System;
using System.Text;
using System.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using Hwavmvid.Formula1.Shared.Items;

namespace Hwavmvid.Formula1.Raceway
{
    public class F1Racewaybase : ComponentBase, IDisposable
    {

        public bool loading { get; set; }

        public const double containerwidth = rows * griditemwidth;
        public const double containerheight = cols * griditemheight;

        public const double griditemwidth = 10;
        public const double griditemheight = 10;

        private const int rows = 80;
        private const int cols = 80;

        public string Landscapegreen { get; set; } = "rgba(33,109,70,0.8)"; // #216d46

        protected override Task OnInitializedAsync()
        {
            this.Map = this.GetMap();
            return base.OnInitializedAsync();
        }

        public F1Racewaymap Map { get; set; }
        public F1Racewaymap GetMap()
        {
            F1Racewaymap map = new F1Racewaymap();
            for (var r = 1; r <= rows; r++)
            {

                F1Racewayrow row = new F1Racewayrow();
                row.RowId = r;
                map.Rows.Add(row);

                for (var c = 1; c <= cols; c++)
                {
                    F1Racewaycolumn column = new F1Racewaycolumn();
                    column.ColumnId = c;
                    column.RowId = r;
                    map.Columns.Add(column);
                }
            }

            return map;
        }

        public F1Racewaymapitem<F1Racewayitemtype> landscapeitem { get; set; }
        public void Initlandscape()
        {
            foreach (var row in this.Map.Rows)
            {
                foreach (var container in this.Map.Columns.Where(item => item.RowId == row.RowId).Select((item, index) => new { item = item, index = index }))
                {
                    this.landscapeitem = new F1Racewaymapitem<F1Racewayitemtype>(Guid.NewGuid().ToString(), F1Racewayitemtype.Landscape);
                    this.landscapeitem.RowId = row.RowId;
                    this.landscapeitem.ColumnId = container.index + 1;
                    this.landscapeitem.Id = Guid.NewGuid().ToString();
                    this.landscapeitem.ZIndex = 1;
                    this.landscapeitem.Opacity = 1;
                    this.landscapeitem.BackgroundColor = this.Landscapegreen;
                    this.landscapeitem.Rotation = 0;
                    this.landscapeitem.ImageWidth = 0;
                    this.landscapeitem.ImageHeight = 0;
                    this.landscapeitem.ImageUrl = string.Empty;
                    this.landscapeitem.ImageUrlExtension = string.Empty;
                    this.landscapeitem.Value = 0;

                    this.AddMapItem(landscapeitem.RowId, landscapeitem.ColumnId, this.landscapeitem);
                }
            }
        }

        public F1Racewaycolumn GetMapColumn(int rowid, int colid)
        {
            return this.Map.Columns.FirstOrDefault(item => item.RowId == rowid && item.ColumnId == colid);
        }
        public void AddMapItem(int rowid, int colid, F1Racewaymapitem<F1Racewayitemtype> item)
        {

            var col = this.GetMapColumn(rowid, colid);
            if (item.Mapitemtype == F1Racewayitemtype.Racecar)
            {
                col.Racecars.Add(item);
            }
        }

        public void Dispose()
        {

        }

    }
}
