using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [ApiController]
    [Route("[controller]/[action]")]
    public class ItemController : ControllerBase
    {

        private readonly ILogger<ItemController> _logger;
        private readonly DALManager _database;

        public ItemController(ILogger<ItemController> logger, DALManager database)
        {
            _logger = logger;
            _database = database;
        }

        [HttpGet(Name = "GetItem")]
        public IEnumerable<Item> GetItem()
        {
            return _database.GetItem();
        }

        [HttpGet(Name = "CallBasket")]
        public IEnumerable<BItem> CallBasket()
        {
            return _database.CallBasket();
        }

        [HttpPost(Name = "AddToBasket")]
        public void AddToBasket([FromBody] PostBasket pb)
        {
            _database.AddToBasket(pb.Id, pb.Amount);
        }

        [HttpPost(Name = "DelFromBasket")]
        public void DelFromBasket([FromBody] PostBasket pb)
        {
            _database.DelFromBasket(pb.Id);
        }

        [HttpGet(Name = "Summary")]
        public IEnumerable<BSum> Summary()
        {
            return _database.Summary();
        }

    }
}