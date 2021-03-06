using System;
using System.Collections.Generic;
using Telegram.Bot.Types.ReplyMarkups;
namespace bot
{
    public class MessageBuilder
    {
        public static ReplyKeyboardMarkup ChooseLanguage()
            => new ReplyKeyboardMarkup
            {
                Keyboard = new List<List<KeyboardButton>>()
                            {
                                new List<KeyboardButton>()
                                {
                                    new KeyboardButton(){ Text = "πΊπΏ Uz" }, 
                                    new KeyboardButton(){ Text = "π¬π§ En", },
                                    new KeyboardButton(){ Text = "π·πΊ Ru", },
                                }
                            },
                ResizeKeyboard = true
            };
        public static ReplyKeyboardMarkup ChoosenextLanguage()
            => new ReplyKeyboardMarkup
            {
                Keyboard = new List<List<KeyboardButton>>()
                            {
                                new List<KeyboardButton>()
                                {
                                    new KeyboardButton(){ Text = "πΊπΏ O'zbekcha" }, 
                                    new KeyboardButton(){ Text = "π·πΊ Π ΡΡΡΠΊΠΈΠΉ", },
                                    new KeyboardButton(){ Text = "π¬π§ English", },
                                }
                            },
                ResizeKeyboard = true
            };
        public static ReplyKeyboardMarkup Menu(string language)
        {
            var menuOption = new List<string>();
            if(language == "πΊπΏ Uz")
            {
                menuOption.Add("Bugungi namoz vaqtlari");
                menuOption.Add("Sozlamalar");
            }
            else if(language == "π¬π§ En")
            {
                menuOption.Add("Today's prayer times");
                menuOption.Add("Settings");
            }
            else if(language == "π·πΊ Ru")
            {
                menuOption.Add("Π‘Π΅Π³ΠΎΠ΄Π½ΡΡΠ½Π΅Π΅ Π²ΡΠ΅ΠΌΡ ΠΌΠΎΠ»ΠΈΡΠ²Ρ");
                menuOption.Add("ΠΠ°ΡΡΡΠΎΠΉΠΊΠΈ");
            };
            return new ReplyKeyboardMarkup
            {
                Keyboard = new List<List<KeyboardButton>>()
                            {
                                new List<KeyboardButton>()
                                {
                                    new KeyboardButton(){ Text = menuOption[0] }, 
                                    new KeyboardButton(){ Text = menuOption[1], },
                                }
                            },
                ResizeKeyboard = true
            };
        }

        public static ReplyKeyboardMarkup LocationRequestButton(string language)
        {
            var menuOption = new List<string>();
            if(language == "πΊπΏ Uz")
            {
                menuOption.Add("Ulashish");
                menuOption.Add("Rad etish");
            }
            else if(language == "π¬π§ En")
            {
                menuOption.Add("Share");
                menuOption.Add("Don't share");
            }
            else if(language == "π·πΊ Ru")
            {
                menuOption.Add("ΠΠΎΠ΄Π΅Π»ΠΈΡΡΡΡ");
                menuOption.Add("ΠΠ΅ ΠΏΠΎΠ΄Π΅Π»ΠΈΡΡΡΡ");
            };
            return new ReplyKeyboardMarkup()
            {
                Keyboard = new List<List<KeyboardButton>>()
                            {
                                new List<KeyboardButton>()
                                {
                                    new KeyboardButton(){ Text = menuOption[0], RequestLocation = true },
                                    new KeyboardButton(){ Text = menuOption[1] } 
                                }
                            },
                ResizeKeyboard = true
            };
        }

        public static ReplyKeyboardMarkup ResetLocationButton(string language)
        {
            var menuOption = new List<string>();
            if(language == "πΊπΏ Uz")
            {
                menuOption.Add("Ulashish");
                menuOption.Add("Menyuga qaytish");
            }
            else if(language == "π¬π§ En")
            {
                menuOption.Add("Share");
                menuOption.Add("Back to menu");
            }
            else if(language == "π·πΊ Ru")
            {
                menuOption.Add("ΠΠΎΠ΄Π΅Π»ΠΈΡΡΡΡ");
                menuOption.Add("ΠΠ΅ΡΠ½ΡΡΡΡ ΠΊ ΠΌΠ΅Π½Ρ");
            };
            return new ReplyKeyboardMarkup()
            {
                Keyboard = new List<List<KeyboardButton>>()
                            {
                                new List<KeyboardButton>()
                                {
                                    new KeyboardButton(){ Text = menuOption[0], RequestLocation = true },
                                    new KeyboardButton(){ Text = menuOption[1] } 
                                }
                            },
                ResizeKeyboard = true
            };
        }

        public static ReplyKeyboardMarkup Settings(string language)
        {
            var menuOption = new List<string>();
            if(language == "πΊπΏ Uz")
            {
                menuOption.Add("Tilni o'zgartirish");
                menuOption.Add("Joylashuvni o'zgartirish");
                menuOption.Add("Eslatmani yoqish/o'chirish");
                menuOption.Add("Menyuga qaytish");
            }
            else if(language == "π¬π§ En")
            {
                menuOption.Add("Change language");
                menuOption.Add("Change Location");
                menuOption.Add("Notification On/Off");
                menuOption.Add("Back to menu");   
            }
            else if(language == "π·πΊ Ru")
            {
                menuOption.Add("ΠΠ·ΠΌΠ΅Π½ΠΈΡΡ ΡΠ·ΡΠΊ");
                menuOption.Add("ΠΠ·ΠΌΠ΅Π½ΠΈΡΡ Π³Π΅ΠΎΠ»ΠΎΠΊΠ°ΡΠΈΡ");
                menuOption.Add("ΠΠΊΠ»ΡΡΠΈΡΡ/ΠΡΠΊΠ»ΡΡΠΈΡΡ ΡΠ²Π΅Π΄ΠΎΠΌΠ»Π΅Π½ΠΈΡ");
                menuOption.Add("ΠΠ΅ΡΠ½ΡΡΡΡ ΠΊ ΠΌΠ΅Π½Ρ");
            };

            return new ReplyKeyboardMarkup()
            {
                Keyboard = new List<List<KeyboardButton>>()
                            {
                                new List<KeyboardButton>()
                                {
                                    new KeyboardButton(){ Text = menuOption[0] },
                                },
                                new List<KeyboardButton>()
                                {
                                    new KeyboardButton(){ Text = menuOption[1] },
                                },
                                new List<KeyboardButton>()
                                {
                                    new KeyboardButton(){ Text = menuOption[2] }
                                },
                                new List<KeyboardButton>()
                                {
                                    new KeyboardButton(){ Text = menuOption[3] }
                                }
                                
                            },
                ResizeKeyboard = true
            };
        }
    }
}