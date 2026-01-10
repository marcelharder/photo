namespace fotoservice.extensions;

    public static class AppSettings
    {
        public static IConfiguration Configuration { get; private set; }
        public static string? ConnectionString{get; private set;}
           
        public static void Initialize(IConfiguration configuration)
        {
            ConnectionString = configuration.GetConnectionString("SQLConnection");
        }
        public static string? GetConnectionString()
        {
            return ConnectionString;
        }
    }