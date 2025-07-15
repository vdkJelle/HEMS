namespace HomeEnergyManagementSystem.Dtos;

public class UpdateStateDto
{
	public required Guid Id { get; set; }
	public required bool State { get; set; }
}
