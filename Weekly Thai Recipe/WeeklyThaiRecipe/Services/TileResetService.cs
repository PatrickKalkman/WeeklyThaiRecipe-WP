namespace WeeklyThaiRecipe.Services
{
    using System.Linq;

    using Microsoft.Phone.Shell;

    using System;

    public class TileResetService : ITileResetService
    {
        public void ResetTile()
        {
            var newTileData = new StandardTileData();
            newTileData.BackgroundImage = new Uri("appdata:Images/Tile.png");
            newTileData.BackContent = string.Empty;
            newTileData.Title = "Weekly Recipe";
            newTileData.Count = 0;
            newTileData.BackBackgroundImage = new Uri(string.Empty, UriKind.Relative);
            newTileData.BackTitle = string.Empty;

            ShellTile appTile = ShellTile.ActiveTiles.First();
            
            appTile.Update(newTileData);
        }
    }
}
