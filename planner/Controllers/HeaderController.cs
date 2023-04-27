using Microsoft.AspNetCore.Mvc;
using MySql.Data.MySqlClient;
using static Org.BouncyCastle.Math.EC.ECCurve;

namespace planner.Controllers
{
    public class HeaderController : Controller
    {

        private readonly IConfiguration _config;

        public HeaderController(IConfiguration config)
        {
            _config = config;
        }
    }
}
