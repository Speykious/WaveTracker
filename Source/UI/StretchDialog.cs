﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WaveTracker.UI {
    public class StretchDialog : Dialog {
        PatternEditor parentEditor;
        Button cancel, ok;
        Textbox stretchText;
        int[] stretchPattern;
        public StretchDialog() : base("Stretch selection", 260, 62) {
            PositionInCenterOfScreen();
            cancel = AddNewBottomButton("Cancel", this);
            ok = AddNewBottomButton("OK", this);
            stretchText = new Textbox("Stretch map: ", 6, 12, 200, this);
        }

        public void Open(PatternEditor parentEditor) {
            this.parentEditor = parentEditor;
            stretchText.Text = "1";
            stretchPattern = [1];
            Open();
        }

        public override void Update() {
            if (WindowIsOpen) {
                DoDragging();
                if (cancel.Clicked || ExitButton.Clicked)
                    Close();

                if (ok.Clicked) {
                    parentEditor.StretchSelection(stretchText.Text);
                    Close();
                }
                stretchText.Update();
                if (stretchText.ValueWasChangedInternally) {
                    stretchText.Text = Helpers.FlushString(stretchText.Text, " 0123456789");
                    stretchPattern = StretchPatternToIntArray(stretchText.Text);
                    //stretchText.Text = "";
                    //for (int i = 0; i < stretchPattern.Length; ++i) {
                    //    stretchText.Text += i + (i < stretchPattern.Length - 1 ? " " : "");
                    //}
                }
            }
        }
        int[] StretchPatternToIntArray(string text) {
            List<int> ticks = new List<int>();
            foreach (string word in text.Split(' ')) {
                if (word.IsNumeric()) {
                    if (int.TryParse(word, out int val))
                        ticks.Add(val);
                }
            }
            if (ticks.Count == 0)
                ticks.Add(1);
            return ticks.ToArray();
        }

        public new void Draw() {
            if (WindowIsOpen) {
                base.Draw();
                stretchText.Draw();
                int[] testText = new int[16];

                int r = 0;
                int index = 0;
                string testString = "";
                for (int i = 0; i < 16; ++i) {
                    if (stretchPattern[index] == 0) {
                        testString += "- ";
                    }
                    else {
                        testString += r + " ";
                    }
                    r += stretchPattern[index];
                    index++;
                    if (index >= stretchPattern.Length) {
                        index = 0;
                    }
                }
                Write(Helpers.TrimTextToWidth(width - 12, "Test: " + testString), 6, stretchText.BoundsBottom + 2, UIColors.labelLight);
            }
        }
    }
}