﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Input;
using WaveTracker.UI;
using WaveTracker.Tracker;
using System.Windows.Forms;

namespace WaveTracker.UI {
    public class WaveEditor : Window {
        public static bool enabled;
        public SpriteButton presetSine, presetTria, presetSaw, presetRect50, presetRect25, presetRect12, presetRand, presetClear;
        public Toggle filterNone, filterLinear, filterMix;
        public int startcooldown;
        //public SpriteButton closeButton;
        public UI.Button bCopy, bPaste, bPhaseL, bPhaseR, bMoveUp, bMoveDown, bInvert, bMutate, bSmooth, bNormalize;
        public UI.Textbox waveText;
        public Dropdown ResampleDropdown;
        public int id;
        int holdPosY, holdPosX;
        static string clipboardWave = "";
        static Audio.ResamplingMode clipboardSampleMode = Audio.ResamplingMode.Mix;
        int phase;
        public WaveEditor() : base("Wave Editor", 500, 270) {
            x = 220;
            y = 130;

            int buttonY = 23;
            int buttonX = 420;
            int buttonWidth = 64;


            bCopy = new UI.Button("Copy", buttonX, buttonY, this);
            bCopy.width = buttonWidth / 2 - 1;
            bCopy.SetTooltip("", "Copy wave settings");
            buttonY += 0;
            bPaste = new UI.Button("Paste", buttonX + 32, buttonY, this);
            bPaste.width = buttonWidth / 2 - 1;
            bPaste.SetTooltip("", "Paste wave settings");
            buttonY += 18;

            bPhaseR = new UI.Button("Phase »", buttonX, buttonY, this);
            bPhaseR.width = buttonWidth;
            bPhaseR.SetTooltip("", "Shift phase once to the right");
            buttonY += 14;
            bPhaseL = new UI.Button("Phase «", buttonX, buttonY, this);
            bPhaseL.width = buttonWidth;
            bPhaseL.SetTooltip("", "Shift phase once to the left");
            buttonY += 14;
            bMoveUp = new UI.Button("Shift Up", buttonX, buttonY, this);
            bMoveUp.width = buttonWidth;
            bMoveUp.SetTooltip("", "Raise the wave 1 step up");
            buttonY += 14;
            bMoveDown = new UI.Button("Shift Down", buttonX, buttonY, this);
            bMoveDown.width = buttonWidth;
            bMoveDown.SetTooltip("", "Lower the wave 1 step down");
            buttonY += 18;

            bInvert = new UI.Button("Invert", buttonX, buttonY, this);
            bInvert.width = buttonWidth;
            bInvert.SetTooltip("", "Invert the wave vertically");
            buttonY += 14;
            bSmooth = new UI.Button("Smooth", buttonX, buttonY, this);
            bSmooth.width = buttonWidth;
            bSmooth.SetTooltip("", "Smooth out rough corners in the wave");
            buttonY += 14;
            bMutate = new UI.Button("Mutate", buttonX, buttonY, this);
            bMutate.width = buttonWidth;
            bMutate.SetTooltip("", "Slightly randomize the wave");
            buttonY += 14;
            bNormalize = new UI.Button("Normalize", buttonX, buttonY, this);
            bNormalize.width = buttonWidth;
            bNormalize.SetTooltip("", "Make the wave maximum amplitude");


            presetSine = new SpriteButton(17, 215, 18, 12, Rendering.Graphics.img, 104, 80, this);
            presetSine.SetTooltip("Sine", "Sine wave preset");
            presetTria = new SpriteButton(36, 215, 18, 12, Rendering.Graphics.img, 122, 80, this);
            presetTria.SetTooltip("Triangle", "Triangle wave preset");
            presetSaw = new SpriteButton(55, 215, 18, 12, Rendering.Graphics.img, 140, 80, this);
            presetSaw.SetTooltip("Sawtooth", "Sawtooth wave preset");

            presetRect50 = new SpriteButton(74, 215, 18, 12, Rendering.Graphics.img, 158, 80, this);
            presetRect50.SetTooltip("Pulse 50%", "Pulse wave preset with 50% duty cycle");
            presetRect25 = new SpriteButton(93, 215, 18, 12, Rendering.Graphics.img, 176, 80, this);
            presetRect25.SetTooltip("Pulse 25%", "Pulse wave preset with 25% duty cycle");
            presetRect12 = new SpriteButton(112, 215, 18, 12, Rendering.Graphics.img, 194, 80, this);
            presetRect12.SetTooltip("Pulse 12.5%", "Pulse wave preset with 12.5% duty cycle");

            presetRand = new SpriteButton(131, 215, 18, 12, Rendering.Graphics.img, 212, 80, this);
            presetRand.SetTooltip("Random", "Create random noise");
            presetClear = new SpriteButton(150, 215, 18, 12, Rendering.Graphics.img, 230, 80, this);
            presetClear.SetTooltip("Clear", "Clear wave");

            //closeButton = new SpriteButton(490, 0, 10, 9, UI.NumberBox.buttons, 4, this);
            ExitButton.SetTooltip("Close", "Close wave editor");

            waveText = new UI.Textbox("", 17, 188, 384, 384, this);
            waveText.canEdit = true;
            waveText.maxLength = 192;

            ResampleDropdown = new Dropdown(385, 215, this);
            ResampleDropdown.SetMenuItems(new string[] { "Harsh (None)", "Smooth (Linear)", "Mix (None + Linear)" });
        }
        public void Open(int waveIndex) {
            base.Open();
            //Input.internalDialogIsOpen = true;
            startcooldown = 10;
            id = waveIndex;
            enabled = true;
            //Input.focus = this;
        }

        public new void Close() {
            enabled = false;
            base.Close();
            //Input.internalDialogIsOpen = false;
            //Input.focus = null;
        }

        public int GetPianoMouseInput() {
            if (!enabled || !InFocus)
                return -1;
            if (MouseX < 10 || MouseX > 488 || MouseY > 258 || MouseY < 235)
                return -1;
            if (!Input.GetClick(KeyModifier.None))
                return -1;
            else {
                return ((MouseX + 38) / 4);
            }
        }

        bool IsMouseInCanvasBounds() {
            return InFocus && CanvasMouseX > 0 && CanvasMouseY > 0 && CanvasMouseX < 384 && CanvasMouseY < 160;
        }
        int CanvasMouseX => MouseX - 17;
        int CanvasMouseY => MouseY - 23;
        int CanvasPosX => CanvasMouseX / 6;
        int CanvasPosY => 31 - (CanvasMouseY / 5);

        public void Update() {
            if (windowIsOpen) {
                DoDragging();
                if (WaveBank.currentWave < 0) return;
                if (WaveBank.currentWave > 99) return;
                if (Input.GetKeyRepeat(Microsoft.Xna.Framework.Input.Keys.Left, KeyModifier.None)) {
                    WaveBank.currentWave--;
                    if (WaveBank.currentWave < 0) {
                        WaveBank.currentWave += 100;
                    }
                    id = WaveBank.currentWave;
                }
                if (Input.GetKeyRepeat(Microsoft.Xna.Framework.Input.Keys.Right, KeyModifier.None)) {
                    WaveBank.currentWave++;
                    if (WaveBank.currentWave >= 100) {
                        WaveBank.currentWave -= 100;
                    }
                    id = WaveBank.currentWave;

                }
                WaveBank.lastSelectedWave = id;


                phase++;

                ResampleDropdown.Value = (int)App.CurrentModule.WaveBank[WaveBank.currentWave].resamplingMode;
                if (startcooldown > 0) {
                    waveText.Text = App.CurrentModule.WaveBank[WaveBank.currentWave].ToNumberString();
                    startcooldown--;
                }
                else {

                    if (ExitButton.Clicked || Input.GetKeyDown(Microsoft.Xna.Framework.Input.Keys.Escape, KeyModifier.None)) {
                        Close();
                    }
                    waveText.Update();
                    if (waveText.ValueWasChangedInternally) {
                        App.CurrentModule.SetDirty();
                    }
                    if (waveText.ValueWasChanged) {
                        App.CurrentModule.WaveBank[WaveBank.currentWave].SetWaveformFromNumber(waveText.Text);
                    }
                    else {
                        waveText.Text = App.CurrentModule.WaveBank[WaveBank.currentWave].ToNumberString();
                    }

                    ResampleDropdown.Update();
                    if (ResampleDropdown.ValueWasChangedInternally) {
                        App.CurrentModule.SetDirty();
                    }
                    App.CurrentModule.WaveBank[WaveBank.currentWave].resamplingMode = (Audio.ResamplingMode)ResampleDropdown.Value;
                    //if (filterNone.Clicked)
                    //    App.CurrentModule.WaveBank[WaveBank.currentWave].resamplingMode = Audio.ResamplingModes.None;
                    //if (filterLinear.Clicked)
                    //    App.CurrentModule.WaveBank[WaveBank.currentWave].resamplingMode = Audio.ResamplingModes.Linear;
                    //if (filterMix.Clicked)
                    //    App.CurrentModule.WaveBank[WaveBank.currentWave].resamplingMode = Audio.ResamplingModes.Mix;

                    if (presetSine.Clicked) {
                        App.CurrentModule.WaveBank[WaveBank.currentWave].SetWaveformFromString("HJKMNOQRSTUUVVVVVVVUUTSRQONMKJHGECB9875432110000000112345789BCEF");
                        App.CurrentModule.SetDirty();
                    }
                    if (presetTria.Clicked) {
                        App.CurrentModule.WaveBank[WaveBank.currentWave].SetWaveformFromString("GHIJKLMNOPQRSTUVVUTSRQPONMLKJIHGFEDCBA98765432100123456789ABCDEF");
                        App.CurrentModule.SetDirty();
                    }
                    if (presetSaw.Clicked) {
                        App.CurrentModule.WaveBank[WaveBank.currentWave].SetWaveformFromString("GGHHIIJJKKLLMMNNOOPPQQRRSSTTUUVV00112233445566778899AABBCCDDEEFF");
                        App.CurrentModule.SetDirty();
                    }
                    if (presetRect50.Clicked) {
                        App.CurrentModule.WaveBank[WaveBank.currentWave].SetWaveformFromString("VVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVV00000000000000000000000000000000");
                        App.CurrentModule.SetDirty();
                    }
                    if (presetRect25.Clicked) {
                        App.CurrentModule.WaveBank[WaveBank.currentWave].SetWaveformFromString("VVVVVVVVVVVVVVVV000000000000000000000000000000000000000000000000");
                        App.CurrentModule.SetDirty();
                    }
                    if (presetRect12.Clicked) {
                        App.CurrentModule.WaveBank[WaveBank.currentWave].SetWaveformFromString("VVVVVVVV00000000000000000000000000000000000000000000000000000000");
                        App.CurrentModule.SetDirty();
                    }
                    if (presetRand.Clicked) {
                        App.CurrentModule.WaveBank[WaveBank.currentWave].Randomize();
                        App.CurrentModule.SetDirty();
                    }
                    if (presetClear.Clicked) {
                        App.CurrentModule.WaveBank[WaveBank.currentWave].SetWaveformFromString("GGGGGGGGGGGGGGGGGGGGGGGGGGGGGGGGGGGGGGGGGGGGGGGGGGGGGGGGGGGGGGGG");
                        App.CurrentModule.SetDirty();
                    }
                    if (bCopy.Clicked) {
                        clipboardWave = App.CurrentModule.WaveBank[WaveBank.currentWave].ToString();
                        clipboardSampleMode = App.CurrentModule.WaveBank[WaveBank.currentWave].resamplingMode;
                    }
                    if (bPaste.Clicked) {
                        if (clipboardWave.Length == 64) {
                            App.CurrentModule.WaveBank[WaveBank.currentWave].SetWaveformFromString(clipboardWave);
                            App.CurrentModule.WaveBank[WaveBank.currentWave].resamplingMode = clipboardSampleMode;
                            App.CurrentModule.SetDirty();
                        }
                    }

                    if (bPhaseL.Clicked) {
                        App.CurrentModule.WaveBank[WaveBank.currentWave].ShiftPhase(1);
                        App.CurrentModule.SetDirty();
                    }
                    if (bPhaseR.Clicked) {
                        App.CurrentModule.WaveBank[WaveBank.currentWave].ShiftPhase(-1);
                        App.CurrentModule.SetDirty();
                    }
                    if (bMoveUp.Clicked) {
                        App.CurrentModule.WaveBank[WaveBank.currentWave].Move(1);
                        App.CurrentModule.SetDirty();
                    }
                    if (bMoveDown.Clicked) {
                        App.CurrentModule.WaveBank[WaveBank.currentWave].Move(-1);
                        App.CurrentModule.SetDirty();
                    }
                    if (bInvert.Clicked) {
                        App.CurrentModule.WaveBank[WaveBank.currentWave].Invert();
                        App.CurrentModule.SetDirty();
                    }
                    if (bSmooth.Clicked) {
                        App.CurrentModule.WaveBank[WaveBank.currentWave].Smooth(2);
                        App.CurrentModule.SetDirty();
                    }
                    if (bMutate.Clicked) {
                        App.CurrentModule.WaveBank[WaveBank.currentWave].Mutate();
                        App.CurrentModule.SetDirty();
                    }
                    if (bNormalize.Clicked) {
                        App.CurrentModule.WaveBank[WaveBank.currentWave].Normalize();
                        App.CurrentModule.SetDirty();
                    }
                    if (IsMouseInCanvasBounds()) {
                        if (Input.GetClickDown(KeyModifier._Any)) {
                            holdPosX = CanvasPosX;
                            holdPosY = CanvasPosY;
                        }


                        if (Input.GetClick(KeyModifier.None)) {
                            App.CurrentModule.WaveBank[WaveBank.currentWave].samples[CanvasPosX] = (byte)CanvasPosY;
                            App.CurrentModule.SetDirty();
                        }
                        if (Input.GetClickUp(KeyModifier.Shift)) {
                            int diff = Math.Abs(holdPosX - CanvasPosX);
                            if (diff > 0) {
                                if (holdPosX < CanvasPosX) {
                                    for (int i = holdPosX; i <= CanvasPosX; ++i) {
                                        App.CurrentModule.WaveBank[WaveBank.currentWave].samples[i] = (byte)Math.Round(MathHelper.Lerp(holdPosY, CanvasPosY, (float)(i - holdPosX) / diff));
                                    }
                                }
                                else {
                                    for (int i = CanvasPosX; i <= holdPosX; ++i) {
                                        App.CurrentModule.WaveBank[WaveBank.currentWave].samples[i] = (byte)Math.Round(MathHelper.Lerp(CanvasPosY, holdPosY, (float)(i - CanvasPosX) / diff));
                                    }
                                }
                                App.CurrentModule.SetDirty();
                            }
                            else {
                                App.CurrentModule.WaveBank[WaveBank.currentWave].samples[CanvasPosX] = (byte)CanvasPosY;
                                App.CurrentModule.SetDirty();
                            }
                        }
                    }
                }
            }
        }

        void DrawPiano(int x, int y) {
            DrawRect(x, y, 482, 26, UIColors.black);
            for (int i = 0; i < 10; ++i) {
                // draw 1 octave of the piano sprite
                DrawSprite(x + i * 48 + 1, y + 1, new Rectangle(0, 80, 48, 24));
            }
        }

        public new void Draw() {
            if (windowIsOpen) {
                name = "Edit Wave " + id.ToString("D2");
                base.Draw();
                Write("Draw Wave", 17, 14, UIColors.label);
                DrawSprite(16, 22, new Rectangle(0, 144, 386, 162));
                //DrawRect(-x, -y, 960, 600, Helpers.Alpha(Color.Black, 90));
                //DrawSprite( 0, 0, new Rectangle(0, 60, 500, 270));
                //Write("Edit Wave " + id.ToString("D2"), 4, 1, new Color(64, 72, 115));

                Write("Presets", 17, 205, UIColors.label);
                presetSine.Draw();
                presetTria.Draw();
                presetRect50.Draw();
                presetSaw.Draw();
                presetRect25.Draw();
                presetRect12.Draw();
                presetClear.Draw();
                presetRand.Draw();
                //filterNone.Draw();
                //filterLinear.Draw();
                //filterMix.Draw();
                waveText.Draw();

                Write("Tools", 441, 14, UIColors.label);
                bCopy.Draw();
                bPaste.Draw();
                bPhaseL.Draw();
                bPhaseR.Draw();
                bMoveUp.Draw();
                bMoveDown.Draw();
                bInvert.Draw();
                bSmooth.Draw();
                bMutate.Draw();
                bNormalize.Draw();

                Color waveColor = new Color(200, 212, 93);
                Color waveBG = new Color(59, 125, 79, 150);
                for (int i = 0; i < 64; ++i) {
                    int samp = App.CurrentModule.WaveBank[WaveBank.currentWave].getSample(i);
                    int samp2 = App.CurrentModule.WaveBank[WaveBank.currentWave].getSample(i + phase);

                    DrawRect(17 + i * 6, 102, 6, -5 * (samp - 16), waveBG);
                    DrawRect(17 + i * 6, 183 - samp * 5, 6, -5, waveColor);
                    DrawRect(419 + i, 183, 1, 16 - samp2, new Color(190, 192, 211));
                    DrawRect(419 + i, 199 - samp2, 1, 1, new Color(118, 124, 163));
                }
                if (IsMouseInCanvasBounds() && InFocus) {
                    DrawRect(17 + (CanvasMouseX / 6 * 6), 183 - ((31 - CanvasMouseY / 5) * 5), 6, -5, Helpers.Alpha(Color.White, 80));
                    if (Input.GetClick(KeyModifier.Shift)) {
                        int diff = Math.Abs(holdPosX - CanvasPosX);
                        if (diff > 0) {
                            if (holdPosX < CanvasPosX) {
                                for (int i = holdPosX; i <= CanvasPosX; ++i) {
                                    int y = (int)Math.Round(MathHelper.Lerp(holdPosY, CanvasPosY, (float)(i - holdPosX) / diff));
                                    DrawRect(17 + i * 6, 183 - y * 5, 6, -5, Helpers.Alpha(Color.White, 80));
                                }
                            }
                            else {
                                for (int i = CanvasPosX; i <= holdPosX; ++i) {
                                    int y = (int)Math.Round(MathHelper.Lerp(CanvasPosY, holdPosY, (float)(i - CanvasPosX) / diff));
                                    DrawRect(17 + i * 6, 183 - y * 5, 6, -5, Helpers.Alpha(Color.White, 80));
                                }
                            }
                        }
                    }
                }
                DrawPiano(9, 234);
                if (App.pianoInput > -1) {
                    if (App.pianoInput >= 12 && App.pianoInput < 132) {
                        if (Helpers.IsNoteBlackKey(App.pianoInput))
                            DrawSprite(App.pianoInput * 4 - 38, 235, new Rectangle(52, 80, 4, 24));
                        else
                            DrawSprite(App.pianoInput * 4 - 38, 235, new Rectangle(48, 80, 4, 24));
                    }
                }
                Write("Resampling Filter", 413, 205, UIColors.label);
                ResampleDropdown.Draw();
            }
        }
    }
}