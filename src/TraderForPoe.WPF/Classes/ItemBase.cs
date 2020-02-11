using System;
using System.Windows.Media.Imaging;
using static System.String;

namespace TraderForPoe.WPF.Classes
{

    public enum ItemType
    {
        UNKNOWN, CHAOS, ALCHEMY, ALTERATION, ANCIENT, ANNULMENT, APPRENTICE_SEXTANT,
        ARMOUR_SCRAP, AUGMENTATION, BAUBLE, BESTIARY_ORB, BINDING_ORB, BLACKSMITH_WHETSTONE,
        BLESSING_CHAYULAH, BLESSING_ESH, BLESSING_TUL, BLESSING_UUL, BLESSING_XOPH, BLESSE,
        CHANCE, CHISEL, CHROM, DIVINE, ENGINEER, ETERNAL, EXALTED, FUSING, GEMCUTTERS, HARBINGER_ORB,
        HORIZON_ORB, IMPRINTED_BESTIARY, JEWELLER, JOURNEYMAN_SEXTANT, MASTER_SEXTANT, MIRROR, PORTAL,
        REGAL, REGRET, SCOUR, SILVER, SPLINTER_CHAYULA, SPLINTER_ESH, SPLINTER_TUL, SPLINTER_UUL,
        SPLINTER_XOPH, TRANSMUTE, VAAL, WISDOM, DIVINE_VESSEL, OFFERING_GODDESS, SACRIFICE_DAWN,
        SACRIFICE_DUSK, SACRIFICE_MIDNIGHT, SACRIFICE_NOON, PERANDUS_COIN
    };

    /// <summary>
    /// Provides a class, wich contains most used items in game.
    /// </summary>
    public class ItemBase
    {
        public ItemBase(string itemArg, decimal amountArg = 1)
        {
            ItemAsString = itemArg;
            Amount = amountArg;
            ItemAsType = GetItemType(itemArg);
            Image = GetItemImage(ItemAsType);
        }

        private ItemType GetItemType(string s)
        {
            if (IsNullOrEmpty(s)) return ItemType.UNKNOWN;
            var strPrice = s.ToLower();

            if (strPrice.Contains("chaos") && !strPrice.Contains("shard"))
            {
                return ItemType.CHAOS;
            }

            if (strPrice.Contains("alch") && !strPrice.Contains("shard"))
            {
                return ItemType.ALCHEMY;
            }
            if (strPrice.Contains("alt") && !strPrice.Contains("ex"))
            {
                return ItemType.ALTERATION;
            }
            if (strPrice.Contains("ancient"))
            {
                return ItemType.ANCIENT;
            }
            if (strPrice.Contains("annulment") && !strPrice.Contains("shard"))
            {
                return ItemType.ANNULMENT;
            }
            if (strPrice.Contains("apprentice") && strPrice.Contains("sextant"))
            {
                return ItemType.APPRENTICE_SEXTANT;
            }
            if (strPrice.Contains("armour") || strPrice.Contains("scrap"))
            {
                return ItemType.ARMOUR_SCRAP;
            }
            if (strPrice.Contains("aug"))
            {
                return ItemType.AUGMENTATION;
            }
            if (strPrice.Contains("bauble"))
            {
                return ItemType.BAUBLE;
            }
            if (strPrice.Contains("bestiary") && strPrice.Contains("orb"))
            {
                return ItemType.BESTIARY_ORB;
            }
            if (strPrice.Contains("binding") && strPrice.Contains("orb"))
            {
                return ItemType.BINDING_ORB;
            }
            if (strPrice.Contains("whetstone") || strPrice.Contains("blacksmith"))
            {
                return ItemType.BLACKSMITH_WHETSTONE;
            }
            if (strPrice.Contains("blessing") && strPrice.Contains("chayulah"))
            {
                return ItemType.BLESSING_CHAYULAH;
            }
            if (strPrice.Contains("blessing") && strPrice.Contains("esh"))
            {
                return ItemType.BLESSING_ESH;
            }
            if (strPrice.Contains("blessing") && strPrice.Contains("tul"))
            {
                return ItemType.BLESSING_TUL;
            }
            if (strPrice.Contains("blessing") && strPrice.Contains("uul"))
            {
                return ItemType.BLESSING_UUL;
            }
            if (strPrice.Contains("blessing") && strPrice.Contains("xoph"))
            {
                return ItemType.BLESSING_XOPH;
            }
            if (strPrice.Contains("blesse"))
            {
                return ItemType.BLESSE;
            }
            if (strPrice.Contains("chance"))
            {
                return ItemType.CHANCE;
            }
            if (strPrice.Contains("chisel"))
            {
                return ItemType.CHISEL;
            }
            if (strPrice.Contains("chrom") || strPrice.Contains("chrome"))
            {
                return ItemType.CHROM;
            }
            if ((strPrice.Contains("divine") || strPrice.Contains("div")) && !strPrice.Contains("vessel"))
            {
                return ItemType.DIVINE;
            }
            if (strPrice.Contains("engineer") && strPrice.Contains("orb"))
            {
                return ItemType.ENGINEER;
            }
            if (strPrice.Contains("ete"))
            {
                return ItemType.ETERNAL;
            }
            if ((strPrice.Contains("ex") || strPrice.Contains("exa") || strPrice.Contains("exalted")) && !strPrice.Contains("shard") && !strPrice.Contains("sext"))
            {
                return ItemType.EXALTED;
            }
            if (strPrice.Contains("fuse") || strPrice.Contains("fus"))
            {
                return ItemType.FUSING;
            }
            if (strPrice.Contains("gcp") || strPrice.Contains("gemc"))
            {
                return ItemType.GEMCUTTERS;
            }
            if (strPrice.Contains("harbinger") && strPrice.Contains("orb"))
            {
                return ItemType.HARBINGER_ORB;
            }
            if (strPrice.Contains("horizon") && strPrice.Contains("orb"))
            {
                return ItemType.HORIZON_ORB;
            }
            if (strPrice.Contains("imprinted") && strPrice.Contains("bestiary"))
            {
                return ItemType.IMPRINTED_BESTIARY;
            }
            if (strPrice.Contains("jew"))
            {
                return ItemType.JEWELLER;
            }
            if (strPrice.Contains("journeyman") && strPrice.Contains("sextant"))
            {
                return ItemType.JOURNEYMAN_SEXTANT;
            }
            if (strPrice.Contains("master") && strPrice.Contains("sextant"))
            {
                return ItemType.MASTER_SEXTANT;
            }
            if (strPrice.Contains("mir") || strPrice.Contains("kal"))
            {
                return ItemType.MIRROR;
            }
            if (strPrice.Contains("coin"))
            {
                return ItemType.PERANDUS_COIN;
            }
            if (strPrice.Contains("port"))
            {
                return ItemType.PORTAL;
            }
            if (strPrice.Contains("rega"))
            {
                return ItemType.REGAL;
            }
            if (strPrice.Contains("regr"))
            {
                return ItemType.REGRET;
            }
            if (strPrice.Contains("dawn"))
            {
                return ItemType.SACRIFICE_DAWN;
            }
            if (strPrice.Contains("dusk"))
            {
                return ItemType.SACRIFICE_DUSK;
            }
            if (strPrice.Contains("midnight"))
            {
                return ItemType.SACRIFICE_MIDNIGHT;
            }
            if (strPrice.Contains("noon"))
            {
                return ItemType.SACRIFICE_NOON;
            }
            if (strPrice.Contains("scour"))
            {
                return ItemType.SCOUR;
            }
            if (strPrice.Contains("silver"))
            {
                return ItemType.SILVER;
            }
            if (strPrice.Contains("splinter") && strPrice.Contains("chayula"))
            {
                return ItemType.SPLINTER_CHAYULA;
            }
            if (strPrice.Contains("splinter") && strPrice.Contains("esh"))
            {
                return ItemType.SPLINTER_ESH;
            }
            if (strPrice.Contains("splinter") && strPrice.Contains("tul"))
            {
                return ItemType.SPLINTER_TUL;
            }
            if (strPrice.Contains("splinter") && strPrice.Contains("uul"))
            {
                return ItemType.SPLINTER_UUL;
            }
            if (strPrice.Contains("splinter") && strPrice.Contains("xoph"))
            {
                return ItemType.SPLINTER_XOPH;
            }
            if (strPrice.Contains("tra"))
            {
                return ItemType.TRANSMUTE;
            }
            if (strPrice.Contains("vaal"))
            {
                return ItemType.VAAL;
            }
            if (strPrice.Contains("wis"))
            {
                return ItemType.WISDOM;
            }
            if (strPrice.Contains("divine") && strPrice.Contains("vessel"))
            {
                return ItemType.DIVINE_VESSEL;
            }
            if (strPrice.Contains("offering") || strPrice.Contains("offer"))
            {
                return ItemType.OFFERING_GODDESS;
            }
            return ItemType.UNKNOWN;
        }

        private BitmapImage GetItemImage(ItemType itemAsType)
        {
            switch (itemAsType)
            {
                case ItemType.CHAOS:
                    return new BitmapImage(new Uri("pack://application:,,,/TraderForPoe.WPF;component/Resources/Currency/curr_chaos.png"));
                case ItemType.ALCHEMY:
                    return new BitmapImage(new Uri("pack://application:,,,/TraderForPoe.WPF;component/Resources/Currency/curr_alch.png"));
                case ItemType.ALTERATION:
                    return new BitmapImage(new Uri("pack://application:,,,/TraderForPoe.WPF;component/Resources/Currency/curr_alt.png"));
                case ItemType.ANCIENT:
                    return new BitmapImage(new Uri("pack://application:,,,/TraderForPoe.WPF;component/Resources/Currency/curr_ancient.png"));
                case ItemType.ANNULMENT:
                    return new BitmapImage(new Uri("pack://application:,,,/TraderForPoe.WPF;component/Resources/Currency/curr_annul.png"));
                case ItemType.APPRENTICE_SEXTANT:
                    return new BitmapImage(new Uri("pack://application:,,,/TraderForPoe.WPF;component/Resources/Currency/curr_appr_carto_sextant.png"));
                case ItemType.ARMOUR_SCRAP:
                    return new BitmapImage(new Uri("pack://application:,,,/TraderForPoe.WPF;component/Resources/Currency/curr_armour_scrap.png"));
                case ItemType.AUGMENTATION:
                    return new BitmapImage(new Uri("pack://application:,,,/TraderForPoe.WPF;component/Resources/Currency/curr_aug.png"));
                case ItemType.BAUBLE:
                    return new BitmapImage(new Uri("pack://application:,,,/TraderForPoe.WPF;component/Resources/Currency/curr_bauble.png"));
                case ItemType.BESTIARY_ORB:
                    return new BitmapImage(new Uri("pack://application:,,,/TraderForPoe.WPF;component/Resources/Currency/curr_bestiary_orb.png"));
                case ItemType.BINDING_ORB:
                    return new BitmapImage(new Uri("pack://application:,,,/TraderForPoe.WPF;component/Resources/Currency/curr_binding.png"));
                case ItemType.BLACKSMITH_WHETSTONE:
                    return new BitmapImage(new Uri("pack://application:,,,/TraderForPoe.WPF;component/Resources/Currency/curr_black_whetstone.png"));
                case ItemType.BLESSING_CHAYULAH:
                    return new BitmapImage(new Uri("pack://application:,,,/TraderForPoe.WPF;component/Resources/Currency/curr_bless_chayula.png"));
                case ItemType.BLESSING_ESH:
                    return new BitmapImage(new Uri("pack://application:,,,/TraderForPoe.WPF;component/Resources/Currency/curr_bless_chayula.png"));
                case ItemType.BLESSING_TUL:
                    return new BitmapImage(new Uri("pack://application:,,,/TraderForPoe.WPF;component/Resources/Currency/curr_bless_tul.png"));
                case ItemType.BLESSING_UUL:
                    return new BitmapImage(new Uri("pack://application:,,,/TraderForPoe.WPF;component/Resources/Currency/curr_bless_uul.png"));
                case ItemType.BLESSING_XOPH:
                    return new BitmapImage(new Uri("pack://application:,,,/TraderForPoe.WPF;component/Resources/Currency/curr_bless_xoph.png"));
                case ItemType.BLESSE:
                    return new BitmapImage(new Uri("pack://application:,,,/TraderForPoe.WPF;component/Resources/Currency/curr_blessed.png"));
                case ItemType.CHANCE:
                    return new BitmapImage(new Uri("pack://application:,,,/TraderForPoe.WPF;component/Resources/Currency/curr_chance.png"));
                case ItemType.CHISEL:
                    return new BitmapImage(new Uri("pack://application:,,,/TraderForPoe.WPF;component/Resources/Currency/curr_chisel.png"));
                case ItemType.CHROM:
                    return new BitmapImage(new Uri("pack://application:,,,/TraderForPoe.WPF;component/Resources/Currency/curr_chrom.png"));
                case ItemType.DIVINE:
                    return new BitmapImage(new Uri("pack://application:,,,/TraderForPoe.WPF;component/Resources/Currency/curr_divine.png"));
                case ItemType.ENGINEER:
                    return new BitmapImage(new Uri("pack://application:,,,/TraderForPoe.WPF;component/Resources/Currency/curr_engineer.png"));
                case ItemType.ETERNAL:
                    return new BitmapImage(new Uri("pack://application:,,,/TraderForPoe.WPF;component/Resources/Currency/curr_eternal.png"));
                case ItemType.EXALTED:
                    return new BitmapImage(new Uri("pack://application:,,,/TraderForPoe.WPF;component/Resources/Currency/curr_ex.png"));
                case ItemType.FUSING:
                    return new BitmapImage(new Uri("pack://application:,,,/TraderForPoe.WPF;component/Resources/Currency/curr_fuse.png"));
                case ItemType.GEMCUTTERS:
                    return new BitmapImage(new Uri("pack://application:,,,/TraderForPoe.WPF;component/Resources/Currency/curr_gcp.png"));
                case ItemType.HARBINGER_ORB:
                    return new BitmapImage(new Uri("pack://application:,,,/TraderForPoe.WPF;component/Resources/Currency/curr_harbinger.png"));
                case ItemType.HORIZON_ORB:
                    return new BitmapImage(new Uri("pack://application:,,,/TraderForPoe.WPF;component/Resources/Currency/curr_horizon.png"));
                case ItemType.IMPRINTED_BESTIARY:
                    return new BitmapImage(new Uri("pack://application:,,,/TraderForPoe.WPF;component/Resources/Currency/curr_impr_bestiary.png"));
                case ItemType.JEWELLER:
                    return new BitmapImage(new Uri("pack://application:,,,/TraderForPoe.WPF;component/Resources/Currency/curr_jew.png"));
                case ItemType.JOURNEYMAN_SEXTANT:
                    return new BitmapImage(new Uri("pack://application:,,,/TraderForPoe.WPF;component/Resources/Currency/curr_journ_carto_sextant.png"));
                case ItemType.MASTER_SEXTANT:
                    return new BitmapImage(new Uri("pack://application:,,,/TraderForPoe.WPF;component/Resources/Currency/curr_master_carto_sextant.png"));
                case ItemType.MIRROR:
                    return new BitmapImage(new Uri("pack://application:,,,/TraderForPoe.WPF;component/Resources/Currency/curr_mirror.png"));
                case ItemType.PERANDUS_COIN:
                    return new BitmapImage(new Uri("pack://application:,,,/TraderForPoe.WPF;component/Resources/Currency/curr_perandus_coin.png"));
                case ItemType.PORTAL:
                    return new BitmapImage(new Uri("pack://application:,,,/TraderForPoe.WPF;component/Resources/Currency/curr_port.png"));
                case ItemType.REGAL:
                    return new BitmapImage(new Uri("pack://application:,,,/TraderForPoe.WPF;component/Resources/Currency/curr_regal.png"));
                case ItemType.REGRET:
                    return new BitmapImage(new Uri("pack://application:,,,/TraderForPoe.WPF;component/Resources/Currency/curr_regret.png"));
                case ItemType.SACRIFICE_DAWN:
                    return new BitmapImage(new Uri("pack://application:,,,/TraderForPoe.WPF;component/Resources/Currency/curr_sacrifice_dawn.png"));
                case ItemType.SACRIFICE_DUSK:
                    return new BitmapImage(new Uri("pack://application:,,,/TraderForPoe.WPF;component/Resources/Currency/curr_sacrifice_dusk.png"));
                case ItemType.SACRIFICE_MIDNIGHT:
                    return new BitmapImage(new Uri("pack://application:,,,/TraderForPoe.WPF;component/Resources/Currency/curr_sacrifice_midnight.png"));
                case ItemType.SACRIFICE_NOON:
                    return new BitmapImage(new Uri("pack://application:,,,/TraderForPoe.WPF;component/Resources/Currency/curr_sacrifice_noon.png"));
                case ItemType.SCOUR:
                    return new BitmapImage(new Uri("pack://application:,,,/TraderForPoe.WPF;component/Resources/Currency/curr_scour.png"));
                case ItemType.SILVER:
                    return new BitmapImage(new Uri("pack://application:,,,/TraderForPoe.WPF;component/Resources/Currency/curr_silver.png"));
                case ItemType.SPLINTER_CHAYULA:
                    return new BitmapImage(new Uri("pack://application:,,,/TraderForPoe.WPF;component/Resources/Currency/curr_splinter_chayula.png"));
                case ItemType.SPLINTER_ESH:
                    return new BitmapImage(new Uri("pack://application:,,,/TraderForPoe.WPF;component/Resources/Currency/curr_splinter_esh.png"));
                case ItemType.SPLINTER_TUL:
                    return new BitmapImage(new Uri("pack://application:,,,/TraderForPoe.WPF;component/Resources/Currency/curr_splinter_tul.png"));
                case ItemType.SPLINTER_UUL:
                    return new BitmapImage(new Uri("pack://application:,,,/TraderForPoe.WPF;component/Resources/Currency/curr_splinter_uul.png"));
                case ItemType.SPLINTER_XOPH:
                    return new BitmapImage(new Uri("pack://application:,,,/TraderForPoe.WPF;component/Resources/Currency/curr_splinter_xoph.png"));
                case ItemType.TRANSMUTE:
                    return new BitmapImage(new Uri("pack://application:,,,/TraderForPoe.WPF;component/Resources/Currency/curr_tra.png"));
                case ItemType.VAAL:
                    return new BitmapImage(new Uri("pack://application:,,,/TraderForPoe.WPF;component/Resources/Currency/curr_vaal.png"));
                case ItemType.WISDOM:
                    return new BitmapImage(new Uri("pack://application:,,,/TraderForPoe.WPF;component/Resources/Currency/curr_wis.png"));
                case ItemType.DIVINE_VESSEL:
                    return new BitmapImage(new Uri("pack://application:,,,/TraderForPoe.WPF;component/Resources/Currency/curr_divine_vessel.png"));
                case ItemType.OFFERING_GODDESS:
                    return new BitmapImage(new Uri("pack://application:,,,/TraderForPoe.WPF;component/Resources/Currency/curr_offering_to_the_goddess.png"));
                case ItemType.UNKNOWN:
                    return null;
                default:
                    return null;
            }
        }

        public string ItemAsString { get; }

        public decimal Amount { get; }

        public ItemType ItemAsType { get; } = ItemType.UNKNOWN;

        public BitmapImage Image { get; }
    }
}
