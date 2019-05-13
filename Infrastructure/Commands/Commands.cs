using Infrastructure.Settlements;

namespace Infrastructure.Commands
{
    public class Command
    {
        private readonly string _command;
        public string Type { get; }
        public IAsset Asset { get; }

        public Command(Village village)
        {
            Type = CommandType.BUID_VILLAGE;
            Asset = village;

            var coord = village.Coordinates;
            _command = string.Format(Type, coord.Item1, coord.Item2);
        }

        public Command(Road road)
        {
            Type = CommandType.BUID_ROAD;
            Asset = road;

            var coord1 = road.A;
            var coord2 = road.B;
            _command = string.Format(Type, coord1.Item1, coord1.Item2, coord2.Item1, coord2.Item2);
        }

        public Command(City city)
        {
            Type = CommandType.UPGRADE_VILLAGE;
            Asset = city;

            var coord = city.Coordinates;
            _command = string.Format(Type, coord.Item1, coord.Item2);
        }
    }
}
