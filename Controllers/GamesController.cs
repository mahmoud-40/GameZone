using GameZone.Services;

namespace GameZone.Controllers
{
    public class GamesController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly ICategoriesServices _categoriesServices;
        private readonly IDevicesServices _devicesServices;

        public GamesController(ApplicationDbContext context, ICategoriesServices categoriesServices, IDevicesServices devicesServices)
        {
            _context = context;
            _categoriesServices = categoriesServices;
            _devicesServices = devicesServices;
        }
        
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public IActionResult Create()
        {
            CreateGameFormViewModel viewModel = new()
            {
                Categories = _categoriesServices.GetSelectList(),
                Devices = _devicesServices.GetSelectList()
            };
            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(CreateGameFormViewModel model)
        {
            if (!ModelState.IsValid)
            {
                model.Categories = _categoriesServices.GetSelectList();
                model.Devices = _devicesServices.GetSelectList();
                return View(model);
            }

            // save game to db
            // save cover to server
            return RedirectToAction(nameof(Index));
        }
    }
}
