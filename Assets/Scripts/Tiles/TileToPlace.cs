namespace Tiles
{
    public class TileToPlace : Tile
    {
        private bool _isFilled = false;
        private Tower.Tower _placedTower;
        
        public bool IsFilled => _isFilled;
        public Tower.Tower PlacedTower => _placedTower;
        
        public void Filled()
        {
            _isFilled = true;
        }

        public void SetPlacedTower(Tower.Tower placedTower)
        {
            _placedTower = placedTower;
        }
    }
}