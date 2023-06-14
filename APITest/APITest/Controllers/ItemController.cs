using Microsoft.AspNetCore.Mvc;

namespace APITest.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ItemController : ControllerBase
    {

        private readonly ILogger<ItemController> _logger;
        private readonly DatabaseService _database;

        public ItemController(ILogger<ItemController> logger, DatabaseService database)
        {
            _logger = logger;
            _database = database;
        }

        [HttpGet(Name = "GetItem")]
        public IEnumerable<Item> Get()
        {
            //List<Item> items = new List<Item>();
            //items = _database.GetItem();
            //foreach(Item d in items) {
            //    Console.WriteLine(d.Name);
            //}
            return _database.GetItem();
        }
    }
}