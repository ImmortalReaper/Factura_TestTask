namespace ShootingCar.Feature.UIModule
{
    public class UIConfig {
        public string Prefab;
        public int SortingOrder;

        public UIConfig(string prefab, int sortingOrder) {
            Prefab = prefab;
            SortingOrder = sortingOrder;
        }
    }
}
