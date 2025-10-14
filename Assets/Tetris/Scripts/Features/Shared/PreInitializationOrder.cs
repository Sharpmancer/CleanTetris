namespace Features.Shared
{
    public static class PreInitializationOrder
    {
        public const int DEFAULT = 0;
        public const int EARLY = -1000;
        public const int LATE = 1000;

        public const int HYDRATE_GAMEPLAY_FEATURES = -500;
        public const int HYDRATE_MAIN_MENU_FEATURES = -500;
    }
}