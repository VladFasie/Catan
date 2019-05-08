using Infrastructure;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace MapGeneratorApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MapController : ControllerBase
    {
        [HttpGet]
        [Route("generate/small")]
        public IEnumerable<Cell> GenerateSmall()
        {
            return MapBuilder.GetMap(MapSize.Small).Cells;
        }

        [HttpGet]
        [Route("generate/big")]
        public IEnumerable<Cell> GenerateBig()
        {
            return MapBuilder.GetMap(MapSize.Big).Cells;
        }
    }
}