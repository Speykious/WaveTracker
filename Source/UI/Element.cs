﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using WaveTracker.Rendering;

namespace WaveTracker.UI {
    public abstract class Element {
        public int x, y;
        public Element parent;
        protected int MouseX { get { return Input.MousePositionX - (x + OffX); } }
        protected int MouseY { get { return Input.MousePositionY - (y + OffY); } }

        protected int OffX { get { if (parent == null) return 0; return parent.x + parent.OffX; } }
        protected int OffY { get { if (parent == null) return 0; return parent.y + parent.OffY; } }

        protected int GlobalX { get { return x + OffX; } }
        protected int GlobalY { get { return y + OffY; } }

        public bool InFocus {
            get {
                if (Input.focus == null) return true;
                if (parent == null) return Input.focus == this;
                if (parent.InFocus)
                    return true;
                return Input.focus == this;
            }
        }

        public void SetParent(Element parent) {
            this.parent = parent;
        }
        protected void Write(string text, int x, int y, Color color) {
            Graphics.Write(text, this.x + x + OffX, this.y + y + OffY, color);
        }

        protected void WriteMultiline(string text, int x, int y, int width, Color color, int lineSpacing = 10) {
            string str = "";
            string[] words = text.Split(' ');
            int w = 0;
            foreach (string word in words) {
                w += Helpers.GetWidthOfText(word + " ");
                if (w > width) {
                    str += "\n" + word + " ";
                    w = Helpers.GetWidthOfText(word + " ");
                }
                else {
                    str += word + " ";
                }
            }
            string[] lines = str.Split('\n');
            foreach (string line in lines) {
                Write(line, x, y, color);
                y += lineSpacing;
            }
        }
        protected void WriteTwiceAsBig(string text, int x, int y, Color c) {
            Graphics.WriteTwiceAsBig(text, this.x + x + OffX, this.y + y + OffY, c);
        }

        protected void WriteRightAlign(string text, int x, int y, Color color) {
            Graphics.WriteRightJustified(text, this.x + x + OffX, this.y + y + OffY, color);
        }

        protected void WriteCenter(string text, int x, int y, Color color) {
            Write(text, x - Helpers.GetWidthOfText(text) / 2, y, color);
        }

        protected void WriteMonospaced(string text, int x, int y, Color color, int width = 4) {
            Graphics.WriteMonospaced(text, this.x + x + OffX, this.y + y + OffY, color, width);
        }

        protected void DrawRect(int x, int y, int width, int height, Color color) {
            Graphics.DrawRect(this.x + x + OffX, this.y + y + OffY, width, height, color);
        }

        protected void DrawRoundedRect(int x, int y, int width, int height, Color color) {
            Graphics.DrawRect(this.x + x + OffX, this.y + y + OffY + 1, width, height - 2, color);
            Graphics.DrawRect(this.x + x + OffX + 1, this.y + y + OffY, width - 2, height, color);
        }

        protected void DrawSprite(int x, int y, Rectangle bounds) {
            Graphics.DrawSprite(this.x + x + OffX, this.y + y + OffY, bounds);
        }
        protected void DrawSprite(int x, int y, int width, int height, Rectangle spriteBounds) {
            Graphics.DrawSprite(this.x + x + OffX, this.y + y + OffY, width, height, spriteBounds);
        }
        protected void DrawSprite(int x, int y, int width, int height, Rectangle spriteBounds, Color col) {
            Graphics.DrawSprite(this.x + x + OffX, this.y + y + OffY, width, height, spriteBounds, col);
        }

        public Point GlobalPointToLocalPoint(Point p) {
            return new Point(p.X - GlobalX, p.Y - GlobalY);
        }

        public Point LastClickPos => GlobalPointToLocalPoint(Input.lastClickLocation);
    }
}