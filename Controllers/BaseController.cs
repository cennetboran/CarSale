using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using CarSale.Infrastructure;

namespace CarSale.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class BaseController<T> : ControllerBase
        where T : class
    {
        protected readonly IRepository<T> _repository;
        protected readonly IMapper _mapper;
        protected readonly ICurrentUserService _currentUserService;
        public BaseController(IRepository<T> repo, IMapper mapper, ICurrentUserService currentUserService)
        {
            _repository = repo;
            _mapper = mapper;
            _currentUserService = currentUserService;
        }
    }
}
