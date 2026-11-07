using System;
using System.Runtime.InteropServices;
using MelonLoader;
using Il2Cpp; // Пространство имен игры, где находятся Player и PlayerWallet

// Атрибуты мода для MelonLoader 0.7.3
[assembly: MelonInfo(typeof(ScritchyScratchy_MoneyMod.MoneyMod), "Scritchy Money Mod", "1.0.0", "Yoru")]
[assembly: MelonGame(null, "Scritchy Scratchy")]

namespace ScritchyScratchy_MoneyMod
{
    public class MoneyMod : MelonMod
    {
        // Импортируем Windows API для считывания клавиш без вылетов в IL2CPP
        [DllImport("user32.dll")]
        private static extern short GetAsyncKeyState(int vKey);

        public override void OnInitializeMelon()
        {
            MelonLogger.Msg("=== Мод на деньги загружен! ===");
            MelonLogger.Msg("Нажмите клавишу F6 во время игры, чтобы получить $10,000.");
        }

         public override void OnUpdate()
        {
            // 0x75 — это код клавиши F6 в Windows
            if ((GetAsyncKeyState(0x75) & 1) != 0)
            {
                try
                {
                    // Ищем объект Player на сцене средствами Unity вместо использования .Instance
                    Player player = UnityEngine.Object.FindObjectOfType<Player>();

                    if (player != null && player.Wallet != null)
                    {
                        // Вызываем оригинальный метод игры
                        player.Wallet.AddMoney(10000.0, "CheatMenu");
                        
                        MelonLogger.Msg($"[MoneyMod] Успешно начислено $10,000! Текущий баланс: {player.Wallet.Money}");
                    }
                    else
                    {
                        MelonLogger.Warning("[MoneyMod] Игрок или кошелек не найдены на сцене. Зайдите непосредственно в игровой процесс.");
                    }
                }
                catch (Exception ex)
                {
                    MelonLogger.Error($"[MoneyMod] Ошибка при начислении денег: {ex.Message}");
                }
            }
        }
    }
}