﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Xml.Serialization;

namespace WaveTracker {
    public class Colors {
        public static ColorTheme theme = ColorTheme.Default;
    }

    public class ColorTheme {
        [XmlElement(ElementName = "patternText")]
        public Color patternText;
        [XmlElement(ElementName = "patternTextHighlighted")]
        public Color patternTextHighlighted;
        [XmlElement(ElementName = "patternTextSubHighlight")]
        public Color patternTextSubHighlight;
        [XmlElement(ElementName = "patternEmptyTextAlpha")]
        public int patternEmptyTextAlpha;

        [XmlElement(ElementName = "instrumentColumnWave")]
        public Color instrumentColumnWave;
        [XmlElement(ElementName = "instrumentColumnSample")]
        public Color instrumentColumnSample;
        [XmlElement(ElementName = "volumeColumn")]
        public Color volumeColumn;
        [XmlElement(ElementName = "effectColumn")]
        public Color effectColumn;
        [XmlElement(ElementName = "effectColumnParameter")]
        public Color effectColumnParameter;

        [XmlElement(ElementName = "backgroundHighlighted")]
        public Color backgroundHighlighted;
        [XmlElement(ElementName = "backgroundSubHighlight")]
        public Color backgroundSubHighlight;
        [XmlElement(ElementName = "background")]
        public Color background;

        [XmlElement(ElementName = "rowPlaybackColor")]
        public Color rowPlaybackColor;
        [XmlElement(ElementName = "rowPlaybackText")]
        public Color rowPlaybackText;
        [XmlElement(ElementName = "rowEditColor")]
        public Color rowEditColor;
        [XmlElement(ElementName = "rowEditText")]
        public Color rowEditText;
        [XmlElement(ElementName = "rowCurrentColor")]
        public Color rowCurrentColor;
        [XmlElement(ElementName = "rowCurrentText")]
        public Color rowCurrentText;
        [XmlElement(ElementName = "cursor")]
        public Color cursor;
        [XmlElement(ElementName = "rowSeparator")]
        public Color rowSeparator;
        [XmlElement(ElementName = "selection")]
        public Color selection;

        static Color AddBrightness(Color color, float amt) {
            return Helpers.LerpColor(color, Color.White, amt);
        }
        public static ColorTheme Default {

            get {
                ColorTheme ret = new ColorTheme();

                ret.background = Helpers.HexCodeToColor("14182e");
                ret.backgroundHighlighted = new(33, 40, 64);
                ret.backgroundSubHighlight = new(26, 31, 54);

                ret.patternText = Color.White;
                ret.patternTextHighlighted = new(202, 245, 254);
                ret.patternTextSubHighlight = new(187, 215, 254);
                ret.patternEmptyTextAlpha = 18;

                ret.instrumentColumnWave = new(90, 234, 61);
                ret.instrumentColumnSample = new(255, 153, 50);
                ret.volumeColumn = new(80, 233, 230);
                ret.effectColumn = new(255, 82, 119);
                ret.effectColumnParameter = new(255, 208, 208);

                ret.selection = new(128, 128, 255, 150);
                ret.cursor = new(126, 133, 168);

                ret.rowCurrentColor = new(27, 55, 130);
                ret.rowCurrentText = new(42, 83, 156);

                ret.rowEditColor = new(109, 29, 78);
                ret.rowEditText = new(162, 39, 107);

                ret.rowPlaybackColor = new(42, 29, 81);
                ret.rowPlaybackText = new(60, 37, 105);

                ret.rowSeparator = new(49, 56, 89);

                return ret;
            }
        }
        public static ColorTheme Famitracker {

            get {
                ColorTheme ret = new ColorTheme();

                ret.background = new(0, 0, 0);
                ret.backgroundHighlighted = new(16, 16, 0);
                ret.backgroundSubHighlight = new(32, 32, 0);

                ret.patternText = new(0, 255, 0);
                ret.patternTextHighlighted = new(240, 240, 0);
                ret.patternTextSubHighlight = new(255, 255, 96);
                ret.patternEmptyTextAlpha = 50;

                ret.instrumentColumnWave = new(128, 255, 128);
                ret.instrumentColumnSample = new(128, 255, 128);
                ret.volumeColumn = new(128, 128, 255);
                ret.effectColumn = new(255, 128, 128);
                ret.effectColumnParameter = ret.patternText;

                ret.selection = new(134, 125, 242, 150);
                ret.cursor = new(128, 128, 128);

                ret.rowCurrentColor = new(48, 32, 160);
                ret.rowCurrentText = AddBrightness(ret.rowCurrentColor, 0.17f);

                ret.rowEditColor = new(128, 32, 48);
                ret.rowEditText = AddBrightness(ret.rowEditColor, 0.17f);

                ret.rowPlaybackColor = new(80, 0, 64);
                ret.rowPlaybackText = AddBrightness(ret.rowPlaybackColor, 0.17f);

                ret.rowSeparator = new(60, 60, 60);

                return ret;
            }
        }

        public static ColorTheme OpenMPT {

            get {
                ColorTheme ret = new ColorTheme();

                ret.background = Helpers.HexCodeToColor("ffffff");
                ret.backgroundHighlighted = Helpers.HexCodeToColor("f2f6f2");
                ret.backgroundSubHighlight = Helpers.HexCodeToColor("e0e8e0");

                ret.patternText = Helpers.HexCodeToColor("000080");
                ret.patternTextHighlighted = Helpers.HexCodeToColor("000080");
                ret.patternTextSubHighlight = Helpers.HexCodeToColor("000080");
                ret.patternEmptyTextAlpha = 255;

                ret.instrumentColumnWave = Helpers.HexCodeToColor("008080");
                ret.instrumentColumnSample = Helpers.HexCodeToColor("008080");
                ret.volumeColumn = Helpers.HexCodeToColor("008000");
                ret.effectColumn = Helpers.HexCodeToColor("800000");
                ret.effectColumnParameter = ret.effectColumn;

                ret.selection = Helpers.HexCodeToColor("8080ff");
                ret.cursor = Helpers.HexCodeToColor("8080ff");

                ret.rowCurrentColor = Helpers.HexCodeToColor("c0c0c0");
                ret.rowCurrentText = Helpers.HexCodeToColor("000000");

                ret.rowEditColor = Helpers.HexCodeToColor("c0c0c0");
                ret.rowEditText = Helpers.HexCodeToColor("000000");

                ret.rowPlaybackColor = Helpers.HexCodeToColor("ffff80");
                ret.rowPlaybackText = Helpers.HexCodeToColor("000000");

                ret.rowSeparator = Helpers.HexCodeToColor("a0a0a0");

                return ret;
            }
        }
        public static ColorTheme BambooTracker {

            get {
                ColorTheme ret = new ColorTheme();

                ret.background = Helpers.HexCodeToColor("000228");
                ret.backgroundHighlighted = Helpers.HexCodeToColor("1b2750");
                ret.backgroundSubHighlight = Helpers.HexCodeToColor("1f327f");

                ret.patternText = Helpers.HexCodeToColor("d29672");
                ret.patternTextHighlighted = Helpers.HexCodeToColor("e7691b");
                ret.patternTextSubHighlight = Helpers.HexCodeToColor("e7691b");
                ret.patternEmptyTextAlpha = 60;

                ret.instrumentColumnWave = Helpers.HexCodeToColor("2bcbbe");
                ret.instrumentColumnSample = Helpers.HexCodeToColor("2bcbbe");
                ret.volumeColumn = Helpers.HexCodeToColor("e78504");
                ret.effectColumn = Helpers.HexCodeToColor("27c792");
                ret.effectColumnParameter = ret.effectColumn;

                ret.selection = Helpers.HexCodeToColor("0000ff80");
                ret.cursor = Helpers.HexCodeToColor("88a8d3");

                ret.rowCurrentColor = Helpers.HexCodeToColor("1152a8");
                ret.rowCurrentText = Helpers.HexCodeToColor("ffffff");

                ret.rowEditColor = Helpers.HexCodeToColor("a84b4c");
                ret.rowEditText = Helpers.HexCodeToColor("ffffff");

                ret.rowPlaybackColor = Helpers.HexCodeToColor("436998");
                ret.rowPlaybackText = Helpers.HexCodeToColor("b4b4b4");

                ret.rowSeparator = Helpers.HexCodeToColor("000000");

                return ret;
            }
        }

    }

    public class UIColors {
        /// <summary>
        /// (20, 24, 46)
        /// </summary>
        public static readonly Color black = new(20, 24, 46);
        /// <summary>
        /// (222, 223, 231)
        /// </summary>
        public static readonly Color panel = new(222, 223, 231);
        /// <summary>
        /// (43, 49, 81)
        /// </summary>
        public static readonly Color panelTitle = new(43, 49, 81);
        /// <summary>
        /// (64, 73, 115)
        /// </summary>
        public static readonly Color labelDark = new(64, 73, 115);
        /// <summary>
        /// (104, 111, 153)
        /// </summary>
        public static readonly Color label = new(104, 111, 153);
        /// <summary>
        /// (163, 167, 194)
        /// </summary>
        public static readonly Color labelLight = new(163, 167, 194);
        /// <summary>
        /// (8, 124, 232)
        /// </summary>
        public static readonly Color selection = new(8, 124, 232);
        /// <summary>
        /// (8, 124, 232)
        /// </summary>
        public static readonly Color selectionLight = new(180, 215, 248);

    }
}