﻿
using System.Collections.Generic;
using AIsOfCatan.API;

namespace AIsOfCatan
{
    public class MainActions : IGameActions
    {
        private readonly Player player;
        private readonly GameController controller;
        private bool valid;
        private bool hasPlayedDevCard;
        private bool isAfterDieRoll;
        public MainActions(Player player, GameController controller)
        {
            this.player = player;
            this.controller = controller;
            valid = true;
            this.hasPlayedDevCard = false;
            this.isAfterDieRoll = false;
        }

        /// <summary>
        /// Allow the build actions to be called
        /// </summary>
        public void DieRoll()
        {
            isAfterDieRoll = true;
        }

        /// <summary>
        /// Invalidate this action object making all methods throw IllegalActionExceptions if called
        /// </summary>
        public void Invalidate()
        {
            valid = false;
        }

        
        public GameState TradeBank(Resource giving, Resource receiving)
        {
            if (!valid) throw new IllegalActionException("Tried to trade on an invalid GameAction");
            if (!isAfterDieRoll) throw new IllegalActionException("Tried to trade before the die roll");
            return controller.TradeBank(player, giving, receiving);
        }

        //Development cards

        public GameState DrawDevelopmentCard()
        {
            if (!valid) throw new IllegalActionException("Tried to perform an action on an invalid GameAction");
            if (!isAfterDieRoll) throw new IllegalActionException("Tried to draw developmentcard before the die roll");
            var result = controller.DrawDevelopmentCard(player);
            return result;
        }

        public GameState PlayKnight()
        {
            if (!valid) throw new IllegalActionException("Tried to perform an action on an invalid GameAction");
            if (hasPlayedDevCard)
                throw new IllegalActionException("Max one development card can be played each turn");
            hasPlayedDevCard = true;
            return controller.PlayKnight(player);
        }

        public GameState PlayRoadBuilding(Edge firstEdge, Edge secondEdge)
        {
            if (!valid) throw new IllegalActionException("Tried to perform an action on an invalid GameAction");
            if (hasPlayedDevCard)
                throw new IllegalActionException("Max one development card can be played each turn");
            hasPlayedDevCard = true;
            return controller.PlayRoadBuilding(player, firstEdge, secondEdge);
        }

        public GameState PlayYearOfPlenty(Resource resource1, Resource resource2)
        {
            if (!valid) throw new IllegalActionException("Tried to perform an action on an invalid GameAction");
            if (hasPlayedDevCard)
                throw new IllegalActionException("Max one development card can be played each turn");
            hasPlayedDevCard = true;
            return controller.PlayYearOfPlenty(player, resource1, resource2);
        }

        public GameState PlayMonopoly(Resource resource)
        {
            if (!valid) throw new IllegalActionException("Tried to perform an action on an invalid GameAction");
            if (hasPlayedDevCard)
                throw new IllegalActionException("Max one development card can be played each turn");
            hasPlayedDevCard = true;
            return controller.PlayMonopoly(player, resource);
        }

        //Trading

        public Dictionary<int, ITrade> ProposeTrade(List<List<Resource>> give, List<List<Resource>> take)
        {
            if (!valid) throw new IllegalActionException("Tried to perform an action on an invalid GameAction");
            if (!isAfterDieRoll) throw new IllegalActionException("Tried to propose trade before the die roll");
            return controller.ProposeTrade(player, give, take);
        }

        public GameState Trade(int otherPlayer)
        {
            if (!valid) throw new IllegalActionException("Tried to perform an action on an invalid GameAction");
            if (!isAfterDieRoll) throw new IllegalActionException("Tried to trade before the die roll");
            return controller.CompleteTrade(player, otherPlayer);
        }

        //Building

        public GameState BuildSettlement(Intersection intersection)
        {
            if (!valid) throw new IllegalActionException("Tried to perform an action on an invalid GameAction");
            if (!isAfterDieRoll) throw new IllegalActionException("Tried to build before the die roll");
            return controller.BuildSettlement(player, intersection);
        }

        public GameState BuildCity(Intersection intersection)
        {
            if (!valid) throw new IllegalActionException("Tried to perform an action on an invalid GameAction");
            if (!isAfterDieRoll) throw new IllegalActionException("Tried to build before the die roll");
            return controller.BuildCity(player, intersection);
        }

        public GameState BuildRoad(Edge edge)
        {
            if (!valid) throw new IllegalActionException("Tried to perform an action on an invalid GameAction");
            if (!isAfterDieRoll) throw new IllegalActionException("Tried to build before the die roll");
            return controller.BuildRoad(player, edge);
        }
    }
}
