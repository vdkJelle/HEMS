namespace HomeEnergyManagementSystem.Models;

public class HeatingSystem : Device
{
	public override string Type => "HeatingSystem";

	public override double GetConsumption(DateTime time)
	{
		var hour = time.Hour + time.Minute / 60.0;

		// Morning usage, starts at highest power at 6:00, decreased to turned off at 10:00
		if (hour >= 6 && hour < 10)
		{
			return 2000 * (1 - (hour - 6) / 4); //W
		}
		// Evening usage, starts turned off at 18:00, increased to highest power at 24:00
		else if (hour >= 18 && hour < 24)
		{
			return 2000 * ((hour - 18) / 6); //W
		}
		else
		{
			return 0;
		}
	}

	public override double GetProduction(DateTime time)
	{
		return 0;
	}
}
