using System;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Text.RegularExpressions;
using System.Windows;

namespace TraderForPoe.WPF.Classes
{
    public enum TradeTypeEnum { BUY, SELL };

    public class TradeObject
    {
        //TODO eventuell entfernen. wird nicht benötigt.
        public static ObservableCollection<TradeObject> TradeObjectList { get; set; } = new ObservableCollection<TradeObject>();

        /// <summary>
        /// Constructor for the TradeObject
        /// </summary>
        public TradeObject(string whisper)
        {
            Whisper = whisper;
            ParseWhisper(whisper);
            TradeObjectList.Add(this);
        }

        public TradeTypeEnum TradeType { get; set; }

        public string Whisper { get; set; }

        public string Customer { get; set; }

        public string League { get; set; }

        public string Stash { get; set; }

        /// <summary>
        /// The item position in stash as point
        /// </summary>
        public Point Position { get; set; }

        public Item Item { get; set; }

        public string AdditionalText { get; set; }

        private void ParseWhisper(string whisper)
        {
            var poeTradeRegex = new Regex("@(?<messageType>.*) (?<customer>.*): Hi, I would like to buy your (?<item>.*) listed for (?<amountPrice>[0-9]*[.]?[0-9]+) (?<itemPrice>.*) in (?<league>.*) [(]stash tab \"(?<stashName>.*)[\"]; position: left (?<stashPositionX>[0-9]*), top (?<stashPositionY>[0-9]*)[)](?<additionalText>.*)");
            var poeTradeUnpricedRegex = new Regex("@(?<messageType>.*) (?<customer>.*): Hi, I would like to buy your (?<item>.*) listed in (?<league>.*) [(]stash tab \"(?<stashName>.*)[\"]; position: left (?<stashPositionX>[0-9]*), top (?<stashPositionY>[0-9]*)[)](?<additionalText>.*)");
            var poeTradeNoLocationRegex = new Regex("@(?<messageType>.*) (?<customer>.*): Hi, I would like to buy your (?<item>.*) listed for (?<amountPrice>[0-9]*[.]?[0-9]+) (?<itemPrice>.*) in (?<league>[^\\s]+)(?<additionalText>.*)");
            var poeTradeCurrencyRegex = new Regex("@(?<messageType>.*) (?<customer>.*): Hi, I'd like to buy your (?<amountItem>[0-9]*[.]?[0-9]+) (?<item>.*) for my (?<amountPrice>[0-9]*[.]?[0-9]+) (?<itemPrice>.*) in (?<league>.*)[.](?<additionalText>.*)");

            var poeAppRegEx = new Regex("@(?<messageType>.*) (?<customer>.*): wtb (?<item>.*) listed for (?<amountPrice>[0-9]*[.]?[0-9]+) (?<itemPrice>.*) in (?<league>.*) [(]stash \"(?<stashName>.*)[\"]; left (?<stashPositionX>[0-9]*), top (?<stashPositionY>[0-9]*)[)](?<additionalText>.*)");
            var poeAppUnpricedRegex = new Regex("@(?<messageType>.*) (?<customer>.*): wtb (?<item>.*) in (?<league>.*) [(]stash \"(?<stashName>.*)[\"]; left (?<stashPositionX>[0-9]*), top (?<stashPositionY>[0-9]*)[)](?<additionalText>.*)");
            var poeAppCurrencyRegex = new Regex("@(?<messageType>.*) (?<customer>.*): I'd like to buy your (?<amountItem>[0-9]*[.]?[0-9]+) (?<item>.*) for my (?<amountPrice>[0-9]*[.]?[0-9]+) (?<itemPrice>.*) in (?<league>.*)[.](?<additionalText>.*)");

            if (poeTradeRegex.IsMatch(whisper))
            {
                var matches = Regex.Matches(whisper, poeTradeRegex.ToString());

                foreach (Match match in matches)
                {
                    TradeType = GetTradeType(match.Groups["messageType"].Value);

                    Customer = match.Groups["customer"].Value;

                    Item = new Item(match.Groups["item"].Value, 1);
                    Item.Price = new ItemBase(match.Groups["itemPrice"].Value, decimal.Parse(match.Groups["amountPrice"].Value, NumberStyles.Any, CultureInfo.InvariantCulture));

                    League = match.Groups["league"].Value;

                    Stash = match.Groups["stashName"].Value;

                    Position = new Point(Convert.ToInt16(match.Groups["stashPositionX"].Value), Convert.ToInt16(match.Groups["stashPositionY"].Value));

                    AdditionalText = match.Groups["additionalText"].Value.Trim();
                }
            }
            else if (poeTradeUnpricedRegex.IsMatch(whisper) && !whisper.Contains("listed for"))
            {
                var matches = Regex.Matches(whisper, poeTradeUnpricedRegex.ToString());

                foreach (Match match in matches)
                {
                    TradeType = GetTradeType(match.Groups["messageType"].Value);

                    Customer = match.Groups["customer"].Value;

                    Item = new Item(match.Groups["item"].Value, 1);

                    League = match.Groups["league"].Value;

                    Stash = match.Groups["stashName"].Value;

                    Position = new Point(Convert.ToInt16(match.Groups["stashPositionX"].Value), Convert.ToInt16(match.Groups["stashPositionY"].Value));

                    AdditionalText = match.Groups["additionalText"].Value.Trim();
                }
            }
            else if (poeTradeNoLocationRegex.IsMatch(whisper))
            {
                var matches = Regex.Matches(whisper, poeTradeNoLocationRegex.ToString());

                foreach (Match match in matches)
                {
                    TradeType = GetTradeType(match.Groups["messageType"].Value);

                    Customer = match.Groups["customer"].Value;

                    Item = new Item(match.Groups["item"].Value, 1)
                    {
                        // Set price
                        Price = new ItemBase(match.Groups["itemPrice"].Value, decimal.Parse(match.Groups["amountPrice"].Value, NumberStyles.Any, CultureInfo.InvariantCulture))
                    };

                    League = match.Groups["league"].Value;

                    AdditionalText = match.Groups["additionalText"].Value.Trim();
                }

            }
            else if (poeTradeCurrencyRegex.IsMatch(whisper))
            {
                var matches = Regex.Matches(whisper, poeTradeCurrencyRegex.ToString());

                foreach (Match match in matches)
                {
                    TradeType = GetTradeType(match.Groups["messageType"].Value);

                    Customer = match.Groups["customer"].Value;

                    Item = new Item(match.Groups["item"].Value, decimal.Parse(match.Groups["amountItem"].Value, NumberStyles.Any, CultureInfo.InvariantCulture))
                    {
                        // Set price
                        Price = new ItemBase(match.Groups["itemPrice"].Value, decimal.Parse(match.Groups["amountPrice"].Value, NumberStyles.Any, CultureInfo.InvariantCulture))
                    };

                    League = match.Groups["league"].Value;

                    AdditionalText = match.Groups["additionalText"].Value.Trim();
                }


            }
            else if (poeAppRegEx.IsMatch(whisper))
            {
                var matches = Regex.Matches(whisper, poeAppRegEx.ToString());

                foreach (Match match in matches)
                {
                    TradeType = GetTradeType(match.Groups["messageType"].Value);

                    Customer = match.Groups["customer"].Value;

                    Item = new Item(match.Groups["item"].Value, 1)
                    {
                        // Set price
                        Price = new ItemBase(match.Groups["itemPrice"].Value, decimal.Parse(match.Groups["amountPrice"].Value, NumberStyles.Any, CultureInfo.InvariantCulture))
                    };

                    League = match.Groups["league"].Value;

                    Stash = match.Groups["stashName"].Value;

                    Position = new Point(Convert.ToInt16(match.Groups["stashPositionX"].Value), Convert.ToInt16(match.Groups["stashPositionY"].Value));

                    AdditionalText = match.Groups["additionalText"].Value.Trim();
                }
            }
            else if (!whisper.Contains("listed for") && poeAppUnpricedRegex.IsMatch(whisper))
            {
                var matches = Regex.Matches(whisper, poeAppUnpricedRegex.ToString());

                foreach (Match match in matches)
                {
                    TradeType = GetTradeType(match.Groups["messageType"].Value);

                    Customer = match.Groups["customer"].Value;

                    Item = new Item(match.Groups["item"].Value, 1);

                    League = match.Groups["league"].Value;

                    Stash = match.Groups["stashName"].Value;

                    Position = new Point(Convert.ToInt16(match.Groups["stashPositionX"].Value), Convert.ToInt16(match.Groups["stashPositionY"].Value));

                    AdditionalText = match.Groups["additionalText"].Value.Trim();
                }
            }
            else if (!whisper.Contains("Hi, ") && poeAppCurrencyRegex.IsMatch(whisper))
            {
                var matches = Regex.Matches(whisper, poeAppCurrencyRegex.ToString());

                foreach (Match match in matches)
                {
                    TradeType = GetTradeType(match.Groups["messageType"].Value);

                    Customer = match.Groups["customer"].Value;

                    Item = new Item(match.Groups["item"].Value, decimal.Parse(match.Groups["amountItem"].Value, NumberStyles.Any, CultureInfo.InvariantCulture))
                    {
                        // Set price
                        Price = new ItemBase(match.Groups["itemPrice"].Value, decimal.Parse(match.Groups["amountPrice"].Value, NumberStyles.Any, CultureInfo.InvariantCulture))
                    };

                    League = match.Groups["league"].Value;

                    AdditionalText = match.Groups["additionalText"].Value.Trim();
                }


            }
            else
            {
                //TODO Korrekt implementieren
                throw new NotImplementedException();
            }
        }

        private TradeTypeEnum GetTradeType(string arg)
        {
            if (arg.ToLower().StartsWith("to"))
            {
                return TradeTypeEnum.BUY;
            }
            else if (arg.ToLower().StartsWith("from"))
            {
                return TradeTypeEnum.SELL;
            }
            else
            {
                throw new NotImplementedException();
            }
        }

        public override string ToString()
        {
            //TODO Korrekt implementieren
            return Customer + "\n" + Item.ItemAsString + "\n" + TradeType + "\n" + Stash;
        }

        public static bool IsLogTradeWhisper(string arg)
        {
            if ((arg.Contains("@From") || arg.Contains("@To") && (arg.Contains("like to buy your ") || arg.Contains(": wtb "))))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public static bool IsTradeWhisper(string arg)
        {
            if (arg.StartsWith("@") && (arg.Contains("like to buy your ") || arg.Contains(" wtb ")))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public static bool ItemExists(TradeObject arg)
        {
            foreach (var to in TradeObjectList)
            {
                if (to.Item == arg.Item &&
                    to.Customer == arg.Customer &&
                    to.Item.Price.Amount == arg.Item.Price.Amount &&
                    to.Position == arg.Position &&
                    to.TradeType == arg.TradeType)
                {
                    return true;
                }
            }

            return false;
        }

        public static void RemoveItemFromList(TradeObject arg)
        {
            for (var i = 0; i < TradeObjectList.Count; i++)
            {
                if (TradeObjectList[i].Item == arg.Item &&
                    TradeObjectList[i].Customer == arg.Customer &&
                    TradeObjectList[i].Item.Price.Amount == arg.Item.Price.Amount &&
                    TradeObjectList[i].Position == arg.Position &&
                    TradeObjectList[i].TradeType == arg.TradeType)
                {
                    TradeObjectList.RemoveAt(i);
                }
            }
        }
    }







}

