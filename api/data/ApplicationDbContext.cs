namespace fotoservice.data;

    public class ApplicationDbContext : IdentityDbContext<
    AppUser, 
    AppRole, 
    int, 
    IdentityUserClaim<int>,
    AppUserRole,
    IdentityUserLogin<int>,
    IdentityRoleClaim<int>, 
    IdentityUserToken<int>
    >
    
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }
        public DbSet<models.Image> Images { get; set; }
        public DbSet<Category> Categories { get; set; }
       



        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
        }
    }

