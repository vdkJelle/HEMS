using System.ComponentModel.DataAnnotations;

namespace HomeEnergyManagementSystem.Dtos;

public class CreateDeviceDto
{
	[Required]
	public required string Type { get; set; }
}
