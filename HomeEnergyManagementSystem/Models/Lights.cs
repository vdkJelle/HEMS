namespace HomeEnergyManagementSystem.Models;

public class Lights : Device
{
	public override string Type => "Lights";

	public override double GetConsumption(DateTime time)
	{
		var hour = time.Hour;

		if (hour < 18 || hour > 23)
		{
			return 10; //W
		}
		return 0;
	}

	public override double GetProduction(DateTime time)
	{
		return 0;
	}
}
