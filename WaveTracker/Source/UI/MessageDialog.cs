﻿using Microsoft.Xna.Framework;
using System;

namespace WaveTracker.UI {
    public class MessageDialog : Dialog {
        protected Action<string> OnDialogExit;
        public enum Icon { Information, Error, Warning, Question, None }

        private Icon icon;
        public string Message { get; protected set; }
        protected Button[] buttons;
        private int textHeight;
        private int textWidth;
        private bool playSound;
        public MessageDialog(string message, Icon icon, string[] buttonNames, Action<string> onExitCallback, bool playSound = true) : base("WaveTracker", 240, 80, hasExitButton: false) {
            Message = message;
            this.icon = icon;
            this.playSound = playSound;
            ClearBottomButtons();
            buttons = new Button[buttonNames.Length];
            for (int i = buttonNames.Length - 1; i >= 0; --i) {
                buttons[i] = AddNewBottomButton(buttonNames[i], this);
            }
            OnDialogExit = onExitCallback;
            textWidth = width - (icon == Icon.None ? 16 : 64);
            textHeight = Helpers.GetHeightOfMultilineText(Message, textWidth);
        }

        /// <summary>
        /// Displays a message to the user
        /// </summary>
        /// <param name="message">The message to display to the user</param>
        /// <param name="icon">The icon to display alongside the message</param>
        /// <param name="buttonNames">A list of buttons to add to close the message</param>
        /// <param name="onExitCallback">Callback where the name of the pressed button is passed in as a parameter</param>
        public new void Open() {
#if WINDOWS
            if (icon == Icon.Information) {
                System.Media.SystemSounds.Asterisk.Play();
            }
            else if (icon == Icon.Error) {
                System.Media.SystemSounds.Hand.Play();
            }
            else if (icon == Icon.Warning) {
                System.Media.SystemSounds.Exclamation.Play();
            }
            else if (icon == Icon.Question) {
                System.Media.SystemSounds.Question.Play();
            }
#endif
            base.Open();
        }

        public override void Update() {
            if (WindowIsOpen) {
                DoDragging();
                foreach (Button b in buttons) {
                    if (b.Clicked) {
                        Close(b.Label);
                    }
                }
            }
        }

        public void Close(string result) {
            Input.CancelClick();
            base.Close();
            OnDialogExit?.Invoke(result);
        }

        public new void Draw() {
            if (WindowIsOpen) {
                base.Draw();
                // 220
                DrawRect(0, 9, width, height - 28, Color.White);

                int textY = 10 + (height - 25 - textHeight) / 2;
                if (icon == Icon.Information) {
                    DrawSprite(8, 19, new Rectangle(256, 80, 32, 32));
                }

                if (icon == Icon.Error) {
                    DrawSprite(8, 19, new Rectangle(288, 80, 32, 32));
                }

                if (icon == Icon.Warning) {
                    DrawSprite(8, 19, new Rectangle(320, 80, 32, 32));
                }

                if (icon == Icon.Question) {
                    DrawSprite(8, 19, new Rectangle(352, 80, 32, 32));
                }

                WriteMultiline(Message, icon == Icon.None ? 8 : 48, textY, textWidth, UIColors.labelDark);
            }
        }

    }
}
