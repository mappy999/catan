﻿using System.Linq;
using AIsOfCatan.API;

namespace AIsOfCatan
{
    public class StartActions : IStartActions
    {
        private readonly Player player;
        private readonly GameController controller;
        private bool settlementBuilt = false;
        private bool roadBuilt = false;
        private Intersection settlementPosition;
        private Edge roadPosition;
        public StartActions(Player player, GameController controller)
        {
            this.player = player;
            this.controller = controller;
        }

        /// <summary>
        /// Returns whether the start action has been completed
        /// </summary>
        public bool IsComplete()
        {
            return roadBuilt && settlementBuilt;
        }

        /// <summary>
        /// Internal method used for handing out resources
        /// </summary>
        public Intersection GetSettlementPosition()
        {
            return settlementPosition;
        }

        /// <summary>
        /// Internal method used for handing out resources
        /// </summary>
        public Edge GetRoadPosition()
        {
            return roadPosition;
        }


        /// <summary>
        /// Build a settlement on the board
        /// If you try to build too close to another building a IllegalBuildPosition is thrown
        /// Must be called before BuildRoad, otherwise an IllegalActionException is thrown
        /// </summary>
        /// <param name="firstTile">The intersection</param>
        public void BuildSettlement(Intersection intersection)
        {
            if (settlementBuilt) throw new IllegalActionException("Only one settlement may be built in a turn during the startup");
            settlementPosition = intersection;
            controller.BuildFirstSettlement(player, intersection);
            settlementBuilt = true;
        }

        /// <summary>
        /// Build a road on the board
        /// If you try to build at a position not connected to the newly placed settlement an IllegalBuildPositionException is thrown
        /// If you try to build more than one road an IllegalActionException is thrown
        /// </summary>
        /// <param name="firstTile">The first tile that the road will be along</param>
        /// <param name="secondTile">The second tile that the road will be along</param>
        public void BuildRoad(Edge edge)
        {
            if (roadBuilt) throw new IllegalActionException("Only one road may be built in a turn during the startup");
            if (!settlementBuilt) throw new IllegalActionException("The settlement must be placed before the road");
            int[] array = new int[] { settlementPosition.FirstTile, settlementPosition.SecondTile, settlementPosition.ThirdTile };
            if (!(array.Contains(edge.FirstTile) && array.Contains(edge.SecondTile)))
                throw new IllegalBuildPositionException("The road must be placed next to the settlement");
            roadPosition = edge;
            controller.BuildFirstRoad(player, edge);
            roadBuilt = true;
        }
    }
}
