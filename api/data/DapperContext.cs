namespace fotoservice.data;

    public class DapperContext
    {
       
    
    private readonly string? _connectionString;

    private readonly IConfiguration AppSettings;

    public DapperContext(IConfiguration configuration)
    {
        AppSettings = configuration;
        _connectionString = AppSettings.GetConnectionString("SQLConnection");
    }

     public IDbConnection CreateConnection()
         => new MySqlConnection(_connectionString);

    }
