using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Infrastructure.Commands;
using Infrastructure.Extensions;
using Infrastructure.Settlements;
using Infrastructure.Tradeing;

namespace Infrastructure.PlayerDetails
{
    public class RandomPlayer : BasePlayer
    {
        private readonly Random rnd;

        public RandomPlayer(PlayerColor color, ITradeSystem tradeSystem) : base(color, tradeSystem)
        {
            rnd = new Random();
        }

        public override ReadOnlyResourceBag DropResources(int toDrop)
        {
            var result = new ResourceBag();
            var currentRes = new ResourceBag
            {
                Clay = Resources.Clay,
                Grain = Resources.Grain,
                Ore = Resources.Ore,
                Wood = Resources.Wood,
                Wool = Resources.Wool
            };

            while (toDrop > 0)
            {
                var res = (ResourceType)rnd.Next(Enum.GetNames(typeof(ResourceType)).Length);
                if (currentRes[res] > 0)
                {
                    result[res]++;
                    currentRes[res]--;
                    toDrop--;
                }
            }

            return result.AsReadOnly();
        }

        public override Tuple<int, int> MoveThief()
        {
            throw new NotImplementedException();
        }

        public override Tuple<Village, Road> PickFirstSettlementAndRoad()
        {
            return PickVillageAndRoad();
        }

        public override Tuple<Village, Road> PickSecondSettlementAndRoad()
        {
            return PickVillageAndRoad();
        }

        public override IEnumerable<Command> PlayTurn()
        {
            //var haveResourcesForVillage = BuildingsCosts.CanBuildVillage(Resources);
            //var haveResourcesForRoad

            return new List<Command>();
        }

        public override bool RespondTo(Trade trade, PlayerColor playerColor)
        {
            throw new NotImplementedException();
        }

        private Tuple<Village, Road> PickVillageAndRoad()
        {
            var cellsNumber = Map.Cells.Count;
            var allSettlementCoordinates = Map.Settlements.Select(x => x.Coordinates);
            Village village = null;
            Road road = null;

            while (village == null)
            {
                var idx = rnd.Next(cellsNumber);
                var cellCoord = CoordinatesHelper.CellCoordinatesByIndexAndSize(idx, Map.Size);
                var allSettlements = CoordinatesHelper.NeighbourSettlementsCoordinatesForCell(cellCoord, Map.Size);
                var possibleSettlements = allSettlements.Where(x => !allSettlementCoordinates.Contains(x));
                var validSettlements = possibleSettlements.Where(
                    x => CoordinatesHelper.NeighbourSettlementsCoordinatesForSettlement(x, Map.Size)
                        .All(y => !allSettlementCoordinates.Contains(y))
                    );

                if (validSettlements.Count() > 0)
                {
                    village = new Village(Color, validSettlements.RandomELement());

                    var secondCoord = CoordinatesHelper.NeighbourSettlementsCoordinatesForSettlement(village.Coordinates, Map.Size)
                        .RandomELement();
                    road = new Road(Color, village.Coordinates, secondCoord);
                }
            }

            return new Tuple<Village, Road>(village, road);
        }
    }
}
