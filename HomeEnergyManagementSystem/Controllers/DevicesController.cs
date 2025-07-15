using Microsoft.AspNetCore.Mvc;
using HomeEnergyManagementSystem.Services;
using HomeEnergyManagementSystem.Dtos;

namespace HomeEnergyManagementSystem.Controllers;

[ApiController]
[Route("api/[controller]")]
public class DevicesController : ControllerBase
{
	private readonly IDeviceService _deviceManagementService;

	public DevicesController(IDeviceService deviceManagementService)
	{
		_deviceManagementService = deviceManagementService;
	}

	[HttpGet]
	[Route("")] // ~/api/Devices
	public IActionResult GetAllDevices()
	{
		var devices = _deviceManagementService.GetAllDevices();
		return Ok(devices);
	}

	[HttpPost]
	[Route("~/api/Device")]
	public IActionResult AddNewDevice([FromBody] CreateDeviceDto createDeviceDto)
	{
		if (!ModelState.IsValid)
		{
			return BadRequest(ModelState);
		}

		var createdDevice = _deviceManagementService.AddNewDevice(createDeviceDto);
		return Created(string.Empty, createdDevice);
	}

	[HttpPut]
	[Route("~/api/Device")]
	public IActionResult UpdateStateDevice([FromBody] UpdateStateDto updateStateDto)
	{
		if (!ModelState.IsValid)
		{
			return BadRequest(ModelState);
		}

		try
		{
			var updatedDevice = _deviceManagementService.UpdateStateDevice(updateStateDto);
			return Ok(updatedDevice);
		}
		catch (KeyNotFoundException e)
		{
			return NotFound(new { message = e.Message });
		}
	}

	[HttpDelete]
	[Route("~/api/Device")]
	public IActionResult Delete([FromQuery] Guid id)
	{
		try
		{
			var deletedDevice = _deviceManagementService.DeleteDevice(id);
			return Ok(deletedDevice);
		}
		catch (KeyNotFoundException e)
		{
			return NotFound(new { message = e.Message });
		}
	}
}
