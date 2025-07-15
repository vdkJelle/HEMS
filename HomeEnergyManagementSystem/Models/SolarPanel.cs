namespace HomeEnergyManagementSystem.Models;

public class SolarPanel : Device
{
	public override string Type => "SolarPanel";

	public override double GetConsumption(DateTime time)
	{
		return 0;
	}

	public override double GetProduction(DateTime time)
	{
		var hour = time.Hour;

		if (hour < 6 || hour >= 18)
		{
			return 0;
		}

		// Variable production, peak at 12:00, lowest at 6:00 and 17:00
		double peakProduction = 5; //kWh
		double production = peakProduction * Math.Sin(Math.PI * (hour - 6) / 12);
		return production * 1000; //W
	}
}
