namespace HomeEnergyManagementSystem.Dtos;

public class EnergyUsageDto
{
	public required string Time { get; set; }
	public double Consumption {  get; set; }
	public double Production { get; set; }
	public double Usage { get; set; }
}
