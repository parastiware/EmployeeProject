using Microsoft.Extensions.Configuration;

namespace Employee.Repository
{
    public  class CommonRepository : ICommonRepository
    {
        private readonly  IConfiguration _configuration;
        public CommonRepository(IConfiguration configuration = null)
        {
            _configuration = configuration;
        }
        public  string GetConnectionstring()
        {
            return _configuration.GetConnectionString("sqlServer");
        }
    }
}
