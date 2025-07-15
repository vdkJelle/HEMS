using HomeEnergyManagementSystem.Services;
using Microsoft.AspNetCore.Mvc;

namespace HomeEnergyManagementSystem.Controllers;

[ApiController]
[Route("api/[controller]")]
public class EnergyUsageController : Controller
{
	private readonly IEnergyUsageService _energyUsageService;

	public EnergyUsageController(IEnergyUsageService energyUsageService)
	{
		_energyUsageService = energyUsageService;
	}

	[HttpGet]
	public IActionResult GetEnergyUsageAtTimestamp([FromQuery] DateTimeOffset time)
	{
		var result = _energyUsageService.GetEnergyUsageAtTimestamp(time.UtcDateTime);

		return Ok(new
		{
			time = time.ToString(),
			consumption =  Math.Round(result.Consumption),
			production = Math.Round(result.Production),
			usage = Math.Round(result.Usage)
		});
	}
}
